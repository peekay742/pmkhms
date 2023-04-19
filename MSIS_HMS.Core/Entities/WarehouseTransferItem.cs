﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using static MSIS_HMS.Core.Entities.Attributes.CustomAttribute;

namespace MSIS_HMS.Core.Entities
{
    [Table("WarehouseTransferItem")]
    public class WarehouseTransferItem
    {
        public WarehouseTransferItem()
        {
        }

        public int Id { get; set; }
        public int WarehouseTransferId { get; set; }
        public int ItemId { get; set; }
        public int BatchId { get; set; }
        public int UnitId { get; set; }
        public int Qty { get; set; }
        public int QtyInSmallestUnit { get; set; }
        public int SortOrder { get; set; }

        [NotMapped]
        [SkipProperty]
        public string ItemName { get; set; }
        [NotMapped]
        [SkipProperty]
        public string BatchName { get; set; }
        [NotMapped]
        [SkipProperty]
        public string UnitName { get; set; }

        [SkipProperty]
        public WarehouseTransfer WarehouseTransfer { get; set; }
        [SkipProperty]
        public Item Item { get; set; }
        [SkipProperty]
        public Batch Batch { get; set; }
        [SkipProperty]
        public Unit Unit { get; set; }
    }
}
