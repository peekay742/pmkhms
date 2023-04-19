using MSIS_HMS.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static MSIS_HMS.Core.Entities.Attributes.CustomAttribute;

namespace MSIS_HMS.Core.Entities
{
    [Table("ImagingOrder")]
    public class ImagingOrder:BranchEntity
    {
        public ImagingOrder()
        {
        }

        public DateTime Date { get; set; }
        [Required]
        public string VoucherNo { get; set; }
        public int PatientId { get; set; }
        public decimal Tax { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public decimal Balance { get; set; }
        public bool IsPaid { get; set; }
        public DateTime? PaidDate { get; set; }
        public string Remark { get; set; }

        [NotMapped]
        public string PatientName { get; set; }
        [NotMapped]
        public string PatientGuardian { get; set; }
        [NotMapped]
        public string BranchName { get; set; }
        [NotMapped]
        public string BranchAddress { get; set; }
        [NotMapped]
        public string BranchPhone { get; set; }
        [NotMapped]
        public bool isCompleteResult { get; set; }   
        [SkipProperty]
        public Patient Patient { get; set; }
        [SkipProperty]
        public ICollection<ImagingOrderTest> ImagingOrderTests { get; set; }
        [SkipProperty]
        public ICollection<IPDImaging> IPDImagings { get; set; }
    }
}
