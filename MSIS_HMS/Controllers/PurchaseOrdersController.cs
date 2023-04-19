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
using MSIS_HMS.Services;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Reporting.NETCore;

namespace MSIS_HMS.Controllers
{
    public class PurchaseOrdersController : Controller
    {
        //HWA
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly IBranchService _branchService;
        private readonly ILogger<PurchaseOrdersController> _logger;
        private readonly Pagination _pagination;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUserService _userService;
        //
        //
        public PurchaseOrdersController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IPurchaseOrderRepository purchaseOrderRepository, IBranchService branchService, ILogger<PurchaseOrdersController> logger, IOptions<Pagination> pagination, IWebHostEnvironment webHostEnvironment, IUserService userService)
        {
            _userManager = userManager;
            _context = context;
            _purchaseOrderRepository = purchaseOrderRepository;
            _branchService = branchService;
            _logger = logger;
            _pagination = pagination.Value;
            _webHostEnvironment = webHostEnvironment;
            _userService = userService;
            _userService = userService;
        }

        public void Initialize(PurchaseOrder purchaseOrder = null)
        {

            var branch = _branchService.GetBranchByUser();
            ViewData["UseVoucherFormat"] = branch.UseVoucherFormatForPurchaseOrder;
            ViewData["PurchaseOrderNO"] = _purchaseOrderRepository.GetAll(_userService.Get(User).BranchId).GetSelectListItems("Id", "PurchaseOrderNO", purchaseOrder);
            //ViewData["Branches"] = _branchService.GetSelectListItems(branchId);
        }
        public IActionResult Index(int? page = 1, DateTime? StartPurchaseOrderDate = null, DateTime? EndPurchaseOrderDate = null, string PurchaseOrderNo = null)
        {
            var purchaseOrders = _purchaseOrderRepository.GetAll(_branchService.GetBranchIdByUser(), null,null, null, null, PurchaseOrderNo, null, StartPurchaseOrderDate, EndPurchaseOrderDate);
            //var warehouses = _warehouseRepository.GetAll(_branchService.GetBranchIdByUser());
            //purchases.ForEach(x => x.Warehouse = warehouses.SingleOrDefault(w => w.Id == x.WarehouseId));
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            //ViewData["Warehouse"] = _warehouseService.GetSelectListItems(warehouseId);
            return View(purchaseOrders.OrderByDescending(x => x.UpdatedAt).ToList().ToPagedList((int)page, pageSize));
        }

        public IActionResult Create()
        {
            Initialize();
            var purchaseOrder = new PurchaseOrder
            {
                PurchaseOrderNO = _branchService.GetVoucherNo(VoucherTypeEnum.PurchaseOrder)
            };
            return View(purchaseOrder);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PurchaseOrder purchaseOrder)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    ModelState.Remove("purchaseOrderId");
                    if (ModelState.IsValid)
                    {
                        if (purchaseOrder.PurchaseItems != null && purchaseOrder.PurchaseItems.Count() > 0)
                        {
                            purchaseOrder.PurchaseOrderNO = _branchService.GetVoucherNo(VoucherTypeEnum.PurchaseOrder, purchaseOrder.PurchaseOrderNO);
                            if (string.IsNullOrEmpty(purchaseOrder.PurchaseOrderNO))
                            {
                                ModelState.AddModelError("VoucherNo", "This field is required.");
                                Initialize(purchaseOrder);
                                return View(purchaseOrder);
                            }

                            purchaseOrder.Total = purchaseOrder.PurchaseItems.CalculateTotal();
                            purchaseOrder = await _userManager.AddUserAndTimestamp(purchaseOrder, User, DbEnum.DbActionEnum.Create);

                            var _purchaseOrder = await _purchaseOrderRepository.AddAsync(purchaseOrder);
                            if (_purchaseOrder != null)
                            {
                                await _branchService.IncreaseVoucherNo(VoucherTypeEnum.PurchaseOrder);
                                await transaction.CommitAsync();
                                TempData["notice"] = StatusEnum.NoticeStatus.Success;
                                _logger.LogInformation(Infrastructure.Enums.EnumExtension.ToDescription(StatusEnum.NoticeStatus.Success));
                                return RedirectToAction(nameof(Index));
                            }
                        }

                        else
                        {
                            ViewData["Error"] = "Purchase Items are required";
                        }
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e.InnerException.Message);
                    await transaction.RollbackAsync();
                }
            }

            Initialize(purchaseOrder);
            return View(purchaseOrder);
        }

        public IActionResult Edit(int id)
        {
            var purchaseOrder = _purchaseOrderRepository.Get(id);
            Initialize(purchaseOrder);
            return View(purchaseOrder);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PurchaseOrder purchaseOrder)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        if (purchaseOrder.PurchaseItems != null && purchaseOrder.PurchaseItems.Count() > 0)
                        {
                            purchaseOrder.Total = purchaseOrder.PurchaseItems.CalculateTotal();
                            purchaseOrder = await _userManager.AddUserAndTimestamp(purchaseOrder, User, DbEnum.DbActionEnum.Update);

                            var _purchaseOrder = await _purchaseOrderRepository.UpdateAsync(purchaseOrder);
                            if (_purchaseOrder != null)
                            {
                                //await _warehouseRepository.UpdateStockAsync(purchase.PurchaseItems.GetWarehouseItemForUpdate(_purchaseRepository.GetPurchaseItemsForUpdate(purchase.Id), purchase.WarehouseId));
                                await transaction.CommitAsync();
                                TempData["notice"] = StatusEnum.NoticeStatus.Edit;
                                _logger.LogInformation(Infrastructure.Enums.EnumExtension.ToDescription(StatusEnum.NoticeStatus.Edit));
                                return RedirectToAction("Index");
                            }
                        }
                        else
                        {
                            ViewData["Error"] = "PurchaseOrder Items are required";
                        }
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e.InnerException.Message);
                    await transaction.RollbackAsync();
                }
            }
            Initialize(purchaseOrder);
            return View(purchaseOrder);
        }

        public async Task<IActionResult> Delete(int id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var purchaseOrder = _purchaseOrderRepository.Get(id);
                    //var warehouseItemsForDelete = purchase.PurchaseItems.GetWarehouseItemForDelete(_purchaseRepository.GetPurchaseItemsForUpdate(purchase.Id), purchase.WarehouseId);
                    var isSucceed = await _purchaseOrderRepository.DeleteAsync(id);
                    if (isSucceed)
                    {
                        // await _warehouseRepository.UpdateStockAsync(warehouseItemsForDelete);
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
        public IActionResult PrintReceipt(int id)
        {
            string renderFormat = "PDF";
            string extension = "pdf";
            string mimetype = "application/pdf";
            using (var report = new LocalReport())
            {
                var purchaseOrder = _purchaseOrderRepository.Get(id);
                var purchaseOrderInfo = _context.Branches.Where(x => x.Id ==purchaseOrder.BranchId).FirstOrDefault();
                var purchaseOrderItem = purchaseOrder.PurchaseItems.Select(x => new
                {
                    Item= x.ItemName,
                    Unit= x.UnitName,
                    Batch= x.BatchName,
                    UnitPrice = x.UnitPrice.ToString("0.00"),
                    Tax = purchaseOrder.Tax.ToString("0.00"),
                    PurchaseOrderNO = purchaseOrder.PurchaseOrderNO,
                    Total=purchaseOrder.Total.ToString("0.00"),
                    Date = purchaseOrder.PurchaseOrderDate.ToString("dd-MM-yyyy"),
                    Discount = purchaseOrder.Discount.ToString("0.00"),
                    Qty = x.Qty,
                    Price = (x.UnitPrice * x.Qty).ToString("0.00"),
                    BranchName = purchaseOrder.BranchName,
                    Address = purchaseOrder.BranchAddress,
                    Phone= purchaseOrderInfo.Phone,
                    Email=purchaseOrderInfo.Email,
                    

                }).ToList();
                report.DataSources.Add(new ReportDataSource("PurchaseOrderDataSet", purchaseOrderItem));
                report.ReportPath = $"{_webHostEnvironment.WebRootPath}\\ReportFiles\\PurchaseOrderReceipt.rdlc";

                var pdf = report.Render(renderFormat);
                return File(pdf, mimetype, purchaseOrder.PurchaseOrderNO + "_PurchaseOrder_report." + extension);
            }
        }
        public IActionResult GetPurchaseOrderFromPurchaseOrderItem()
        {
            var purchaseOrders = _purchaseOrderRepository.GetPurchaseOrderFromPurchaseOrderItem(_userService.Get(User).BranchId);
            return Ok(purchaseOrders);
        }
    }
}
