using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MSIS_HMS.Core.Enums
{
    public enum ServiceChargesEnum
    {
        [Description("Urgent")]
        Urgent = 3000,
        [Description("Regular")]
        Regular = 1000,
        [Description("Home Type1")]
        HomeType1 = 25000,
        [Description ("Home Type2")]
        HomeType2= 10000,

    }
}
