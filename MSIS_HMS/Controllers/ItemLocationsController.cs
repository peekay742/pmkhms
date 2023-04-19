using System;
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
using MSIS_HMS.Core.Entities;
using MSIS_HMS.Core.Repositories;
using MSIS_HMS.Enums;
using MSIS_HMS.Infrastructure.Data;
using MSIS_HMS.Infrastructure.Enums;
using MSIS_HMS.Infrastructure.Helpers;
using MSIS_HMS.Infrastructure.Repositories;
using MSIS_HMS.Interfaces;
using MSIS_HMS.Models;
using NLog;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using X.PagedList;

namespace MSIS_HMS.Controllers
{
    public class ItemLocationsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly Pagination _pagination;
        private readonly IItemRepository _itemRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IItemLocationRepository _itemLocationRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly ILogger<ItemLocationsController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnv;
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IWarehouseService _warehouseService;
        private readonly IBranchService _branchService;



        public ItemLocationsController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IOptions<Pagination> pagination, IItemRepository itemRepository,
                                      ILocationRepository locationRepository, IItemLocationRepository itemLocationRepository, IBranchRepository branchRepository, ILogger<ItemLocationsController> logger, IConfiguration configuration, IHostingEnvironment hostingEnvironment, IWarehouseRepository warehouseRepository, IWarehouseService warehouseService, IBranchService branchService)
        {
            _userManager = userManager;
            _context = context;
            _pagination = pagination.Value;
            _itemRepository = itemRepository;
            _locationRepository = locationRepository;
            _itemLocationRepository = itemLocationRepository;
            _branchRepository = branchRepository;
            _logger = logger;
            _configuration = configuration;
            _hostingEnv = hostingEnvironment;
            _warehouseRepository = warehouseRepository;
            _warehouseService = warehouseService;
            _branchService = branchService;
        }

        public void Initialize(ItemLocation itemLocation = null)
        {
            var items = _itemRepository.GetAll();
            var locations = _locationRepository.GetAll();
            var batches = _context.Batches.Where(x => x.IsDelete == false).ToList();
            //var warehouses = _context.Warehouses.Where(x => x.IsDelete == false).ToList();
            ViewData["items"] = new SelectList(items, "Id", "Name", itemLocation?.ItemId);
            ViewData["locations"] = new SelectList(locations, "Id", "Name", itemLocation?.LocationId);
            ViewData["batches"] = new SelectList(batches, "Id", "Name", itemLocation?.BatchId);
            //ViewData["warehouses"] = new SelectList(warehouses, "Id", "Name", itemLocation?.WarehouseId);
            ViewData["Warehouse"] = new SelectList(_warehouseRepository.GetAll(_branchService.GetBranchIdByUser()), "Id", "Name", itemLocation?.Id);
        }

        public IActionResult Index(int? page = 1, int? ItemId = null, int? LocationId = null, int? BatchId = null, int? WarehouseId = null)
        {
            Initialize();
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;

            var itemLocations = _itemLocationRepository.GetAll(null, ItemId, LocationId, null, BatchId, WarehouseId);
            var branches = _branchRepository.GetAll();
            //var locations = _locationRepository.GetAll();
            //var items = _itemRepository.GetAll();

            itemLocations.ForEach(x =>
            {
                x.Branch = branches.SingleOrDefault(b => b.Id == x.BranchId);
                //x.Item = items.SingleOrDefault(i => i.Id == x.ItemId);
                //x.Location = locations.SingleOrDefault(l => l.Id == x.LocationId);
            });

            return View(itemLocations.OrderByDescending(x => x.UpdatedAt).ToList().ToPagedList((int)page, pageSize));
        }

        public IActionResult Create()
        {
            Initialize();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemLocation itemLocation)
        {
            MappedDiagnosticsLogicalContext.Set("userId", _userManager.GetUserId(User));//insert userId into Logs Table
            try
            {
                ModelState.Remove("BranchId");
                if (ModelState.IsValid)
                {
                    itemLocation = await _userManager.AddUserAndTimestamp(itemLocation, User, DbEnum.DbActionEnum.Create);
                    var _itemLocation = await _itemLocationRepository.AddAsync(itemLocation);
                    if (_itemLocation != null)
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
            Initialize();
            return View();
        }


        public IActionResult Edit(int id)
        {
            var itemLocation = _itemLocationRepository.Get(id);
            Initialize(itemLocation);
            return View(itemLocation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ItemLocation itemLocation)
        {
            MappedDiagnosticsLogicalContext.Set("userId", _userManager.GetUserId(User));//insert userId into Logs Table
            try
            {
                if (ModelState.IsValid)
                {
                    itemLocation = await _userManager.AddUserAndTimestamp(itemLocation, User, DbEnum.DbActionEnum.Update);
                    await _itemLocationRepository.UpdateAsync(itemLocation);
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
            return View(itemLocation);
        }
        public async Task<ActionResult> Delete(int id)
        {
            MappedDiagnosticsLogicalContext.Set("userId", _userManager.GetUserId(User));//insert userId into Logs Table
            try
            {
                await _itemLocationRepository.DeleteAsync(id);
                TempData["notice"] = StatusEnum.NoticeStatus.Delete;
                _logger.LogInformation(Infrastructure.Enums.EnumExtension.ToDescription(StatusEnum.NoticeStatus.Delete));

            }
            catch (Exception e)
            {
                _logger.LogError(e.InnerException.Message);
            }
            return RedirectToAction(nameof(Index));

        }

        public IActionResult GetBatch(int ItemId)
        {
            var batches = _context.Batches.Where(x => x.IsDelete == false && x.ItemId == ItemId).ToList();
            return Ok(batches);
        }
        public IActionResult GetLocation(int WarehouseId)
        {
            var locations = _context.Locations.Where(x => x.IsDelete == false && x.WarehouseId == WarehouseId).ToList();
            return Ok(locations);
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
                    ItemLocation itemlocation = new ItemLocation();
                    itemlocation = await _userManager.AddUserAndTimestamp(itemlocation, User, DbEnum.DbActionEnum.Create);
                    dt.Columns.Add("BranchId", typeof(Int32));
                    dt.Columns.Add("IsDelete", typeof(bool));
                    dt.Columns.Add("CreatedAt", typeof(DateTime));
                    dt.Columns.Add("CreatedBy", typeof(string));
                    dt.Columns.Add("UpdatedAt", typeof(DateTime));
                    dt.Columns.Add("UpdatedBy", typeof(string));
                    foreach (var row in dt.AsEnumerable().ToList())
                    {
                        string iname = row["Item"].ToString();
                        string bname = row["Batch"].ToString();
                        string wname = row["Warehouse"].ToString();
                        string lname = row["Location"].ToString();
                        row["Item"] = _context.Items.Where(x => x.Name == iname).First().Id;
                        row["Batch"] = _context.Batches.Where(x => x.Name == bname).First().Id;
                        row["Location"] = _context.Locations.Where(x => x.Name == lname).First().Id;
                        row["Warehouse"] = _context.Warehouses.Where(x => x.Name == wname).First().Id;
                        row["BranchId"] = itemlocation.BranchId;
                        row["IsDelete"] = itemlocation.IsDelete;
                        row["CreatedAt"] = itemlocation.CreatedAt;
                        row["CreatedBy"] = itemlocation.CreatedBy;
                        row["UpdatedAt"] = itemlocation.UpdatedAt;
                        row["UpdatedBy"] = itemlocation.UpdatedBy;
                    }
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name.
                        sqlBulkCopy.DestinationTableName = "dbo.ItemLocation";

                        // Map the Excel columns with that of the database table, this is optional but good if you do
                        // 
                        sqlBulkCopy.ColumnMappings.Add("CreatedAt", "CreatedAt");
                        sqlBulkCopy.ColumnMappings.Add("CreatedBy", "CreatedBy");
                        sqlBulkCopy.ColumnMappings.Add("UpdatedAt", "UpdatedAt");
                        sqlBulkCopy.ColumnMappings.Add("UpdatedBy", "UpdatedBy");
                        sqlBulkCopy.ColumnMappings.Add("IsDelete", "IsDelete");
                        sqlBulkCopy.ColumnMappings.Add("BranchId", "BranchId");
                        sqlBulkCopy.ColumnMappings.Add("Item", "ItemId");
                        sqlBulkCopy.ColumnMappings.Add("Location", "LocationId");
                        sqlBulkCopy.ColumnMappings.Add("Remark", "Remark");
                        sqlBulkCopy.ColumnMappings.Add("Batch", "BatchId");
                        sqlBulkCopy.ColumnMappings.Add("Warehouse", "WarehouseId");
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
            dt.Columns.AddRange(new DataColumn[5] { new DataColumn("Item"),
                                            new DataColumn("Location"),
                                            new DataColumn("Remark"),
                                            new DataColumn("Batch"),
                                            new DataColumn("Warehouse")

                                        });



            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ItemLocation.xlsx");
                }
            }
        }

    }
}
