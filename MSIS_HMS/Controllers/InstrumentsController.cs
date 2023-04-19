using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MSIS_HMS.Core.Entities;
using MSIS_HMS.Core.Repositories;
using MSIS_HMS.Enums;
using MSIS_HMS.Infrastructure.Data;
using MSIS_HMS.Infrastructure.Enums;
using MSIS_HMS.Infrastructure.Helpers;
using MSIS_HMS.Interfaces;
using MSIS_HMS.Models;
using NLog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MSIS_HMS.Controllers
{
    public class InstrumentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IInstrumentRepository _instrumentRepository;
        private readonly IUserService _userService;
        private readonly IBranchService _branchService;
        private readonly ILogger<InstrumentsController> _logger;
        private readonly Pagination _pagination;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnv;

        public InstrumentsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IInstrumentRepository instrumentRepository, IUserService userService, IBranchService branchService, ILogger<InstrumentsController> logger, IOptions<Pagination> pagination, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _instrumentRepository = instrumentRepository;
            _userService = userService;
            _logger = logger;
            _branchService = branchService;
            _pagination = pagination.Value;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _hostingEnv = hostingEnvironment;
        }
        public void Initialize()
        {

        }
        public IActionResult Index()
        {
            var instrument = _instrumentRepository.GetAll();
            return View(instrument);
        }

        public IActionResult Create()
        {
            Initialize();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Instrument instrument)
        {
            MappedDiagnosticsLogicalContext.Set("userId", _userManager.GetUserId(User));
            try
            {
                if (ModelState.IsValid)
                {
                    instrument = await _userManager.AddUserAndTimestamp(instrument, User, DbEnum.DbActionEnum.Create);
                    var _instrument = await _instrumentRepository.AddAsync(instrument);
                    if (_instrument != null)
                    {
                        TempData["notice"] = StatusEnum.NoticeStatus.Success;
                        _logger.LogInformation(Infrastructure.Enums.EnumExtension.ToDescription(StatusEnum.NoticeStatus.Success));
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                _logger.LogError(e.InnerException.Message);
            }
            Initialize();
            return View();
        }
        public IActionResult Edit(int id)
        {
            Initialize();
            var instrument = _instrumentRepository.Get(id);
            return View(instrument);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(Instrument instrument)
        {
            MappedDiagnosticsLogicalContext.Set("userId", _userManager.GetUserId(User));
            try
            {
                if (ModelState.IsValid)
                {
                    instrument = await _userManager.AddUserAndTimestamp(instrument, User, DbEnum.DbActionEnum.Update);
                    var _instrument = await _instrumentRepository.UpdateAsync(instrument);
                    TempData["notice"] = StatusEnum.NoticeStatus.Edit;
                    _logger.LogInformation(Infrastructure.Enums.EnumExtension.ToDescription(StatusEnum.NoticeStatus.Edit));
                    return RedirectToAction("Index");

                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                _logger.LogError(e.InnerException.Message);
            }

            Initialize();
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            MappedDiagnosticsLogicalContext.Set("userId", _userManager.GetUserId(User));
            try
            {
                var _instrument = await _instrumentRepository.DeleteAsync(id);
                TempData["notice"] = StatusEnum.NoticeStatus.Delete;
                _logger.LogInformation(Infrastructure.Enums.EnumExtension.ToDescription(StatusEnum.NoticeStatus.Delete));
            }
            catch (Exception e)
            {
                _logger.LogError(e.InnerException.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult GetAll()
        {
            var units = _instrumentRepository.GetAll(_userService.Get(User).BranchId);
            return Ok(units.OrderBy(x => x.Name).ToList());
        }
    }
}
