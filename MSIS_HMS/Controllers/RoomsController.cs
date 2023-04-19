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
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using MSIS_HMS.Core.Enums;
using X.PagedList;
using EnumExtension = MSIS_HMS.Infrastructure.Enums.EnumExtension;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
    public class RoomsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRoomRepository _roomRepository;

        private readonly ApplicationDbContext _context;
        private readonly Pagination _pagination;
        private readonly IItemService _itemService;
        private readonly IUserService _userService;
        private readonly IWardRepository _wardRepository;
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly ILogger<RoomsController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnv;
        public RoomsController(UserManager<ApplicationUser> userManager, IRoomRepository roomRepository, ApplicationDbContext context, IOptions<Pagination> pagination, IItemService itemService, IUserService userService, ILogger<RoomsController> logger, IWardRepository wardRepository, IRoomTypeRepository roomTypeRepository, IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _roomRepository = roomRepository;
            _context = context;
            _pagination = pagination.Value;
            _itemService = itemService;
            _userService = userService;
            _logger = logger;
            _wardRepository = wardRepository;
            _roomTypeRepository = roomTypeRepository;
            _configuration = configuration;
            _hostingEnv = hostingEnvironment;
        }

        public void Initialize()
        {
            var roomtypes = _context.RoomTypes.Where(x => x.IsDelete == false).ToList();
            ViewData["RoomTypes"] = new SelectList(roomtypes, "Id", "Name");
            var wards = _context.Wards.Where(x => x.IsDelete == false).ToList();
            ViewData["Wards"] = new SelectList(wards, "Id", "Name");
            var enumData = from RoomStatusEnum e in Enum.GetValues(typeof(RoomStatusEnum))
                           select new
                           {
                               ID = (int)e,
                               Name = EnumExtension.ToDescription(e),
                           };
            ViewData["RoomStatus"] = new SelectList(enumData, "Name", "Name");
        }

        public IActionResult Index(int? RoomTypeId = null, string Status = null, decimal? Price = null, int? WardId = null, int? page = 1, string RoomStatus = null, string RoomNo = null)

        {
            Initialize();
            var pageSize = _pagination.PageSize;
            ViewData["Page"] = page;
            ViewData["PageSize"] = pageSize;
            var room = _roomRepository.GetAll(null, RoomTypeId, RoomStatus, Price, WardId, RoomNo).ToList();
            //var wards = _wardRepository.GetAll();
            //var roomtypes = _roomTypeRepository.GetAll();
            //room.ForEach(x => x.Ward = wards.SingleOrDefault(b => b.Id == x.WardId));
            //room.ForEach(x=>x.RoomType=roomtypes.SingleOrDefault(b=>b.Id==x.RoomTypeId));
            return View(room.OrderByDescending(x => x.UpdatedAt).ToList().ToPagedList((int)page, pageSize));
        }
        public IActionResult Create()
        {
            Initialize();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Room room)
        {

            if (ModelState.IsValid)
            {
                room = await _userManager.AddUserAndTimestamp(room, User, DbEnum.DbActionEnum.Create);
                var _room = await _roomRepository.AddAsync(room);
                if (room != null)
                {
                    TempData["notice"] = StatusEnum.NoticeStatus.Success;

                }
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        public IActionResult Edit(int id)
        {
            Initialize();
            var room = _roomRepository.Get(id);
            return View(room);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Room room)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    room = await _userManager.AddUserAndTimestamp(room, User, DbEnum.DbActionEnum.Update);
                    var _room = await _roomRepository.UpdateAsync(room);
                    TempData["notice"] = StatusEnum.NoticeStatus.Edit;
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.InnerException.Message);
            }

            return View(room);
        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var _room = await _roomRepository.DeleteAsync(id);
                TempData["notice"] = StatusEnum.NoticeStatus.Delete;
                _logger.LogInformation(Infrastructure.Enums.EnumExtension.ToDescription(StatusEnum.NoticeStatus.Delete));
            }
            catch (Exception e)
            {
                _logger.LogError(e.InnerException.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        //public ActionResult Upload()
        //{
        //    IFormFile file = Request.Form.Files[0];
        //    string folderName = "UploadExcel";
        //    string webRootPath = _hostingEnv.WebRootPath;
        //    string newPath = Path.Combine(webRootPath, folderName);
        //    StringBuilder sb = new StringBuilder();
        //    if (!Directory.Exists(newPath))
        //    {
        //        Directory.CreateDirectory(newPath);
        //    }
        //    if (file.Length > 0)
        //    {
        //        string sFileExtension = Path.GetExtension(file.FileName).ToLower();
        //        ISheet sheet;
        //        string fullPath = Path.Combine(newPath, file.FileName);
        //        using (var stream = new FileStream(fullPath, FileMode.Create))
        //        {
        //            file.CopyTo(stream);
        //            stream.Position = 0;
        //            if (sFileExtension == ".xls")
        //            {
        //                HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats  
        //                sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
        //            }
        //            else
        //            {
        //                XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
        //                sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
        //            }
        //            IRow headerRow = sheet.GetRow(0); //Get Header Row
        //            int cellCount = headerRow.LastCellNum;
        //            sb.Append("<table class='table table-bordered'><tr>");
        //            for (int j = 0; j < cellCount; j++)
        //            {
        //                NPOI.SS.UserModel.ICell cell = headerRow.GetCell(j);
        //                if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
        //                sb.Append("<th>" + cell.ToString() + "</th>");
        //            }
        //            sb.Append("</tr>");
        //            sb.AppendLine("<tr>");
        //            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
        //            {
        //                IRow row = sheet.GetRow(i);
        //                if (row == null) continue;
        //                if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
        //                for (int j = row.FirstCellNum; j < cellCount; j++)
        //                {
        //                    if (row.GetCell(j) != null)
        //                        sb.Append("<td>" + row.GetCell(j).ToString() + "</td>");
        //                }
        //                sb.AppendLine("</tr>");
        //            }
        //            sb.Append("</table>");
        //        }
        //    }
        //    return this.Content(sb.ToString());
        //}
        //public IActionResult Import()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> ImportExcelFile(IFormFile FormFile)
        //{
        //    if (FormFile == null)
        //    {
        //        return RedirectToAction("Import");
        //    }
        //    else
        //    {


        //        //get file name
        //        var filename = ContentDispositionHeaderValue.Parse(FormFile.ContentDisposition).FileName.Trim('"');
        //        //get path
        //        var MainPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads");

        //        //create directory "Uploads" if it doesn't exists
        //        if (!Directory.Exists(MainPath))
        //        {
        //            Directory.CreateDirectory(MainPath);
        //        }

        //        //get file path 
        //        var filePath = Path.Combine(MainPath, FormFile.FileName);
        //        using (System.IO.Stream stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            await FormFile.CopyToAsync(stream);
        //        }

        //        //get extension
        //        string extension = Path.GetExtension(filename);


        //        string conString = string.Empty;

        //        switch (extension)
        //        {
        //            case ".xls": //Excel 97-03.
        //                conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
        //                break;
        //            case ".xlsx": //Excel 07 and above.
        //                conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES'";
        //                break;
        //        }

        //        DataTable dt = new DataTable();
        //        conString = string.Format(conString, filePath);

        //        using (OleDbConnection connExcel = new OleDbConnection(conString))
        //        {
        //            using (OleDbCommand cmdExcel = new OleDbCommand())
        //            {
        //                using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
        //                {
        //                    cmdExcel.Connection = connExcel;

        //                    //Get the name of First Sheet.
        //                    connExcel.Open();
        //                    DataTable dtExcelSchema;
        //                    dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        //                    string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
        //                    connExcel.Close();

        //                    //Read Data from First Sheet.
        //                    connExcel.Open();
        //                    cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
        //                    odaExcel.SelectCommand = cmdExcel;
        //                    odaExcel.Fill(dt);
        //                    connExcel.Close();
        //                }
        //            }
        //        }
        //        //your database connection string
        //        conString = _configuration.GetConnectionString("DefaultConnection");
        //        using (SqlConnection con = new SqlConnection(conString))
        //        {
        //            Room room = new Room();
        //            room = await _userManager.AddUserAndTimestamp(room, User, DbEnum.DbActionEnum.Create);
        //            dt.Columns.Add("BranchId", typeof(Int32));
        //            dt.Columns.Add("IsDelete", typeof(bool));
        //            dt.Columns.Add("CreatedAt", typeof(DateTime));
        //            dt.Columns.Add("CreatedBy", typeof(string));
        //            dt.Columns.Add("UpdatedAt", typeof(DateTime));
        //            dt.Columns.Add("UpdatedBy", typeof(string));
        //            foreach (var row in dt.AsEnumerable().ToList())
        //            {
        //                string name = row["WardId"].ToString();
        //                string rtname = row["RoomTypeId"].ToString();
        //                row["RoomTypeId"] = _context.RoomTypes.Where(x => x.Name == name).First().Id;
        //                row["WardId"] = _context.Wards.Where(x => x.Name == name).First().Id;
        //                row["BranchId"] = room.BranchId;
        //                row["IsDelete"] = room.IsDelete;
        //                row["CreatedAt"] = room.CreatedAt;
        //                row["CreatedBy"] = room.CreatedBy;
        //                row["UpdatedAt"] = room.UpdatedAt;
        //                row["UpdatedBy"] = room.UpdatedBy;
        //            }
        //            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
        //            {
        //                //Set the database table name.
        //                sqlBulkCopy.DestinationTableName = "dbo.Room";

        //                // Map the Excel columns with that of the database table, this is optional but good if you do
        //                // 
        //                sqlBulkCopy.ColumnMappings.Add("CreatedAt", "CreatedAt");
        //                sqlBulkCopy.ColumnMappings.Add("CreatedBy", "CreatedBy");
        //                sqlBulkCopy.ColumnMappings.Add("UpdatedAt", "UpdatedAt");
        //                sqlBulkCopy.ColumnMappings.Add("UpdatedBy", "UpdatedBy");
        //                sqlBulkCopy.ColumnMappings.Add("IsDelete", "IsDelete");
        //                sqlBulkCopy.ColumnMappings.Add("BranchId", "BranchId");
        //                sqlBulkCopy.ColumnMappings.Add("RoomNo", "RoomNo");
        //                sqlBulkCopy.ColumnMappings.Add("RoomTypeId", "RoomTypeId");
        //                sqlBulkCopy.ColumnMappings.Add("Status", "Status");
        //                sqlBulkCopy.ColumnMappings.Add("Price", "Price");
        //                sqlBulkCopy.ColumnMappings.Add("WardId", "WardId");
        //                con.Open();
        //                sqlBulkCopy.WriteToServer(dt);
        //                con.Close();
        //            }
        //        }
        //        //if the code reach here means everthing goes fine and excel data is imported into database
        //    }


        //    return RedirectToAction("Index");

        //}

        //public IActionResult Export()
        //{
        //    DataTable dt = new DataTable("Grid");
        //    dt.Columns.AddRange(new DataColumn[5] { new DataColumn("RoomNo"),
        //                                    new DataColumn("RoomTypeId"),
        //                                    new DataColumn("Status"),
        //                                    new DataColumn("Price"),
        //                                    new DataColumn("WardId")

        //                                });



        //    using (XLWorkbook wb = new XLWorkbook())
        //    {
        //        wb.Worksheets.Add(dt);
        //        using (MemoryStream stream = new MemoryStream())
        //        {
        //            wb.SaveAs(stream);
        //            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Room.xlsx");
        //        }
        //    }
        //}


    }

}
