using MSIS_HMS.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static MSIS_HMS.Core.Entities.Attributes.CustomAttribute;

namespace MSIS_HMS.Core.Entities
{
    [Table("IPDImaging")]
    public class IPDImaging:Entity
    {
        public decimal Amount { get; set; }
        public int SortOrder { get; set; }
        public int ImagingOrderId { get; set; }
        public int IPDRecordId { get; set; }
        public DateTime Date { get; set; }
        [SkipProperty]
        public ImagingOrder imagingOrder { get; set; }
        [SkipProperty]
        public IPDRecord iPDRecord { get; set; }
    }
}
