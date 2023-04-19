using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MSIS_HMS.Core.Enums
{
    public enum ItemCategoryEnum : int 
    {
        [Description("OT Item")]
        [Display(Name = "OT Item")]
        OTItem = 1 ,

        [Description("Ward Item")]
        [Display(Name = "Ward Item")]
        WardItem = 2,

        [Description("Aneasthetist Item")]
        [Display(Name = "Aneasthetist Item")]
        Aneasthetist = 3 
    }
}
