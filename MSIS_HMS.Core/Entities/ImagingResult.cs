using MSIS_HMS.Core.Entities.Base;
using MSIS_HMS.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static MSIS_HMS.Core.Entities.Attributes.CustomAttribute;
using static MSIS_HMS.Core.Enums.EnumFeeType;

namespace MSIS_HMS.Core.Entities
{
    [Table("ImagingResult")]
    public class ImagingResult: BranchEntity
    {
        public ImagingResult()
        {
        }

        public DateTime Date { get; set; }
        [Required]
        public string ResultNo { get; set; }
        public int PatientId { get; set; }
        public int LabTestId { get; set; }
        public decimal UnitPrice { get; set; }

        public int? TechnicianId { get; set; }
        public decimal TechnicianFee { get; set; }
        public FeeTypeEnum TechnicianFeeType { get; set; }

        public int? ConsultantId { get; set; }
        public decimal ConsultantFee { get; set; }
        public FeeTypeEnum ConsultantFeeType { get; set; }

        public bool IsCompleted { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string Remark { get; set; }


        [NotMapped]
        public string PatientName { get; set; }
        [NotMapped]
        [SkipProperty]
        public string PatientGender { get; set; }
        [NotMapped]
        [SkipProperty]
        public string DateString { get; set; }
        [NotMapped]

        public int PatientSex { get; set; }
        [NotMapped]

        public int PatientAge { get; set; }
        [NotMapped]
        public string PatientGuardian { get; set; }
        [NotMapped]
        public string BranchName { get; set; }
        [NotMapped]
        public string BranchAddress { get; set; }
        [NotMapped]
        public string BranchPhone { get; set; }
        [NotMapped]
        public string TechnicianName { get; set; }
        [NotMapped]
        public string ConsultantName { get; set; }
        [NotMapped]
        public string LabTestName { get; set; }
        [NotMapped]
        [SkipProperty]
        public string TechanicianFeeTypeStr { get; set; }
        [NotMapped]
        [SkipProperty]
        public string ConsultantFeeTypeStr { get; set; }
        [NotMapped]
        [SkipProperty]
        public decimal TechanicianAmount { get; set; }
        [NotMapped]
        [SkipProperty]
        public decimal ConsultantAmount { get; set; }
        [SkipProperty]
        public LabTest LabTest { get; set; }
        [SkipProperty]
        public Patient Patient { get; set; }
        [SkipProperty]
        public LabPerson Technician { get; set; }
        [SkipProperty]
        public LabPerson Consultant { get; set; }
        [SkipProperty]
        [NotMapped]
        public int ImagingOrderId { get; set; }

        [SkipProperty]
        public ICollection<ImagingResultDetail> ImagingResultDetails { get; set; }
    }
}
