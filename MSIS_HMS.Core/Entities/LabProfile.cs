using MSIS_HMS.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static MSIS_HMS.Core.Entities.Attributes.CustomAttribute;

namespace MSIS_HMS.Core.Entities
{
    [Table("LabProfile")]
    public class LabProfile : BranchEntity
    {
        public LabProfile()
        {

        }

        [Required]
        public String Name { get; set; }

        public String Description { get; set; }

        [SkipProperty]
        public ICollection<LabTest> LabTests { get; set; }
    }
}
