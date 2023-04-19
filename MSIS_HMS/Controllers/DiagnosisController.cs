using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
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
    public class DiagnosisController : Controller
    {
        private readonly IDiagnosisRepository _diagnosisRepository;
        //private readonly IDoctorRepository _doctorRepository;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Pagination _pagination;
        private readonly IUserService _userService;
        private readonly IConfiguration Configuration;
        private IHostingEnvironment _hostingEnv;

        public DiagnosisController(ApplicationDbContext context, IDiagnosisRepository diagnosisRepository, IDoctorRepository doctorRepository, IOptions<Pagination> pagination, IUserService userService, UserManager<ApplicationUser> userManager, IConfiguration _configuration, IHostingEnvironment hostingEnvironment)
        {
            _diagnosisRepository = diagnosisRepository;
            _context = context;
            _pagination = pagination.Value;
            _userService = userService;
            _userManager = userManager;
            Configuration = _configuration;
            _hostingEnv = hostingEnvironment;
        }
        public void Initialize(Diagnosis diagnosis = null)
        {
        }
        public IActionResult Index(int? page = 1, string diagnosisName = null,int? Id=null)
        {
            Initialize();
            var diagnoses = _diagnosisRepository.GetAll(diagnosisName,Id);
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            return View(diagnoses.OrderByDescending(x => x.UpdatedAt).ToList().ToPagedList((int)page, pageSize));

        }

        public IActionResult Create()
        {
            Initialize();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Diagnosis diagnosis)
        {
            try
            {
               
                if (ModelState.IsValid)
                {
                  
                    diagnosis = await _userManager.AddUserAndTimestamp(diagnosis, User, DbEnum.DbActionEnum.Create);
                    var diagnosis1 = await _diagnosisRepository.AddAsync(diagnosis);
                    if (diagnosis1 != null)
                    {
                        TempData["notice"] = StatusEnum.NoticeStatus.Success;
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Initialize();
            return View();
        }
        public ActionResult Edit(int id)
        {
            Initialize();
            return View(_diagnosisRepository.Get(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Diagnosis diagnosis)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    diagnosis = await _userManager.AddUserAndTimestamp(diagnosis, User, DbEnum.DbActionEnum.Update);
                    var _speciality = await _diagnosisRepository.UpdateAsync(diagnosis);
                    TempData["notice"] = StatusEnum.NoticeStatus.Edit;
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return View();
        }
        public async Task<ActionResult> Delete(int id)
        {
            var _speciality = await _diagnosisRepository.DeleteAsync(id);
            TempData["notice"] = StatusEnum.NoticeStatus.Delete;
            return RedirectToAction(nameof(Index));
            
        }
        public IActionResult GetDiagnosis()
        {
            var diagnoses = _diagnosisRepository.GetAll();
            return Json(diagnoses);
        }
    }
}
