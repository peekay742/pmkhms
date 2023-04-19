using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static MSIS_HMS.Core.Entities.Attributes.CustomAttribute;

namespace MSIS_HMS.Core.Entities
{
    [Table("PatientNursingNote")]
    public class PatientNursingNote
    {
        public PatientNursingNote()
        {

        }
        public int Id { get; set; }
        public int MedicalRecordId { get; set; }

        public string RoutineComplain { get; set; }

        public string NursingAction { get; set; }

        public string FromDate { get; set; }

        public int sortOrder { get; set; }


        [SkipProperty]
        public MedicalRecord MedicalRecord { get; set; }
    }
}
