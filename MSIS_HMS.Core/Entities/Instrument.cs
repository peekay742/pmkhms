using MSIS_HMS.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MSIS_HMS.Core.Entities
{
    [Table(name: "Instrument")]
    public class Instrument : BranchEntity
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Fee { get; set; }

    }
}
