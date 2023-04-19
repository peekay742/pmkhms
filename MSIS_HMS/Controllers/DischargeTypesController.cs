using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using MSIS_HMS.Core.Entities;
using MSIS_HMS.Core.Repositories;
using MSIS_HMS.Enums;
using MSIS_HMS.Infrastructure.Data;
using MSIS_HMS.Infrastructure.Enums;
using MSIS_HMS.Infrastructure.Repositories;
using MSIS_HMS.Infrastructure.Helpers;
using MSIS_HMS.Interfaces;
using MSIS_HMS.Models;
using X.PagedList;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Hosting;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using ClosedXML.Excel;

namespace MSIS_HMS.Controllers
{
    public class DischargeTypesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IDischargeTypeRepository _dischargeTypeRepository;
        private readonly IBranchService _branchService;
        private readonly Pagination _pagination;
        private readonly ILogger<ItemTypesController> _logger;
        private readonly IConfiguration Configuration;
        private IHostingEnvironment _hostingEnv;
        public DischargeTypesController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IDischargeTypeRepository dischargeTypeRepository, IBranchService branchService, IOptions<Pagination> pagination, ILogger<ItemTypesController> logger, IConfiguration _configuration, IHostingEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _context = context;
            _dischargeTypeRepository = dischargeTypeRepository;
            _branchService = branchService;
            _pagination = pagination.Value;
            _logger = logger;
            Configuration = _configuration;
            _hostingEnv = hostingEnvironment;
        }

        public void Initialize(DischargeType dischargeType = null)
        {
            ViewData["Branches"] = _branchService.GetSelectListItems(dischargeType?.BranchId);
        }

        // GET
        public IActionResult Index(string DischargeTypeName = null, int? page = 1)
        {

            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            var dischargeType = _dischargeTypeRepository.GetAll(_branchService.GetBranchIdByUser()).Where(dischargeType =>
                ((DischargeTypeName != null && dischargeType.Name.ToLower().Contains(DischargeTypeName.ToLower())) || (DischargeTypeName == null && dischargeType.Name != null))).ToList();
            var branches = _branchService.GetAll();
            dischargeType.ForEach(x => x.Branch = branches.SingleOrDefault(b => b.Id == x.BranchId));
            return View(dischargeType.OrderByDescending(x => x.UpdatedAt).ToList().ToPagedList((int)page, pageSize));
        }

        public IActionResult Create()
        {
            Initialize();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DischargeType dischargeType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dischargeType = await _userManager.AddUserAndTimestamp(dischargeType, User, DbEnum.DbActionEnum.Create);
                    var _dischargeType = await _dischargeTypeRepository.AddAsync(dischargeType);
                    if (_dischargeType != null)
                    {
                        TempData["notice"] = StatusEnum.NoticeStatus.Success;
                        _logger.LogInformation(Infrastructure.Enums.EnumExtension.ToDescription(StatusEnum.NoticeStatus.Success));
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.InnerException.Message);
            }
            Initialize(dischargeType);
            return View();
        }

        public IActionResult Edit(int id)
        {
            var dischargeType = _dischargeTypeRepository.Get(id);
            Initialize(dischargeType);
            return View(dischargeType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DischargeType dischargeType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dischargeType = await _userManager.AddUserAndTimestamp(dischargeType, User, DbEnum.DbActionEnum.Update);
                    var _dischargeType = await _dischargeTypeRepository.UpdateAsync(dischargeType);
                    TempData["notice"] = StatusEnum.NoticeStatus.Edit;
                    _logger.LogInformation(Infrastructure.Enums.EnumExtension.ToDescription(StatusEnum.NoticeStatus.Edit));
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.InnerException.Message);
            }
            Initialize(dischargeType);
            return View(dischargeType);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var _dischargeType = await _dischargeTypeRepository.DeleteAsync(id);
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
