using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
using MSIS_HMS.Helpers;
using System.Collections.Generic;
using Microsoft.Reporting.NETCore;
using MSIS_HMS.Core.Entities.DTOs;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Headers;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using ClosedXML.Excel;
using MSIS_HMS.Core.Enums;

namespace MSIS_HMS.Controllers
{
    public class ItemsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IItemRepository _itemRepository;
        private readonly IItemTypeRepository _itemTypeRepository;
        private readonly IBranchService _branchService;
        private readonly IUserService _userService;
        private readonly IOutletRepository _outletRepository;
        private readonly Pagination _pagination;
        private readonly ILogger<ItemsController> _logger;
        private readonly IBatchRepository _batchRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnv;

        public ItemsController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IItemRepository itemRepository, IItemTypeRepository itemTypeRepository, IBranchService branchService, IUserService userService, IOptions<Pagination> pagination, ILogger<ItemsController> logger, IBatchRepository batchRepository, IOutletRepository outletRepository, IWebHostEnvironment webHostEnvironment, IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _context = context;
            _itemRepository = itemRepository;
            _itemTypeRepository = itemTypeRepository;
            _branchService = branchService;
            _userService = userService;
            _pagination = pagination.Value;
            _logger = logger;
            _batchRepository = batchRepository;
            _outletRepository = outletRepository;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _hostingEnv = hostingEnvironment;
        }

        public void Initialize(Item item = null)
        {
            int? itemTypeId = null, branchId = null;
            if (item != null)
            {
                itemTypeId = item.ItemTypeId;
                branchId = item.BranchId;
            }
            ViewData["ItemTypes"] = _itemTypeRepository.GetAll(_userService.Get(User).BranchId).GetSelectListItems("Id", "Name", itemTypeId);
            ViewData["Branches"] = _branchService.GetSelectListItems(branchId);
        }

        // GET
        public IActionResult Index(int? page = 1, string ItemName = null, string BarCode = null, string Code = null, int? ItemTypeId = null, int? Price = null,ItemCategoryEnum? Category= null)
        {

            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            var items = _itemRepository.GetAll(_userService.Get(User).BranchId, null, ItemTypeId, ItemName, Code, BarCode,Category);
            var branches = _branchService.GetAll();
            ViewData["ItemTypes"] = _itemTypeRepository.GetAll(_userService.Get(User).BranchId).GetSelectListItems("Id", "Name", ItemTypeId);
            return View(items.OrderByDescending(x => x.UpdatedAt).ToList().ToPagedList((int)page, pageSize));
        }
        public IActionResult DailySaleItemReport(int? page = 1, DateTime? FromDate = null, DateTime? ToDate = null, int? OutletId = null)
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
            var items = _itemRepository.GetSaleItem(_userService.Get(User).BranchId, FromDate, ToDate, OutletId);
            TempData["Page"] = page;
            TempData["FromDate"] = FromDate;
            TempData["ToDate"] = ToDate;
            TempData["OutletId"] = OutletId;
            return View(items.ToList().ToPagedList((int)page, pageSize));
        }
        public IActionResult ExpirationRemindDay(int? page = 1, string ItemName = null, string BarCode = null, string Code = null, int? ItemTypeId = null, int? Price = null)
        {
            Initialize();
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            var items = _itemRepository.GetExpirationRemindDay(_userService.Get(User).BranchId, null, ItemTypeId, ItemName, Code, BarCode);
            var branches = _branchService.GetAll();
            items.ForEach(x => x.Branch = branches.SingleOrDefault(b => b.Id == x.BranchId));
            ViewData["ItemTypes"] = _itemTypeRepository.GetAll(_userService.Get(User).BranchId).GetSelectListItems("Id", "Name", ItemTypeId);
            return View(items.ToPagedList((int)page, pageSize));

        }


        public IActionResult Create()
        {
            Initialize();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Item item)
        {

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                var filename = item.ImageFile != null ? FtpHelper.ftpItemImageFolderPath + item.ImageFile.GetUniqueName() : "";
                try
                {
                    if (ModelState.IsValid)
                    {
                        var didUploaded = true;
                        if (item.ImageFile != null)
                        {
                            didUploaded = false;
                            var uploadRes = FtpHelper.UploadFileToServer(item.ImageFile, filename);
                            if (uploadRes.IsSucceed())
                            {
                                didUploaded = true;
                                item.Image = uploadRes.ResponseUri.AbsolutePath;
                            }
                        }
                        if (didUploaded)
                        {
                            item = await _userManager.AddUserAndTimestamp(item, User, DbEnum.DbActionEnum.Create);
                            await _itemRepository.AddAsync(item);
                            await transaction.CommitAsync();
                            TempData["notice"] = StatusEnum.NoticeStatus.Success;
                            return RedirectToAction("Index");
                        }
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError("Not successful save Blog");
                    Console.WriteLine(e.Message);
                    await transaction.RollbackAsync();
                    if (FtpHelper.CheckIfFileExistsOnServer(filename))
                    {
                        var deleteRes = FtpHelper.DeleteFileOnServer(filename);
                        if (deleteRes.IsSucceed())
                        {
                            _logger.LogError("Not successful delete item thumbnail");
                            Console.WriteLine(e.Message);
                        }
                    }
                    Initialize(item);
                    return View(item);
                }
            }
            Initialize(item);
            return View(item);
        }

        public IActionResult Edit(int id)
        {
            var item = _itemRepository.GetWithPackingUnit(id);
            item.ImageContent = item.Image.GetBase64();
            Initialize(item);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Item item)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                var filename = item.ImageFile != null ? FtpHelper.ftpItemImageFolderPath + item.ImageFile.GetUniqueName() : "";
                try
                {
                    if (ModelState.IsValid)
                    {
                        var didUploaded = true;
                        if (item.ImageFile != null)
                        {
                            didUploaded = false;
                            var uploadRes = FtpHelper.UploadFileToServer(item.ImageFile, filename);
                            if (uploadRes.IsSucceed())
                            {
                                var didDeleted = true;
                                if (FtpHelper.CheckIfFileExistsOnServer(item.Image))
                                {
                                    didDeleted = false;
                                    var deleteRes = FtpHelper.DeleteFileOnServer(item.Image);
                                    if (deleteRes.IsSucceed())
                                    {
                                        didDeleted = true;
                                    }
                                }
                                if (didDeleted)
                                {
                                    didUploaded = true;
                                    item.Image = uploadRes.ResponseUri.AbsolutePath;
                                }
                            }
                        }
                        if (didUploaded)
                        {
                            item = await _userManager.AddUserAndTimestamp(item, User, DbEnum.DbActionEnum.Update);
                            await _itemRepository.UpdateAsync(item);
                            await transaction.CommitAsync();
                            _logger.LogInformation(Infrastructure.Enums.EnumExtension.ToDescription(StatusEnum.NoticeStatus.Edit));
                            return RedirectToAction("Index");
                        }
                        throw new Exception();
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError("Not successful save Blog");
                    Console.WriteLine(e.Message);
                    await transaction.RollbackAsync();
                    if (FtpHelper.CheckIfFileExistsOnServer(filename))
                    {
                        var deleteRes = FtpHelper.DeleteFileOnServer(filename);
                        if (deleteRes.IsSucceed())
                        {
                            _logger.LogError("Not successful delete item thumbnail");
                            Console.WriteLine(e.Message);
                        }
                    }
                    Initialize(item);
                    return View(item);
                }
            }
            Initialize(item);
            return View(item);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var _item = await _itemRepository.DeleteAsync(id);
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
            var units = _itemRepository.GetAllWithPackingUnits(_userService.Get(User).BranchId);
            return Ok(units.OrderBy(x => x.Name).ToList());
        }
        public IActionResult GetItemByOutlet()
        {
            var units = _itemRepository.GetAllByOutletId(_userService.Get(User).BranchId, _userService.Get(User).OutletId);
            return Ok(units.OrderBy(x => x.Name).ToList());
        }



        public ActionResult Image(string path)
        {
            return File(FtpHelper.DownloadFileFromServer(path), "image/png");
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
                List<SaleItemDTO> Orders = new List<SaleItemDTO>();
                List<Branch> branches = new List<Branch>();

                var user = _userService.Get(User);
                Orders = _itemRepository.GetSaleItem(_userService.Get(User).BranchId, fromDate, toDate, outletId);

                var branch = _branchService.GetBranchById((int)user.BranchId);
                branches.Add(branch);
                ReportParameter[] parameters = new ReportParameter[2];
                parameters[0] = new ReportParameter("FromDate", fromDate.ToString());
                parameters[1] = new ReportParameter("ToDate", toDate.ToString());

                report.DataSources.Add(new ReportDataSource("Branch", branches));
                report.DataSources.Add(new ReportDataSource("dsSaleItem", Orders));

                report.ReportPath = $"{_webHostEnvironment.WebRootPath}\\ReportFiles\\SaleItemReport.rdlc";
                report.SetParameters(parameters);
                var pdf = report.Render(renderFormat); TempData["Page"] = page;
                TempData["FromDate"] = fromDate;
                TempData["ToDate"] = toDate;
                TempData["OutletId"] = outletId;
                return File(pdf, mimetype, "DailySaleItemReport_" + DateTime.Now + "." + extension);
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
                    Item item = new Item();
                    item = await _userManager.AddUserAndTimestamp(item, User, DbEnum.DbActionEnum.Create);
                    dt.Columns.Add("BranchId", typeof(Int32));
                    dt.Columns.Add("IsDelete", typeof(bool));
                    dt.Columns.Add("CreatedAt", typeof(DateTime));
                    dt.Columns.Add("CreatedBy", typeof(string));
                    dt.Columns.Add("UpdatedAt", typeof(DateTime));
                    dt.Columns.Add("UpdatedBy", typeof(string));
                    try
                    {
                        foreach (var row in dt.AsEnumerable().ToList())
                        {
                          
                            string itname = row["ItemType"].ToString();
                            //string uname = row["Unit"].ToString();

                            row["ItemType"] = _context.ItemTypes.Where(x => x.Name == itname).First().Id;
                            //row["Unit"] = _context.Units.Where(x => x.Name == uname).First().Id;
                            row["BranchId"] = item.BranchId;
                            row["IsDelete"] = item.IsDelete;
                            row["CreatedAt"] = item.CreatedAt;
                            row["CreatedBy"] = item.CreatedBy;
                            row["UpdatedAt"] = item.UpdatedAt;
                            row["UpdatedBy"] = item.UpdatedBy;

                        }
                    }
                    catch(Exception ex)
                    {

                    }
                    
                    DataTable dtp = new DataTable();

                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name.
                        sqlBulkCopy.DestinationTableName = "dbo.Item";

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
                        sqlBulkCopy.ColumnMappings.Add("Barcode", "Barcode");
                        sqlBulkCopy.ColumnMappings.Add("Brand", "Brand");
                        sqlBulkCopy.ColumnMappings.Add("ChemicalName", "ChemicalName");
                        sqlBulkCopy.ColumnMappings.Add("Classification", "Classification");
                        sqlBulkCopy.ColumnMappings.Add("Country", "Country");
                        sqlBulkCopy.ColumnMappings.Add("Description", "Description");
                        sqlBulkCopy.ColumnMappings.Add("ItemType", "ItemTypeId");
                        sqlBulkCopy.ColumnMappings.Add("UnitPrice", "UnitPrice");
                        sqlBulkCopy.ColumnMappings.Add("PercentageForSale", "PercentageForSale");
                        sqlBulkCopy.ColumnMappings.Add("Remark", "Remark");
                        sqlBulkCopy.ColumnMappings.Add("Manufacturer", "Manufacturer");
                        sqlBulkCopy.ColumnMappings.Add("PercentageForDiscount", "PercentageForDiscount");
                        sqlBulkCopy.ColumnMappings.Add("ExpirationRemindDay", "ExpirationRemindDay");
                        sqlBulkCopy.ColumnMappings.Add("Image", "Image");

                        con.Open();
                        sqlBulkCopy.WriteToServer(dt);
                        con.Close();
                    }

                    //foreach (var row in dt.AsEnumerable().ToList())
                    //{

                    //    string uname = row["Unit"].ToString();
                    //    string iname = row["Name"].ToString();
                    //    row["Name"] = _context.Items.Where(x => x.Name == iname).First().Id;
                    //    //row["ItemType"] = _context.ItemTypes.Where(x => x.Name == itname).First().Id;
                    //    row["Unit"] = _context.Units.Where(x => x.Name == uname).First().Id;


                    //}
                    //using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    //{
                    //    //Set the database table name.
                    //    sqlBulkCopy.DestinationTableName = "dbo.PackingUnit";
                    //    sqlBulkCopy.ColumnMappings.Add("Name", "ItemId");
                    //    sqlBulkCopy.ColumnMappings.Add("Unit", "UnitId");
                    //    sqlBulkCopy.ColumnMappings.Add("QtyInParent", "QtyInParent");
                    //    sqlBulkCopy.ColumnMappings.Add("PurchaseAmount", "PurchaseAmount");
                    //    sqlBulkCopy.ColumnMappings.Add("SaleAmount", "SaleAmount");
                    //    sqlBulkCopy.ColumnMappings.Add("IsDefault", "IsDefault");
                    //    sqlBulkCopy.ColumnMappings.Add("UnitLevel", "UnitLevel");
                    //    con.Open();
                    //    sqlBulkCopy.WriteToServer(dt);
                    //    con.Close();
                    //}
                    //if the code reach here means everthing goes fine and excel data is imported into database
                }
            }

            return RedirectToAction("Index");

        }

        public IActionResult Export()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[17] { new DataColumn("Name"),
                                            new DataColumn("Code"),
                                            new DataColumn("Barcode"),
                                            new DataColumn("Brand"),
                                            new DataColumn("ChemicalName"),
                                            new DataColumn("Composition"),
                                            new DataColumn("Classification"),
                                            new DataColumn("Country"),
                                            new DataColumn("Description"),
                                            new DataColumn("ItemType"),
                                            new DataColumn("UnitPrice"),
                                            new DataColumn("PercentageForSale"),
                                            new DataColumn("Remark"),
                                            new DataColumn("Manufacturer"),
                                            new DataColumn("PercentageForDiscount"),
                                            new DataColumn("ExpirationRemindDay"),
                                            new DataColumn("Image"),
                                            //new DataColumn("QtyInParent"),
                                            //new DataColumn("PurchaseAmount"),
                                            //new DataColumn("SaleAmount"),
                                            //new DataColumn("IsDefault"),
                                            //new DataColumn("UnitLevel"),
                                            //new DataColumn("Unit")
                                        });



            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Item.xlsx");
                }
            }
        }
        public IActionResult PackingUnitImport()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PackingUnitImportExcelFile(IFormFile FormFile)
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
                    PackingUnit packingUnit = new PackingUnit();
                   

                    foreach (var row in dt.AsEnumerable().ToList())
                    {
                        try
                        {
                            string uname = row["Unit"].ToString();
                            string iname = row["Item"].ToString();
                            if (uname == "" || uname == null)
                            {

                            }
                            row["Item"] = _context.Items.Where(x => x.Name == iname).First().Id;
                            //row["ItemType"] = _context.ItemTypes.Where(x => x.Name == itname).First().Id;
                            row["Unit"] = _context.Units.Where(x => x.Name == uname).First().Id;
                        }
                        catch(Exception ex)
                        {
                            
                        }
                        


                    }
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name.
                        sqlBulkCopy.DestinationTableName = "dbo.PackingUnit";
                        sqlBulkCopy.ColumnMappings.Add("Item", "ItemId");
                        sqlBulkCopy.ColumnMappings.Add("Unit", "UnitId");
                        sqlBulkCopy.ColumnMappings.Add("QtyInParent", "QtyInParent");
                        sqlBulkCopy.ColumnMappings.Add("PurchasePrice", "PurchaseAmount");
                        sqlBulkCopy.ColumnMappings.Add("SalePrice", "SaleAmount");
                        sqlBulkCopy.ColumnMappings.Add("IsDefault", "IsDefault");
                        sqlBulkCopy.ColumnMappings.Add("UnitLevel", "UnitLevel");
                        con.Open();
                        sqlBulkCopy.WriteToServer(dt);
                        con.Close();
                    }
                    //if the code reach here means everthing goes fine and excel data is imported into database
                }
            }

            return RedirectToAction("Index");

        }

        public IActionResult PackingUnitExport()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[7] {
                                            new DataColumn("Item"),
                                            new DataColumn("Unit"),
                                            new DataColumn("QtyInParent"),
                                            new DataColumn("PurchasePrice"),
                                            new DataColumn("SalePrice"),
                                            new DataColumn("IsDefault"),
                                            new DataColumn("UnitLevel")
                                        });



            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PackingUnit.xlsx");
                }
            }
        }

    }
}
