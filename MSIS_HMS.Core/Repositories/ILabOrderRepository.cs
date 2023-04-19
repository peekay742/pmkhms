using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MSIS_HMS.Core.Entities;
using MSIS_HMS.Core.Entities.DTOs;
using MSIS_HMS.Core.Enums;
using MSIS_HMS.Core.Repositories.Base;

namespace MSIS_HMS.Core.Repositories
{
    public interface ILabOrderRepository : IRepository<LabOrder>
    {
        List<LabOrder> GetAll(int? BranchId = null, int? LabOrderId = null, int? PatientId = null, string VoucherNo = null, bool? IsPaid = null,  DateTime? Date = null, DateTime? StartDate = null, DateTime? EndDate = null, DateTime? PaidDate = null, DateTime? StartPaidDate = null, DateTime? EndPaidDate = null, int? TestId = null, int?CollectionGroupId=null, string? BarCode = null, string? QRCode = null, bool? GetCollection = null, DateTime? GetCollectionDate = null,OrderByEnum? OrderBy= null,bool? Cancel=null);
        List<LabOrderTest> GetLabOrderTests(int Id);
        List<CollectionGroup> GetCollectionGroups(int Id);
        List<DailyAndMonthlyOrderIncomeDTO> GetIncomeForLabDashboard(int? BranchId = null, DateTime? StartDate = null, DateTime? EndDate = null);
        List<LabOrder> GetLabOrderResultComplete(int? BranchId = null);
        List<LabOrder> GetLabOrderFromLabOrderTest(int? BranchId = null);
        Task UpdateLabOrderTest(int labOrderId, int labResultId, int labTestId);
        //public async Task UpdateLabOrderTest(int labOrderId, int labResultId, int labTestId)
    }
}
