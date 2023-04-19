using System;
using MSIS_HMS.Core.Entities.Base;
using System.Collections.Generic;
using System.Text;
using static MSIS_HMS.Core.Entities.Attributes.CustomAttribute;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MSIS_HMS.Core.Enums;

namespace MSIS_HMS.Core.Entities
{
    [Table("IPDRecord")]
    public class IPDRecord : BranchEntity
    {

        public DateTime DOA { get; set; }
        public DateTime? DODC { get; set; }
        public int PatientId { get; set; }
        [Required]
        public string VoucherNo { get; set; }
        [Required]
        public PaymentTypeEnum PaymentType { get; set; }
        [Required]
        public AdmissionTypeEnum AdmissionType { get; set; }
        public string Status { get; set; }
        public int? RoomId { get; set; }
        public int? BedId { get; set; }
        public decimal Tax { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public decimal Balance { get; set; }
        public bool IsPaid { get; set; }
        public DateTime? PaidDate { get; set; }
        public int DepartmentId { get; set; }
        public int? FloorId { get; set; }
        public int? DischargeTypeId { get; set; }
        public string Remark { get; set; }
        public string DiseaseName { get; set;}
        public string DiseaseSummary { get; set; }  
        public string PhotographicExaminationAnswer { get; set; }
        public string MedicalTreatment { get; set; }
        public string? ResasonofDODC { get; set; }

        public string MedicalOfficer { get; set; }

        public string service { get; set; }

        public string AdmittedFor { get; set; }
        public IPDStatusEnum IPDStatusEnum { get; set; }


        [NotMapped]
        [SkipProperty]
        public string Image { get; set; }
        [NotMapped]
        [SkipProperty]
        public string SelectedDate { get; set; }
        [NotMapped]
        [SkipProperty]
        public int? OutletId { get; set; }
        [NotMapped]
        
        public string RoomNo { get; set; }
        [NotMapped]
     
        public string BedNo { get; set; }
        [NotMapped]
        
        public string PatientName { get; set; }
        [NotMapped]
       
        public string DepartmentName { get; set; }
        [NotMapped]
        public string FloorName { get; set; }
        [SkipProperty]
        public DischargeType DischargeType { get; set; }
        [NotMapped]
        [SkipProperty]
        public string WardName { get; set; }
        
        [SkipProperty]
        public Patient Patient { get; set; }
        [SkipProperty]
        public Room Room { get; set; }
        [SkipProperty]
        public Department Department { get; set; }
        [SkipProperty]
        public Floor Floor { get; set; }
        [SkipProperty]
        public Bed Bed { get; set; }
        [SkipProperty]
        [NotMapped]
        public TimeSpan CheckInTime { get; set; }
        [SkipProperty]
        [NotMapped]
        public TimeSpan CheckOutTime { get; set; }
        [SkipProperty]
        public ICollection<IPDOrderItem> IPDOrderItems { get; set; }
        [SkipProperty]
        public ICollection<IPDOrderService> IPDOrderServices { get; set; }
        [SkipProperty]
        public ICollection<IPDRound> IPDRounds { get; set; }
        [SkipProperty]
        public ICollection<IPDStaff> IPDStaffs { get; set; }
        [SkipProperty]
        public ICollection<IPDFood> IPDFoods { get; set; }
        [SkipProperty]
        public ICollection<IPDAllotment> IPDAllotments { get; set; }
        [SkipProperty]
        public ICollection<IPDPayment> IPDPayments { get; set; }       
        [SkipProperty]
        public ICollection<IPDLab> IPDLabs { get; set; }
        [SkipProperty]
        public ICollection<IPDImaging> IPDImagings { get; set; }


    }
}
