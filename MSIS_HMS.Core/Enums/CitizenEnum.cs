using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MSIS_HMS.Core.Enums
{
    public class CitizenEnum
    {
        public enum Citizen : int
        {
            [Description("Myanmar")]
            Myanmar = 1 ,
            [Description("Foreigner")]
            Foreigner = 2
        }
    }
}
