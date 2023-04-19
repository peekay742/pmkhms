using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MSIS_HMS.Core.Enums
{
    public enum AdmissionTypeEnum
    {
        [Description("SOPD Admission")]
        SOPDAdmission = 1,
        [Description("Emergency")]
        Emergency = 2,
        

    }
}
