using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static MSIS_HMS.Core.Entities.Attributes.CustomAttribute;

namespace MSIS_HMS.Core.Entities
{
    [Table("PatientVital")]
    public class PatientVital
    {
        public PatientVital()
        {

        }
        public int Id { get; set; }
        public int MedicalRecordId { get; set; }

        public string GCS { get; set; }

        public string BPSystolic { get; set; }

        public string BPDiastolic { get; set; }

        public string Temperature { get; set; }
        public string Pulse { get; set; }
        public string SPO2 { get; set; }
        public string RespiratoryRate { get; set; }
        public string Weight { get; set; }
        public string Height { get; set; }


        [SkipProperty]
        public MedicalRecord MedicalRecord { get; set; }
    }
}
