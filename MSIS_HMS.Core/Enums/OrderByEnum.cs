                              
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace MSIS_HMS.Core.Enums
{
    public enum OrderByEnum:int
    {

        [Description("OPD")]
        [Display(Name = "OPD")]
        OPD = 1,
        [Description("IPD")]
        [Display(Name = "IPD")]
        IPD = 2,
        [Description("LetPadan")]
        [Display(Name = "LetPadan")]
        LetPadan = 3,
        [Description("Main")]
        [Display(Name = "Main")]
        Main = 4,

    }
}

