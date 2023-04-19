using Microsoft.EntityFrameworkCore;
using MSIS_HMS.Core.Entities;
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
   public class ImagingOrderRepository:Repository<ImagingOrder>,IImagingOrderRepository
    {
        public ImagingOrderRepository(ApplicationDbContext context,IConfigService config):base(context,config)
        {

        }

        public List<ImagingOrder> GetAll(int? BranchId = null, string? BarCode = null, string? QRCode = null, int? ImgOrderId = null, int? PatientId = null, string VoucherNo = null, bool? IsPaid = null, DateTime? Date = null, DateTime? StartDate = null, DateTime? EndDate = null, DateTime? PaidDate = null, DateTime? StartPaidDate = null, DateTime? EndPaidDate = null, int? TestId = null)
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetImgOrders", new Dictionary<string, object>() {
                { "BranchId", BranchId },
                {"BarCode",BarCode },
                {"QRCode",QRCode }, 
                { "ImagingOrderId", ImgOrderId },
                { "PatientId", PatientId },
                { "VoucherNo", VoucherNo },
                { "IsPaid", IsPaid },
                { "Date", Date },
                { "StartDate", StartDate },
                { "EndDate", EndDate },
                { "PaidDate", PaidDate },
                { "StartPaidDate", StartPaidDate },
                { "EndPaidDate", EndPaidDate },
                {"TestId",TestId }
            });
            var imgOrders = ds.Tables[0].ToList<ImagingOrder>();
            return imgOrders;
        }
        public override ImagingOrder Get(int Id)
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetImgOrders", new Dictionary<string, object>() { { "ImagingOrderId", Id } });
            var imgOrders = ds.Tables[0].ToList<ImagingOrder>();
            var imgOrder = imgOrders.Count > 0 ? imgOrders[0] : null;
            if (imgOrder != null)
            {
                imgOrder.ImagingOrderTests = GetImgOrderTests(Id);
            }
            return imgOrder;
        }
        public List<ImagingOrderTest> GetImgOrderTests(int Id)
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetImgOrderTests", new Dictionary<string, object>() { { "ImagingOrderId", Id } });
            var imgOrderTests = ds.Tables[0].ToList<ImagingOrderTest>();
            return imgOrderTests;
        }
        public override async Task<ImagingOrder> UpdateAsync(ImagingOrder imgOrder)
        {
            if (imgOrder != null)
            {
                imgOrder.ImagingOrderTests.ToList().ForEach(x => x.ImagingOrderId = imgOrder.Id);
                var existingOrder = _context.ImagingOrder
                    .Where(p => p.Id == imgOrder.Id)
                    .Include(x => x.ImagingOrderTests)
                    .SingleOrDefault();

                if (existingOrder != null)
                {
                    // Update parent
                    imgOrder.CreatedAt = existingOrder.CreatedAt;
                    imgOrder.CreatedBy = existingOrder.CreatedBy;
                    _context.Entry(existingOrder).CurrentValues.SetValues(imgOrder);

                    // Delete children
                    _context.ImagingOrderTests.RemoveRange(existingOrder.ImagingOrderTests);

                    // Insert children
                    existingOrder.ImagingOrderTests = imgOrder.ImagingOrderTests;

                    await _context.SaveChangesAsync();
                    return imgOrder;
                }
            }
            return null;
        }

        public async Task UpdateImgOrderTest(int imgOrderId, int imgResultId, int labTestId)
        {
            ImagingOrderTest imgOrderTest = new ImagingOrderTest();
            imgOrderTest = _context.ImagingOrderTests.Where(x => x.ImagingOrderId == imgOrderId && x.LabTestId == labTestId).FirstOrDefault();
            if (imgOrderTest != null)
            {
                imgOrderTest.ImagingResultId = imgResultId;
                await _context.SaveChangesAsync();
            }

        }
        public override async Task<bool> DeleteAsync(int Id)
        {
            try
            {
                var imgOrder = await _context.ImagingOrder.FindAsync(Id);
                if (imgOrder == null)
                {
                    return false;
                }
                imgOrder.IsDelete = true;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        public List<ImagingOrder> GetImgOrderFromImgOrderTest(int? BranchId = null)
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetImgOrderFromImgOrderTest", new Dictionary<string, object>() {
                { "BranchId", BranchId }
            });
            var completeLabOrder = ds.Tables[0].ToList<ImagingOrder>();

            return completeLabOrder;
        }
      

    }
}
