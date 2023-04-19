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
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace MSIS_HMS.Controllers
{
    public class LabProfilesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;
        private readonly ILabProfileRepository _labProfileRepository;
        private readonly Pagination _pagination;
        private readonly IConfiguration Configuration;
        private IHostingEnvironment _hostingEnv;

        public LabProfilesController(ApplicationDbContext context,UserManager<ApplicationUser> userManager,IUserService userService,IConfiguration _configuration,IHostingEnvironment hostingEnvironment, IOptions<Pagination> pagination, ILabProfileRepository labProfileRepository)
        {
            _context = context;
            _userManager = userManager;
            _userService = userService;
            _labProfileRepository = labProfileRepository;
            _pagination = pagination.Value;
            Configuration = _configuration;
            _hostingEnv = hostingEnvironment;
        }

        public void Initialize(LabProfile labProfile=null)
        {

        }
        public IActionResult Index(string Name = null,int? page =1 )
        {
            
            var user = _userService.Get(User);
            var labProfiles = _labProfileRepository.GetAll(user.BranchId,Name).ToList();
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            return View(labProfiles.OrderByDescending(x => x.UpdatedAt).ToList().ToPagedList((int)page,pageSize)); 
        }

        public IActionResult Create()
        {
            Initialize();   
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(LabProfile labProfile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    labProfile = await _userManager.AddUserAndTimestamp(labProfile, User, DbEnum.DbActionEnum.Create);
                    var _labProfile = await _labProfileRepository.AddAsync(labProfile);
                    if(labProfile != null)
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

        public IActionResult Edit(int id)
        {
            Initialize();
            var labProfiles = _labProfileRepository.Get(id);
            return View(labProfiles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(LabProfile labProfile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    labProfile = await _userManager.AddUserAndTimestamp(labProfile, User, DbEnum.DbActionEnum.Update);
                    var _labProfile = await _labProfileRepository.UpdateAsync(labProfile);
                    TempData["notice"] = StatusEnum.NoticeStatus.Edit;
                    return RedirectToAction("Index");
                }
            }
            catch(Exception e )
            {
                Console.WriteLine(e.Message);
            }
            Initialize();
            return View(labProfile);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var labProfile = await _labProfileRepository.DeleteAsync(id);
            TempData["notice"] = StatusEnum.NoticeStatus.Delete;
            return RedirectToAction(nameof(Index));
        }
    }
}
