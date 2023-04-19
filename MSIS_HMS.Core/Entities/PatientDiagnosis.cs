using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static MSIS_HMS.Core.Entities.Attributes.CustomAttribute;

namespace MSIS_HMS.Core.Entities
{
    [Table("PatientDiagnosis")]
    public class PatientDiagnosis
    {
        public PatientDiagnosis()
        {
        }
        public int Id { get; set; }
        public int MedicalRecordId { get; set; }
        public int? DiagnosisId { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public string FromDate { get; set; }
        public int SortOrder { get; set; }

        //[NotMapped]
        //public string SymptomDesc { get; set; }

        [SkipProperty]
        public MedicalRecord MedicalRecord { get; set; }
        [SkipProperty]
        public Diagnosis Diagnosis { get; set; }

    }
}
