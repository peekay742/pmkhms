using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using X.PagedList;
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
using MSIS_HMS.Core.Enums;

namespace MSIS_HMS.Controllers
{
    public class WardsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWardRepository _wardRepository;
        private readonly IFloorRepository _floorRepository;
        private readonly IOutletRepository _outletRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ApplicationDbContext _context;
        private readonly Pagination _pagination;
        private readonly IItemService _itemService;
        private readonly IUserService _userService;
        private readonly ILogger<WardsController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnv;
        public WardsController(UserManager<ApplicationUser> userManager, IWardRepository wardRepository, ApplicationDbContext context, IOptions<Pagination> pagination, IItemService itemService, IUserService userService, ILogger<WardsController> logger, IDepartmentRepository departmentRepository, IFloorRepository floorRepository, IConfiguration configuration, IHostingEnvironment hostingEnvironment, IOutletRepository outletRepository)
        {
            _userManager = userManager;
            _wardRepository = wardRepository;
            _context = context;
            _pagination = pagination.Value;
            _itemService = itemService;
            _userService = userService;
            _logger = logger;
            _departmentRepository = departmentRepository;
            _floorRepository = floorRepository;
            _configuration = configuration;
            _hostingEnv = hostingEnvironment;
            _outletRepository = outletRepository;
        }

        public void Initialize(Ward ward = null)
        {
            ViewData["Departments"] = _departmentRepository.GetAll(_userService.Get(User).BranchId).Where(x => x.Type == DepartmentTypeEnum.EnumDepartmentType.IPD).ToList().GetSelectListItems("Id", "Name", ward?.DepartmentId);
            //var departments = _context.Departments.Where(x => x.IsDelete == false).ToList();
            //ViewData["Departments"] = new SelectList(departments, "Id", "Name", ward?.DepartmentId);
            ViewData["Floors"] = _floorRepository.GetAll(_userService.Get(User).BranchId).GetSelectListItems("Id", "Name", ward?.FloorId);
            //var floors = _context.Floors.Where(x => x.IsDelete == false).ToList();
            //ViewData["Floors"] = new SelectList(floors, "Id", "Name", ward?.FloorId);
            ViewData["Outlets"] = _outletRepository.GetAll(_userService.Get(User).BranchId).GetSelectListItems("Id", "Name", ward?.OutletId);
            //var outlets = _context.Outlets.Where(x => x.IsDelete == false).ToList();
            //ViewData["Outlets"] = new SelectList(outlets, "Id", "Name", ward?.OutletId);
        }

        public IActionResult Index(string WardName = null, int? DepartmentId = null, int? FloorId = null, int? page = 1)
        {
            Initialize();
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;

            var ward = _wardRepository.GetAll(null, WardName, DepartmentId, FloorId).ToList();
            //var floors = _floorRepository.GetAll();
            //var departments = _departmentRepository.GetAll(_userService.Get(User).BranchId);
            return View(ward.OrderByDescending(x => x.UpdatedAt).ToList().ToPagedList((int)page, pageSize));
        }

        public IActionResult Create()
        {
            Initialize();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ward ward)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        ward = await _userManager.AddUserAndTimestamp(ward, User, DbEnum.DbActionEnum.Create);
                        var _ward = await _wardRepository.AddAsync(ward);
                        await transaction.CommitAsync();
                        if (ward != null)
                        {
                            TempData["notice"] = StatusEnum.NoticeStatus.Success;
                        }
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(e.InnerException.Message);
                }
            }
            return View(ward);
        }

        public IActionResult Edit(int id)
        {
            var ward = _wardRepository.Get(id);
            Initialize(ward);
            return View(ward);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Ward ward)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        ward = await _userManager.AddUserAndTimestamp(ward, User, DbEnum.DbActionEnum.Update);
                        var _ward = await _wardRepository.UpdateAsync(ward);
                        await transaction.CommitAsync();
                        TempData["notice"] = StatusEnum.NoticeStatus.Edit;
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(e.InnerException.Message);
                }
            }
            Initialize(ward);
            return View(ward);
        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var _ward = await _wardRepository.DeleteAsync(id);
                TempData["notice"] = StatusEnum.NoticeStatus.Delete;
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
                conString = _configuration.GetConnectionString("DefaultConnection");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    Ward ward = new Ward();
                    ward = await _userManager.AddUserAndTimestamp(ward, User, DbEnum.DbActionEnum.Create);
                    dt.Columns.Add("BranchId", typeof(Int32));
                    dt.Columns.Add("IsDelete", typeof(bool));
                    dt.Columns.Add("CreatedAt", typeof(DateTime));
                    dt.Columns.Add("CreatedBy", typeof(string));
                    dt.Columns.Add("UpdatedAt", typeof(DateTime));
                    dt.Columns.Add("UpdatedBy", typeof(string));
                    foreach (var row in dt.AsEnumerable().ToList())
                    {
                        string dname = row["Department"].ToString();
                        string fname = row["Floor"].ToString();
                        string oname = row["Outlet"].ToString();
                        row["Department"] = _context.Departments.Where(x => x.Name == dname).First().Id;
                        row["Floor"] = _context.Floors.Where(x => x.Name == fname).First().Id;
                        row["Outlet"] = _context.Outlets.Where(x => x.Name == oname).First().Id;
                        //row["BranchId"] = ward.BranchId;
                        row["IsDelete"] = ward.IsDelete;
                        row["CreatedAt"] = ward.CreatedAt;
                        row["CreatedBy"] = ward.CreatedBy;
                        row["UpdatedAt"] = ward.UpdatedAt;
                        row["UpdatedBy"] = ward.UpdatedBy;
                    }
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name.
                        sqlBulkCopy.DestinationTableName = "dbo.Ward";

                        // Map the Excel columns with that of the database table, this is optional but good if you do
                        // 
                        sqlBulkCopy.ColumnMappings.Add("CreatedAt", "CreatedAt");
                        sqlBulkCopy.ColumnMappings.Add("CreatedBy", "CreatedBy");
                        sqlBulkCopy.ColumnMappings.Add("UpdatedAt", "UpdatedAt");
                        sqlBulkCopy.ColumnMappings.Add("UpdatedBy", "UpdatedBy");
                        sqlBulkCopy.ColumnMappings.Add("IsDelete", "IsDelete");
                        //sqlBulkCopy.ColumnMappings.Add("BranchId", "BranchId");
                        sqlBulkCopy.ColumnMappings.Add("No", "No");
                        sqlBulkCopy.ColumnMappings.Add("Name", "Name");
                        sqlBulkCopy.ColumnMappings.Add("Department", "DepartmentId");
                        sqlBulkCopy.ColumnMappings.Add("Floor", "FloorId");
                        sqlBulkCopy.ColumnMappings.Add("Outlet", "OutletId");
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
                                            new DataColumn("Department"),
                                            new DataColumn("Floor"),
                                            new DataColumn("Outlet"),
                                            new DataColumn("No")

                                        });



            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Ward.xlsx");
                }
            }
        }

    }

}
