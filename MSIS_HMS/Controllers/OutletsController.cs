using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using NLog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using ClosedXML.Excel;
using System.IO;
using Microsoft.Reporting.NETCore;
using MSIS_HMS.Core.Entities.DTOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using System.Net.Http.Headers;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace MSIS_HMS.Controllers
{
    [Authorize]
    public class OutletsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IOutletRepository _outletRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IWarehouseService _warehouseService;
        private readonly IUserService _userService;
        private readonly IBranchService _branchService;
        private readonly ILogger<OutletsController> _logger;
        private readonly IItemService _itemService;
        private readonly Pagination _pagination;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnv;


        public OutletsController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IOutletRepository outletRepository, IWarehouseRepository warehouseRepository, IWarehouseService warehouseService, IUserService userService, IBranchService branchService, ILogger<OutletsController> logger, IOptions<Pagination> pagination, IItemService itemService, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _context = context;
            _outletRepository = outletRepository;
            _warehouseRepository = warehouseRepository;
            _warehouseService = warehouseService;
            _branchService = branchService;
            _userService = userService;
            _logger = logger;
            _pagination = pagination.Value;
            _itemService = itemService;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _hostingEnv = hostingEnvironment;
        }

        public void Initialize(Outlet outlet = null)
        {
            ViewData["Warehouse"] = new SelectList(_warehouseRepository.GetAll(_branchService.GetBranchIdByUser()), "Id", "Name", outlet?.Id);
            ViewData["Branches"] = _branchService.GetSelectListItems(outlet?.BranchId);
        }

        public IActionResult Index(int? page = 1, string OutletName = null, string OutletCode = null, int? WarehouseId = null)
        {
            var outlets = _outletRepository.GetAll(_branchService.GetBranchIdByUser(), OutletName, OutletCode, WarehouseId);
            var branches = _branchService.GetAll();
            //var warehouses = _warehouseService.GetAll();
            outlets.ForEach(x => x.Branch = branches.SingleOrDefault(b => b.Id == x.BranchId));
            //outlets.ForEach(x => x.Warehouse = warehouses.SingleOrDefault(b => b.Id == x.WarehouseId));
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            var warehouses = _warehouseRepository.GetAll(_branchService.GetBranchIdByUser());
            ViewData["Warehouse"] = new SelectList(warehouses, "Id", "Name");
            //ViewData["Warehouse"] = _warehouseService.GetSelectListItems(WarehouseId);
            return View(outlets.OrderByDescending(x => x.UpdatedAt).ToList().ToPagedList((int)page, pageSize));


        }

        public IActionResult Stock(int? page = 1, int? WarehouseId = null, int? OutletId = null, int? ItemId = null)
        {
            var warehouseItems = _outletRepository.GetOutetStocks(_branchService.GetBranchIdByUser(), WarehouseId, OutletId, ItemId);
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            ViewData["Warehouses"] = _warehouseService.GetSelectListItems(WarehouseId);
            ViewData["Items"] = _itemService.GetSelectListItems(ItemId);
            return View(warehouseItems.ToPagedList((int)page, pageSize));
        }

        public IActionResult OutletStockReport(int? page = 1, int? WarehouseId = null, int? OutletId = null, int? ItemId = null)
        {
            var warehouseItems = _outletRepository.GetOutetStocks(_branchService.GetBranchIdByUser(), WarehouseId, OutletId, ItemId);
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            ViewData["Warehouses"] = _warehouseService.GetSelectListItemsByBranch(_branchService.GetBranchIdByUser(), WarehouseId);
            ViewData["Items"] = _itemService.GetSelectListItems(ItemId);
            TempData["WarehouseId"] = WarehouseId;
            TempData["OutletId"] = OutletId;
            return View(warehouseItems.ToPagedList((int)page, pageSize));
        }

        public IActionResult Create()
        {
            Initialize();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Outlet outlet)
        {
            MappedDiagnosticsLogicalContext.Set("userId", _userManager.GetUserId(User));
            try
            {
                if (ModelState.IsValid)
                {
                    outlet = await _userManager.AddUserAndTimestamp(outlet, User, DbEnum.DbActionEnum.Create);
                    var _outlet = await _outletRepository.AddAsync(outlet);
                    if (_outlet != null)
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
            Initialize(outlet);
            return View();
        }
        public ActionResult Edit(int id)
        {
            Initialize();
            var locations = _outletRepository.Get(id);
            return View(locations);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Outlet outlet)
        {
            MappedDiagnosticsLogicalContext.Set("userId", _userManager.GetUserId(User));
            try
            {
                if (ModelState.IsValid)
                {
                    outlet = await _userManager.AddUserAndTimestamp(outlet, User, DbEnum.DbActionEnum.Update);
                    var _location = await _outletRepository.UpdateAsync(outlet);
                    TempData["notice"] = StatusEnum.NoticeStatus.Edit;
                    _logger.LogInformation(Infrastructure.Enums.EnumExtension.ToDescription(StatusEnum.NoticeStatus.Edit));
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.InnerException.Message);
            }
            Initialize();
            return View(outlet);
        }
        public async Task<ActionResult> Delete(int id)
        {
            MappedDiagnosticsLogicalContext.Set("userId", _userManager.GetUserId(User));
            try
            {
                var _outlet = await _outletRepository.DeleteAsync(id);
                TempData["notice"] = StatusEnum.NoticeStatus.Delete;
                _logger.LogInformation(Infrastructure.Enums.EnumExtension.ToDescription(StatusEnum.NoticeStatus.Delete));

            }
            catch (Exception e)
            {
                _logger.LogError(e.InnerException.Message);
            }

            return RedirectToAction(nameof(Index));
        }
        public IActionResult GetOutletItems(int outletId)
        {
            var outletItems = _outletRepository.GetItemsFromOutlet((int)_userService.Get(User).BranchId, outletId);
            return Ok(outletItems);
        }
        public IActionResult GetOutletItemsByUserOutlet()
        {
            var user = _userService.Get(User);
            if (user == null)
            {
                return Unauthorized();
            }
            if (user.OutletId != null)
            {
                var outletItems = _outletRepository.GetItemsFromOutlet((int)_branchService.GetBranchIdByUser(), (int)user.OutletId);
                return Ok(outletItems);
            }
            return BadRequest();
        }

        //addnewbyakh

        public IActionResult GetOutletOTItems(int outletId)
        {
            var outletOTItems =_outletRepository.GetOTItemsFromOutlet((int)_userService.Get(User).BranchId, outletId);
            return Ok(outletOTItems);
        }

        public IActionResult GetOutletOTItemsByUserOutlet()
        {
            var user = _userService.Get(User);
            if( user == null)
            {
                return Unauthorized();
            }
            if(user.OutletId != null)
            {
                var outletOTItems = _outletRepository.GetOTItemsFromOutlet((int)_branchService.GetBranchIdByUser(), (int)user.OutletId);
                return Ok(outletOTItems);
            }
            return BadRequest();
        }

        public IActionResult GeOutletAnaesthetists(int outletId)
        {
            var outletAnaesthetistItems = _outletRepository.GetAnaesthetistItemsFromOutlet((int)_userService.Get(User).BranchId, outletId);
            return Ok(outletAnaesthetistItems);
        }

        public IActionResult GetOutletanaesthetistItemsByUserOutlet()
        {
            var user = _userService.Get(User);
            if (user == null)
            {
                return Unauthorized();
            }
            if(user.OutletId != null)
            {
                var outletAnaesthetistItems = _outletRepository.GetAnaesthetistItemsFromOutlet((int)_branchService.GetBranchIdByUser(), (int)user.OutletId);
                return Ok(outletAnaesthetistItems);
            }

            return BadRequest();
        }


        //addnewbyakh


        public IActionResult GetAll(int? BranchId)
        {
            var outletItems = _outletRepository.GetAll(BranchId ?? -1).OrderBy(x => x.Name);
            return Ok(outletItems);
        }

        public IActionResult GetAllOutlets()
        {
            var outletItems = _outletRepository.GetAll(_userService.Get(User).BranchId).OrderBy(x => x.Name);
            return Ok(outletItems);
        }

        public IActionResult GetOutletByWarehouseId(int? warehouseId)
        {
            var outlets = _outletRepository.GetAll(_branchService.GetBranchIdByUser(), null, null, warehouseId ?? -1, null).OrderBy(x => x.Name);
            return Ok(outlets);
        }

        [HttpGet]
        public IActionResult ExcelExport(int? WarehouseId = null, int? OutletId = null, int? ItemId = null)
        {
            var outletItems = _outletRepository.GetOutetStocks(_branchService.GetBranchIdByUser(), WarehouseId, OutletId, ItemId);
            // var result = _reportService.WarehouseItemByExRemindDayexcelExport(warehouseItems);
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("OutletStock");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "Warhouse";
                worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 2).Value = "Outlet";
                worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 3).Value = "Item(Code)";
                worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 4).Value = "Qty";
                worksheet.Cell(currentRow, 4).Style.Font.Bold = true;


                foreach (var w in outletItems)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = w.WarehouseName;
                    worksheet.Cell(currentRow, 2).Value = w.OutletName;
                    worksheet.Cell(currentRow, 3).Value = w.ItemName + "(" + w.ItemCode + ")";
                    worksheet.Cell(currentRow, 4).Value = w.Qty;

                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Outlet.xlsx");
                }

            }

        }

        public IActionResult DownloadReport()
        {
            var page = TempData["Page"];
            int? warehouseId = null;
            int? outletId = null;
            //int? itemId = null;
            if (TempData["WarehouseId"] != null)
            {
                warehouseId = (int)TempData["WarehouseId"];
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
                var user = _userService.Get(User);
                List<OutletStockItemDTO> warehouseItems = new List<OutletStockItemDTO>();
                List<Branch> branches = new List<Branch>();
                var branch = _branchService.GetBranchById((int)user.BranchId);
                branches.Add(branch);
                // var user = _userService.Get(User);
                warehouseItems = _outletRepository.GetOutetStocks(_branchService.GetBranchIdByUser(), warehouseId, outletId, null);

                report.DataSources.Add(new ReportDataSource("Branch", branches));
                report.DataSources.Add(new ReportDataSource("dsOutletStock", warehouseItems));

                report.ReportPath = $"{_webHostEnvironment.WebRootPath}\\ReportFiles\\OutletStockReport.rdlc";
                var pdf = report.Render(renderFormat);
                TempData["Page"] = page;
                TempData["WarehouseId"] = warehouseId;
                TempData["OutletId"] = outletId;

                return File(pdf, mimetype, "OutletStockReport_" + DateTime.Now + "." + extension);
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
                    Outlet outlet = new Outlet();
                    outlet = await _userManager.AddUserAndTimestamp(outlet, User, DbEnum.DbActionEnum.Create);
                    dt.Columns.Add("BranchId", typeof(Int32));
                    dt.Columns.Add("IsDelete", typeof(bool));
                    dt.Columns.Add("CreatedAt", typeof(DateTime));
                    dt.Columns.Add("CreatedBy", typeof(string));
                    dt.Columns.Add("UpdatedAt", typeof(DateTime));
                    dt.Columns.Add("UpdatedBy", typeof(string));

                    foreach (var row in dt.AsEnumerable().ToList())
                    {
                        string name = row["WarehouseId"].ToString();
                        row["WarehouseId"] = _context.Warehouses.Where(x => x.Name == name).First().Id;
                        row["BranchId"] = outlet.BranchId;
                        row["IsDelete"] = outlet.IsDelete;
                        row["CreatedAt"] = outlet.CreatedAt;
                        row["CreatedBy"] = outlet.CreatedBy;
                        row["UpdatedAt"] = outlet.UpdatedAt;
                        row["UpdatedBy"] = outlet.UpdatedBy;
                        string a = "b";
                        a = "c";
                    }
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name.
                        sqlBulkCopy.DestinationTableName = "dbo.Outlet";

                        // Map the Excel columns with that of the database table, this is optional but good if you do
                        // 
                        sqlBulkCopy.ColumnMappings.Add("CreatedAt", "CreatedAt");
                        sqlBulkCopy.ColumnMappings.Add("CreatedBy", "CreatedBy");
                        sqlBulkCopy.ColumnMappings.Add("UpdatedAt", "UpdatedAt");
                        sqlBulkCopy.ColumnMappings.Add("UpdatedBy", "UpdatedBy");
                        sqlBulkCopy.ColumnMappings.Add("IsDelete", "IsDelete");
                        sqlBulkCopy.ColumnMappings.Add("BranchId", "BranchId");
                        sqlBulkCopy.ColumnMappings.Add("Name", "Name");
                        sqlBulkCopy.ColumnMappings.Add("Code", "Code");
                        sqlBulkCopy.ColumnMappings.Add("Location", "Location");
                        sqlBulkCopy.ColumnMappings.Add("WarehouseId", "WarehouseId");

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
            dt.Columns.AddRange(new DataColumn[4] { new DataColumn("Name"),
                                            new DataColumn("Code"),
                                            new DataColumn("Location"),
                                            new DataColumn("WarehouseId")

                                        });



            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Outlet.xlsx");
                }
            }
        }
    }
}

