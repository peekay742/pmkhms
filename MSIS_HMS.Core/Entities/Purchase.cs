﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MSIS_HMS.Core.Entities.Base;
using static MSIS_HMS.Core.Entities.Attributes.CustomAttribute;

namespace MSIS_HMS.Core.Entities
{
    [Table("Purchase")]
    public class Purchase : BranchEntity
    {
        public Purchase()
        {
        }

        public string VoucherNo { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Supplier { get; set; } 
        public int WarehouseId { get; set; } 
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public string Remark { get; set; }

        //[SkipProperty]
        //[NotMapped]
        //public int PurchaseOrderId { get; set; }

        [NotMapped]
        public string WarehouseName { get; set; }
       
        [SkipProperty]
        public Warehouse Warehouse { get; set; }

        [SkipProperty]
        public ICollection<PurchaseItem> PurchaseItems { get; set; }
    }
}
