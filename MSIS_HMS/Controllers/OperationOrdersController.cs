using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MSIS_HMS.Core.Entities;
using MSIS_HMS.Core.Enums;
using MSIS_HMS.Core.Repositories;
using MSIS_HMS.Enums;
using MSIS_HMS.Infrastructure.Data;
using MSIS_HMS.Infrastructure.Enums;
using MSIS_HMS.Infrastructure.Helpers;
using MSIS_HMS.Interfaces;
using MSIS_HMS.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace MSIS_HMS.Controllers
{
    public class OperationOrdersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IOperationRoomRepository _operationRoomRepository;
        private readonly IOperationTypeRepository _operationTypeRepository;
        private readonly IOperationOrderRepository _operationOrderRepository;
        private readonly IBranchService _branchService;
        private readonly ILogger<OperationOrder> _logger;
        private readonly Pagination _pagination;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public OperationOrdersController(UserManager<ApplicationUser> userManager, IUserService userService, IBranchService branchService, ApplicationDbContext context,IPatientRepository patientRepository,IDoctorRepository doctorRepository,IBranchService service,ILogger<OperationOrder> logger, IOptions<Pagination> pagination,IWebHostEnvironment webHostEnvironment,IOperationOrderRepository operationOrderRepository,IOperationTypeRepository operationTypeRepository,IOperationRoomRepository operationRoomRepository)
        {
            _userManager = userManager;
            _context = context;
            _userService = userService;
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
            _branchService = branchService;
            _logger = logger;
            _pagination = pagination.Value;
            _webHostEnvironment = webHostEnvironment;
            _operationRoomRepository = operationRoomRepository;
            _operationTypeRepository = operationTypeRepository;
            _operationOrderRepository = operationOrderRepository;
        }

        public void Initialize(OperationOrder operationOrder=null)
        {
            ViewData["Patients"] = _patientRepository.GetAll(_userService.Get(User).BranchId).GetSelectListItems("Id", "Name", operationOrder?.PatientId);
            ViewData["Doctors"] = _doctorRepository.GetAnaesthetistDoctor(_userService.Get(User).BranchId, false).GetSelectListItems("Id", "Name", operationOrder?.ChiefSurgeonDoctorId);
            if (operationOrder?.OperationRoomId != null)
            {
                ViewData["OperationRooms"] = _operationRoomRepository.GetAll(_userService.Get(User).BranchId, null).GetSelectListItems("Id", "RoomNo", operationOrder?.OperationRoomId);

            }
            else
            {
                ViewData["OperationRooms"] = _operationRoomRepository.GetAll(_userService.Get(User).BranchId, null, "Available").GetSelectListItems("Id", "RoomNo", operationOrder?.OperationRoomId);
            }
            ViewData["OperationTypes"] = _operationTypeRepository.GetAll(_userService.Get(User).BranchId).GetSelectListItems("Id", "Name", operationOrder?.OpeartionTypeId);
            var branch = _branchService.GetBranchByUser();
        }

        public IActionResult Index(int? page = 1, int? OrderId = null, int? PatientId = null, int? DoctorId = null,DateTime? OTDate =null,DateTime? AdmitDate = null, DateTime? FromDate = null, DateTime? ToDate = null,OTOrderStatusEnum? Status = null)
        {
            var user = _userService.Get(User);

            var orders = _operationOrderRepository.GetAll(user.BranchId,null,PatientId,DoctorId, OTDate, AdmitDate, FromDate,ToDate,Status );

            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            Initialize();
            return View(orders.OrderBy(x => x.OTDate).ToList().ToPagedList((int)page, pageSize));

        }

        public IActionResult Create()
        {
            Initialize();
            string format = "0000000";

            var operationOrders = _operationOrderRepository.GetAll(_userService.Get(User).BranchId);
            var ordercount = operationOrders.Count + 1;
            var ordercountFormat = ordercount.ToString(format);

            var operationOrder = new OperationOrder()
            {
                OrderNo = ordercountFormat,
                Status = OTOrderStatusEnum.Booked
            };

            return View(operationOrder);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(OperationOrder operationOrder)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    
                        var branch = _branchService.GetBranchByUser();
                        if (string.IsNullOrEmpty(operationOrder.OrderNo))
                        {
                            ModelState.AddModelError("OrderNo", "The field is required");
                            Initialize(operationOrder);
                            return View(operationOrder);
                        }

                        operationOrder = await _userManager.AddUserAndTimestamp(operationOrder, User, DbEnum.DbActionEnum.Create);

                        var _order = await _operationOrderRepository.AddAsync(operationOrder);
                        if(_order != null)
                        {
                            await transaction.CommitAsync();
                            TempData["notice"] = StatusEnum.NoticeStatus.Success;
                            _logger.LogInformation(Infrastructure.Enums.EnumExtension.ToDescription(StatusEnum.NoticeStatus.Success));
                            return RedirectToAction(nameof(Index));
                        }
                        throw new Exception();
                    
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    await transaction.RollbackAsync();
                }
            }

            Initialize(operationOrder);
            return View(operationOrder);

        }
        public IActionResult Edit(int id)
        {
            var operationOrder = _operationOrderRepository.Get(id);
            Initialize(operationOrder);
            return View(operationOrder);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(OperationOrder operationOrder)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        operationOrder = await _userManager.AddUserAndTimestamp(operationOrder, User, DbEnum.DbActionEnum.Update);
                        var _operationOrder = await _operationOrderRepository.UpdateAsync(operationOrder);
                        if(_operationOrder != null)
                        {
                            await transaction.CommitAsync();
                            TempData["notice"] = StatusEnum.NoticeStatus.Edit;
                            _logger.LogInformation(Infrastructure.Enums.EnumExtension.ToDescription(StatusEnum.NoticeStatus.Edit));
                            return RedirectToAction("Index");
                        }
                    }
                }
                catch(Exception e)
                {
                    _logger.LogError(e.InnerException.Message);
                    await transaction.RollbackAsync();
                }
            }
            Initialize(operationOrder);
            return View(operationOrder);
        }


        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var _operationOrder = await _operationOrderRepository.DeleteAsync(id);
                TempData["notice"] = StatusEnum.NoticeStatus.Delete;
                _logger.LogInformation(Infrastructure.Enums.EnumExtension.ToDescription(StatusEnum.NoticeStatus.Delete));
            }
            catch(Exception e)
            {
                if (e.InnerException != null)
                {
                    _logger.LogError(e.InnerException.Message);
                }
                else
                {
                    _logger.LogError(e.Message);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> IsOperate(int orderstatus,int id)
        {
            var operationOrder = await _context.OperationOrders.FindAsync(id);
            operationOrder.Cancelled = orderstatus;
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
