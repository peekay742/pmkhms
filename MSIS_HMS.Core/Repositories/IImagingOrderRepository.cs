using MSIS_HMS.Core.Entities;
using MSIS_HMS.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSIS_HMS.Core.Repositories
{
   public interface IImagingOrderRepository:IRepository<ImagingOrder>
    {
        List<ImagingOrder> GetAll(int? BranchId = null, string? BarCode = null, string? QRCode = null, int? ImgOrderId = null, int? PatientId = null, string VoucherNo = null, bool? IsPaid = null, DateTime? Date = null, DateTime? StartDate = null, DateTime? EndDate = null, DateTime? PaidDate = null, DateTime? StartPaidDate = null, DateTime? EndPaidDate = null, int? TestId = null);
        List<ImagingOrder> GetImgOrderFromImgOrderTest(int? BranchId = null);
        Task UpdateImgOrderTest(int imgOrderId, int imgResultId, int labTestId);
    }
}
