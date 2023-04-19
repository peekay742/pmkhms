using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MSIS_HMS.Core.Enums
{
    public enum BookStatusEnum
    {
        [Description("Booked")]
        Booked = 1,
        //[Description("InProcess")]
        //InProcess = 2,
        [Description("Visit")]
        Visited = 3,
        [Description("Cancel")]
        Cancel = 4
    }
}
