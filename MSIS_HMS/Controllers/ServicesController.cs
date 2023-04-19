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
using MSIS_HMS.Infrastructure.Data;
using MSIS_HMS.Infrastructure.Enums;
using MSIS_HMS.Infrastructure.Helpers;
using MSIS_HMS.Interfaces;
using MSIS_HMS.Models;
using NLog;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using X.PagedList;

namespace MSIS_HMS.Controllers
{
    public class ServicesController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IServiceTypeRepository _serviceTypeRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IUserService _userService;
        private readonly IOutletRepository _outletRepository;
        private readonly IBranchService _branchService;
        private readonly ILogger<ServicesController> _logger;
        private readonly Pagination _pagination;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnv;

        public ServicesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IUserService userService, IDepartmentRepository departmentRepository, IServiceTypeRepository serviceTypeRepository, IServiceRepository serviceRepository, ILogger<ServicesController> logger, IOutletRepository outletRepository, IBranchService branchService, IOptions<Pagination> pagination, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _departmentRepository = departmentRepository;
            _serviceTypeRepository = serviceTypeRepository;
            _serviceRepository = serviceRepository;
            _userService = userService;
            _logger = logger;
            _outletRepository = outletRepository;
            _branchService = branchService;
            _pagination = pagination.Value;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _hostingEnv = hostingEnvironment;
        }

        public void Initialize()
        {
            var serviceTypes = _serviceTypeRepository.GetAll(_userService.Get(User).BranchId);
            ViewData["ServiceTypes"] = new SelectList(serviceTypes, "Id", "Name");
        }

        public IActionResult Index()
        {
            var service = _serviceRepository.GetAll();
            foreach (var obj in service)
            {
                obj.ServiceType = _context.ServiceTypes.FirstOrDefault(u => u.Id == obj.ServiceTypeId);
            }
            return View(service);
        }
        public IActionResult DailyServiceReport(int? page = 1, DateTime? FromDate = null, DateTime? ToDate = null, int? OutletId = null)
        {
            if (FromDate == null)
            {
                FromDate = DateTime.Now.Date;
                ToDate = DateTime.Now.Date;
            }
            var outlets = _outletRepository.GetAll(_branchService.GetBranchIdByUser());
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            ViewData["Outlets"] = new SelectList(outlets, "Id", "Name");
            var items = _serviceRepository.GetServiceFromOrder(_userService.Get(User).BranchId, FromDate, ToDate, OutletId);
            TempData["Page"] = page;
            TempData["FromDate"] = FromDate;
            TempData["ToDate"] = ToDate;
            TempData["OutletId"] = OutletId;
            return View(items.ToList().ToPagedList((int)page, pageSize));
        }
        public ActionResult Create()
        {
            Initialize();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Service service)
        {
            MappedDiagnosticsLogicalContext.Set("userId", _userManager.GetUserId(User));
            try
            {
                if (ModelState.IsValid)
                {
                    service = await _userManager.AddUserAndTimestamp(service, User, DbEnum.DbActionEnum.Create);
                    var _service = await _serviceRepository.AddAsync(service);
                    if (_service != null)
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

        public ActionResult Edit(int id)
        {
            Initialize();
            var _service = _serviceRepository.Get(id);
            return View(_service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Service service)
        {
            MappedDiagnosticsLogicalContext.Set("userId", _userManager.GetUserId(User));
            try
            {
                if (ModelState.IsValid)
                {
                    service = await _userManager.AddUserAndTimestamp(service, User, DbEnum.DbActionEnum.Update);
                    var _service = await _serviceRepository.UpdateAsync(service);
                    TempData["notice"] = StatusEnum.NoticeStatus.Edit;
                    _logger.LogInformation(Infrastructure.Enums.EnumExtension.ToDescription(StatusEnum.NoticeStatus.Edit));
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                _logger.LogError(e.InnerException.Message);
            }
            Initialize();
            return View(service);
        }

        public async Task<ActionResult> Delete(int id)
        {
            MappedDiagnosticsLogicalContext.Set("userId", _userManager.GetUserId(User));
            try
            {
                var _service = await _serviceRepository.DeleteAsync(id);
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
            var units = _serviceRepository.GetAll(_userService.Get(User).BranchId);
            return Ok(units.OrderBy(x => x.Name).ToList());
        }
        public IActionResult DownloadReport()
        {
            var page = TempData["Page"];

            DateTime? fromDate = Convert.ToDateTime(TempData["FromDate"]);
            DateTime? toDate = Convert.ToDateTime(TempData["ToDate"]);
            int? outletId = null;
            if (TempData["FromDate"] == null)
            {
                fromDate = DateTime.Now.Date;
                toDate = DateTime.Now.Date;

            }
            if (TempData["OutletId"] != null)
            {
                outletId = (int)TempData["OutletId"];
            }
            string renderFormat = "PDF";
            string extension = "pdf";
            string mimetype = "application/pdf";
            using (var report = new LocalReport())
            {
                List<ServiceDTO> Orders = new List<ServiceDTO>();
                List<Branch> branches = new List<Branch>();

                var user = _userService.Get(User);
                Orders = _serviceRepository.GetServiceFromOrder(_userService.Get(User).BranchId, fromDate, toDate, outletId);

                var branch = _branchService.GetBranchById((int)user.BranchId);
                branches.Add(branch);
                ReportParameter[] parameters = new ReportParameter[2];
                parameters[0] = new ReportParameter("FromDate", fromDate.ToString());
                parameters[1] = new ReportParameter("ToDate", toDate.ToString());

                report.DataSources.Add(new ReportDataSource("Branch", branches));
                report.DataSources.Add(new ReportDataSource("dsService", Orders));

                report.ReportPath = $"{_webHostEnvironment.WebRootPath}\\ReportFiles\\ServiceReport.rdlc";
                report.SetParameters(parameters);
                var pdf = report.Render(renderFormat); TempData["Page"] = page;
                TempData["FromDate"] = fromDate;
                TempData["ToDate"] = toDate;
                TempData["OutletId"] = outletId;
                return File(pdf, mimetype, "DailyServiceReport_" + DateTime.Now + "." + extension);
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
                    Service service = new Service();
                    service = await _userManager.AddUserAndTimestamp(service, User, DbEnum.DbActionEnum.Create);
                    dt.Columns.Add("BranchId", typeof(Int32));
                    dt.Columns.Add("IsDelete", typeof(bool));
                    dt.Columns.Add("CreatedAt", typeof(DateTime));
                    dt.Columns.Add("CreatedBy", typeof(string));
                    dt.Columns.Add("UpdatedAt", typeof(DateTime));
                    dt.Columns.Add("UpdatedBy", typeof(string));
                    foreach (var row in dt.AsEnumerable().ToList())
                    {
                        string name = row["ServiceType"].ToString();
                        row["ServiceType"] = _context.ServiceTypes.Where(x => x.Name == name).First().Id;
                        row["BranchId"] = service.BranchId;
                        row["IsDelete"] = service.IsDelete;
                        row["CreatedAt"] = service.CreatedAt;
                        row["CreatedBy"] = service.CreatedBy;
                        row["UpdatedAt"] = service.UpdatedAt;
                        row["UpdatedBy"] = service.UpdatedBy;
                    }
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name.
                        sqlBulkCopy.DestinationTableName = "dbo.Service";

                        // Map the Excel columns with that of the database table, this is optional but good if you do
                        // 
                        sqlBulkCopy.ColumnMappings.Add("CreatedAt", "CreatedAt");
                        sqlBulkCopy.ColumnMappings.Add("CreatedBy", "CreatedBy");
                        sqlBulkCopy.ColumnMappings.Add("UpdatedAt", "UpdatedAt");
                        sqlBulkCopy.ColumnMappings.Add("UpdatedBy", "UpdatedBy");
                        sqlBulkCopy.ColumnMappings.Add("IsDelete", "IsDelete");
                        sqlBulkCopy.ColumnMappings.Add("BranchId", "BranchId");
                        sqlBulkCopy.ColumnMappings.Add("Name", "Name");
                        sqlBulkCopy.ColumnMappings.Add("Description", "Description");
                        sqlBulkCopy.ColumnMappings.Add("ServiceType", "ServiceTypeId");
                        sqlBulkCopy.ColumnMappings.Add("Code", "Code");
                        sqlBulkCopy.ColumnMappings.Add("ServiceFee", "ServiceFee");
                        con.Open();
                        sqlBulkCopy.WriteToServer(dt);
                        con.Close();
                    }
                }
                //if the code reach here means everthing goes fine and excel data is imported into database
            }


            return RedirectToAction("Index");

        }

        public IActionResult Export()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[5] { new DataColumn("Name"),
                                            new DataColumn("Description"),
                                            new DataColumn("ServiceType"),
                                            new DataColumn("Code"),
                                            new DataColumn("ServiceFee")

                                        });



            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Service.xlsx");
                }
            }
        }

    }
}
