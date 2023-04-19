using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MSIS_HMS.Core.Enums
{
    public class EnumFeeType
    {
        public enum FeeTypeEnum
        {
            [Description("Percentage")]
            [Display(Name = "Percentage")]
            Percentage = 1,
            [Description("Fixed Amount")]
            [Display(Name = "Fixed Amount")]
            FixedAmount = 2
        }
    }
}
