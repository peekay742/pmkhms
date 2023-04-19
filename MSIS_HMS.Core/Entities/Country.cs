﻿using MSIS_HMS.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static MSIS_HMS.Core.Entities.Attributes.CustomAttribute;

namespace MSIS_HMS.Core.Entities
{
    [Table("Country")]
    public class Country : Entity
    {
        public Country()
        {

        }
        public string Name { get; set; }
        public string Code { get; set; }
        [SkipProperty]
        public ICollection<State> Sates { get; set; }
    }
}
