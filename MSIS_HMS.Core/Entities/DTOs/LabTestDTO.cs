using System;
using System.Collections.Generic;
using System.Text;

namespace MSIS_HMS.Core.Entities.DTOs
{
    public class LabTestDTO
    {
        public int LabOrderId { get; set; }
        public string LabTestName { get; set; }
        public string CollectionGroupName { get; set; }
    }
}
