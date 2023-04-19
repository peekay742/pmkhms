using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Reporting.NETCore;
using MSIS_HMS.Core.Entities;
using MSIS_HMS.Core.Repositories;
using MSIS_HMS.Enums;
using MSIS_HMS.Infrastructure.Data;
using MSIS_HMS.Infrastructure.Enums;
using MSIS_HMS.Infrastructure.Helpers;
using MSIS_HMS.Interfaces;
using MSIS_HMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace MSIS_HMS.Controllers
{
    public class ImagingOrderController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IImagingOrderRepository _imgOrderRepository;
        private readonly IUserService _userService;
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IOutletRepository _outletRepository;
        private readonly IBranchService _branchService;
        private readonly ILogger<ImagingOrderController> _logger;
        private readonly ILabTestRepository _labTestRepository;
        private readonly Pagination _pagination;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ImagingOrderController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IBranchService branchService, ILabOrderRepository labOrderRepository, IUserService userService, ILogger<ImagingOrderController> logger, IOptions<Pagination> pagination, IDoctorRepository doctorRepository, IPatientRepository patientRepository, IOutletRepository outletRepository, IWebHostEnvironment webHostEnvironment, ILabTestRepository labTestRepository,IImagingOrderRepository imgOrderRepository)
        {
            _userManager = userManager;
            _context = context;
            _imgOrderRepository = imgOrderRepository;
            _userService = userService;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
            _outletRepository = outletRepository;
            _branchService = branchService;
            _logger = logger;
            _pagination = pagination.Value;
            _webHostEnvironment = webHostEnvironment;
            _labTestRepository = labTestRepository;
        }
        public void Initialize(ImagingOrder imgOrder = null)
        {
            ViewData["Patients"] = _patientRepository.GetAll(_userService.Get(User).BranchId).GetSelectListItems("Id", "Name", "RegNo", imgOrder?.PatientId);
            var branch = _branchService.GetBranchByUser();
            ViewData["UseVoucherFormat"] = branch.UseVoucherFormatForOrder;
        }
        public IActionResult Index(int? page = 1, string? BarCode = null, string? QRCode = null, int? OrderId = null, int? PatientId = null, int? DoctorId = null, string VoucherNo = null, DateTime? StartDate = null, DateTime? EndDate = null)
        {
            var user = _userService.Get(User);
            var imgOrders = _imgOrderRepository.GetAll(user.BranchId,BarCode,QRCode, null, PatientId, VoucherNo, null, null, StartDate, EndDate, null, null, null);
            ViewData["Patients"] = _patientRepository.GetAll(_userService.Get(User).BranchId).GetSelectListItems("Id", "Name", "RegNo", PatientId);
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            return View(imgOrders.OrderByDescending(x => x.UpdatedAt).ToList().ToPagedList((int)page, pageSize));
        }
        public IActionResult Create()
        {
            Initialize();
            var imgOrder = new ImagingOrder
            {
                //VoucherNo = _branchService.GetVoucherNo(VoucherTypeEnum.Lab)
            };
            return View(imgOrder);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ImagingOrder imgOrder)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    ModelState.Remove("labResultId");
                    if (ModelState.IsValid)
                    {
                        if (imgOrder.ImagingOrderTests != null && imgOrder.ImagingOrderTests.Count() > 0)
                        {
                            var branch = _branchService.GetBranchByUser();
                            //labOrder.VoucherNo = _branchService.GetVoucherNo(VoucherTypeEnum.Lab, labOrder.VoucherNo);
                            //if (string.IsNullOrEmpty(labOrder.VoucherNo))
                            //{
                            //    ModelState.AddModelError("VoucherNo", "This field is required.");
                            //    Initialize(labOrder);
                            //    return View(labOrder);
                            //}
                            if (branch.AutoPaidForOrder)
                            {
                                imgOrder.IsPaid = true;
                                imgOrder.PaidDate = DateTime.Now;
                            }
                            imgOrder.Total = imgOrder.ImagingOrderTests.CalculateTotal() + imgOrder.Tax - imgOrder.Discount; //_labOrderRepository.CalculateTotal(labOrder.OrderItems.ToList());
                            imgOrder = await _userManager.AddUserAndTimestamp(imgOrder, User, DbEnum.DbActionEnum.Create);
                            var _imgOrder = await _imgOrderRepository.AddAsync(imgOrder);
                            if (_imgOrder != null)
                            {
                                await _branchService.IncreaseVoucherNo(VoucherTypeEnum.Lab);
                                await transaction.CommitAsync();
                                TempData["notice"] = StatusEnum.NoticeStatus.Success;
                                _logger.LogInformation(Infrastructure.Enums.EnumExtension.ToDescription(StatusEnum.NoticeStatus.Success));
                                return RedirectToAction(nameof(Index));
                            }
                            throw new Exception();
                        }
                        else
                        {
                            ViewData["Error"] = "Order Items are required";
                        }
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    await transaction.RollbackAsync();
                }
            }
            Initialize(imgOrder);
            return View(imgOrder);
        }

        public IActionResult Edit(int id)
        {
            var imgOrder = _imgOrderRepository.Get(id);
            Initialize(imgOrder);
            return View(imgOrder);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ImagingOrder imgOrder)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                 
                    if (ModelState.IsValid)
                    {
                        if (imgOrder.ImagingOrderTests != null && imgOrder.ImagingOrderTests.Count() > 0)
                        {
                            imgOrder.Total = imgOrder.ImagingOrderTests.CalculateTotal() + imgOrder.Tax - imgOrder.Discount; //_labOrderRepository.CalculateTotal(labOrder.OrderItems.ToList());
                            imgOrder = await _userManager.AddUserAndTimestamp(imgOrder, User, DbEnum.DbActionEnum.Update);
                            var _imgOrder = await _imgOrderRepository.UpdateAsync(imgOrder);
                            if (_imgOrder != null)
                            {
                                await transaction.CommitAsync();
                                TempData["notice"] = StatusEnum.NoticeStatus.Edit;
                                _logger.LogInformation(Infrastructure.Enums.EnumExtension.ToDescription(StatusEnum.NoticeStatus.Edit));
                                return RedirectToAction("Index");
                            }
                        }
                        else
                        {
                            ViewData["Error"] = "Tests are required";
                        }
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e.InnerException.Message);
                    await transaction.RollbackAsync();
                }
            }
            Initialize(imgOrder);
            return View(imgOrder);
        }
        public IActionResult GetImgOrderFromImgOrderTest()
        {
            var imgOrders = _imgOrderRepository.GetImgOrderFromImgOrderTest(_userService.Get(User).BranchId);
            return Ok(imgOrders);
        }
        [HttpGet]
        public IActionResult GetPatientByImgOrderId(int imgOrderId)
        {
            List<Patient> patients = new List<Patient>();
            var imgOrders = _context.ImagingOrder.Include(x => x.Patient).Where(x => x.Id == imgOrderId).FirstOrDefault();
            patients.Add(imgOrders.Patient);
            return Json(patients);
        }
        public async Task<IActionResult> Paid(int id) // If payment transaction is complete then you can call paid function.
        {
            var imgOrder = await _context.ImagingOrder.FindAsync(id);
            if (imgOrder != null)
            {
                imgOrder.IsPaid = true;
                imgOrder.PaidDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            else
            {
                TempData["notice"] = StatusEnum.NoticeStatus.Fail;
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var _patient = await _imgOrderRepository.DeleteAsync(id);
                TempData["notice"] = StatusEnum.NoticeStatus.Delete;
                _logger.LogInformation(Infrastructure.Enums.EnumExtension.ToDescription(StatusEnum.NoticeStatus.Delete));
            }
            catch (Exception e)
            {
                _logger.LogError(e.InnerException.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult PrintReceipt(int id)// print receipt of imaging test order// 
        {
            string renderFormat = "PDF";
            string extension = "pdf";
            string mimetype = "application/pdf";
            using (var report = new LocalReport())
            {
                List<Branch> branches = new List<Branch>();
                var user = _userService.Get(User);
                var imgOrder = _imgOrderRepository.Get(id);
                var patientInfo = _context.Patients.Where(x => x.Id == imgOrder.PatientId).FirstOrDefault();

                var imgOrderTests = imgOrder.ImagingOrderTests.Select(x => new
                {
                    LabTestName = x.LabTestName,
                    UnitPrice = x.UnitPrice.ToString("0.00"),
                    Qty = 1,
                    Price = (x.UnitPrice * x.Qty).ToString("0.00"),
                    VoucherNo = imgOrder.VoucherNo,
                    Date = imgOrder.Date.ToString("dd-MM-yyyy"),
                    Tax = imgOrder.Tax.ToString("0.00"),
                    Discount = imgOrder.Discount.ToString("0.00"),
                    Patient = imgOrder.PatientName,
                    Age = patientInfo.AgeYear == null ? (DateTime.Now.Year - Convert.ToDateTime(patientInfo.DateOfBirth).Year) : patientInfo.AgeYear


                }).ToList();
                var branch = _branchService.GetBranchById((int)user.BranchId);
                branches.Add(branch);
                report.DataSources.Add(new ReportDataSource("Branch", branches));
                report.DataSources.Add(new ReportDataSource("ImagingOrderDataset", imgOrderTests));
                report.ReportPath = $"{_webHostEnvironment.WebRootPath}\\ReportFiles\\ImagingOrderReceipt.rdlc";

                var pdf = report.Render(renderFormat);
                return File(pdf, mimetype, patientInfo.Name+"_Imaging_Report." + extension);
            }
        }


    }
}
