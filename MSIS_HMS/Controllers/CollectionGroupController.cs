using DocumentFormat.OpenXml.ExtendedProperties;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
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
    public class CollectionGroupsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;
        private readonly ILabProfileRepository _labProfileRepository;
        private readonly ICollectionGroupRepository _collectionGroupRepository;
        private readonly Pagination _pagination;
        private readonly IConfiguration Configuration;
        private IHostingEnvironment _hostingEnv;

        public CollectionGroupsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IUserService userService, IConfiguration _configuration, IHostingEnvironment hostingEnvironment, IOptions<Pagination> pagination, ILabProfileRepository labProfileRepository, ICollectionGroupRepository collectionGroupRepository)
        {
            _context = context;
            _userManager = userManager;
            _userService = userService;
            _labProfileRepository = labProfileRepository;
            _pagination = pagination.Value;
            Configuration = _configuration;
            _hostingEnv = hostingEnvironment;
            _collectionGroupRepository = collectionGroupRepository;
        }
        public void initialize(CollectionGroup collectionGroup = null)
        {

        }
        public IActionResult Index(string Name = null, int? page = 1)
        {
            var user = _userService.Get(User);
            var collectionGroup = _collectionGroupRepository.GetAll(user.BranchId, Name).ToList();
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            return View(collectionGroup.OrderByDescending(x => x.UpdatedAt).ToList().ToPagedList((int)page, pageSize));
        }
        public IActionResult Create()
        {
            initialize();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(CollectionGroup collectionGroup)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    collectionGroup = await _userManager.AddUserAndTimestamp(collectionGroup, User, DbEnum.DbActionEnum.Create);
                    var _collectionGroup = await _collectionGroupRepository.AddAsync(collectionGroup);
                    if (collectionGroup != null)
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
            initialize();
            return View();
        }

        public IActionResult Edit(int id)
        {
            initialize();
            var collectionGroups = _collectionGroupRepository.Get(id);
            return View(collectionGroups);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CollectionGroup collectionGroup)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    collectionGroup = await _userManager.AddUserAndTimestamp(collectionGroup, User, DbEnum.DbActionEnum.Update);
                    var _collectionGroup = await _collectionGroupRepository.UpdateAsync(collectionGroup);
                    TempData["notice"] = StatusEnum.NoticeStatus.Edit;
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            initialize();
            return View(collectionGroup);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var collectionGroup = await _collectionGroupRepository.DeleteAsync(id);
            TempData["notice"] = StatusEnum.NoticeStatus.Delete;
            return RedirectToAction(nameof(Index));
        }

    }
}
