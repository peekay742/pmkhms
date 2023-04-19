using MSIS_HMS.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static MSIS_HMS.Core.Entities.Attributes.CustomAttribute;

namespace MSIS_HMS.Core.Entities
{
    [Table("Collection")]
    public class Collection : BranchEntity
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal UnitPrice { get; set; }

    }
}
