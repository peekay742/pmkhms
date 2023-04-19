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
using static MSIS_HMS.Infrastructure.Enums.DbEnum;

namespace MSIS_HMS.Infrastructure.Repositories
{
    public class PurchaseOrderRepository : Repository<PurchaseOrder>, IPurchaseOrderRepository
    {
        public PurchaseOrderRepository(ApplicationDbContext context, IConfigService configService) : base(context, configService)
        {
        }
        public override List<PurchaseOrder> GetAll()
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetPurchaseOrders");
            var purchases = ds.Tables[0].ToList<PurchaseOrder>();
            return purchases;
        }
        public override List<PurchaseOrder> GetAll(int? BranchId)
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetPurchaseOrders", new Dictionary<string, object>() { { "BranchId", BranchId } });
            var purchaseOrders = ds.Tables[0].ToList<PurchaseOrder>();
            return purchaseOrders;
        }

        public List<PurchaseOrder> GetAll(int? BranchId, int? PurchaseOrderId, int?PurchaseItemId,int? ItemId, string PurchaseOrderNo = null, string Supplier = null, DateTime? PurchaseOrderDate = null, DateTime? StartPurchaseOrderDate = null, DateTime? EndPurchaseOrderDate = null)
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetPurchaseOrders", new Dictionary<string, object>()
            {
                { "BranchId", BranchId },
                { "PurchaseOrderId", PurchaseOrderId },
                { "PurchaseItemId",PurchaseItemId },
                { "ItemId", ItemId },
                { "PurchaseOrderNo", PurchaseOrderNo },
                { "Supplier", Supplier },
                { "PurchaseOrderDate", PurchaseOrderDate },
                { "StartPurchaseOrderDate", StartPurchaseOrderDate },
                { "EndPurchaseOrderDate", EndPurchaseOrderDate },
            });
            var purchaseOrders = ds.Tables[0].ToList<PurchaseOrder>();
            return purchaseOrders;
        }

        public List<PurchaseItem> GetPurchaseOrderItems(int PurchaseOrderId)
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetPurchaseOrderItems", new Dictionary<string, object>() { { "PurchaseOrderId", PurchaseOrderId } });
            var purchaseOrderItems = ds.Tables[0].ToList<PurchaseItem>();
            return purchaseOrderItems;
        }

        private Item GetItem(int Id)
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetItems", new Dictionary<string, object>() { { "ItemId", Id } });
            var items = ds.Tables[0].ToList<Item>();
            return items.Count > 0 ? items[0] : null;
        }
        private Unit GetUnit(int Id)
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetUnits", new Dictionary<string, object>() { { "UnitId", Id } });
            var units = ds.Tables[0].ToList<Unit>();
            return units.Count > 0 ? units[0] : null;
        }
        private Batch GetBatch(int Id)
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetBatches", new Dictionary<string, object>() { { "BatchId", Id } });
            var batches = ds.Tables[0].ToList<Batch>();
            return batches.Count > 0 ? batches[0] : null;
        }
        //Delete
        //public override PurchaseOrder Get(int Id)
        //{
        //    DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetPurchaseOrders", new Dictionary<string, object>() { { "PurchaseOrderId", Id } });
        //    var purchaseOrders = ds.Tables[0].ToList<PurchaseOrder>();
        //    var purchaseOrder = purchaseOrders.Count > 0 ? purchaseOrders[0] : null;

        //    return purchaseOrder;
        //}
        public override PurchaseOrder Get(int Id)
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetPurchaseOrders", new Dictionary<string, object>() { { "PurchaseOrderId", Id } });
            var purchaseOrders = ds.Tables[0].ToList<PurchaseOrder>();
            var purchaseOrder = purchaseOrders.Count > 0 ? purchaseOrders[0] : null;
            if (purchaseOrder != null)
            {
                var purchaseItems = GetPurchaseOrderItems(purchaseOrder.Id);
                purchaseItems.ForEach(x =>
                {
                    x.Item = GetItem(x.ItemId);
                    x.Unit = GetUnit(x.UnitId);
                    x.Batch = GetBatch(x.BatchId);
                    x.ItemName = x.Item.Name;
                    x.BatchName = x.Batch.Name;
                });
                purchaseOrder.PurchaseItems = purchaseItems;
            }
            return purchaseOrder;
        }

        public override async Task<bool> DeleteAsync(int Id)
        {
            try
            {
                var purchaseOrder = await _context.PurchaseOrders.FindAsync(Id);
                if (purchaseOrder == null)
                {
                    return false;
                }
                purchaseOrder.IsDelete = true;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbException e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }
        public List<PurchaseOrder> GetPurchaseOrderFromPurchaseOrderItem(int? BranchId = null)
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetPurchaseOrderFromPurchaseOrderItem", new Dictionary<string, object>() {
                { "BranchId", BranchId }
            });
            var PurchaseOrder = ds.Tables[0].ToList<PurchaseOrder>();

            return PurchaseOrder;
        }
        //public async Task UpdatePurchaseOrderItem(int purchaseOrderId, int purchaseId, int purchaseItemId)
        //{
        //    PurchaseItem purchaseOrderItem = new PurchaseItem();
        //    purchaseOrderItem = _context.PurchaseItems.Where(x => x.PurchaseOrderId == purchaseOrderId && x.ItemId == purchaseId).FirstOrDefault();
        //    if (purchaseOrderItem != null)
        //    {
        //        purchaseOrderItem.PurchaseId = purchaseId;
        //        await _context.SaveChangesAsync();
        //    }
        //}
    }
}
