using MSIS_HMS.Core.Entities;
using MSIS_HMS.Core.Enums;
using MSIS_HMS.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSIS_HMS.Core.Repositories
{
    public interface IOperationOrderRepository : IRepository<OperationOrder>
    {
        List<OperationOrder> GetAll(int? BranchId = null, int? OrderId = null, int? PatientId = null, int? DoctorId = null, DateTime? OTDate = null, DateTime? AdmitDate = null, DateTime? StartDate = null, DateTime? EndDate = null, OTOrderStatusEnum? Status = null);

    }
}
