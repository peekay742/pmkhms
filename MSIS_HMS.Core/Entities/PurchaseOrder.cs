
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MSIS_HMS.Core.Entities.Base;
using MSIS_HMS.Core.Enums;
using static MSIS_HMS.Core.Entities.Attributes.CustomAttribute;


namespace MSIS_HMS.Core.Entities
{
    [Table("PurchaseOrder")]
    public class PurchaseOrder : BranchEntity
    {

        public PurchaseOrder()
        {

        }

        public string PurchaseOrderNO { get; set; }
        public DateTime PurchaseOrderDate { get; set; }
        public string Supplier { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public string Remark { get; set; }

        [NotMapped]
        public string BranchName { get; set; }
        [NotMapped]
        public string BranchAddress { get; set; }

        [SkipProperty]
        public ICollection<PurchaseItem> PurchaseItems { get; set; }

    }
}
