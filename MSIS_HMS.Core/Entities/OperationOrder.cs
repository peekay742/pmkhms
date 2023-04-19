using MSIS_HMS.Core.Entities.Base;
using MSIS_HMS.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static MSIS_HMS.Core.Entities.Attributes.CustomAttribute;
using static MSIS_HMS.Core.Enums.GenderEnum;

namespace MSIS_HMS.Core.Entities
{
    [Table("OperationOrder")]
    public class OperationOrder : BranchEntity
    {
        public OperationOrder()
        {

        }

        public string OrderNo { get; set; }

        public int PatientId { get; set; }

        public int ChiefSurgeonDoctorId { get; set; }

        public int OperationRoomId { get; set; }

        public int OpeartionTypeId { get; set; }

        public int Cancelled { get; set; }

        public OTOrderStatusEnum Status { get; set; }

        public string ReasonForCancellation { get; set; }

        public DateTime OTDate { get; set; }

        public DateTime AdmitDate { get; set; }


        [NotMapped]
        public string PatientName { get; set; }

        [NotMapped]
        public string DoctorName { get; set; }

        [NotMapped]
        public string OperationTypeName { get; set; }

        [NotMapped]
        public string RoomNo { get; set; }

        [SkipProperty]
        public Patient Patient { get; set; }
        [SkipProperty]
        public Doctor Doctor { get; set; }

        [SkipProperty]
        public OperationRoom OperationRoom { get; set; }
        


    }
}
