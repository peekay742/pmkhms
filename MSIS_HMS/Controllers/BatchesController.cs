using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MSIS_HMS.Core.Entities;
using MSIS_HMS.Core.Repositories;
using MSIS_HMS.Enums;
using MSIS_HMS.Infrastructure.Data;
using MSIS_HMS.Infrastructure.Enums;
using MSIS_HMS.Infrastructure.Helpers;
using MSIS_HMS.Interfaces;
using MSIS_HMS.Models;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;


namespace MSIS_HMS.Controllers

{

    [Authorize]

    public class BatchesController : Controller

    {

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ApplicationDbContext _context;

        private readonly IItemService _itemService;

        private readonly IBatchRepository _batchRepository;

        private readonly IBranchService _branchService;

        private readonly ILogger<BatchesController> _logger;

        private readonly Pagination _pagination;

        private readonly IConfiguration _configuration;

        private readonly IHostingEnvironment _hostingEnv;


        public BatchesController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IItemService itemService, IBatchRepository batchRepository, IBranchService branchService, IOptions<Pagination> pagination, ILogger<BatchesController> logger, IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {

            _userManager = userManager;

            _context = context;

            _itemService = itemService;

            _batchRepository = batchRepository;

            _branchService = branchService;

            _pagination = pagination.Value;

            _logger = logger;

            _configuration = configuration;

            _hostingEnv = hostingEnvironment;

        }


        public void Initialize(Batch batch = null)

        {

            ViewData["Items"] = _itemService.GetSelectListItems(batch?.ItemId);

        }


        // GET

        public IActionResult Index(int? page = 1, int? ItemId = null, string BatchName = null, string BatchCode = null, string BatchNumber = null, DateTime? StartExpiryDate = null, DateTime? EndExpiryDate = null)

        {
            if (_branchService.GetBranchIdByUser() == null)
            {
                return View("~/Views/Account/Login.cshtml");
            }
            var batches = _batchRepository.GetAll(_branchService.GetBranchIdByUser(), ItemId, null, BatchName, BatchCode, BatchNumber, null, StartExpiryDate, EndExpiryDate);

            //var items = _itemService.GetAll();

            var pageSize = _pagination.PageSize;

            ViewData["Page"] = page;

            ViewData["PageSize"] = pageSize;

            ViewData["Items"] = _itemService.GetSelectListItems(ItemId);

            return View(batches.OrderByDescending(x => x.UpdatedAt).ToList().ToPagedList((int)page, pageSize));

        }


        public IActionResult Create()
        {

            Initialize();

            return View();

        }


        [HttpPost]

        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Batch batch)
        {

            try

            {

                ModelState.Remove("BranchId");

                if (ModelState.IsValid)

                {

                    batch = await _userManager.AddUserAndTimestamp(batch, User, DbEnum.DbActionEnum.Create);

                    var _batch = await _batchRepository.AddAsync(batch);

                    if (_batch != null)

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

            Initialize(batch);

            return View(batch);

        }


        public IActionResult Edit(int id)
        {

            var batch = _batchRepository.Get(id);

            Initialize(batch);

            return View(batch);

        }


        [HttpPost]

        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(Batch batch)
        {

            try

            {

                if (ModelState.IsValid)

                {

                    batch = await _userManager.AddUserAndTimestamp(batch, User, DbEnum.DbActionEnum.Update);

                    var _batch = await _batchRepository.UpdateAsync(batch);

                    TempData["notice"] = StatusEnum.NoticeStatus.Edit;

                    _logger.LogInformation(Infrastructure.Enums.EnumExtension.ToDescription(StatusEnum.NoticeStatus.Edit));

                    return RedirectToAction("Index");

                }

            }

            catch (Exception e)

            {

                _logger.LogError(e.InnerException.Message);

            }

            Initialize(batch);

            return View(batch);

        }


        public async Task<IActionResult> Delete(int id)
        {

            try

            {

                var _batch = await _batchRepository.DeleteAsync(id);

                TempData["notice"] = StatusEnum.NoticeStatus.Delete;

                _logger.LogInformation(Infrastructure.Enums.EnumExtension.ToDescription(StatusEnum.NoticeStatus.Delete));


            }

            catch (Exception e)

            {

                _logger.LogError(e.InnerException.Message);

            }

            return RedirectToAction(nameof(Index));

        }


        public IActionResult GetAll(int? ItemId)

        {

            var batches = _batchRepository.GetAll(_branchService.GetBranchIdByUser(), ItemId);

            return Ok(batches.OrderByDescending(x => x.UpdatedAt).ToList());

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

                    Batch batch = new Batch();

                    batch = await _userManager.AddUserAndTimestamp(batch, User, DbEnum.DbActionEnum.Create);

                    dt.Columns.Add("BranchId", typeof(Int32));

                    dt.Columns.Add("IsDelete", typeof(bool));

                    dt.Columns.Add("CreatedAt", typeof(DateTime));

                    dt.Columns.Add("CreatedBy", typeof(string));

                    dt.Columns.Add("UpdatedAt", typeof(DateTime));

                    dt.Columns.Add("UpdatedBy", typeof(string));

                    foreach (var row in dt.AsEnumerable().ToList())

                    {

                        string name = row["ItemId"].ToString();

                        row["ItemId"] = _context.Items.Where(x => x.Name == name).First().Id;

                        row["BranchId"] = batch.BranchId;

                        row["IsDelete"] = batch.IsDelete;

                        row["CreatedAt"] = batch.CreatedAt;

                        row["CreatedBy"] = batch.CreatedBy;

                        row["UpdatedAt"] = batch.UpdatedAt;

                        row["UpdatedBy"] = batch.UpdatedBy;

                    }

                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))

                    {

                        //Set the database table name.

                        sqlBulkCopy.DestinationTableName = "dbo.Batch";


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

                        sqlBulkCopy.ColumnMappings.Add("BatchNumber", "BatchNumber");

                        sqlBulkCopy.ColumnMappings.Add("ItemId", "ItemId");

                        sqlBulkCopy.ColumnMappings.Add("ExpiryDate", "ExpiryDate");

                        sqlBulkCopy.ColumnMappings.Add("Description", "Description");

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

            dt.Columns.AddRange(new DataColumn[6] { new DataColumn("Name"),

                                            new DataColumn("Code"),

                                            new DataColumn("BatchNumber"),

                                            new DataColumn("ItemId"),

                                            new DataColumn("ExpiryDate"),

                                            new DataColumn("Description")

                                        });




            using (XLWorkbook wb = new XLWorkbook())

            {

                wb.Worksheets.Add(dt);

                using (MemoryStream stream = new MemoryStream())

                {

                    wb.SaveAs(stream);

                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Batch.xlsx");

                }

            }

        }


    }

}
