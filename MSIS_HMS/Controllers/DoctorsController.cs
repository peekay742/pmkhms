using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MSIS_HMS.Core.Entities;
using MSIS_HMS.Core.Entities.DTOs;
using MSIS_HMS.Core.Repositories;
using MSIS_HMS.Enums;
using MSIS_HMS.Helpers;
using MSIS_HMS.Infrastructure.Data;
using MSIS_HMS.Infrastructure.Enums;
using MSIS_HMS.Infrastructure.Helpers;
using MSIS_HMS.Interfaces;
using MSIS_HMS.Models;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using X.PagedList;
using static MSIS_HMS.Core.Enums.GenderEnum;

namespace MSIS_HMS.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISpecialityRepository _specialityRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _context;
        private readonly Pagination _pagination;
        private readonly ILogger<DoctorsController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnv;

        public DoctorsController(ISpecialityRepository specialityRepository, IDepartmentRepository departmentRepository, UserManager<ApplicationUser> userManager, IUserService userService, IDoctorRepository doctorRepository, ApplicationDbContext context, IOptions<Pagination> pagination, ILogger<DoctorsController> logger, IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _specialityRepository = specialityRepository;
            _doctorRepository = doctorRepository;
            _departmentRepository = departmentRepository;
            _userService = userService;
            _context = context;
            _pagination = pagination.Value;
            _logger = logger;
            _configuration = configuration;
            _hostingEnv = hostingEnvironment;
        }

        public void Initialize(Doctor doctor = null)
        {
            ViewData["Specialities"] = _specialityRepository.GetAll(_userService.Get(User).BranchId).GetSelectListItems("Id", "Name", doctor?.SpecialityId);
        }

        // GET
        public IActionResult Index(string DoctorName = null, string Code = null, string SamaNumber = null, int? DepartmentId = null, int? SpecialityId = null, int? page = 1)
        {
            Initialize();
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            var doctors = _doctorRepository.GetAll(_userService.Get(User).BranchId, null, DoctorName, Code, SamaNumber, DepartmentId,null, SpecialityId);

            foreach (var obj in doctors)
            {
                obj.Speciality = _context.Specialities.FirstOrDefault(u => u.Id == obj.SpecialityId);
            }
            ViewData["Departments"] = _departmentRepository.GetAll(_userService.Get(User).BranchId).GetSelectListItems("Id", "Name", DepartmentId);
            ViewData["Specialities"] = _specialityRepository.GetAll(_userService.Get(User).BranchId).GetSelectListItems("Id", "Name", SpecialityId);
            return View(doctors.OrderByDescending(doctor => doctor.UpdatedAt).ToList().ToPagedList((int)page, pageSize));
        }

        public ActionResult Create()
        {
            Initialize();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Doctor doctor)
        {

            try
            {
                var filename = doctor.ImageFile != null ? FtpHelper.ftpDoctorImageFolderPath + doctor.ImageFile.GetUniqueName() : "";

                if (ModelState.IsValid)
                {
                    var didUploaded = true;
                    if (doctor.ImageFile != null)
                    {
                        didUploaded = false;
                        var uploadRes = FtpHelper.UploadFileToServer(doctor.ImageFile, filename);
                        if (uploadRes.IsSucceed())
                        {
                            didUploaded = true;
                            doctor.Image = uploadRes.ResponseUri.AbsolutePath;
                        }
                    }
                    if (didUploaded)
                    {
                        doctor = await _userManager.AddUserAndTimestamp(doctor, User, DbEnum.DbActionEnum.Create);
                        doctor = await _userManager.AddUserAndTimestamp(doctor, User, DbEnum.DbActionEnum.Update);
                        doctor.CFFeeForHospital = doctor.CFFeeForHospital / 100;
                        doctor.RoundFeeForHospital = doctor.RoundFeeForHospital / 100;
                        if (doctor.Schedules != null)
                        {
                            doctor.Schedules.ToList().ForEach(x => x.BranchId = doctor.BranchId);
                        }
                        var _doctor = await _doctorRepository.AddAsync(doctor);

                        if (_doctor != null)
                        {
                            TempData["notice"] = StatusEnum.NoticeStatus.Success;
                            _logger.LogInformation(Infrastructure.Enums.EnumExtension.ToDescription(StatusEnum.NoticeStatus.Success));
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.InnerException.Message);
            }

            Initialize();
            return View();
        }

        public ActionResult Edit(int id)
        {
            Initialize();
            var _doctors = _doctorRepository.Get(id);
            _doctors.CFFeeForHospital = _doctors.CFFeeForHospital * 100;
            _doctors.RoundFeeForHospital = _doctors.RoundFeeForHospital * 100;
            _doctors.ImageContent = _doctors.Image.GetBase64();
            return View(_doctors);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Doctor doctor)
        {
            try
            {
                var filename = doctor.ImageFile != null ? FtpHelper.ftpDoctorImageFolderPath + doctor.ImageFile.GetUniqueName() : "";
                var didUploaded = true;
                if (doctor.ImageFile != null)
                {
                    didUploaded = false;
                    var uploadRes = FtpHelper.UploadFileToServer(doctor.ImageFile, filename);
                    if (uploadRes.IsSucceed())
                    {
                        var didDeleted = true;
                        if (FtpHelper.CheckIfFileExistsOnServer(doctor.Image))
                        {
                            didDeleted = false;
                            var deleteRes = FtpHelper.DeleteFileOnServer(doctor.Image);
                            if (deleteRes.IsSucceed())
                            {
                                didDeleted = true;
                            }
                        }
                        if (didDeleted)
                        {
                            didUploaded = true;
                            doctor.Image = uploadRes.ResponseUri.AbsolutePath;
                        }
                    }
                }
                if (didUploaded)
                {
                    if (ModelState.IsValid)
                    {
                        doctor = await _userManager.AddUserAndTimestamp(doctor, User, DbEnum.DbActionEnum.Update);
                        doctor.CFFeeForHospital = doctor.CFFeeForHospital / 100;
                        doctor.RoundFeeForHospital = doctor.RoundFeeForHospital / 100;
                        var _doctor = await _doctorRepository.UpdateAsync(doctor);
                        TempData["notice"] = StatusEnum.NoticeStatus.Edit;
                        _logger.LogInformation(Infrastructure.Enums.EnumExtension.ToDescription(StatusEnum.NoticeStatus.Edit));
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.InnerException.Message);
            }
            Initialize();
            return View(doctor);

        }

        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var _doctor = await _doctorRepository.DeleteAsync(id);
                TempData["notice"] = StatusEnum.NoticeStatus.Delete;
                _logger.LogInformation(Infrastructure.Enums.EnumExtension.ToDescription(StatusEnum.NoticeStatus.Delete));
            }
            catch (Exception e)
            {
                _logger.LogError(e.InnerException.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult GetAll(int? DepartmentType = null, int? DepartmentId = null, int? SpecialityId = null)
        {
            var doctors = _doctorRepository.GetAll(_userService.Get(User).BranchId, DepartmentId: DepartmentId, DepartmentType: DepartmentType, SpecialityId: SpecialityId);
            return Ok(doctors);
        }
        public IActionResult GetDoctorTypeEnum()
        {
            List<OTDoctorTypeEnumDTO> oTDoctorTypeEnumDTOs = new List<OTDoctorTypeEnumDTO>();
            var doctorTypeEnum = Enum.GetValues(typeof(MSIS_HMS.Core.Enums.OTDoctorTypeEnum))
            .Cast<MSIS_HMS.Core.Enums.OTDoctorTypeEnum>().ToDictionary(k => k.ToString(), v => (int)v);
            //.Select(v => v.ToString()).ToList();
            foreach (var e in doctorTypeEnum)
            {
                OTDoctorTypeEnumDTO oTDoctorTypeEnumDTO = new OTDoctorTypeEnumDTO();
                oTDoctorTypeEnumDTO.Name = e.Key;
                oTDoctorTypeEnumDTO.value = e.Value;
                oTDoctorTypeEnumDTOs.Add(oTDoctorTypeEnumDTO);

            }
            return Ok(oTDoctorTypeEnumDTOs);
        }
        public IActionResult GetAvailableDoctors(int? DepartmentType = null, int? DepartmentId = null, int? SpecialityId = null) // get doctor from today schedule show on visit or booking 
        {
            var currentTime = DateTime.Now.TimeOfDay;
            var currentDay = DateTime.Now.DayOfWeek;
            //var docs = _context.Schedules.Include(x => x.Doctor).Include(x => x.Department).ToList();
            var available_doctors = _context.Schedules.Include(x => x.Doctor).Include(x => x.Department).Where(x =>
                    x.BranchId == _userService.Get(User).BranchId &&
                    (DepartmentType == null || (int)x.Department.Type == (int)DepartmentType) &&
                    (DepartmentId == null || x.DepartmentId == DepartmentId) &&
                    (SpecialityId == null || x.Doctor.SpecialityId == SpecialityId) &&
                    x.DayOfWeek == currentDay &&
                    currentTime >= x.FromTime && currentTime <= x.ToTime
                ).AsEnumerable().Select(x =>
                {
                    var patientInQueue = GetPatientInQueue(x.DoctorId);
                    return new AvailableDoctor()
                    {
                        Id = x.DoctorId,
                        Name = x.Doctor.Name,
                        PatientInQueue = patientInQueue,
                        EstWaitingTime = GetEstWaitingTime(patientInQueue, x.DoctorId),
                        FromTime = x.FromTime,
                        ToTime = x.ToTime
                    };
                }).ToList();

            return Ok(available_doctors);
        }

        public int GetPatientInQueue(int DoctorId) 
        {
            // && DateTime.Equals(x.Date.Date, DateTime.Now.Date)
            var patientInQueue = _context.Visits.ToList().Where(x => !x.IsDelete && x.Date.Date == DateTime.Now.Date && x.DoctorId == DoctorId && x.Status == Core.Enums.VisitStatusEnum.Booked).ToList();
            return patientInQueue.Count();
        }

        public string GetEstWaitingTime(int PatientInQueue, int DoctorId) // get estimate waiting time of a doctor. This time is to tell the patient to wait. 
        {
            var DoctorInfo = _context.Doctors.Where(x => x.Id == DoctorId).FirstOrDefault();
            var waitingTimePerPatient = DoctorInfo.EstimatewaitingTime; // 8 min per patient
            var totalMin = PatientInQueue * waitingTimePerPatient;
            if (totalMin < 60)
            {
                return string.Format("{0}min", totalMin);
            }
            TimeSpan estWaitingTime = TimeSpan.FromMinutes(totalMin);
            return estWaitingTime.Minutes > 0 ? string.Format("{0}hr {1}min", estWaitingTime.Hours, estWaitingTime.Minutes) : string.Format("{0}hr", estWaitingTime.Hours);
        }

        public ActionResult Upload()
        {
            IFormFile file = Request.Form.Files[0];
            string folderName = "UploadExcel";
            string webRootPath = _hostingEnv.WebRootPath;
            string newPath = Path.Combine(webRootPath, folderName);
            StringBuilder sb = new StringBuilder();
            if (!Directory.Exists(newPath))
            {
                Directory.CreateDirectory(newPath);
            }
            if (file.Length > 0)
            {
                string sFileExtension = Path.GetExtension(file.FileName).ToLower();
                ISheet sheet;
                string fullPath = Path.Combine(newPath, file.FileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                    stream.Position = 0;
                    if (sFileExtension == ".xls")
                    {
                        HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
                    }
                    else
                    {
                        XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
                        sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                    }
                    IRow headerRow = sheet.GetRow(0); //Get Header Row
                    int cellCount = headerRow.LastCellNum;
                    sb.Append("<table class='table table-bordered'><tr>");
                    for (int j = 0; j < cellCount; j++)
                    {
                        NPOI.SS.UserModel.ICell cell = headerRow.GetCell(j);
                        if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                        sb.Append("<th>" + cell.ToString() + "</th>");
                    }
                    sb.Append("</tr>");
                    sb.AppendLine("<tr>");
                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue;
                        if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            if (row.GetCell(j) != null)
                                sb.Append("<td>" + row.GetCell(j).ToString() + "</td>");
                        }
                        sb.AppendLine("</tr>");
                    }
                    sb.Append("</table>");
                }
            }
            return this.Content(sb.ToString());
        }
        public IActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ImportExcelFile(IFormFile FormFile)
        {
            if (FormFile == null)
            {
                return RedirectToAction("Import");
            }
            else
            {


                //get file name
                var filename = ContentDispositionHeaderValue.Parse(FormFile.ContentDisposition).FileName.Trim('"');
                //get path
                var MainPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads");

                //create directory "Uploads" if it doesn't exists
                if (!Directory.Exists(MainPath))
                {
                    Directory.CreateDirectory(MainPath);
                }

                //get file path 
                var filePath = Path.Combine(MainPath, FormFile.FileName);
                using (System.IO.Stream stream = new FileStream(filePath, FileMode.Create))
                {
                    await FormFile.CopyToAsync(stream);
                }

                //get extension
                string extension = Path.GetExtension(filename);


                string conString = string.Empty;

                switch (extension)
                {
                    case ".xls": //Excel 97-03.
                        conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
                        break;
                    case ".xlsx": //Excel 07 and above.
                        conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
                        break;
                }

                DataTable dt = new DataTable();
                conString = string.Format(conString, filePath);

                using (OleDbConnection connExcel = new OleDbConnection(conString))
                {
                    using (OleDbCommand cmdExcel = new OleDbCommand())
                    {
                        using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                        {
                            cmdExcel.Connection = connExcel;

                            //Get the name of First Sheet.
                            connExcel.Open();
                            DataTable dtExcelSchema;
                            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                            connExcel.Close();

                            //Read Data from First Sheet.
                            connExcel.Open();
                            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                            odaExcel.SelectCommand = cmdExcel;
                            odaExcel.Fill(dt);
                            connExcel.Close();
                        }
                    }
                }
                //your database connection string
                conString = _configuration.GetConnectionString("DefaultConnection");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    Doctor doctor = new Doctor();
                    doctor = await _userManager.AddUserAndTimestamp(doctor, User, DbEnum.DbActionEnum.Create); dt.Columns.Add("BranchId", typeof(Int32));
                    dt.Columns.Add("IsDelete", typeof(bool));
                    dt.Columns.Add("CreatedAt", typeof(DateTime));
                    dt.Columns.Add("CreatedBy", typeof(string));
                    dt.Columns.Add("UpdatedAt", typeof(DateTime));
                    dt.Columns.Add("UpdatedBy", typeof(string));


                    foreach (var row in dt.AsEnumerable().ToList())
                    {
                        string sname = row["Speciality"].ToString();
                        //string uname = row["Unit"].ToString();

                        row["Speciality"] = _context.Specialities.Where(x => x.Name == sname).First().Id;
                        //row["Unit"] = _context.Units.Where(x => x.Name == uname).First().Id;
                        row["BranchId"] = doctor.BranchId;
                        row["IsDelete"] = doctor.IsDelete;
                        row["CreatedAt"] = doctor.CreatedAt;
                        row["CreatedBy"] = doctor.CreatedBy;
                        row["UpdatedAt"] = doctor.UpdatedAt;
                        row["UpdatedBy"] = doctor.UpdatedBy;
                        var genders = Enum.GetValues(typeof(Gender))
                        .OfType<Gender>().ToList();
                        var selectgender = new SelectList(genders, "Value", "Text", row["Gender"]);
                        int gender = ((int)Enum.Parse(typeof(Gender), selectgender.SelectedValue.ToString()));
                        row["Gender"] = gender;

                    }


                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name.
                        sqlBulkCopy.DestinationTableName = "dbo.Doctor";

                        // Map the Excel columns with that of the database table, this is optional but good if you do
                        // 
                        sqlBulkCopy.ColumnMappings.Add("CreatedAt", "CreatedAt");
                        sqlBulkCopy.ColumnMappings.Add("CreatedBy", "CreatedBy");
                        sqlBulkCopy.ColumnMappings.Add("UpdatedAt", "UpdatedAt");
                        sqlBulkCopy.ColumnMappings.Add("UpdatedBy", "UpdatedBy");
                        sqlBulkCopy.ColumnMappings.Add("IsDelete", "IsDelete");
                        sqlBulkCopy.ColumnMappings.Add("BranchId", "BranchId");
                        sqlBulkCopy.ColumnMappings.Add("Name", "Name");
                        sqlBulkCopy.ColumnMappings.Add("NRC", "NRC");
                        sqlBulkCopy.ColumnMappings.Add("DOB", "DOB");
                        sqlBulkCopy.ColumnMappings.Add("Phone", "Phone");
                        sqlBulkCopy.ColumnMappings.Add("Speciality", "SpecialityId");
                        sqlBulkCopy.ColumnMappings.Add("Age", "Age");
                        sqlBulkCopy.ColumnMappings.Add("Gender", "Gender");
                        sqlBulkCopy.ColumnMappings.Add("ProfileImage", "ProfileImage");
                        sqlBulkCopy.ColumnMappings.Add("Brief", "Brief");
                        sqlBulkCopy.ColumnMappings.Add("SamaNumber", "SamaNumber");
                        sqlBulkCopy.ColumnMappings.Add("CFFee", "CFFee");
                        sqlBulkCopy.ColumnMappings.Add("Code", "Code");
                        sqlBulkCopy.ColumnMappings.Add("Image", "Image");
                        sqlBulkCopy.ColumnMappings.Add("RoundFee", "RoundFee");
                        sqlBulkCopy.ColumnMappings.Add("EstimatewaitingTime", "EstimatewaitingTime");
                        sqlBulkCopy.ColumnMappings.Add("CFFeeForHospital", "CFFeeForHospital");
                        sqlBulkCopy.ColumnMappings.Add("RoundFeeForHospital", "RoundFeeForHospital");
                        sqlBulkCopy.ColumnMappings.Add("ReadingFee", "ReadingFee");
                        sqlBulkCopy.ColumnMappings.Add("DressingFee", "DressingFee");
                        con.Open();
                        sqlBulkCopy.WriteToServer(dt);
                        con.Close();
                    }
                    
                   

                }
            }


            return RedirectToAction("ScheduleImport");

        }

        public IActionResult Export()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[19] { new DataColumn("Name"),
                                            new DataColumn("NRC"),
                                            new DataColumn("DOB"),
                                            new DataColumn("Phone"),
                                            new DataColumn("Speciality"),
                                            new DataColumn("Age"),
                                            new DataColumn("Gender"),
                                            new DataColumn("ProfileImage"),
                                            new DataColumn("Brief"),
                                            new DataColumn("SamaNumber"),
                                            new DataColumn("CFFee"),
                                            new DataColumn("Code"),
                                            new DataColumn("Image"),
                                            new DataColumn("RoundFee"),
                                            new DataColumn("EstimatewaitingTime"),
                                            new DataColumn("CFFeeForHospital"),
                                            new DataColumn("RoundFeeForHospital"),
                                            new DataColumn("ReadingFee"),
                                            new DataColumn("DressingFee"),
                                            

                                        });



            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Doctor.xlsx");
                }
            }
        }
        public IActionResult ScheduleImport()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ScheduleImportExcelFile(IFormFile FormFile)
        {
            if (FormFile == null)
            {
                return RedirectToAction("Import");
            }
            else
            {


                //get file name
                var filename = ContentDispositionHeaderValue.Parse(FormFile.ContentDisposition).FileName.Trim('"');
                //get path
                var MainPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads");

                //create directory "Uploads" if it doesn't exists
                if (!Directory.Exists(MainPath))
                {
                    Directory.CreateDirectory(MainPath);
                }

                //get file path 
                var filePath = Path.Combine(MainPath, FormFile.FileName);
                using (System.IO.Stream stream = new FileStream(filePath, FileMode.Create))
                {
                    await FormFile.CopyToAsync(stream);
                }

                //get extension
                string extension = Path.GetExtension(filename);


                string conString = string.Empty;

                switch (extension)
                {
                    case ".xls": //Excel 97-03.
                        conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
                        break;
                    case ".xlsx": //Excel 07 and above.
                        conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
                        break;
                }

                DataTable dt = new DataTable();
                conString = string.Format(conString, filePath);

                using (OleDbConnection connExcel = new OleDbConnection(conString))
                {
                    using (OleDbCommand cmdExcel = new OleDbCommand())
                    {
                        using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                        {
                            cmdExcel.Connection = connExcel;

                            //Get the name of First Sheet.
                            connExcel.Open();
                            DataTable dtExcelSchema;
                            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                            connExcel.Close();

                            //Read Data from First Sheet.
                            connExcel.Open();
                            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                            odaExcel.SelectCommand = cmdExcel;
                            odaExcel.Fill(dt);
                            connExcel.Close();
                        }
                    }
                }
                //your database connection string
                conString = _configuration.GetConnectionString("DefaultConnection");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    Schedule schedule = new Schedule();
                    schedule = await _userManager.AddUserAndTimestamp(schedule, User, DbEnum.DbActionEnum.Create); dt.Columns.Add("BranchId", typeof(Int32));
                    dt.Columns.Add("IsDelete", typeof(bool));
                    dt.Columns.Add("CreatedAt", typeof(DateTime));
                    dt.Columns.Add("CreatedBy", typeof(string));
                    dt.Columns.Add("UpdatedAt", typeof(DateTime));
                    dt.Columns.Add("UpdatedBy", typeof(string));


                   
                   
                    List<Schedule> schedules = new List<Schedule>();
                    foreach (var row in dt.AsEnumerable().ToList())
                    {

                        string dname = row["Doctor"].ToString();
                        string dename = row["Department"].ToString();
                        row["Doctor"] = _context.Doctors.Where(x => x.Name == dname).First().Id;
                        row["Department"] = _context.Departments.Where(x => x.Name == dename).First().Id;
                        DateTime dttime = Convert.ToDateTime(row["ToTime"]);
                        DateTime dftime = Convert.ToDateTime(row["FromTime"]);
                        object ttSpan = dttime.TimeOfDay;
                        object tfSpan = dftime.TimeOfDay;
                        //string ttstring = dttime.ToString("hh:mm:ss");
                        //string tfstring = dftime.ToString("hh:mm:ss");
                        //row["ToTime"] = ttSpan.ToString();
                        //row["FromTime"] = tfSpan.ToString();
                        //Debug.WriteLine("My debug string here");
                        //Debug.WriteLine(row["ToTime"]);



                        //schedule.DoctorId = _context.Doctors.Where(x => x.Name == dname).First().Id;
                        //schedule.DepartmentId = _context.Departments.Where(x => x.Name == dename).First().Id;
                        var daysOfWeek = Enum.GetValues(typeof(DayOfWeek))
                        .OfType<DayOfWeek>().ToList();
                        //.OrderBy(day => day < DayOfWeek.Monday)
                        //.Select(x => new SelectListItem(x.ToString(), ((int)x).ToString()));
                        var selectlist = new SelectList(daysOfWeek, "Value", "Text", row["DayOfWeek"]);

                        int dayOfWeek = ((int)Enum.Parse(typeof(DayOfWeek), selectlist.SelectedValue.ToString()));
                        row["DayOfWeek"] = dayOfWeek;
                        row["FromTime"] = (TimeSpan)tfSpan;
                        row["ToTime"] = (TimeSpan)ttSpan;
                        //schedule.FromTime = ttSpan;
                        //schedule.ToTime = tfSpan;
                        //schedules.Add(schedule);
                    }
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name.
                        sqlBulkCopy.DestinationTableName = "dbo.Schedule";
                        sqlBulkCopy.ColumnMappings.Add("CreatedAt", "CreatedAt");
                        sqlBulkCopy.ColumnMappings.Add("CreatedBy", "CreatedBy");
                        sqlBulkCopy.ColumnMappings.Add("UpdatedAt", "UpdatedAt");
                        sqlBulkCopy.ColumnMappings.Add("UpdatedBy", "UpdatedBy");
                        sqlBulkCopy.ColumnMappings.Add("IsDelete", "IsDelete");
                        sqlBulkCopy.ColumnMappings.Add("BranchId", "BranchId");
                        sqlBulkCopy.ColumnMappings.Add("Doctor", "DoctorId");
                        sqlBulkCopy.ColumnMappings.Add("Department", "DepartmentId");
                        sqlBulkCopy.ColumnMappings.Add("DayOfWeek", "DayOfWeek");
                        sqlBulkCopy.ColumnMappings.Add("FromTime", "FromTime");
                        sqlBulkCopy.ColumnMappings.Add("ToTime", "ToTime");

                        con.Open();
                        sqlBulkCopy.WriteToServer(dt);
                        con.Close();
                    }

                }
            }


            return RedirectToAction("Import");

        }

        public IActionResult ScheduleExport()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[5] { 
                                            new DataColumn("Doctor"),
                                            new DataColumn("Department"),
                                            new DataColumn("DayOfWeek"),
                                            new DataColumn("FromTime",typeof(TimeSpan)),
                                            new DataColumn("ToTime",typeof(TimeSpan))

                                        });



            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Schedule.xlsx");
                }
            }
        }

    }
}