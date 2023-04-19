using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MSIS_HMS.Core.Enums
{
    public class MaterialStatusEnum {
        public enum MaterialStatus : int
        {
            [Description("Married")]
            [Display(Name = "Married")]
            Married = 1,
            [Description("Single")]
            [Display(Name = "Single")]
            Single = 2,
            [Description("Other")]
            [Display(Name = "Other")]
            Other = 3
        }
    }


}
