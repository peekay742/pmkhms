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
    public class CollectionController : Controller
    {
        private readonly ICollectionRepository _collectionRepository;
        //private readonly IDoctorRepository _doctorRepository;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Pagination _pagination;
        private readonly IUserService _userService;
        private readonly IConfiguration Configuration;
        private IHostingEnvironment _hostingEnv;

        public CollectionController(ApplicationDbContext context, ICollectionRepository collectionRepository, IDoctorRepository doctorRepository, IOptions<Pagination> pagination, IUserService userService, UserManager<ApplicationUser> userManager, IConfiguration _configuration, IHostingEnvironment hostingEnvironment)
        {
            _collectionRepository = collectionRepository;
            _context = context;
            _pagination = pagination.Value;
            _userService = userService;
            _userManager = userManager;
            Configuration = _configuration;
            _hostingEnv = hostingEnvironment;
        }
        public void Initialize(Collection collection = null)
        {
        }
        public IActionResult Index(int? page = 1, string collectionName = null, int? Id = null)
        {
            Initialize();
            var collections = _collectionRepository.GetAll(_userService.Get(User).BranchId, collectionName, Id);
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            return View(collections.OrderByDescending(x => x.UpdatedAt).ToList().ToPagedList((int)page, pageSize));

        }

        public IActionResult Create()
        {
            Initialize();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Collection collection)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    collection = await _userManager.AddUserAndTimestamp(collection, User, DbEnum.DbActionEnum.Create);
                    var collection1 = await _collectionRepository.AddAsync(collection);
                    if (collection1 != null)
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
            return View(_collectionRepository.Get(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Collection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    collection = await _userManager.AddUserAndTimestamp(collection, User, DbEnum.DbActionEnum.Update);
                    var _collection = await _collectionRepository.UpdateAsync(collection);
                    TempData["notice"] = StatusEnum.NoticeStatus.Edit;
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Initialize();
            return View(collection);
        }
        public async Task<ActionResult> Delete(int id)
        {
            var _collection = await _collectionRepository.DeleteAsync(id);
            TempData["notice"] = StatusEnum.NoticeStatus.Delete;
            return RedirectToAction(nameof(Index));

        }
        public IActionResult GetAll()
        {
           
                var collections = _collectionRepository.GetAll(_userService.Get(User).BranchId);
                return Ok(collections);
            
        }
        //public IActionResult GetDiagnosis()
        //{
        //    var diagnoses = _diagnosisRepository.GetAll();
        //    return Json(diagnoses);
        //}
    }
}
