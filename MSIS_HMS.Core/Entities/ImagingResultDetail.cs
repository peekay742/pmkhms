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
    [Table("ImagingResultDetail")]
    public class ImagingResultDetail
    {
        public ImagingResultDetail()
        {
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int ImagingResultId { get; set; }
        public string FileName { get; set; }
        public string AttachmentPath { get; set; }
        public bool IsTitle { get; set; }
        public bool IsPrinted { get; set; }
        public string Remark { get; set; }
        public int SortOrder { get; set; }
        public int LabTestId { get; set; }
        public int? TechnicianId { get; set; }
        public decimal TechnicianFee { get; set; }
        public FeeTypeEnum TechnicianFeeType { get; set; }

        public int? ConsultantId { get; set; }
        public decimal ConsultantFee { get; set; }
        public FeeTypeEnum ConsultantFeeType { get; set; }
        [NotMapped]
        public string LabTestName { get; set; }
        [NotMapped]
        public string TechnicianName { get; set; }
        [NotMapped]
        public string ConsultantName { get; set; }

        [NotMapped]
        public string ImagingResultNo { get; set; }

        [SkipProperty]
        public ImagingResult ImagingResult { get; set; }
        [SkipProperty]
        public LabTest LabTest { get; set; }
        [SkipProperty]
        public LabPerson Technician { get; set; }
        [SkipProperty]
        public LabPerson Consultant { get; set; }
    }
}
