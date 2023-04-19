using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MSIS_HMS.Core.Entities;
using MSIS_HMS.Core.Repositories;
using MSIS_HMS.Enums;
using MSIS_HMS.Infrastructure.Data;
using MSIS_HMS.Infrastructure.Enums;
using MSIS_HMS.Infrastructure.Repositories;
using MSIS_HMS.Infrastructure.Helpers;
using MSIS_HMS.Interfaces;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using MSIS_HMS.Models;
using Microsoft.Extensions.Options;
using X.PagedList;
using static MSIS_HMS.Infrastructure.Enums.DbEnum;
using Microsoft.Reporting.NETCore;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using DevExpress.Portable;
using DocumentFormat.OpenXml.Drawing.Charts;
using ZXing.QrCode.Internal;
using System.Diagnostics;
using MSIS_HMS.Core.Enums;

namespace MSIS_HMS.Controllers
{
    public class LabOrdersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly ILabOrderRepository _labOrderRepository;
        private readonly IUserService _userService;
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IOutletRepository _outletRepository;
        private readonly IBranchService _branchService;
        private readonly ILogger<LabOrdersController> _logger;
        private readonly ILabTestRepository _labTestRepository;
        private readonly Pagination _pagination;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ICollectionGroupRepository _collectionGroupRepository;

        public LabOrdersController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IBranchService branchService, ILabOrderRepository labOrderRepository, IUserService userService, ILogger<LabOrdersController> logger, IOptions<Pagination> pagination, IDoctorRepository doctorRepository, IPatientRepository patientRepository, IOutletRepository outletRepository, IWebHostEnvironment webHostEnvironment, ILabTestRepository labTestRepository,ICollectionGroupRepository collectionGroupRepository)
        {
            _userManager = userManager;
            _context = context;
            _labOrderRepository = labOrderRepository;
            _userService = userService;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
            _outletRepository = outletRepository;
            _branchService = branchService;
            _logger = logger;
            _pagination = pagination.Value;
            _webHostEnvironment = webHostEnvironment;
            _labTestRepository = labTestRepository;
            _collectionGroupRepository = collectionGroupRepository;
        }

        public void Initialize(LabOrder labOrder = null)
        {
            ViewData["Patients"] = _patientRepository.GetAll(_userService.Get(User).BranchId).GetSelectListItems("Id", "Name", "RegNo", labOrder?.PatientId);
            var branch = _branchService.GetBranchByUser();
            ViewData["UseVoucherFormat"] = branch.UseVoucherFormatForOrder;            

            var collectionGroups = _collectionGroupRepository.GetAll(_userService.Get(User).BranchId);
            ViewData["CollectionGroups"] = new SelectList(collectionGroups, "Id", "Name");
        }

        // GET
        public IActionResult Index(int? page = 1, int? OrderId = null, int? PatientId = null, int? DoctorId = null, string VoucherNo = null, DateTime? StartDate = null, DateTime? EndDate = null, string? BarCode = null, string? QRCode = null,int?CollectionGroupId=null,OrderByEnum?OrderBy=null)
        {
            var user = _userService.Get(User);
            
            var labOrders = _labOrderRepository.GetAll(user.BranchId, null, PatientId, VoucherNo, null, null, StartDate, EndDate, null,null, null, null,null,BarCode,QRCode,null,null,OrderBy,null);
            //ViewData["OrderBy"] = _labOrderRepository.GetAll(_userService.Get(User).BranchId).GetSelectListItems("OrderBy", labOrders?.OrderBy.ToString());
            ViewData["Patients"] = _patientRepository.GetAll(_userService.Get(User).BranchId).GetSelectListItems("Id", "Name", "RegNo", PatientId);
            ViewData["CollectionGroups"] = _collectionGroupRepository.GetAll(_userService.Get(User).BranchId).GetSelectListItems("Id", "Name", CollectionGroupId);
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            return View(labOrders.OrderByDescending(x => x.UpdatedAt).ToList().ToPagedList((int)page, pageSize));
        }

        public IActionResult LabOrderReport(int? page = 1, int? OrderId = null,int? PatientId=null, int? DoctorId = null, DateTime? FromDate = null, DateTime? ToDate = null,int? TestId=null,int?CollectionGroupId=null)
        {
            var user = _userService.Get(User);
            ViewData["Patients"] = _patientRepository.GetAll(_userService.Get(User).BranchId).GetSelectListItems("Id", "Name");
            ViewData["Tests"] = _labTestRepository.GetAll(_userService.Get(User).BranchId).GetSelectListItems("Id", "Name");
            ViewData["CollectionGroups"] = _collectionGroupRepository.GetAll(_userService.Get(User).BranchId).GetSelectListItems("Id", "Name");
            if (FromDate==null)
            {
                FromDate = DateTime.Now.Date;
                ToDate = DateTime.Now.Date;
            }

            var labOrders = _labOrderRepository.GetAll(user.BranchId, null, PatientId, null, true, null, FromDate, ToDate, null, null, null,TestId,CollectionGroupId);
            //ViewData["Patients"] = _patientRepository.GetAll(_userService.Get(User).BranchId).GetSelectListItems("Id", "Name", "RegNo", PatientId);
            foreach (var labO in labOrders)
            {
                var lTest = _labTestRepository.GetLabTestByLabOrderId(labO.Id);
               //Console.WriteLine(lTest.ToString());
                for (int i = 0; i < lTest.Count; i++)
                {
                    if (lTest.Count == 1)
                    {
                        labO.LabTest = lTest[i].LabTestName;
                        labO.CollectionGroup = lTest[i].CollectionGroupName;
                    }
  
                    else if (i != lTest.Count - 1)
                    {
                        labO.LabTest += lTest[i].LabTestName + ",";
                        labO.CollectionGroup += lTest[i].CollectionGroupName + ",";
                    }
                        
                    else
                    {
                        labO.LabTest += lTest[i].LabTestName;
                        labO.CollectionGroup += lTest[i].CollectionGroupName;
                    }
                        
                }

            }
            
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;

            TempData["Page"] = page;
            TempData["FromDate"] = FromDate;
            TempData["ToDate"] = ToDate;
            TempData["PatientId"] = PatientId;
            TempData["TestId"] = TestId;
            TempData["CollectionGroupId"] =CollectionGroupId;

            return View(labOrders.OrderByDescending(x => x.UpdatedAt).ToList().ToPagedList((int)page, pageSize));
        }

        
        public IActionResult Create(int? patientId = null)
        {
            Initialize();
            var labOrder = new LabOrder
            {
                VoucherNo = _branchService.GetVoucherNo(VoucherTypeEnum.Lab)
            };
            if (patientId != null)
            {
                labOrder.PatientId = (int)patientId;
            }
            return View(labOrder);  
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LabOrder labOrder)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    ModelState.Remove("labResultId");
                    if (ModelState.IsValid)
                    {
                        if (labOrder.LabOrderTests != null && labOrder.LabOrderTests.Count() > 0)
                        {
                            var branch = _branchService.GetBranchByUser();
                            labOrder.VoucherNo = _branchService.GetVoucherNo(VoucherTypeEnum.Lab, labOrder.VoucherNo);
                            if (string.IsNullOrEmpty(labOrder.VoucherNo))
                            {
                                ModelState.AddModelError("VoucherNo", "This field is required.");
                                Initialize(labOrder);
                                return View(labOrder);
                            }
                            if (branch.AutoPaidForOrder)
                            {   
                                labOrder.IsPaid = true;
                                labOrder.PaidDate = DateTime.Now;
                            }
                            labOrder.Total = labOrder.LabOrderTests.CalculateTotal() + labOrder.Tax + labOrder.RefundFee - labOrder.Discount - labOrder.ExtraFee; //_labOrderRepository.CalculateTotal(labOrder.OrderItems.ToList());
                            labOrder = await _userManager.AddUserAndTimestamp(labOrder, User, DbEnum.DbActionEnum.Create);
                            var _labOrder = await _labOrderRepository.AddAsync(labOrder);
                            if (_labOrder != null)
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
            Initialize(labOrder);
            return View(labOrder);
        }

        public IActionResult Edit(int id)
        {
            var labOrder = _labOrderRepository.Get(id);
            Initialize(labOrder);
            return View(labOrder);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LabOrder labOrder)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    ModelState.Remove("labResultId");
                    if (ModelState.IsValid)
                    {
                        if (labOrder.LabOrderTests != null && labOrder.LabOrderTests.Count() > 0)
                        {
                            labOrder.Total = labOrder.LabOrderTests.CalculateTotal() + labOrder.Tax + labOrder.RefundFee - labOrder.Discount - labOrder.ExtraFee; //_labOrderRepository.CalculateTotal(labOrder.OrderItems.ToList());
                            labOrder = await _userManager.AddUserAndTimestamp(labOrder, User, DbEnum.DbActionEnum.Update);
                            var _labOrder = await _labOrderRepository.UpdateAsync(labOrder);
                            if (_labOrder != null)
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
            Initialize(labOrder);
            return View(labOrder);
        }

        public async Task<IActionResult> Delete(int id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var labOrder = _labOrderRepository.Get(id);
                    var isSucceed = await _labOrderRepository.DeleteAsync(id);
                    if (isSucceed)
                    {
                        await transaction.CommitAsync();
                        TempData["notice"] = StatusEnum.NoticeStatus.Delete;
                        _logger.LogInformation(Infrastructure.Enums.EnumExtension.ToDescription(StatusEnum.NoticeStatus.Delete));
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e.InnerException.Message);
                    await transaction.RollbackAsync();
                    TempData["notice"] = StatusEnum.NoticeStatus.Fail;
                }
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Paid(int id)
        {
            var labOrder = await _context.LabOrders.FindAsync(id);
            if (labOrder != null)
            {
                labOrder.IsPaid = true;
                labOrder.PaidDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            else
            {
                TempData["notice"] = StatusEnum.NoticeStatus.Fail;
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> GetCollection(int id)
        {
            var labOrder = await _context.LabOrders.FindAsync(id);
            if (labOrder != null)
            {
                labOrder.GetCollection = true;
                labOrder.GetCollectionDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            else
            {
                TempData["notice"] = StatusEnum.NoticeStatus.Fail;
            }
            return RedirectToAction(nameof(Index));
        }
        //Cancels
        public async Task<IActionResult> Cancelled(int id)
        {
            var labOrder = await _context.LabOrders.FindAsync(id);
            if (labOrder != null)
            {
                labOrder.Cancel = true;
                await _context.SaveChangesAsync();
            }
            else
            {
                TempData["notice"] = StatusEnum.NoticeStatus.Fail;
            }
            return RedirectToAction(nameof(Index));
        }
                
        public IActionResult PrintReceipt(int id)
        {
            string renderFormat = "PDF";
            string extension = "pdf";
            string mimetype = "application/pdf";
            using (var report = new LocalReport())
            {
                var labOrder = _labOrderRepository.Get(id);
                var patientInfo = _context.Patients.Where(x => x.Id == labOrder.PatientId).FirstOrDefault();

                var labOrderTests = labOrder.LabOrderTests.Select(x => new
                {
                    Item = x.LabTestName,
                    Collection = x.CollectionName,
                    UnitPrice = x.UnitPrice.ToString("0.00"),
                    CollectionFee = x.CollectionFee.ToString("0.00"),
                    CollectionQty = x.CollectionQty,
                    Qty = x.Qty,
                    ServiceCharges = labOrder.ServiceCharges,
                    Price = ((x.UnitPrice * x.Qty)+(x.CollectionFee * x.CollectionQty)).ToString("0.00"),
                    Total = labOrder.Total.ToString("0.00"),
                    VoucherNo = labOrder.VoucherNo,
                    Date = labOrder.Date.ToString("dd-MM-yyyy"),
                    IsPaid = labOrder.IsPaid,
                    Tax = labOrder.Tax.ToString("0.00"),
                    Discount = labOrder.Discount.ToString("0.00"),
                    RefundFee=labOrder.RefundFee.ToString("0.00"),
                    ExtraFee=labOrder.ExtraFee.ToString("0.00"),
                    PatientName = labOrder.PatientName,
                    BranchName = labOrder.BranchName,
                    Address = labOrder.BranchAddress,
                    Phone = labOrder.BranchPhone,
                    PatientId=labOrder.PatientId,
                    Age = patientInfo.AgeYear == null ? (DateTime.Now.Year - Convert.ToDateTime(patientInfo.DateOfBirth).Year) : patientInfo.AgeYear,
                    DoctorName=x.ReferrerName,

                }).ToList();
                report.DataSources.Add(new ReportDataSource("dsPharmacyReceipt", labOrderTests));
                report.ReportPath = $"{_webHostEnvironment.WebRootPath}\\ReportFiles\\LabOrderReceipt.rdlc";

                var pdf = report.Render(renderFormat);
                return File(pdf, mimetype, patientInfo.Name+"_LabOrder_report." + extension);
            }
        }

        public IActionResult PrintSlip(int id)
        {
            string renderFormat = "PDF";
            string extension = "pdf";
            string mimetype = "application/pdf";
            using (var report = new LocalReport())
            {
                var labOrder = _labOrderRepository.Get(id);

                var labOrderTests = labOrder.LabOrderTests.Select(x => new
                {
                    Item = x.LabTestName,
                    Collection =x.CollectionName,
                    UnitPrice = x.UnitPrice.ToString("0.00"),
                    Qty = x.Qty,
                    ServiceCharges = labOrder.ServiceCharges,
                    CollectionFee = x.CollectionFee.ToString("0.00"),
                    CollectionQty = x.CollectionQty,
                    Price = ((x.UnitPrice * x.Qty) + (x.CollectionFee * x.CollectionQty)).ToString("0.00"),
                    Total = labOrder.Total.ToString("0.00"),
                    VoucherNo = labOrder.VoucherNo,
                    Date = labOrder.Date.ToString("dd-MM-yyyy  h:mm"),
                    IsPaid = labOrder.IsPaid,
                    Tax = labOrder.Tax.ToString("0.00"),
                    Discount = labOrder.Discount.ToString("0.00"),
                    RefundFee=labOrder.RefundFee.ToString("0.00"),
                    ExtraFee=labOrder.ExtraFee.ToString("0.00"),
                    PatientName = labOrder.PatientName,
                    PatientId=labOrder.PatientId,
                    BranchName = labOrder.BranchName,
                    Address = labOrder.BranchAddress,
                    Phone = labOrder.BranchPhone,
                    UnitShortForm = "",
                    DoctorName=x.ReferrerName,
                    

                }).ToList();
                report.DataSources.Add(new ReportDataSource("dsPharmacyReceipt", labOrderTests));
                report.ReportPath = $"{_webHostEnvironment.WebRootPath}\\ReportFiles\\PharmacyReceipt80mm.rdlc";

                var pdf = report.Render(renderFormat);
                return File(pdf, mimetype, "report." + extension);
            }
        }

        public IActionResult DownloadReport()
        {
            var page = TempData["Page"];

            DateTime? fromDate = Convert.ToDateTime(TempData["FromDate"]);
            DateTime? toDate = Convert.ToDateTime(TempData["ToDate"]);
            int? PatientId = null;
            int? TestId = null;
            if (TempData["FromDate"] == null)
            {
                fromDate = null;
                toDate = null;

            }
            if(TempData["PatientId"]!=null)
            {
                PatientId =(int) TempData["PatientId"];
            }
            if(TempData["TestId"]!=null)
            {
                TestId = (int)TempData["TestId"];
            }
            string renderFormat = "PDF";
            string extension = "pdf";
            string mimetype = "application/pdf";
            using (var report = new LocalReport())
            {
                List<LabOrder> labOrders = new List<LabOrder>();
                List<Branch> branches = new List<Branch>();
                var user = _userService.Get(User);
                labOrders = _labOrderRepository.GetAll(user.BranchId, null, PatientId, null, true, null, fromDate, toDate, null, null, null,TestId);
                foreach (var labO in labOrders)
                {
                    var lTest = _labTestRepository.GetLabTestByLabOrderId(labO.Id);
                    for (int i = 0; i < lTest.Count; i++)
                    {
                        if (lTest.Count == 1)
                            labO.LabTest = lTest[i].LabTestName;
                        else if (i != lTest.Count - 1)
                            labO.LabTest += lTest[i].LabTestName + ",";
                        else
                            labO.LabTest += lTest[i].LabTestName;

                    }

                }
                var branch = _branchService.GetBranchById((int)user.BranchId);
                branches.Add(branch);
                report.DataSources.Add(new ReportDataSource("Branch", branches));
                report.DataSources.Add(new ReportDataSource("dsLabOrder", labOrders));
                report.ReportPath = $"{_webHostEnvironment.WebRootPath}\\ReportFiles\\LabOrderReport.rdlc";

                var pdf = report.Render(renderFormat);
                TempData["Page"] = page;
                TempData["FromDate"] = fromDate;
                TempData["ToDate"] = toDate;
                TempData["PatientId"] = PatientId;
                TempData["TestId"] = TestId;
                return File(pdf, mimetype, "_LabOrderReport_" + DateTime.Now + "." + extension);
            }
        }

        public IActionResult DownloadCollectionLabels()
        {
            var page = TempData["Page"];

            DateTime? fromDate = Convert.ToDateTime(TempData["FromDate"]);
            DateTime? toDate = Convert.ToDateTime(TempData["ToDate"]);
            int? PatientId = null;
            int? TestId = null;
            int? CollectionGroupId = null;
            if (TempData["FromDate"] == null)
            {
                fromDate = null;
                toDate = null;

            }
            if (TempData["PatientId"] != null)
            {
                PatientId = (int)TempData["PatientId"];
            }
            if (TempData["TestId"] != null)
            {
                TestId = (int)TempData["TestId"];
            }
            if (TempData["CollectionGroupId"]!=null)
            {
                CollectionGroupId = (int)TempData["CollectionGroupId"];
            }
            string renderFormat = "PDF";
            string extension = "pdf";
            string mimetype = "application/pdf";

            using (var report = new LocalReport())
            {
                List<LabOrder> labOrders = new List<LabOrder>();
                List<Branch> branches = new List<Branch>();
                var user = _userService.Get(User);
                labOrders = _labOrderRepository.GetAll(user.BranchId, null, PatientId, null, true, null, fromDate, toDate, null, null, null, TestId,CollectionGroupId);
                
                foreach (var labO in labOrders)
                {
                    var lTest = _labTestRepository.GetLabTestByLabOrderId(labO.Id);
                    for (int i = 0; i < lTest.Count; i++)
                    {
                        if (lTest.Count == 1 )
                        {
                            labO.LabTest = lTest[i].LabTestName;
                            labO.CollectionGroup = lTest[i].CollectionGroupName;
                          
                        }
                        else if (i != lTest.Count - 1)
                        {
                            labO.LabTest += lTest[i].LabTestName + ",";
                            labO.CollectionGroup += lTest[i].CollectionGroupName + ",";

                        }

                        else
                        {
                            labO.LabTest += lTest[i].LabTestName;
                            labO.CollectionGroup += lTest[i].CollectionGroupName;

                        }

                    }

                }
                
                var branch = _branchService.GetBranchById((int)user.BranchId);
                branches.Add(branch);
                report.DataSources.Add(new ReportDataSource("Branch", branches));
                report.DataSources.Add(new ReportDataSource("dsCollectionLabelTape", labOrders));
                report.ReportPath = $"{_webHostEnvironment.WebRootPath}\\ReportFiles\\CollectionLabelReports.rdlc";
                var pdf = report.Render(renderFormat);
                TempData["Page"] = page;
                TempData["FromDate"] = fromDate;
                TempData["ToDate"] = toDate;
                TempData["PatientId"] = PatientId;
                TempData["TestId"] = TestId;
                TempData["CollectionGroupId"] = CollectionGroupId;
                return File(pdf, mimetype, "_LabOrderReport_" + DateTime.Now + "." + extension);
            }
        }
        //public IActionResult DownloadCollectionLabels()
        //{
        //    var fromDate = Convert.ToDateTime(TempData["FromDate"]);
        //    var toDate = Convert.ToDateTime(TempData["ToDate"]);
        //    int? patientId = null;
        //    int? testId = null;
        //    if (TempData["PatientId"] != null)
        //    {
        //        patientId = (int)TempData["PatientId"];
        //    }
        //    if (TempData["TestId"] != null)
        //    {
        //        testId = (int)TempData["TestId"];
        //    }
        //    string renderFormat = "PDF";
        //    string extension = "pdf";
        //    string mimetype = "application/pdf";

        //    var labOrders = _labOrderRepository.GetAll(null, null, patientId, null, true, null, fromDate, toDate, null, null, null, testId);

        //    foreach (var labOrder in labOrders)
        //    {
        //        var lTest = _labTestRepository.GetLabTestByLabOrderId(labOrder.Id);
        //        for (int i = 0; i < lTest.Count; i++)
        //        {
        //            if (lTest.Count == 1)
        //            {
        //                labOrder.LabTest = lTest[i].LabTestName;
        //                labOrder.CollectionGroup = lTest[i].CollectionGroupName;
        //            }
        //            else if (i != lTest.Count - 1)
        //            {
        //                labOrder.LabTest += lTest[i].LabTestName + ",";
        //                labOrder.CollectionGroup += lTest[i].CollectionGroupName + ",";
        //            }
        //            else
        //            {
        //                labOrder.LabTest += lTest[i].LabTestName;
        //                labOrder.CollectionGroup += lTest[i].CollectionGroupName;
        //            }
        //        }

        //        using (var report = new LocalReport())
        //        {
        //            var branches = new List<Branch>();
        //            var user = _userService.Get(User);
        //            var branch = _branchService.GetBranchById((int)user.BranchId);
        //            branches.Add(branch);

        //            report.DataSources.Add(new ReportDataSource("Branch", branches));
        //            report.DataSources.Add(new ReportDataSource("dsLabOrder", new[] { labOrder }));
        //            report.DataSources.Add(new ReportDataSource("dsCollectionLabelTape", new[] { labOrder }));

        //            report.ReportPath = $"{_webHostEnvironment.WebRootPath}\\ReportFiles\\CollectionLabelReports.rdlc";
        //            var pdf = report.Render(renderFormat);
        //            var fileName = $"_LabOrderReport_{DateTime.Now:mm-dd-yyy}.{extension}";
        //            return File(pdf, mimetype, fileName);
        //        }
        //    }

        //    return new EmptyResult();
        //}


        public IActionResult DownLoadLabelTape(int id)
        {
            string renderFormat = "PDF";
            string extension = "pdf";
            string mimetype = "application/pdf";
            using (var report = new LocalReport())
            {
                var labOrder = _labOrderRepository.Get(id);
                var labOrderTests = labOrder.LabOrderTests.Select(x => new
                {
                    LabTestName = x.LabTestName,
                    VoucherNo = labOrder.VoucherNo + labOrder.Date.ToString("dd-mm-yyy"),
                    //Date = labOrder.Date.ToString("dd-MM-yyyy"),
                    PatientName = labOrder.PatientName,
                    
                    CollectionGroup=x.CollectionGroupName,
                    PatientId = labOrder.PatientId,
                    //CollectionGroupName = x.LabTest != null ? x.LabTest.CollectionGroupName : null,
                }).ToList();
                report.DataSources.Add(new ReportDataSource("dsLabOrder", labOrderTests));
                report.DataSources.Add(new ReportDataSource("dsCollectionGroupLabelTape", labOrderTests));
                report.ReportPath = $"{_webHostEnvironment.WebRootPath}\\ReportFiles\\CollectionLabelTapeReport.rdlc";

                var pdf = report.Render(renderFormat);
                //TempData["CollectionGroupName"] = CollectionGroupName;
                return File(pdf, mimetype, "report." + extension);
            }
        }

        public IActionResult GetLabOrderFromLabOrderTest()
        {
            var labOrders = _labOrderRepository.GetLabOrderFromLabOrderTest(_userService.Get(User).BranchId);
            return Ok(labOrders);
        }

        [HttpGet]
        public IActionResult GetPatientByLabOrderId(int labOrderId)
        {
            List<Patient> patients = new List<Patient>();
            var labOrders = _context.LabOrders.Include(x => x.Patient).Where(x => x.Id == labOrderId).FirstOrDefault();
             patients .Add(labOrders.Patient);
            return Json(patients);
        }
        //public async Task<IActionResult> IsCancel(int cancelStatus, int id)
        //{
        //    var labOrder = await _context.LabOrders.FindAsync(id);
        //    labOrder.Cancelled = cancelStatus;
        //    //book.Status = status;
        //    await _context.SaveChangesAsync();
        //    return Ok();
        //}
    }
}