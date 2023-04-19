using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MSIS_HMS.Core.Entities.Base;
using static MSIS_HMS.Core.Entities.Attributes.CustomAttribute;


namespace MSIS_HMS.Core.Entities
{
    [Table(name:"ServiceType")]
    public class ServiceType : BranchEntity
    {
        public ServiceType()
        {
        }
        
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}