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
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using System.Net.Http.Headers;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using ClosedXML.Excel;

namespace MSIS_HMS.Controllers
{
    public class VisitTypesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IVisitTypeRepository _visitTypeRepository;
        private readonly IBranchService _branchService;
        private readonly IUserService _userService;
        private readonly Pagination _pagination;
        private readonly ILogger<VisitTypesController> _logger;
        private IConfiguration Configuration;
        private IHostingEnvironment _hostingEnv;

        public VisitTypesController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IVisitTypeRepository visitTypeRepository, IBranchService branchService, IUserService userService, IOptions<Pagination> pagination, ILogger<VisitTypesController> logger, IConfiguration _configuration, IHostingEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _context = context;
            _visitTypeRepository = visitTypeRepository;
            _branchService = branchService;
            _userService = userService;
            _pagination = pagination.Value;
            _logger = logger;
            Configuration = _configuration;
            _hostingEnv = hostingEnvironment;
        }

        public void Initialize(VisitType visitType = null)
        {
        }

        // GET
        public IActionResult Index(string VisitTypeName = null, int? page = 1)
        {

            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            var visitTypes = _visitTypeRepository.GetAll(_userService.Get(User).BranchId).Where(itemtype => string.IsNullOrEmpty(VisitTypeName) || (itemtype.Type.ToLower().Contains(VisitTypeName.ToLower())) || VisitTypeName.ToLower().Contains(itemtype.Type.ToLower())).ToList();
            var branches = _branchService.GetAll();
            visitTypes.ForEach(x => x.Branch = branches.SingleOrDefault(b => b.Id == x.BranchId));
            return View(visitTypes.OrderByDescending(x => x.UpdatedAt).ToList().ToPagedList((int)page, pageSize));
        }

        public IActionResult Create()
        {
            Initialize();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VisitType visitType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    visitType = await _userManager.AddUserAndTimestamp(visitType, User, DbEnum.DbActionEnum.Create);
                    var _visitType = await _visitTypeRepository.AddAsync(visitType);
                    if (_visitType != null)
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
            Initialize(visitType);
            return View();
        }

        public IActionResult Edit(int id)
        {
            var visitType = _visitTypeRepository.Get(id);
            Initialize(visitType);
            return View(visitType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VisitType visitType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    visitType = await _userManager.AddUserAndTimestamp(visitType, User, DbEnum.DbActionEnum.Update);
                    var _visitType = await _visitTypeRepository.UpdateAsync(visitType);
                    TempData["notice"] = StatusEnum.NoticeStatus.Edit;
                    _logger.LogInformation(Infrastructure.Enums.EnumExtension.ToDescription(StatusEnum.NoticeStatus.Edit));
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.InnerException.Message);
            }
            Initialize(visitType);
            return View(visitType);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var _visitType = await _visitTypeRepository.DeleteAsync(id);
                TempData["notice"] = StatusEnum.NoticeStatus.Delete;
                _logger.LogInformation(Infrastructure.Enums.EnumExtension.ToDescription(StatusEnum.NoticeStatus.Delete));
            }
            catch (Exception e)
            {
                _logger.LogError(e.InnerException.Message);
            }

            return RedirectToAction(nameof(Index));
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
                conString = this.Configuration.GetConnectionString("DefaultConnection");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    VisitType visittype = new VisitType();
                    visittype = await _userManager.AddUserAndTimestamp(visittype, User, DbEnum.DbActionEnum.Create);
                    dt.Columns.Add("IsDelete", typeof(bool));
                    dt.Columns.Add("BranchId", typeof(Int32));
                    dt.Columns.Add("CreatedAt", typeof(DateTime));
                    dt.Columns.Add("CreatedBy", typeof(string));
                    dt.Columns.Add("UpdatedAt", typeof(DateTime));
                    dt.Columns.Add("UpdatedBy", typeof(string));
                    foreach (var row in dt.AsEnumerable().ToList())
                    {
                        row["IsDelete"] = visittype.IsDelete;
                        row["BranchId"] = visittype.BranchId;
                        row["CreatedAt"] = visittype.CreatedAt;
                        row["CreatedBy"] = visittype.CreatedBy;
                        row["UpdatedAt"] = visittype.UpdatedAt;
                        row["UpdatedBy"] = visittype.UpdatedBy;
                    }
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name.
                        sqlBulkCopy.DestinationTableName = "dbo.VisitType";

                        // Map the Excel columns with that of the database table, this is optional but good if you do
                        // 
                        sqlBulkCopy.ColumnMappings.Add("CreatedAt", "CreatedAt");
                        sqlBulkCopy.ColumnMappings.Add("CreatedBy", "CreatedBy");
                        sqlBulkCopy.ColumnMappings.Add("UpdatedAt", "UpdatedAt");
                        sqlBulkCopy.ColumnMappings.Add("UpdatedBy", "UpdatedBy");
                        sqlBulkCopy.ColumnMappings.Add("Type", "Type");
                        sqlBulkCopy.ColumnMappings.Add("Remark", "Remark");
                        sqlBulkCopy.ColumnMappings.Add("BranchId", "BranchId");
                        sqlBulkCopy.ColumnMappings.Add("IsDelete", "IsDelete");

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
            dt.Columns.AddRange(new DataColumn[2] { new DataColumn("Type"),
                                        new DataColumn("Remark")

                                        });



            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "VisitType.xlsx");
                }
            }
        }

    }
}
