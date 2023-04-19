using MSIS_HMS.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MSIS_HMS.Core.Entities
{
    [Table("OTStaffType")]
    public class OTStaffType : Entity
    {
        public OTStaffType()
        {

        }
        public string Name { get; set; }
        public decimal Fee { get; set; }
    }
}
