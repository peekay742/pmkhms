using System;
using System.ComponentModel;

namespace MSIS_HMS.Core.Enums
{
    public class DepartmentTypeEnum
    {

        public enum EnumDepartmentType : int
        {
            [Description("Out Patient Department")]
            OPD = 1,
            [Description("In Patient Department")]
            IPD = 2,
            [Description("Operation Theatre")]
            OT = 3
        }
    }

}