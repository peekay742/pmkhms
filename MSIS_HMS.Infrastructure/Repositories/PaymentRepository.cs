using Microsoft.EntityFrameworkCore;
using MSIS_HMS.Core.Entities;
using MSIS_HMS.Core.Entities.DTOs;
using MSIS_HMS.Core.Enums;
using MSIS_HMS.Core.Repositories;
using MSIS_HMS.Infrastructure.Data;
using MSIS_HMS.Infrastructure.Helpers;
using MSIS_HMS.Infrastructure.Interfaces;
using MSIS_HMS.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSIS_HMS.Infrastructure.Repositories
{
    public class PaymentRepository : Repository<IPDPayment>, IPaymentRepository
    {
        public PaymentRepository(ApplicationDbContext context, IConfigService configService) : base(context, configService)
        {

        }
        public override List<IPDPayment> GetAll(int? RecordId)
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetPayments", new Dictionary<string, object>
            { { "RecordId",RecordId} }
            );
            var ipdPayments = ds.Tables[0].ToList<IPDPayment>();
            return ipdPayments;
        }
        public override IPDPayment Get(int id)
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetPayments", new Dictionary<string, object>
            { { "Id",id} }
            );
            var ipdPayments = ds.Tables[0].ToList<IPDPayment>();
            return ipdPayments.Count > 0 ? ipdPayments[0] : null;
        }
        public PaymentCommonDTO GetRecordByRecordId(int? RecordId)
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetIPDRecordByRecordId", new Dictionary<string, object>()
            { { "RecordId", RecordId } });
            var records = ds.Tables[0].ToList<PaymentCommonDTO>();
            if (records[0] != null)
            {
                if (records[0].PaymentType == Core.Enums.PaymentTypeEnum.Advance)
                {
                    records[0].PaymentTypeName = Core.Enums.PaymentTypeEnum.Advance.ToDescription();
                }
                else if (records[0].PaymentType == Core.Enums.PaymentTypeEnum.Cash)
                {
                    records[0].PaymentTypeName = Core.Enums.PaymentTypeEnum.Cash.ToDescription();
                }
                else
                {
                    records[0].PaymentTypeName = Core.Enums.PaymentTypeEnum.Credit.ToDescription();
                }

            }
            return records.Count > 0 ? records[0] : null;
        }
        public override async Task<bool> DeleteAsync(int Id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var payment = await _context.IPDPayments.FindAsync(Id);
                    if (payment == null)
                    {
                        return false;
                    }
                    payment.IsDelete = true;
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (DbException e)
                {
                    Console.WriteLine(e.Message);
                    await transaction.RollbackAsync();
                }
            }
            return false;
        }

        public List<PaymentDetailDTO> GetPaymentDetail(int? RecordId)
        {
            List<PaymentDetailDTO> lstiPDPaymentDTOs = new List<PaymentDetailDTO>();
            var ipdRecord = GetRecordByRecordId(RecordId);
            var patientInfo = _context.Patients.Find(ipdRecord.PatientId);
            var branchInfo = _context.Branches.Find(patientInfo.BranchId);
            int branchCheckIn = branchInfo.CheckInTime.Hours;
            int branchCheckout = branchInfo.CheckOutTime.Hours;

            List<DateTime> dateTimes = new List<DateTime>();
            List<int> days = new List<int>();
            if (ipdRecord.DODC == Convert.ToDateTime("1/1/0001 12:00:00 AM") || ipdRecord.DODC == null)
            {
                var curdate = DateTime.Now.Date;
                var setdate = new DateTime();
                if (ipdRecord.DOA.Hour < branchCheckIn)
                {
                    var doa = ipdRecord.DOA.Date.AddDays(-1);
                    //ipdRecord.DOA = new DateTime(doa.Year, doa.Month, doa.Day, ipdRecord.DOA.Hour, ipdRecord.DOA.Minute, ipdRecord.DOA.Second);
                    setdate = new DateTime(doa.Year, doa.Month, doa.Day, ipdRecord.DOA.Hour, ipdRecord.DOA.Minute, ipdRecord.DOA.Second); //ipdRecord.DOA;
                    dateTimes.Add(setdate);
                }
                else
                {
                    setdate = ipdRecord.DOA.Date;
                    dateTimes.Add(setdate);
                }
                days.Add(0);
                while (setdate != curdate)
                {


                    var h = new DateTime(ipdRecord.DOA.Year, ipdRecord.DOA.Month, ipdRecord.DOA.Day, branchCheckIn, 0, 0);
                    setdate = setdate.AddDays(1).Date;
                    var ch = new DateTime(setdate.Year, setdate.Month, setdate.Day, branchCheckout, 0, 0);
                    TimeSpan diffT = ch - h;
                    days.Add(diffT.Days);

                    //setdate = setdate.AddDays(1).Date;
                    dateTimes.Add(setdate);


                }


            }
            else
            {
                var curdate = ipdRecord.DODC.Date;
                DateTime setdate = new DateTime();
                if (ipdRecord.DOA.Hour < branchCheckIn)
                {
                    var doa = ipdRecord.DOA.Date.AddDays(-1);
                    //ipdRecord.DOA = new DateTime(doa.Year, doa.Month, doa.Day, ipdRecord.DOA.Hour, ipdRecord.DOA.Minute, ipdRecord.DOA.Second);
                    setdate = new DateTime(doa.Year, doa.Month, doa.Day, ipdRecord.DOA.Hour, ipdRecord.DOA.Minute, ipdRecord.DOA.Second);
                    dateTimes.Add(setdate);
                }
                else
                {
                    setdate = ipdRecord.DOA.Date;
                    dateTimes.Add(setdate);
                }
                days.Add(0);

                while (setdate != curdate)
                {
                    var h = new DateTime(ipdRecord.DOA.Year, ipdRecord.DOA.Month, ipdRecord.DOA.Day, branchCheckIn, 0, 0);
                    setdate = setdate.AddDays(1).Date;
                    var ch = new DateTime(setdate.Year, setdate.Month, setdate.Day, branchCheckout, 0, 0);
                    TimeSpan diffT = ch - h;
                    days.Add(diffT.Days);
                    dateTimes.Add(setdate);
                }
            }
            int i = 0;
            decimal roomCharges = 0;
            foreach (var d in dateTimes)
            {

                DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetPaymentAmountBydate", new Dictionary<string, object>()
                { { "date", d< ipdRecord.DOA.Date?ipdRecord.DOA.Date:d},
                  { "IPDRecordId",RecordId} });
                var iPDPaymentDTOs = ds.Tables[0].ToList<PaymentDetailDTO>();
                var iPDPaymentDTO = iPDPaymentDTOs.Count > 0 ? iPDPaymentDTOs[0] : new PaymentDetailDTO();

                if (iPDPaymentDTO.RoomCharges == 0)
                {
                    iPDPaymentDTO.RoomCharges = roomCharges;
                }
                else
                {
                    roomCharges = iPDPaymentDTO.RoomCharges;
                }
                //if (d < ipdRecord.DOA.Date) //check in time မရောက်ခင် လာသော admission လုပ်သော patient များကို တစ်ရက်စာကောက်မည်ဆိုပါက ဒီ flow မလိုပါ
                //{
                //    iPDPaymentDTO.Date = ipdRecord.DOA.Date;
                //}
                //else
                //{
                iPDPaymentDTO.Date = d;
                //}

                DataSet dsAllotment = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetIPDAllotmentByDateandRecordId", new Dictionary<string, object>()
                { { "date",  d< ipdRecord.DOA.Date?ipdRecord.DOA.Date:d==ipdRecord.DODC.Date?ipdRecord.DOA.Date:d},
                  { "IPDRecordId",RecordId} });
                var iPDAllotmentDTO = dsAllotment.Tables[0].ToList<IPDAllotment>();
                //TimeSpan time;
                //var cIn =TimeSpan.Parse(iPDAllotmentDTO[0].CheckInTime.ToString(),out time);
                if (iPDAllotmentDTO.Count > 0 && (d < ipdRecord.DOA.Date || d == ipdRecord.DODC.Date))
                {
                    if ((iPDAllotmentDTO[0].CheckInTime < branchInfo.CheckInTime) || (iPDAllotmentDTO[0].CheckOutTime > branchInfo.CheckOutTime))
                    {
                        //var bIn = Convert.ToDateTime(branchCheckIn);
                        var chIn = iPDAllotmentDTO[0].CheckInTime != null ? iPDAllotmentDTO[0].CheckInTime.ToString() : "";
                        int diffIn = branchCheckIn - Convert.ToDateTime(chIn).Hour;
                        if (diffIn <= 4)
                        {
                            iPDPaymentDTO.RoomCharges = iPDPaymentDTO.RoomCharges / 2;
                        }
                    }
                }

                iPDPaymentDTO.Day = days[i];
                iPDPaymentDTO.Total = iPDPaymentDTO.RoomCharges + iPDPaymentDTO.Services + iPDPaymentDTO.Food + iPDPaymentDTO.Medications + iPDPaymentDTO.Fees + iPDPaymentDTO.Lab;
                lstiPDPaymentDTOs.Add(iPDPaymentDTO);
                i++;
            }
            return lstiPDPaymentDTOs;
        }

        public List<PaymentAmountDTO> GetPaymentAmount(int ipdRecordId)
        {
            List<PaymentAmountDTO> iPDPaymentDTOs = new List<PaymentAmountDTO>();
            var ipdRecord = GetRecordByRecordId(ipdRecordId);
            var patientInfo = _context.Patients.Find(ipdRecord.PatientId);
            var branchInfo = _context.Branches.Find(patientInfo.BranchId);
            int branchCheckIn = branchInfo.CheckInTime.Hours;
            int branchCheckout = branchInfo.CheckOutTime.Hours;
            List<DateTime> dateTimes = new List<DateTime>();
            List<int> days = new List<int>();
            if (ipdRecord.DODC == Convert.ToDateTime("1/1/0001 12:00:00 AM") || ipdRecord.DODC == null)
            {
                var curdate = DateTime.Now.Date;
                var setdate = new DateTime();
                if (ipdRecord.DOA.Hour < branchCheckIn)
                {
                    var doa = ipdRecord.DOA.Date.AddDays(-1);
                    ipdRecord.DOA = new DateTime(doa.Year, doa.Month, doa.Day, ipdRecord.DOA.Hour, ipdRecord.DOA.Minute, ipdRecord.DOA.Second);
                    setdate = ipdRecord.DOA;
                    dateTimes.Add(setdate);
                }
                else
                {
                    setdate = ipdRecord.DOA.Date;
                    dateTimes.Add(setdate);
                }
                days.Add(0);
                while (setdate != curdate)
                {


                    var h = new DateTime(ipdRecord.DOA.Year, ipdRecord.DOA.Month, ipdRecord.DOA.Day, branchCheckIn, 0, 0);
                    setdate = setdate.AddDays(1).Date;
                    var ch = new DateTime(setdate.Year, setdate.Month, setdate.Day, branchCheckout, 0, 0);
                    TimeSpan diffT = ch - h;
                    days.Add(diffT.Days);

                    //setdate = setdate.AddDays(1).Date;
                    dateTimes.Add(setdate);


                }


            }
            else
            {
                var curdate = ipdRecord.DODC.Date;
                DateTime setdate = new DateTime();
                if (ipdRecord.DOA.Hour < branchCheckIn)
                {
                    var doa = ipdRecord.DOA.Date.AddDays(-1);
                    ipdRecord.DOA = new DateTime(doa.Year, doa.Month, doa.Day, ipdRecord.DOA.Hour, ipdRecord.DOA.Minute, ipdRecord.DOA.Second);
                    setdate = ipdRecord.DOA;
                    dateTimes.Add(setdate);
                }
                else
                {
                    setdate = ipdRecord.DOA.Date;
                    dateTimes.Add(setdate);
                }
                days.Add(0);

                while (setdate != curdate)
                {
                    var h = new DateTime(ipdRecord.DOA.Year, ipdRecord.DOA.Month, ipdRecord.DOA.Day, branchCheckIn, 0, 0);
                    setdate = setdate.AddDays(1).Date;
                    var ch = new DateTime(setdate.Year, setdate.Month, setdate.Day, branchCheckout, 0, 0);
                    TimeSpan diffT = ch - h;
                    days.Add(diffT.Days);
                    dateTimes.Add(setdate);
                }
            }
            int i = 0;
            decimal roomCharges = 0;
            PaymentAmountDTO paymentDetailDTO = new PaymentAmountDTO();
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetPaymentAmount", new Dictionary<string, object>()
                { { "IPDRecordId",ipdRecordId} }
                    );
            iPDPaymentDTOs = ds.Tables[0].ToList<PaymentAmountDTO>();
            foreach (var d in dateTimes)
            {

                if (d > ipdRecord.DOA)
                {
                    iPDPaymentDTOs[0].Amount += roomCharges;
                }
                else
                {
                    roomCharges = iPDPaymentDTOs[0].Amount;
                }

            }
            //var iPDPaymentDTO = iPDPaymentDTOs.Count > 0 ? iPDPaymentDTOs[0] : new PaymentAmountDTO();

            return iPDPaymentDTOs;
        }

        public List<IPDRecordDetailReportDTO> GetPaymentAmountByDate(int RecordId, DateTime? FromDate=null, DateTime? ToDate=null)
        {
            var ipdRecord = GetRecordByRecordId(RecordId);
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetPaymentDetailByDate", new Dictionary<string, object>()
                { { "FromDate", FromDate },
                  {"ToDate",ToDate },
                  { "RecordId",RecordId} });
            var roomChargesDTOs = ds.Tables[0].ToList<RoomChargesDTO>();
            var medicationDTOs = ds.Tables[1].ToList<MedicationsDTO>();
            var serviceDTOs = ds.Tables[2].ToList<IPDOrderServiceDTO>();
            var feesDTOs = ds.Tables[3].ToList<FeesDTO>();
            var foodDTOs = ds.Tables[4].ToList<FoodDTO>();
            var labDTOs = ds.Tables[5].ToList<LabDTO>();
            var imgDTOs = ds.Tables[6].ToList<ImagingDTO>();
            decimal subtotal = 0;
            int i = 0;
            List<IPDRecordDetailReportDTO> iPDRecordDetailReportDTOs = new List<IPDRecordDetailReportDTO>();
            if (roomChargesDTOs.Count > 0)
            {
                IPDRecordDetailReportDTO roomCharges = new IPDRecordDetailReportDTO();
                roomCharges.Name = "Room Charges";
                iPDRecordDetailReportDTOs.Add(roomCharges);
                foreach (var rc in roomChargesDTOs)
                {
                    i += 1;
                    int count = 0;
                    if (ipdRecord.DODC == Convert.ToDateTime("1/1/0001 12:00:00 AM") || ipdRecord.DODC == null)
                    {
                         count= (Convert.ToDateTime(ToDate).Date-Convert.ToDateTime(FromDate).Date).Days;
                    }
                    if(count==0)
                    {
                        count = 1;
                    }
                    IPDRecordDetailReportDTO iPDRecordDetailReportDTO = new IPDRecordDetailReportDTO();
                    iPDRecordDetailReportDTO.No = i;
                    iPDRecordDetailReportDTO.Name = rc.RoomName + "/" + rc.BedName;
                    iPDRecordDetailReportDTO.UnitPrice = rc.UnitPrice;
                    iPDRecordDetailReportDTO.Qty = count.ToString(); //rc.Qty.ToString();

                    iPDRecordDetailReportDTO.Amount = rc.UnitPrice * count;
                    iPDRecordDetailReportDTOs.Add(iPDRecordDetailReportDTO);

                    subtotal += iPDRecordDetailReportDTO.Amount;

                }
               
                IPDRecordDetailReportDTO roomChargesSubTotal = new IPDRecordDetailReportDTO();
                roomChargesSubTotal.Qty = "SubTotal";
                roomChargesSubTotal.Amount = subtotal;
                roomChargesSubTotal.SubTotal = subtotal;
                iPDRecordDetailReportDTOs.Add(roomChargesSubTotal);
            }
            if (medicationDTOs.Count > 0)
            {
                subtotal = 0;
                i = 0;
                IPDRecordDetailReportDTO medication = new IPDRecordDetailReportDTO();
                medication.Name = "Medications";
                iPDRecordDetailReportDTOs.Add(medication);
                IPDRecordDetailReportDTO iPDRecordDetailReportDTO = new IPDRecordDetailReportDTO();
                foreach (var m in medicationDTOs)//this comment is for Thirisandar Hospital
                {
                    i += 1;
                    //IPDRecordDetailReportDTO iPDRecordDetailReportDTO = new IPDRecordDetailReportDTO();
                    iPDRecordDetailReportDTO.No = i;
                    iPDRecordDetailReportDTO.Name = "Sale Drugs";// m.Name;
                    //iPDRecordDetailReportDTO.UnitPrice = m.UnitPrice;
                    //iPDRecordDetailReportDTO.Qty = m.Qty.ToString();
                    //iPDRecordDetailReportDTO.UnitName = m.UnitName;
                    iPDRecordDetailReportDTO.Amount += m.UnitPrice * m.Qty;
                    //iPDRecordDetailReportDTOs.Add(iPDRecordDetailReportDTO);
                    //subtotal += iPDRecordDetailReportDTO.Amount;
                }
                subtotal = iPDRecordDetailReportDTO.Amount;
                iPDRecordDetailReportDTOs.Add(iPDRecordDetailReportDTO);

                IPDRecordDetailReportDTO medicationSubTotal = new IPDRecordDetailReportDTO();
                medicationSubTotal.Qty = "SubTotal";
                medicationSubTotal.Amount = subtotal;
                medicationSubTotal.SubTotal = subtotal;
                iPDRecordDetailReportDTOs.Add(medicationSubTotal);
            }
            if (serviceDTOs.Count > 0)
            {
                i = 0;
                subtotal = 0;
                IPDRecordDetailReportDTO services = new IPDRecordDetailReportDTO();
                services.Name = "Services";
                iPDRecordDetailReportDTOs.Add(services);
                foreach (var s in serviceDTOs)
                {
                    i += 1;
                    IPDRecordDetailReportDTO iPDRecordDetailReportDTO = new IPDRecordDetailReportDTO();
                    iPDRecordDetailReportDTO.No = i;
                    iPDRecordDetailReportDTO.Name = s.Name;
                    iPDRecordDetailReportDTO.UnitPrice = s.UnitPrice;
                    iPDRecordDetailReportDTO.Qty = s.Qty.ToString();
                    iPDRecordDetailReportDTO.Amount = s.UnitPrice * s.Qty;
                    iPDRecordDetailReportDTOs.Add(iPDRecordDetailReportDTO);
                    subtotal += iPDRecordDetailReportDTO.Amount;
                }

                IPDRecordDetailReportDTO servicesSubTotal = new IPDRecordDetailReportDTO();
                servicesSubTotal.Qty = "SubTotal";
                servicesSubTotal.Amount = subtotal;
                servicesSubTotal.SubTotal = subtotal;
                iPDRecordDetailReportDTOs.Add(servicesSubTotal);
            }
            if (feesDTOs.Count > 0)
            {
                i = 0;
                subtotal = 0;
                IPDRecordDetailReportDTO fees = new IPDRecordDetailReportDTO();
                fees.Name = "Round & Other Fees";
                iPDRecordDetailReportDTOs.Add(fees);
                foreach (var fe in feesDTOs)
                {
                    i += 1;
                    IPDRecordDetailReportDTO iPDRecordDetailReportDTO = new IPDRecordDetailReportDTO();
                    iPDRecordDetailReportDTO.No = i;
                    iPDRecordDetailReportDTO.Name = fe.FeesName;
                    iPDRecordDetailReportDTO.UnitPrice = fe.UnitPrice;
                    iPDRecordDetailReportDTO.Qty = fe.Qty.ToString();
                    iPDRecordDetailReportDTO.Amount = fe.UnitPrice * fe.Qty;
                    iPDRecordDetailReportDTOs.Add(iPDRecordDetailReportDTO);
                    subtotal += iPDRecordDetailReportDTO.Amount;
                }

                IPDRecordDetailReportDTO feesSubTotal = new IPDRecordDetailReportDTO();
                feesSubTotal.Qty = "SubTotal";
                feesSubTotal.Amount = subtotal;
                feesSubTotal.SubTotal = subtotal;
                iPDRecordDetailReportDTOs.Add(feesSubTotal);
            }
            if (foodDTOs.Count > 0)
            {
                i = 0;
                subtotal = 0;
                IPDRecordDetailReportDTO foods = new IPDRecordDetailReportDTO();
                foods.Name = "Foods";
                iPDRecordDetailReportDTOs.Add(foods);
                foreach (var f in foodDTOs)
                {
                    i += 1;
                    IPDRecordDetailReportDTO iPDRecordDetailReportDTO = new IPDRecordDetailReportDTO();
                    iPDRecordDetailReportDTO.No = i;
                    iPDRecordDetailReportDTO.Name = f.Name;
                    iPDRecordDetailReportDTO.UnitPrice = f.UnitPrice;
                    iPDRecordDetailReportDTO.Qty = f.Qty.ToString();
                    iPDRecordDetailReportDTO.Amount = f.UnitPrice * f.Qty;
                    iPDRecordDetailReportDTOs.Add(iPDRecordDetailReportDTO);
                    subtotal += iPDRecordDetailReportDTO.Amount;
                }

                IPDRecordDetailReportDTO foodsSubTotal = new IPDRecordDetailReportDTO();
                foodsSubTotal.Qty = "SubTotal";
                foodsSubTotal.Amount = subtotal;
                foodsSubTotal.SubTotal = subtotal;
                iPDRecordDetailReportDTOs.Add(foodsSubTotal);
            }
            if (labDTOs.Count > 0)
            {
                i = 0;
                subtotal = 0;
                IPDRecordDetailReportDTO labs = new IPDRecordDetailReportDTO();
                labs.Name = "Labs";
                iPDRecordDetailReportDTOs.Add(labs);
                foreach (var l in labDTOs)
                {
                    i += 1;
                    IPDRecordDetailReportDTO iPDRecordDetailReportDTO = new IPDRecordDetailReportDTO();
                    iPDRecordDetailReportDTO.No = i;
                    iPDRecordDetailReportDTO.Name = l.Name;
                    iPDRecordDetailReportDTO.UnitPrice = l.UnitPrice;
                    iPDRecordDetailReportDTO.Qty = l.Qty.ToString();
                    iPDRecordDetailReportDTO.Amount = l.UnitPrice * 1;
                    iPDRecordDetailReportDTOs.Add(iPDRecordDetailReportDTO);
                    subtotal += iPDRecordDetailReportDTO.Amount;
                }

                IPDRecordDetailReportDTO labsSubTotal = new IPDRecordDetailReportDTO();
                labsSubTotal.Qty = "SubTotal";
                labsSubTotal.Amount = subtotal;
                labsSubTotal.SubTotal = subtotal;
                iPDRecordDetailReportDTOs.Add(labsSubTotal);
            }
            if (imgDTOs.Count > 0)
            {
                i = 0;
                subtotal = 0;
                IPDRecordDetailReportDTO imgs = new IPDRecordDetailReportDTO();
                imgs.Name = "Imagings";
                iPDRecordDetailReportDTOs.Add(imgs);
                foreach (var img in imgDTOs)
                {
                    i += 1;
                    IPDRecordDetailReportDTO iPDRecordDetailReportDTO = new IPDRecordDetailReportDTO();
                    iPDRecordDetailReportDTO.No = i;
                    iPDRecordDetailReportDTO.Name = img.Name;
                    iPDRecordDetailReportDTO.UnitPrice = img.UnitPrice;
                    iPDRecordDetailReportDTO.Qty = img.Qty.ToString();
                    iPDRecordDetailReportDTO.Amount = img.UnitPrice * 1;
                    iPDRecordDetailReportDTOs.Add(iPDRecordDetailReportDTO);
                    subtotal += iPDRecordDetailReportDTO.Amount;
                }

                IPDRecordDetailReportDTO imgsSubTotal = new IPDRecordDetailReportDTO();
                imgsSubTotal.Qty = "SubTotal";
                imgsSubTotal.Amount = subtotal;
                imgsSubTotal.SubTotal = subtotal;
                iPDRecordDetailReportDTOs.Add(imgsSubTotal);
            }
            i = 0;                    
            return iPDRecordDetailReportDTOs;
        }

        public IPDRecordDetailDTO GetIPDRecordDetailByRecordId(int RecordId, DateTime date)
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetIPDRecordDetailByRecordId", new Dictionary<string, object>()
                { { "Date", date },
                  { "RecordId",RecordId} });
            var roomChargesDTOs = ds.Tables[0].ToList<RoomChargesDTO>();

            var medicationDTOs = ds.Tables[1].ToList<MedicationsDTO>();
            var serviceDTOs = ds.Tables[2].ToList<IPDOrderServiceDTO>();
            var feesDTOs = ds.Tables[3].ToList<FeesDTO>();
            var foodDTOs = ds.Tables[4].ToList<FoodDTO>();
            var labDTOs = ds.Tables[5].ToList<LabDTO>();
            var imgDTOs = ds.Tables[6].ToList<ImagingDTO>();
            IPDRecordDetailDTO iPDRecordDetailDTO = new IPDRecordDetailDTO();
            iPDRecordDetailDTO.roomChargesDTOs = roomChargesDTOs;
            iPDRecordDetailDTO.medicationsDTOs = medicationDTOs;
            iPDRecordDetailDTO.iPDOrderServiceDTOs = serviceDTOs;
            iPDRecordDetailDTO.feesDTOs = feesDTOs;
            iPDRecordDetailDTO.foodDTOs = foodDTOs;
            iPDRecordDetailDTO.labDTOs = labDTOs;
            iPDRecordDetailDTO.imgDTOs = imgDTOs;
            return iPDRecordDetailDTO;
        }
        public List<IPDRecordDetailReportDTO> GetIPDRecordDetailForReport(int RecordId, DateTime date)
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetIPDRecordDetailByRecordId", new Dictionary<string, object>()
                { { "Date", date },
                  { "RecordId",RecordId} });
            var roomChargesDTOs = ds.Tables[0].ToList<RoomChargesDTO>();
            var medicationDTOs = ds.Tables[1].ToList<MedicationsDTO>();
            var serviceDTOs = ds.Tables[2].ToList<IPDOrderServiceDTO>();
            var feesDTOs = ds.Tables[3].ToList<FeesDTO>();
            var foodDTOs = ds.Tables[4].ToList<FoodDTO>();
            var labDTOs = ds.Tables[5].ToList<LabDTO>();
            var imgDTOs = ds.Tables[6].ToList<ImagingDTO>();
            decimal subtotal = 0;
            int i = 0;
            List<IPDRecordDetailReportDTO> iPDRecordDetailReportDTOs = new List<IPDRecordDetailReportDTO>();
            if (roomChargesDTOs.Count > 0)
            {
                IPDRecordDetailReportDTO roomCharges = new IPDRecordDetailReportDTO();
                roomCharges.Name = "Room Charges";
                iPDRecordDetailReportDTOs.Add(roomCharges);
                foreach (var rc in roomChargesDTOs)
                {
                    i += 1;
                    IPDRecordDetailReportDTO iPDRecordDetailReportDTO = new IPDRecordDetailReportDTO();
                    iPDRecordDetailReportDTO.No = i;
                    iPDRecordDetailReportDTO.Name = rc.RoomName + "/" + rc.BedName;
                    iPDRecordDetailReportDTO.UnitPrice = rc.UnitPrice;
                    iPDRecordDetailReportDTO.Qty = rc.Qty.ToString();
                    iPDRecordDetailReportDTO.Amount = rc.UnitPrice * rc.Qty;
                    iPDRecordDetailReportDTOs.Add(iPDRecordDetailReportDTO);

                    subtotal += iPDRecordDetailReportDTO.Amount;

                }
                IPDRecordDetailReportDTO roomChargesSubTotal = new IPDRecordDetailReportDTO();
                roomChargesSubTotal.Qty = "SubTotal";
                roomChargesSubTotal.Amount = subtotal;
                iPDRecordDetailReportDTOs.Add(roomChargesSubTotal);
            }
            if (medicationDTOs.Count > 0)
            {
                subtotal = 0;
                i = 0;
                IPDRecordDetailReportDTO medication = new IPDRecordDetailReportDTO();
                medication.Name = "Medications";
                iPDRecordDetailReportDTOs.Add(medication);
                foreach (var m in medicationDTOs)
                {
                    i += 1;
                    IPDRecordDetailReportDTO iPDRecordDetailReportDTO = new IPDRecordDetailReportDTO();
                    iPDRecordDetailReportDTO.No = i;
                    iPDRecordDetailReportDTO.Name = m.Name;
                    iPDRecordDetailReportDTO.UnitPrice = m.UnitPrice;
                    iPDRecordDetailReportDTO.Qty = m.Qty.ToString();
                    iPDRecordDetailReportDTO.UnitName = m.UnitName;
                    iPDRecordDetailReportDTO.Amount = m.UnitPrice * m.Qty;
                    iPDRecordDetailReportDTOs.Add(iPDRecordDetailReportDTO);
                    subtotal += iPDRecordDetailReportDTO.Amount;
                }

                IPDRecordDetailReportDTO medicationSubTotal = new IPDRecordDetailReportDTO();
                medicationSubTotal.Qty = "SubTotal";
                medicationSubTotal.Amount = subtotal;
                iPDRecordDetailReportDTOs.Add(medicationSubTotal);
            }
            if (serviceDTOs.Count > 0)
            {
                i = 0;
                subtotal = 0;
                IPDRecordDetailReportDTO services = new IPDRecordDetailReportDTO();
                services.Name = "Services";
                iPDRecordDetailReportDTOs.Add(services);
                foreach (var s in serviceDTOs)
                {
                    i += 1;
                    IPDRecordDetailReportDTO iPDRecordDetailReportDTO = new IPDRecordDetailReportDTO();
                    iPDRecordDetailReportDTO.No = i;
                    iPDRecordDetailReportDTO.Name = s.Name;
                    iPDRecordDetailReportDTO.UnitPrice = s.UnitPrice;
                    iPDRecordDetailReportDTO.Qty = s.Qty.ToString();
                    iPDRecordDetailReportDTO.Amount = s.UnitPrice * s.Qty;
                    iPDRecordDetailReportDTOs.Add(iPDRecordDetailReportDTO);
                    subtotal += iPDRecordDetailReportDTO.Amount;
                }

                IPDRecordDetailReportDTO servicesSubTotal = new IPDRecordDetailReportDTO();
                servicesSubTotal.Qty = "SubTotal";
                servicesSubTotal.Amount = subtotal;
                iPDRecordDetailReportDTOs.Add(servicesSubTotal);
            }
            if (feesDTOs.Count > 0)
            {
                i = 0;
                subtotal = 0;
                IPDRecordDetailReportDTO fees = new IPDRecordDetailReportDTO();
                fees.Name = "Round & Other Fees";
                iPDRecordDetailReportDTOs.Add(fees);
                foreach (var fe in feesDTOs)
                {
                    i += 1;
                    IPDRecordDetailReportDTO iPDRecordDetailReportDTO = new IPDRecordDetailReportDTO();
                    iPDRecordDetailReportDTO.No = i;
                    iPDRecordDetailReportDTO.Name = fe.FeesName;
                    iPDRecordDetailReportDTO.UnitPrice = fe.UnitPrice;
                    iPDRecordDetailReportDTO.Qty = fe.Qty.ToString();
                    iPDRecordDetailReportDTO.Amount = fe.UnitPrice * fe.Qty;
                    iPDRecordDetailReportDTOs.Add(iPDRecordDetailReportDTO);
                    subtotal += iPDRecordDetailReportDTO.Amount;
                }

                IPDRecordDetailReportDTO feesSubTotal = new IPDRecordDetailReportDTO();
                feesSubTotal.Qty = "SubTotal";
                feesSubTotal.Amount = subtotal;
                iPDRecordDetailReportDTOs.Add(feesSubTotal);
            }
            if (foodDTOs.Count > 0)
            {
                i = 0;
                subtotal = 0;
                IPDRecordDetailReportDTO foods = new IPDRecordDetailReportDTO();
                foods.Name = "Foods";
                iPDRecordDetailReportDTOs.Add(foods);
                foreach (var f in foodDTOs)
                {
                    i += 1;
                    IPDRecordDetailReportDTO iPDRecordDetailReportDTO = new IPDRecordDetailReportDTO();
                    iPDRecordDetailReportDTO.No = i;
                    iPDRecordDetailReportDTO.Name = f.Name;
                    iPDRecordDetailReportDTO.UnitPrice = f.UnitPrice;
                    iPDRecordDetailReportDTO.Qty = f.Qty.ToString();
                    iPDRecordDetailReportDTO.Amount = f.UnitPrice * f.Qty;
                    iPDRecordDetailReportDTOs.Add(iPDRecordDetailReportDTO);
                    subtotal += iPDRecordDetailReportDTO.Amount;
                }

                IPDRecordDetailReportDTO foodsSubTotal = new IPDRecordDetailReportDTO();
                foodsSubTotal.Qty = "SubTotal";
                foodsSubTotal.Amount = subtotal;
                iPDRecordDetailReportDTOs.Add(foodsSubTotal);
            }
            if (labDTOs.Count > 0)
            {
                i = 0;
                subtotal = 0;
                IPDRecordDetailReportDTO labs = new IPDRecordDetailReportDTO();
                labs.Name = "Labs";
                iPDRecordDetailReportDTOs.Add(labs);
                foreach (var l in labDTOs)
                {
                    i += 1;
                    IPDRecordDetailReportDTO iPDRecordDetailReportDTO = new IPDRecordDetailReportDTO();
                    iPDRecordDetailReportDTO.No = i;
                    iPDRecordDetailReportDTO.Name = l.Name;
                    iPDRecordDetailReportDTO.UnitPrice = l.UnitPrice;
                    iPDRecordDetailReportDTO.Qty = l.Qty.ToString();
                    iPDRecordDetailReportDTO.Amount = l.UnitPrice * 1;
                    iPDRecordDetailReportDTOs.Add(iPDRecordDetailReportDTO);
                    subtotal += iPDRecordDetailReportDTO.Amount;
                }

                IPDRecordDetailReportDTO labsSubTotal = new IPDRecordDetailReportDTO();
                labsSubTotal.Qty = "SubTotal";
                labsSubTotal.Amount = subtotal;
                iPDRecordDetailReportDTOs.Add(labsSubTotal);
            }
            if (imgDTOs.Count > 0)
            {
                i = 0;
                subtotal = 0;
                IPDRecordDetailReportDTO imgs = new IPDRecordDetailReportDTO();
                imgs.Name = "Imagings";
                iPDRecordDetailReportDTOs.Add(imgs);
                foreach (var l in imgDTOs)
                {
                    i += 1;
                    IPDRecordDetailReportDTO iPDRecordDetailReportDTO = new IPDRecordDetailReportDTO();
                    iPDRecordDetailReportDTO.No = i;
                    iPDRecordDetailReportDTO.Name = l.Name;
                    iPDRecordDetailReportDTO.UnitPrice = l.UnitPrice;
                    iPDRecordDetailReportDTO.Qty = l.Qty.ToString();
                    iPDRecordDetailReportDTO.Amount = l.UnitPrice * 1;
                    iPDRecordDetailReportDTOs.Add(iPDRecordDetailReportDTO);
                    subtotal += iPDRecordDetailReportDTO.Amount;
                }

                IPDRecordDetailReportDTO imgsSubTotal = new IPDRecordDetailReportDTO();
                imgsSubTotal.Qty = "SubTotal";
                imgsSubTotal.Amount = subtotal;
                iPDRecordDetailReportDTOs.Add(imgsSubTotal);
            }
            i = 0;
            return iPDRecordDetailReportDTOs;
        }

        public IPDPayment CalculateDailyPayment(int ipdRecordId)
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_CalculateDailyPayment", new Dictionary<string, object>()
                { { "date", DateTime.Now.Date },
                  { "IPDRecordId",ipdRecordId} });
            var iPDPayments = ds.Tables[0].ToList<IPDPayment>();
            var iPDPayment = iPDPayments.Count > 0 ? iPDPayments[0] : new IPDPayment();

            return iPDPayment;
        }
        public List<IPDPayment> GetIPDPaymentUnderPercent()
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetIPDPayemntUnderPercent", new Dictionary<string, object>()
            { });
            var iPDPayments = ds.Tables[0].ToList<IPDPayment>();
            return iPDPayments;
        }

    }
}
