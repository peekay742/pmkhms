using MSIS_HMS.Core.Entities;
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
using System.Text;
using System.Threading.Tasks;

namespace MSIS_HMS.Infrastructure.Repositories
{
    public class OperationOrderRepository: Repository<OperationOrder> , IOperationOrderRepository
    {
        public OperationOrderRepository(ApplicationDbContext context, IConfigService configService) : base(context, configService)
        {

        }

        public List<OperationOrder> GetAll(int? BranchId = null, int? OrderId = null, int? PatientId = null, int? DoctorId = null, DateTime? OTDate = null, DateTime? AdmitDate = null, DateTime? StartDate = null, DateTime? EndDate = null,OTOrderStatusEnum? Status = null)
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetOperationOrders", new Dictionary<string, object>(){
                { "BranchId", BranchId },
                { "OperationOrderId", OrderId },
                { "PatientId", PatientId },
                { "DoctorId", DoctorId },
                { "OTDate" , OTDate  },
                { "AdmitDate" , AdmitDate },
                { "StartDate", StartDate },
                { "EndDate", EndDate },
                { "Status" , Status }

            });
            var orders = ds.Tables[0].ToList<OperationOrder>();
            return orders;
        }

        public override OperationOrder Get(int Id)
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetOperationOrders", new Dictionary<string, object>() { { "OperationOrderId", Id } });
            var operationOrders = ds.Tables[0].ToList<OperationOrder>();
            var operationOrder = operationOrders.Count > 0 ? operationOrders[0] : null;
            return operationOrder;
        }

        public override async Task<bool> DeleteAsync(int Id)
        {
            try
            {
                var operationOrder = await _context.OperationOrders.FindAsync(Id);
                if (operationOrder == null)
                {
                    return false;
                }
                operationOrder.IsDelete = true;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
