using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MSIS_HMS.Core.Entities.Base;
using MSIS_HMS.Core.Enums;
using static MSIS_HMS.Core.Entities.Attributes.CustomAttribute;

namespace MSIS_HMS.Core.Entities
{
    [Table("Book")]
    public class Book : BranchEntity
    {
        public Book()
        {
        }
        [Required]
        public string BookNo { get; set; }
        public DateTime Date { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int Cancelled { get; set; }
        public BookStatusEnum Status { get; set; }

        [Required]
        [NotMapped]
        public string PatientName { get; set; }
        [NotMapped]
        public string DoctorName { get; set; }

        [NotMapped]
        [SkipProperty]
        public string StatusDesc { get; set; }

        [SkipProperty]
        public Patient Patient { get; set; }
        [SkipProperty]
        public Doctor Doctor { get; set; }

    }
}

