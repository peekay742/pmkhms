using MSIS_HMS.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static MSIS_HMS.Core.Entities.Attributes.CustomAttribute;
using static MSIS_HMS.Core.Enums.EnumFeeType;

namespace MSIS_HMS.Core.Entities
{
    [Table("ImagingOrderTest")]
    public class ImagingOrderTest
    {
        public ImagingOrderTest()
        {
        }

        public int Id { get; set; }
        public int ImagingOrderId { get; set; }
        public int LabTestId { get; set; }
        public int? ReferrerId { get; set; }
        public FeeTypeEnum FeeType { get; set; }
        public decimal Fee { get; set; }
        public decimal ReferralFee { get; set; } // Calculated with Test charges, Fee Type and Fee
        public decimal UnitPrice { get; set; }
        public int Qty { get; set; }
        public int? ImagingResultId { get; set; }
        public int SortOrder { get; set; }

        [NotMapped]
        public string ReferrerName { get; set; }
        [NotMapped]
        public string LabTestName { get; set; }

        [SkipProperty]
        public ImagingOrder ImagingOrder { get; set; }
        [SkipProperty]
        public LabTest LabTest { get; set; }
        [SkipProperty]
        public ImagingResult ImagingResult { get; set; }
        [SkipProperty]
        public Referrer Referrer { get; set; }
    }
}
