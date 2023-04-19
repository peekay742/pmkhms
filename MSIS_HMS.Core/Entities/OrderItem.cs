﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using static MSIS_HMS.Core.Entities.Attributes.CustomAttribute;

namespace MSIS_HMS.Core.Entities
{
    [Table("OrderItem")]
    public class OrderItem
    {
        public OrderItem()
        {
        }

        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int UnitId { get; set; }

        public decimal UnitPrice { get; set; }
        public int Qty { get; set; }
        public int QtyInSmallestUnit { get; set; }
        public bool IsFOC { get; set; } // FOC
        public int SortOrder { get; set; }

        [NotMapped]
        public string UnitName { get; set; }
        [NotMapped]
        public string ItemName { get; set; }
        [NotMapped]
        public string ShortForm { get; set; }

        [SkipProperty]
        public Order Order { get; set; }
        [SkipProperty]
        public Item Item { get; set; }
        [SkipProperty]
        public Unit Unit { get; set; }
    }
}
