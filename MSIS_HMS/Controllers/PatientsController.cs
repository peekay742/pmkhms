using ClosedXML.Excel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Reporting.NETCore;
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
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;
using ZXing;
using ZXing.QrCode;
using static MSIS_HMS.Core.Enums.GenderEnum;

namespace MSIS_HMS.Controllers
{
    public class PatientsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IPatientRepository _patientRepository;
        private readonly IPatientResultImageRepository _patientResultImageRepository;
        private readonly IReferrerRepository _referrerRepository;
        private readonly IBranchService _branchService;
        private readonly IUserService _userService;
        private readonly IMedicalRecordRepository _medicalRecordRepository;
        private readonly IVisitRepository _visitRepository;
        private readonly Pagination _pagination;
        private readonly ILogger<PatientsController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnv;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PatientsController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IPatientRepository patientRepository, IReferrerRepository referrerRepository, IBranchService branchService, IUserService userService, IOptions<Pagination> pagination, ILogger<PatientsController> logger,IMedicalRecordRepository medicalRecordRepository,IVisitRepository visitRepository,IPatientResultImageRepository patientResultImageRepository,IConfiguration configuration, IHostingEnvironment hostingEnvironment,IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _context = context;
            _patientRepository = patientRepository;
            _referrerRepository = referrerRepository;
            _branchService = branchService;
            _userService = userService;
            _pagination = pagination.Value;
            _logger = logger;
            _medicalRecordRepository = medicalRecordRepository;
            _visitRepository = visitRepository;
            _patientResultImageRepository = patientResultImageRepository;
            _configuration = configuration; 
            _hostingEnv = hostingEnvironment;
            _webHostEnvironment = webHostEnvironment;
        }

        public void Initialize(Patient patient = null)
        {
            var countries = _context.Countries.Where(x => x.IsDelete == false).ToList();
            ViewData["Countries"] = new SelectList(countries, "Id", "Name", patient?.CountryId);
            var states = _context.States.Where(x=> x.IsDelete == false && (patient == null || x.CountryId==patient.CountryId)).ToList();
            ViewData["States"] = new SelectList(states, "Id", "Name", patient?.StateId);
            var cities = _context.Cities.Where(x => x.IsDelete == false && (patient == null || x.StateId == patient.StateId)).ToList();
            ViewData["Cities"] = new SelectList(cities, "Id", "Name", patient?.CityId);
            var townships = _context.Townships.Where(x => x.IsDelete == false && (patient==null || x.CityId==patient.CityId)).ToList();
            ViewData["Townships"] = new SelectList(townships, "Id", "Name", patient?.TownshipId);
            ViewData["Referrers"] = _referrerRepository.GetAll(_userService.Get(User).BranchId).GetSelectListItems("Id", "Name", patient?.ReferrerId);
        }

        // GET
        public IActionResult Index(DateTime? StartRegDate = null, DateTime? EndRegDate = null, string RegNo = null, string Name = null, string NRC = null, string Guardian = null, DateTime? DateOfBirth = null, string Phone = null, string BloodType = null,string Code= null, int? page = 1)
        {

            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            var patients = _patientRepository.GetAll(_userService.Get(User).BranchId, null, StartRegDate, EndRegDate, RegNo, Name, NRC, Guardian, DateOfBirth, Phone, BloodType, Code,null).ToList();
            var branches = _branchService.GetAll();
            patients.ForEach(x => x.Branch = branches.SingleOrDefault(b => b.Id == x.BranchId));
            return View(patients.OrderByDescending(x => x.UpdatedAt).ToList().ToPagedList((int)page, pageSize));
        }

        public IActionResult Create()
        {
            Initialize();
            string format = "00000000";


            var patients = _patientRepository.GetAll(_userService.Get(User).BranchId);
            int patientcount = patients.Count + 1;
            var patientcountformat = patientcount.ToString(format);

           


            var patient = new Patient
            {
                //RegNo = _branchService.GetVoucherNo(VoucherTypeEnum.Patient)
                RegNo = "TS" + "-" + patientcountformat 
            };
            return View(patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Patient patient, bool? RedirectToVisit = null)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var filename = patient.ImageFile != null ? FtpHelper.ftpPatientImageFolderPath + patient.ImageFile.GetUniqueName() : "";
                    if (ModelState.IsValid)
                    {
                        var didUploaded = true;
                        if (patient.ImageFile != null)
                        {
                            didUploaded = false;
                            var uploadRes = FtpHelper.UploadFileToServer(patient.ImageFile, filename);
                            if (uploadRes.IsSucceed())
                            {
                                didUploaded = true;
                                patient.Image = uploadRes.ResponseUri.AbsolutePath;
                            }
                        }
                        if (didUploaded)
                        {
                            var branch = _branchService.GetBranchByUser();
                            patient.RegNo = _branchService.GetVoucherNo(VoucherTypeEnum.Patient, patient.RegNo);
                            if (string.IsNullOrEmpty(patient.RegNo))
                            {
                                ModelState.AddModelError("VoucherNo", "This field is required.");
                                Initialize(patient);
                                return View(patient);
                            }
                            patient = await _userManager.AddUserAndTimestamp(patient, User, DbEnum.DbActionEnum.Create);
                            patient.BarCode = patient.RegNo;
                            
                            string formatted = patient.RegDate.ToString("yyyy-MM-dd");

                            patient.QRCode = formatted + " " + patient.RegNo;
                            var _patient = await _patientRepository.AddAsync(patient);
                            if (_patient != null)
                            {
                                await _branchService.IncreaseVoucherNo(VoucherTypeEnum.Patient);
                                await transaction.CommitAsync();

                               

                                if (RedirectToVisit == true)
                                {
                                    return RedirectToAction("Create", "Visits", new { PatientId = _patient.Id });
                                }
                                TempData["notice"] = StatusEnum.NoticeStatus.Success;
                                _logger.LogInformation(Infrastructure.Enums.EnumExtension.ToDescription(StatusEnum.NoticeStatus.Success));
                                return RedirectToAction(nameof(Index));
                            }
                        }
                        throw new Exception();
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError(e.InnerException.Message);
                    await transaction.RollbackAsync();
                }
            }
            Initialize(patient);
            return View();
        }

        public IActionResult Edit(int id)
        {
            var patient = _patientRepository.Get(id);
            //var countries = _context.Countries.Where(x => x.IsDelete == false).ToList();
            //var states = _context.States.Where(x =>x.CountryId==patient.CountryId && x.IsDelete == false).ToList();
            //var cities = _context.Cities.Where(x => x.StateId == patient.StateId && x.IsDelete == false).ToList();
            patient.ImageContent = patient.Image.GetBase64();
            Initialize(patient);
            return View(patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Patient patient, bool? RedirectToVisit = null)
        {
            try
            {
                var filename = patient.ImageFile != null ? FtpHelper.ftpItemImageFolderPath + patient.ImageFile.GetUniqueName() : "";

                if (ModelState.IsValid)
                {
                    var didUploaded = true;
                    if (patient.ImageFile != null)
                    {
                        didUploaded = false;
                        var uploadRes = FtpHelper.UploadFileToServer(patient.ImageFile, filename);
                        if (uploadRes.IsSucceed())
                        {
                            var didDeleted = true;
                            if (FtpHelper.CheckIfFileExistsOnServer(patient.Image))
                            {
                                didDeleted = false;
                                var deleteRes = FtpHelper.DeleteFileOnServer(patient.Image);
                                if (deleteRes.IsSucceed())
                                {
                                    didDeleted = true;
                                }
                            }
                            if (didDeleted)
                            {
                                didUploaded = true;
                                patient.Image = uploadRes.ResponseUri.AbsolutePath;
                            }
                        }
                    }
                    if (didUploaded)
                    {
                        patient = await _userManager.AddUserAndTimestamp(patient, User, DbEnum.DbActionEnum.Update);
                        var _patient = await _patientRepository.UpdateAsync(patient);
                        if (RedirectToVisit == true)
                        {
                            return RedirectToAction("Create", "Visits", new { PatientId = _patient.Id });
                        }
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
            Initialize(patient);
            return View(patient);
        }

       
        public IActionResult PatientDetail(int patientId)
        {
            var user = _userService.Get(User);
            //var patient = _context.Patients.Where(x => x.Id == patientId).FirstOrDefault();
            PatientDetailDTO patientDetailDTO = new PatientDetailDTO();
            patientDetailDTO.Id = patientId;
            var visits = _visitRepository.GetAll(_userService.Get(User).BranchId, null, null, null, null, null, patientId, null, null, null);//_context.Visits.Where(x => x.PatientId == patientId).ToList();
            patientDetailDTO.visits = visits;
            var medicalRecords = _medicalRecordRepository.GetAll(user.BranchId, null, null, null, null, null, patientId, null); //_context.MedicalRecords.Where(x => x.PatientId == patientId).ToList();
            patientDetailDTO.medicalRecords = medicalRecords;
            var ipdRecords = _context.IPDRecords.Where(x => x.PatientId == patientId).ToList();
            patientDetailDTO.iPDRecords = ipdRecords;
            return View(patientDetailDTO);
        }
        [HttpGet]
        public IActionResult GetPatientDetail(int patientId)
        {
            var patient = _context.Patients.Where(x => x.Id == patientId).FirstOrDefault();
            PatientDetailDTO patientDetailDTO = new PatientDetailDTO();
            patientDetailDTO.Id = patient.Id;
            patientDetailDTO.Name = patient.Name;
            patientDetailDTO.Nrc = patient.NRC;
            patientDetailDTO.Address = patient.Address;
            patientDetailDTO.DateOfBirth = patient.DateOfBirth;
            patientDetailDTO.Gender =  patient.Gender.ToString();
            patientDetailDTO.Religion = patient.Religion;
            patientDetailDTO.Phone = patient.Phone;
            patientDetailDTO.Image = patient.Image.GetBase64();

            return Ok(patientDetailDTO);
        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var _patient = await _patientRepository.DeleteAsync(id);
                TempData["notice"] = StatusEnum.NoticeStatus.Delete;
                _logger.LogInformation(Infrastructure.Enums.EnumExtension.ToDescription(StatusEnum.NoticeStatus.Delete));
            }
            catch (Exception e)
            {
                _logger.LogError(e.InnerException.Message);
            }

            return RedirectToAction(nameof(Index));
        }
        public IActionResult GetState(int? countryId)
        {
            var states = _context.States.Where(x => x.CountryId == countryId && x.IsDelete == false).ToList();
            return Ok(states);
        }

        public IActionResult GetCity(int? stateId)
        {
            var cities = _context.Cities.Where(x => x.StateId == stateId && x.IsDelete == false).ToList();
            return Ok(cities);
        }
        public IActionResult GetTownship(int? cityId)
        {
            var townships = _context.Townships.Where(x => x.CityId == cityId && x.IsDelete == false).ToList();
            return Ok(townships);
        }
        public ActionResult Image(string path)
        {
            return File(FtpHelper.DownloadFileFromServer(path), "image/png");
        }

        public IActionResult ResultImage()
        {
            ViewData["Patients"] = _patientRepository.GetAll(_userService.Get(User).BranchId).GetSelectListItems("Id", "Name");

            return View();
        }

        [HttpGet]
        public IActionResult GenerateQRCode(string regno,string date)
        {
            Byte[] byteArray;
            var width = 250; // width of the Qr Code   
            var height = 250; // height of the Qr Code   
            var margin = 0;
            var qrCodeWriter = new ZXing.BarcodeWriterPixelData
            {
                Format = ZXing.BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Height = height,
                    Width = width,
                    Margin = margin
                }
            };

            string qrtext = date + " " + regno;
            var pixelData = qrCodeWriter.Write(qrtext);

            // creating a bitmap from the raw pixel data; if only black and white colors are used it makes no difference   
            // that the pixel data ist BGRA oriented and the bitmap is initialized with RGB   
            using (var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
            {
                using (var ms = new MemoryStream())
                {
                    var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                    try
                    {
                        // we assume that the row stride of the bitmap is aligned to 4 byte multiplied by the width of the image   
                        System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                    }
                    finally
                    {
                        bitmap.UnlockBits(bitmapData);
                    }
                    // save to stream as PNG   
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byteArray = ms.ToArray();
                    string base64String = "data:image/png;base64," + Convert.ToBase64String(byteArray, 0, byteArray.Length);
                    return Ok(base64String);

                }
            }           
        }
        public IActionResult GenerateBarcode(string regno)
        {
            Byte[] byteArray;
            var width = 250; // width of the Qr Code   
            var height = 250; // height of the Qr Code   
            var margin = 0;
            var qrCodeWriter = new ZXing.BarcodeWriterPixelData
            {
                Format = ZXing.BarcodeFormat.CODE_93,
                Options = new QrCodeEncodingOptions
                {
                    Height = height,
                    Width = width,
                    Margin = margin
                }
            };
            var pixelData = qrCodeWriter.Write(regno);

            // creating a bitmap from the raw pixel data; if only black and white colors are used it makes no difference   
            // that the pixel data ist BGRA oriented and the bitmap is initialized with RGB   
            using (var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
            {
                using (var ms = new MemoryStream())
                {
                    var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                    try
                    {
                        // we assume that the row stride of the bitmap is aligned to 4 byte multiplied by the width of the image   
                        System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                    }
                    finally
                    {
                        bitmap.UnlockBits(bitmapData);
                    }
                    // save to stream as PNG   
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byteArray = ms.ToArray();
                    string base64String = "data:image/png;base64," + Convert.ToBase64String(byteArray, 0, byteArray.Length);
                    return Ok(base64String);

                }
            }
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
                    Patient patient = new Patient();
                    patient = await _userManager.AddUserAndTimestamp(patient, User, DbEnum.DbActionEnum.Create);
                    dt.Columns.Add("BranchId", typeof(Int32));
                    dt.Columns.Add("IsDelete", typeof(bool));
                    dt.Columns.Add("CreatedAt", typeof(DateTime));
                    dt.Columns.Add("CreatedBy", typeof(string));
                    dt.Columns.Add("UpdatedAt", typeof(DateTime));
                    dt.Columns.Add("UpdatedBy", typeof(string));

                    foreach (var row in dt.AsEnumerable().ToList())
                    {
                        string name = row["Doctor"].ToString();
                        string cname = row["City"].ToString();
                        string coname = row["Country"].ToString();
                        string sname = row["State"].ToString();
                        string tname = row["Township"].ToString();
                        string rname = row["Referrer"].ToString();
                        row["Referrer"] = _context.Referrers.Where(x => x.Name == rname).First().Id;
                        row["Doctor"] = _context.Doctors.Where(x => x.Name == name).First().Id;
                        row["City"] = _context.Cities.Where(x => x.Name == cname).First().Id;
                        row["Country"]= _context.Countries.Where(x=>x.Name==coname).First().Id;
                        row["State"]= _context.States.Where(x=>x.Name == sname).First().Id;
                        row["Township"] = _context.Townships.Where(x => x.Name == tname).First().Id;
                        row["BranchId"] = patient.BranchId;
                        row["IsDelete"] = patient.IsDelete;
                        row["CreatedAt"] = patient.CreatedAt;
                        row["CreatedBy"] = patient.CreatedBy;
                        row["UpdatedAt"] = patient.UpdatedAt;
                        row["UpdatedBy"] = patient.UpdatedBy;
                        var genders = Enum.GetValues(typeof(Gender))
                       .OfType<Gender>().ToList();
                        var selectgender = new SelectList(genders, "Value", "Text", row["Gender"]);
                        int gender = ((int)Enum.Parse(typeof(Gender), selectgender.SelectedValue.ToString()));
                        row["Gender"] = gender;

                    }
                    DataTable dtp = new DataTable();

                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name.
                        sqlBulkCopy.DestinationTableName = "dbo.Patient";

                        // Map the Excel columns with that of the database table, this is optional but good if you do
                        // 
                        sqlBulkCopy.ColumnMappings.Add("CreatedAt", "CreatedAt");
                        sqlBulkCopy.ColumnMappings.Add("CreatedBy", "CreatedBy");
                        sqlBulkCopy.ColumnMappings.Add("UpdatedAt", "UpdatedAt");
                        sqlBulkCopy.ColumnMappings.Add("UpdatedBy", "UpdatedBy");
                        sqlBulkCopy.ColumnMappings.Add("IsDelete", "IsDelete");
                        sqlBulkCopy.ColumnMappings.Add("BranchId", "BranchId");
                        sqlBulkCopy.ColumnMappings.Add("RegDate", "RegDate");
                        sqlBulkCopy.ColumnMappings.Add("RegNo", "RegNo");
                        sqlBulkCopy.ColumnMappings.Add("Name", "Name");
                        sqlBulkCopy.ColumnMappings.Add("NRC", "NRC");
                        sqlBulkCopy.ColumnMappings.Add("Gender", "Gender");
                        sqlBulkCopy.ColumnMappings.Add("DateOfBirth", "DateOfBirth");
                        sqlBulkCopy.ColumnMappings.Add("AgeYear", "AgeYear");
                        sqlBulkCopy.ColumnMappings.Add("AgeMonth", "AgeMonth");
                        sqlBulkCopy.ColumnMappings.Add("AgeDay", "AgeDay");
                        sqlBulkCopy.ColumnMappings.Add("Address", "Address");
                        sqlBulkCopy.ColumnMappings.Add("Phone", "Phone");
                        sqlBulkCopy.ColumnMappings.Add("Doctor", "DoctorId");
                        sqlBulkCopy.ColumnMappings.Add("IsActive", "IsActive");
                        sqlBulkCopy.ColumnMappings.Add("Allergies", "Allergies");
                        sqlBulkCopy.ColumnMappings.Add("BloodType", "BloodType");
                        sqlBulkCopy.ColumnMappings.Add("Guardian", "Guardian");
                        sqlBulkCopy.ColumnMappings.Add("Occupation", "Occupation");
                        sqlBulkCopy.ColumnMappings.Add("PlaceOfBirth", "PlaceOfBirth");
                        sqlBulkCopy.ColumnMappings.Add("ReferredBy", "ReferredBy");
                        sqlBulkCopy.ColumnMappings.Add("ReferredDate", "ReferredDate");
                        sqlBulkCopy.ColumnMappings.Add("Referrer", "ReferrerId");
                        sqlBulkCopy.ColumnMappings.Add("Religion", "Religion");
                        sqlBulkCopy.ColumnMappings.Add("Status", "Status");
                        sqlBulkCopy.ColumnMappings.Add("City", "CityId");
                        sqlBulkCopy.ColumnMappings.Add("Country", "CountryId");
                        sqlBulkCopy.ColumnMappings.Add("State", "StateId");
                        sqlBulkCopy.ColumnMappings.Add("Township", "TownshipId");
                        sqlBulkCopy.ColumnMappings.Add("Image", "Image");
                        sqlBulkCopy.ColumnMappings.Add("BarCode", "BarCode");
                        sqlBulkCopy.ColumnMappings.Add("QRCode", "QRCode");
                        sqlBulkCopy.ColumnMappings.Add("Code", "Code");

                        con.Open();
                        sqlBulkCopy.WriteToServer(dt);
                        con.Close();
                    }

                    
                   
                }
            }

            return RedirectToAction("Index");

        }

        public IActionResult Export()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[31] { new DataColumn("RegDate"),
                                            new DataColumn("RegNo"),
                                            new DataColumn("Name"),
                                            new DataColumn("NRC"),
                                            new DataColumn("Gender"),
                                            new DataColumn("DateOfBirth"),
                                            new DataColumn("AgeYear"),
                                            new DataColumn("AgeMonth"),
                                            new DataColumn("AgeDay"),
                                            new DataColumn("Address"),
                                            new DataColumn("Phone"),
                                            new DataColumn("Doctor"),
                                            new DataColumn("IsActive"),
                                            new DataColumn("Allergies"),
                                            new DataColumn("BloodType"),
                                            new DataColumn("Guardian"),
                                            new DataColumn("Occupation"),
                                            new DataColumn("PlaceOfBirth"),
                                            new DataColumn("ReferredBy"),
                                            new DataColumn("ReferredDate"),
                                            new DataColumn("Referrer"),
                                            new DataColumn("Religion"),
                                            new DataColumn("Status"),
                                            new DataColumn("City"),
                                            new DataColumn("Country"),
                                            new DataColumn("State"),
                                            new DataColumn("Township"),
                                            new DataColumn("Image"),
                                            new DataColumn("BarCode"),
                                            new DataColumn("QRCode"),
                                            new DataColumn("Code")

                                        });



            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Patient.xlsx");
                }
            }
        }

        public IActionResult PrintBarCodeandQRCode(int patientId)
        {
            ////////////////////Print BarCode and QRCode with PDF////////////////////
            string renderFormat = "PDF";
            string extension = "pdf";
            string mimetype = "application/pdf";
            using (var report = new LocalReport())
            {
                List<PatientQRCodeDTO> patients = new List<PatientQRCodeDTO>();
                PatientQRCodeDTO qrcodeDTO = new PatientQRCodeDTO();
                var patientInfo = _context.Patients.Where(x => x.Id == patientId).FirstOrDefault();


                string[] splitBarCodeVal = patientInfo.BarCodeImg.Split(new string[] { "base64," }, StringSplitOptions.None);
                string imgBarCodeLink = splitBarCodeVal[1];
                qrcodeDTO.BarCodeImg = System.Convert.FromBase64String(imgBarCodeLink);

                string[] splitQRCodeVal = patientInfo.QRCodeImg.Split(new string[] { "base64," }, StringSplitOptions.None);
                string imgQRCodeLink = splitQRCodeVal[1];
                qrcodeDTO.QRCodeImg = System.Convert.FromBase64String(imgQRCodeLink);

                qrcodeDTO.BarCode = patientInfo.BarCode;
                qrcodeDTO.QRCode = patientInfo.QRCode;

                patients.Add(qrcodeDTO);
                List<Branch> branches = new List<Branch>();
                //var branch = _branchService.GetBranchById(medicalRecord.BranchId);
                // branches.Add(branch);
                report.DataSources.Add(new ReportDataSource("PatientInfoDataset", patients));
                //report.DataSources.Add(new ReportDataSource("Symptom", symptom));
                //report.DataSources.Add(new ReportDataSource("Diagnostic", diagnostic));
                //report.DataSources.Add(new ReportDataSource("Prescription", prescription));
                //report.DataSources.Add(new ReportDataSource("Branch", branches));
                report.ReportPath = $"{_webHostEnvironment.WebRootPath}\\ReportFiles\\PrintBarAndQRCode.rdlc";

                var pdf = report.Render(renderFormat);
                return File(pdf, mimetype, "QRCode" + "." + extension);
            }


        }

    }
}   