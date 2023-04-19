using MSIS_HMS.Core.Entities;
using MSIS_HMS.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSIS_HMS.Core.Repositories
{
    public interface IPurchaseOrderRepository : IRepository<PurchaseOrder>
    {
        List<PurchaseOrder> GetAll(int? BranchId, int? PurchaseOrderId, int? PurchaseItemId, int? ItemId, string PurchaseOrderNo = null, string Supplier = null, DateTime? PurchaseOrderDate = null, DateTime? StartPurchaseOrderDate = null, DateTime? EndPurchaseOrderDate = null);
        List<PurchaseItem> GetPurchaseOrderItems(int Id);
        List<PurchaseOrder> GetPurchaseOrderFromPurchaseOrderItem(int? BranchId = null);
    }
}
