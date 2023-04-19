using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MSIS_HMS.Core.Enums;
using static MSIS_HMS.Core.Entities.Attributes.CustomAttribute;
using static MSIS_HMS.Core.Enums.EnumFeeType;

namespace MSIS_HMS.Core.Entities
{
    [Table("LabOrderTest")]
    public class LabOrderTest
    {
        public LabOrderTest()
        {
        }

        public int Id { get; set; }
        public int LabOrderId { get; set; }
        public int LabTestId { get; set; }
        public int? CollectionGroupId { get; set; }
        public int? ReferrerId { get; set; }
        public FeeTypeEnum FeeType { get; set; }
        public decimal Fee { get; set; }
        public decimal ReferralFee { get; set; } // Calculated with Test charges, Fee Type and Fee
        public decimal UnitPrice { get; set; }
        public int Qty { get; set; }
        public int? LabResultId { get; set; }
        public int SortOrder { get; set; }
        public int? CollectionId { get; set; }
        public int CollectionQty { get; set; }
        public decimal CollectionFee { get; set; }
        [NotMapped]
        public string CollectionName { get; set; }
        [SkipProperty]
        public Collection Collection { get; set; }
        [NotMapped]
        public string ReferrerName { get; set; }
        
        [NotMapped]
        public string LabTestName { get; set; }

        [NotMapped]
        public string CollectionGroupName { get; set; }
        [SkipProperty]
        public LabOrder LabOrder { get; set; }
        [SkipProperty]
        public LabTest LabTest { get; set; }
        [SkipProperty]
        public CollectionGroup CollectionGroup { get; set; }
        [SkipProperty]
        public LabResult LabResult { get; set; }
        [SkipProperty]
        public Referrer Referrer { get; set; }
    }
}
