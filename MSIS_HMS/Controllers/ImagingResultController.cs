using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MSIS_HMS.Core.Entities;
using MSIS_HMS.Core.Enums;
using MSIS_HMS.Core.Repositories;
using MSIS_HMS.Enums;
using MSIS_HMS.Helpers;
using MSIS_HMS.Infrastructure.Data;
using MSIS_HMS.Infrastructure.Enums;
using MSIS_HMS.Infrastructure.Helpers;
using MSIS_HMS.Infrastructure.Repositories;
using MSIS_HMS.Interfaces;
using MSIS_HMS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using X.PagedList;

namespace MSIS_HMS.Controllers
{
    public class ImagingResultController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IImagingResultRepository _imgResultRepository;
        private readonly IImagingOrderRepository _imgOrderRepository;
        private readonly ILabPersonRepository _labPersonRepository;
        private readonly ILabTestRepository _labTestRepository;
        private readonly IUserService _userService;
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IOutletRepository _outletRepository;
        private readonly IBranchService _branchService;
        private readonly ILogger<ImagingResultController> _logger;
        private readonly Pagination _pagination;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImagingResultController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IBranchService branchService, ILabResultRepository labResultRepository, ILabPersonRepository labPersonRepository, ILabTestRepository labTestRepository, IUserService userService, ILogger<ImagingResultController> logger, IOptions<Pagination> pagination, IDoctorRepository doctorRepository, IPatientRepository patientRepository, IOutletRepository outletRepository, IWebHostEnvironment webHostEnvironment, IImagingResultRepository imgResultRepository, IImagingOrderRepository imgOrderRepository)
        {
            _userManager = userManager;
            _context = context;
            _imgResultRepository = imgResultRepository;
            _labPersonRepository = labPersonRepository;
            _labTestRepository = labTestRepository;
            _userService = userService;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
            _outletRepository = outletRepository;
            _branchService = branchService;
            _logger = logger;
            _pagination = pagination.Value;
            _imgOrderRepository = imgOrderRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public void Initialize(ImagingResult imgResult = null)
        {
            ViewData["Patients"] = _patientRepository.GetAll(_userService.Get(User).BranchId).GetSelectListItems("Id", "Name", "RegNo", imgResult?.PatientId);
            var labPersons = _labPersonRepository.GetAll(_userService.Get(User).BranchId);
            ViewData["Technicians"] = labPersons.Where(x => x.Type == LabPersonTypeEnum.Technician).ToList().GetSelectListItems("Id", "Name", imgResult?.TechnicianId);
            ViewData["Consultants"] = labPersons.Where(x => x.Type == LabPersonTypeEnum.Consultant).ToList().GetSelectListItems("Id", "Name", imgResult?.ConsultantId);
            var branch = _branchService.GetBranchByUser();
            ViewData["UseVoucherFormat"] = branch.UseVoucherFormatForImaging;
            ViewData["LabOrderNo"] = _imgOrderRepository.GetImgOrderFromImgOrderTest(_userService.Get(User).BranchId).GetSelectListItems("Id", "VoucherNo");
        }

        public IActionResult Index(int? page = 1, int? PatientId = null, string ResultNo = null, int? TechnicianId = null, int? ConsultantId = null, bool? IsCompleted = null, DateTime? FromDate = null, DateTime? ToDate = null, int? LabTestId = null)
        {
            var user = _userService.Get(User);
            var labResults = _imgResultRepository.GetAll(user.BranchId, null, PatientId, ResultNo, TechnicianId, ConsultantId, IsCompleted, null, FromDate, ToDate);
            ViewData["Patients"] = _patientRepository.GetAll(_userService.Get(User).BranchId).GetSelectListItems("Id", "Name", "RegNo", PatientId);
            var labPersons = _labPersonRepository.GetAll(_userService.Get(User).BranchId);
            ViewData["Technicians"] = labPersons.Where(x => x.Type == LabPersonTypeEnum.Technician).ToList().GetSelectListItems("Id", "Name", TechnicianId);
            ViewData["Consultants"] = labPersons.Where(x => x.Type == LabPersonTypeEnum.Consultant).ToList().GetSelectListItems("Id", "Name", ConsultantId);
            ViewData["LabTests"] = _labTestRepository.GetAll(_userService.Get(User).BranchId).ToList().GetSelectListItems("Id", "Name", LabTestId);
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            return View(labResults.OrderByDescending(x => x.UpdatedAt).ToList().ToPagedList((int)page, pageSize));
        }
        public IActionResult GetAll(int? PatientId = null)
        {
            var imgResults = _imgResultRepository.GetAll(_userService.Get(User).BranchId, PatientId: PatientId);
            return Ok(imgResults);
        }
        public IActionResult Create()
        {
            Initialize();
            var imgResult = new ImagingResult
            {
                //ResultNo = _branchService.GetVoucherNo(VoucherTypeEnum.Imaging)
            };
            return View(imgResult);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ImagingResult imgResult)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    ModelState.Remove("TechnicianFeeType");
                    ModelState.Remove("ConsultantFeeType");
                    ModelState.Remove("ImagingOrderId");
                    if (ModelState.IsValid)
                    {
                        if (imgResult.ImagingResultDetails != null && imgResult.ImagingResultDetails.Count() > 0)
                        {
                            var branch = _branchService.GetBranchByUser();
                            //imgResult.ResultNo = _branchService.GetVoucherNo(VoucherTypeEnum.LabResult, imgResult.ResultNo);
                            imgResult.UnitPrice = _labTestRepository.Get(imgResult.LabTestId).UnitPrice;
                            if (imgResult.TechnicianId != null)
                            {
                                var technicianFee = _labPersonRepository.GetLabPerson_LabTests(imgResult.TechnicianId, imgResult.LabTestId).First();
                                imgResult.TechnicianFee = technicianFee.Fee;
                                imgResult.TechnicianFeeType = technicianFee.FeeType;
                            }
                            if (imgResult.ConsultantId != null)
                            {
                                var consultantFee = _labPersonRepository.GetLabPerson_LabTests(imgResult.ConsultantId, imgResult.LabTestId).First();
                                imgResult.ConsultantFee = consultantFee.Fee;
                                imgResult.ConsultantFeeType = consultantFee.FeeType;
                            }
                            foreach (var imgResultDetail in imgResult.ImagingResultDetails)
                            {
                                var consultantFee = _labPersonRepository.GetLabPerson_LabTests(imgResult.ConsultantId, imgResult.LabTestId).First();
                                imgResultDetail.ConsultantFee = consultantFee.Fee;
                                imgResultDetail.ConsultantFeeType = consultantFee.FeeType;
                                
                                var technicianFee = _labPersonRepository.GetLabPerson_LabTests(imgResult.TechnicianId, imgResult.LabTestId).First();
                                imgResultDetail.TechnicianFee = technicianFee.Fee;
                                imgResultDetail.TechnicianFeeType = technicianFee.FeeType;
                               

                            }
                            imgResult = await _userManager.AddUserAndTimestamp(imgResult, User, DbEnum.DbActionEnum.Create);
                            var _labResult = await _imgResultRepository.AddAsync(imgResult);
                            if (_labResult != null)
                            {
                                await _branchService.IncreaseVoucherNo(VoucherTypeEnum.LabResult);

                                if (imgResult.ImagingOrderId != 0)
                                {
                                    foreach (var lab in imgResult.ImagingResultDetails)
                                    {
                                        await _imgOrderRepository.UpdateImgOrderTest(imgResult.ImagingOrderId, _labResult.Id, lab.LabTestId);
                                    }
                                }
                                await transaction.CommitAsync();
                                TempData["notice"] = StatusEnum.NoticeStatus.Success;
                                _logger.LogInformation(Infrastructure.Enums.EnumExtension.ToDescription(StatusEnum.NoticeStatus.Success));
                                return RedirectToAction(nameof(Index));
                            }
                            throw new Exception();
                        }

                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    await transaction.RollbackAsync();
                }
            }
            Initialize(imgResult);
            return View(imgResult);
        }
        public IActionResult Edit(int id)
        {
            var imgResult = _imgResultRepository.Get(id);
            Initialize(imgResult);
            return View(imgResult);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ImagingResult imgResult)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    ModelState.Remove("TechnicianFeeType");
                    ModelState.Remove("ConsultantFeeType");
                    ModelState.Remove("ImagingOrderId");
                    if (ModelState.IsValid)
                    {
                        if (imgResult.ImagingResultDetails != null && imgResult.ImagingResultDetails.Count() > 0)
                        {
                            imgResult = await _userManager.AddUserAndTimestamp(imgResult, User, DbEnum.DbActionEnum.Update);
                            var _labResult = await _imgResultRepository.UpdateAsync(imgResult);
                            if (_labResult != null)
                            {
                                //if (imgResult.ImagingOrderId != 0)
                                //{
                                //    foreach (var lab in imgResult.ImagingResultDetails)
                                //    {
                                //        await _imgOrderRepository.UpdateImgOrderTest(imgResult.ImagingOrderId, _labResult.Id, lab.LabTestId);
                                //    }
                                //}
                                await transaction.CommitAsync();
                                TempData["notice"] = StatusEnum.NoticeStatus.Edit;
                                _logger.LogInformation(Infrastructure.Enums.EnumExtension.ToDescription(StatusEnum.NoticeStatus.Edit));
                                return RedirectToAction("Index");
                            }
                        }
                    
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e.InnerException.Message);
                    await transaction.RollbackAsync();
                }
            }
            Initialize(imgResult);
            return View(imgResult);
        }

        public async Task<IActionResult> Completed(int id) // when result complete change complete status
        {
            var imgResult = await _context.ImagingResult.FindAsync(id);
            if (imgResult != null)
            {
                imgResult.IsCompleted = true;
                imgResult.CompletedDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            else
            {
                TempData["notice"] = StatusEnum.NoticeStatus.Fail;
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DownloadFile(string path) // when complete result you can download completed result file and this file can give to patient 
        {
            try
            {
                byte[] fileData;


                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(FtpHelper.ftpurl + path);
                request.Method = WebRequestMethods.Ftp.DownloadFile;

                //Enter FTP Server credentials.
                request.Credentials = new NetworkCredential(FtpHelper.ftpusername, FtpHelper.ftppassword);
                request.UsePassive = true;
                request.UseBinary = true;
                request.EnableSsl = false;

                //Fetch the Response and read it into a MemoryStream object.
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                using (MemoryStream stream = new MemoryStream())
                {
                    //Download the File.
                    response.GetResponseStream().CopyTo(stream);
                    fileData = stream.ToArray();
                }


                if (fileData != null)
                {

                    HttpContext.Session.SetComplexData("DownloadRecordData", null);

                    HttpContext.Session.SetComplexData("DownloadRecordData", fileData);
                    return Json("000");
                }
                else
                {
                    return Json("Please try again to download!");

                }
                //return File(FtpHelper.DownloadFileFromServer(path), "application/octet-stream");
            }
            catch (Exception ex)
            {
                return Json("");
            }

        }
        public ActionResult DownloadRecordData(string OriginalName)
        {
            if ((HttpContext.Session.GetComplexData<byte[]>("DownloadRecordData")) != null)
            {
                byte[] data = (HttpContext.Session.GetComplexData<byte[]>("DownloadRecordData")) as byte[];
                return File(data, "application/octet-stream", OriginalName);
            }
            else
            {
                return new EmptyResult();
            }

        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var _patient = await _imgResultRepository.DeleteAsync(id);
                TempData["notice"] = StatusEnum.NoticeStatus.Delete;
                _logger.LogInformation(Infrastructure.Enums.EnumExtension.ToDescription(StatusEnum.NoticeStatus.Delete));
            }
            catch (Exception e)
            {
                _logger.LogError(e.InnerException.Message);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
