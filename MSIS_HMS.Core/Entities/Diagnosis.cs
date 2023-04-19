using MSIS_HMS.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static MSIS_HMS.Core.Entities.Attributes.CustomAttribute;

namespace MSIS_HMS.Core.Entities
{
    [Table("Diagnosis")]
    public class Diagnosis:Entity
    {
        public Diagnosis()
        {
        }
        public string Name { get; set; }
        public string Description { get; set; }

        [SkipProperty]
        public ICollection<PatientDiagnosis> PatientDiagnoses { get; set; }

        [SkipProperty]
        public ICollection<OperationTreater> OperationTreaters { get; set; }

    }
}
