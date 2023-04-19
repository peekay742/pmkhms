﻿using MSIS_HMS.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static MSIS_HMS.Core.Entities.Attributes.CustomAttribute;

namespace MSIS_HMS.Core.Entities
{
    [Table("BedType")]
   public class BedType:Entity
    {
        public string Name { get; set; }
        [SkipProperty]
        public ICollection<Bed> Beds { get; set; }
    }
}
