using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MSIS_HMS.Core.Enums
{
    public enum OTTypeEnum : int
    {
        [Description("Major")]
        [Display(Name = "Major")]
        Major = 1, 
        [Description("Minor")]
        [Display(Name = "Minor")]
        Minor = 2,
        [Description("SpecialMajor")]
        [Display(Name = "SpecialMajor")]
        SpecialMajor = 3,
        [Description("SpecialMinor")]
        [Display(Name = "SpecialMinor")]
        SpecialMinor = 4
    }
}
