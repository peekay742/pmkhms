using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MSIS_HMS.Core.Enums
{
    public enum OTOrderStatusEnum
    {
        [Description("Booked")]
        Booked = 1,

        [Description("Cancelled")]
        Cancelled = 2
    }
}
