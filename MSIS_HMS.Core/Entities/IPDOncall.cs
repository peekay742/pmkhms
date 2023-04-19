﻿using MSIS_HMS.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static MSIS_HMS.Core.Entities.Attributes.CustomAttribute;

namespace MSIS_HMS.Core.Entities
{
    [Table("IPDOncall")]
    public class IPDOncall:Entity
    {
        public IPDOncall()
        {

        }

        public int Id { get; set; }

        public DateTime Date { get; set; }
        public decimal Fee { get; set; }

        public int SortOrder { get; set; }
        public int DoctorId { get; set; }
        public int IPDRecordId { get; set; }

        [NotMapped]
        public string DoctorName { get; set; }
        
        [SkipProperty]
        public IPDRecord IPDRecord { get; set; }
        [SkipProperty]
        public Doctor Doctor { get; set; }
    }
}
