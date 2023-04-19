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
    public class BooksController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IBookRepository _bookRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly ISpecialityRepository _specialityRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IBranchService _branchService;
        private readonly IUserService _userService;
        private readonly Pagination _pagination;
        private readonly ILogger<BooksController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BooksController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IBookRepository bookRepository, ISpecialityRepository specialityRepository, IDepartmentRepository departmentRepository, IPatientRepository patientRepository, IDoctorRepository doctorRepository, IBranchService branchService, IUserService userService, IOptions<Pagination> pagination, ILogger<BooksController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _context = context;
            _bookRepository = bookRepository;
            _patientRepository = patientRepository;
            _doctorRepository = doctorRepository;
            _specialityRepository = specialityRepository;
            _departmentRepository = departmentRepository;
            _branchService = branchService;
            _userService = userService;
            _pagination = pagination.Value;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        public void Initialize(Book book = null, int? specialityId = null)
        {
            ViewData["Patients"] = _patientRepository.GetAll(_userService.Get(User).BranchId).GetSelectListItems("Id", "Name", "RegNo", book?.PatientId);
            ViewData["Doctors"] = _doctorRepository.GetAll(_userService.Get(User).BranchId).GetSelectListItems("Id", "Name", book?.DoctorId);
            ViewData["Specialities"] = _specialityRepository.GetAll(_userService.Get(User).BranchId).GetSelectListItems("Id", "Name");
            ViewData["Departments"] = _departmentRepository.GetAll(_userService.Get(User).BranchId).Where(x => x.Type == DepartmentTypeEnum.EnumDepartmentType.OPD).ToList().GetSelectListItems("Id", "Name");
        }

        public IActionResult Index(string BookNo = null, DateTime? StartDate = null, DateTime? EndDate = null, int? PatientId = null, int? DoctorId = null, BookStatusEnum? Status = null, int? page = 1)
        {

            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            var books = _bookRepository.GetAll(_userService.Get(User).BranchId, null, BookNo, null, StartDate, EndDate, PatientId, DoctorId, Status).ToList();
            Book book = new Book
            {
                PatientId = PatientId ?? 0,
                DoctorId = DoctorId ?? 0
            };
            Initialize(book);
            return View(books.OrderBy(x => x.Status).ThenByDescending(x => x.Date).ToList().ToPagedList((int)page, pageSize));
        }

        public IActionResult Create(int? patientId = null, int? specialityId = null)
        {
            Initialize();
            string format = "0000";

            var branch = _context.Branches.Where(x => x.Id == _userService.Get(User).BranchId).FirstOrDefault();
            int branchid = (int)_userService.Get(User).BranchId;
            string branchidwithformat = branchid.ToString(format);

            var patients = _patientRepository.GetAll(_userService.Get(User).BranchId).ToList();
            int patientcount = patients.Count;
            string patientcountformat = patientcount.ToString(format);

            var books = _bookRepository.GetAll(_userService.Get(User).BranchId).ToList();
            int bookcount = books.Count + 1;
            string bookcountformat = bookcount.ToString(format);

            
            var book = new Book
            {
                BookNo = branchidwithformat + "-" + patientcountformat + "-" + bookcountformat,
                Status = BookStatusEnum.Booked,
            };

            if (patientId != null)
            {
                book.PatientId = (int)patientId;
            }
            return View(book);
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Book book, int? specialityId = null)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    //if (ModelState.IsValid)
                    //{
                    var branch = _branchService.GetBranchByUser();
                    //visit.VisitNo = _branchService.GetVoucherNo(VoucherTypeEnum.Visit, visit.VisitNo);
                    //visit.CFFee = Convert.ToDecimal(_doctorRepository.Get(book.DoctorId).CFFee);
                    if (string.IsNullOrEmpty(book.BookNo))
                    {
                        ModelState.AddModelError("BookNo", "This field is required.");
                        Initialize(book, specialityId);
                        return View(book);
                    }
                    book = await _userManager.AddUserAndTimestamp(book, User, DbEnum.DbActionEnum.Create);

                    var _book = await _bookRepository.AddAsync(book);
                    if (_book != null)
                    {
                        //await _branchService.IncreaseVoucherNo(VoucherTypeEnum.Visit);
                        await transaction.CommitAsync();
                        TempData["notice"] = StatusEnum.NoticeStatus.Success;
                        _logger.LogInformation(Infrastructure.Enums.EnumExtension.ToDescription(StatusEnum.NoticeStatus.Success));
                        return RedirectToAction(nameof(Index));
                    }
                    //}
                }
                catch (Exception e)
                {
                    _logger.LogError(e.InnerException.Message);
                }

            }
            Initialize(book, specialityId);
            return View();
        }

        public IActionResult Edit(int id, int? specialityId)
        {
            var book = _bookRepository.Get(id);
            Initialize(book, specialityId);
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Book book, int? specialityId = null)
        {
            try
            {
                //if (ModelState.IsValid)
               // {
                    book = await _userManager.AddUserAndTimestamp(book, User, DbEnum.DbActionEnum.Update);
                    var _book = await _bookRepository.UpdateAsync(book);
                    TempData["notice"] = StatusEnum.NoticeStatus.Edit;
                    _logger.LogInformation(Infrastructure.Enums.EnumExtension.ToDescription(StatusEnum.NoticeStatus.Edit));
                    return RedirectToAction("Index");
                //}
            }
            catch (Exception e)
            {
                _logger.LogError(e.InnerException.Message); 
            }
            Initialize(book, specialityId);
            return View(book);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var _book = await _bookRepository.DeleteAsync(id);
                TempData["notice"] = StatusEnum.NoticeStatus.Delete;
                _logger.LogInformation(Infrastructure.Enums.EnumExtension.ToDescription(StatusEnum.NoticeStatus.Delete));
            }
            catch (Exception e)
            {
                _logger.LogError(e.InnerException.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> IsCancel(int cancelStatus, int id, BookStatusEnum status)
        {
            var book = await _context.Books.FindAsync(id);
            book.Cancelled = cancelStatus;
            book.Status = status;
            await _context.SaveChangesAsync();
            return Ok();
        }
        public async Task<IActionResult> IsVisit(int visitStatus, int id, BookStatusEnum status)
        {
            var book = await _context.Books.FindAsync(id);
            book.Cancelled = visitStatus;
            book.Status = status;
            await _context.SaveChangesAsync();
            return Ok();
        }



    }
}
