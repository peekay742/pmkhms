#pragma checksum "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Dashboard\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "763a7a5acb49703913efeb15854fe11ef27d6e00"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Dashboard_Index), @"mvc.1.0.view", @"/Views/Dashboard/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\_ViewImports.cshtml"
using MSIS_HMS;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\_ViewImports.cshtml"
using MSIS_HMS.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Dashboard\Index.cshtml"
using MSIS_HMS.Core.Entities;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"763a7a5acb49703913efeb15854fe11ef27d6e00", @"/Views/Dashboard/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4f8d343b1f1203f781b2225c85b76acba6afc043", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Dashboard_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Dashboard\Index.cshtml"
  
    ViewData["Title"] = "Dashboard";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""dashboard col-lg-12"">
    <section class=""box nobox marginBottom0"">
        <div class=""content-body"">
            <div class=""row"">

                <div class=""col-lg-3 col-sm-6 col-xs-12"">
                    <div class=""r4_counter db_box"">
");
            WriteLiteral("\r\n                        <div class=\"stats1\">\r\n                            <h3 class=\"mb-5\">\r\n                                ");
#nullable restore
#line 17 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Dashboard\Index.cshtml"
                           Write(ViewBag.DailyIncome);

#line default
#line hidden
#nullable disable
            WriteLiteral("    <small>MMK</small>\r\n                                <i class=\"pull-right ico-icon icon-md icon-primary mt-10\">\r\n                                    <img src=\"../data/icons/AdminDashboard/color/daily-income.svg\" class=\"ico-icon-o\"");
            BeginWriteAttribute("alt", " alt=\"", 776, "\"", 782, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                                </i>
                            </h3>

                            <span>Daily Income</span>

                        </div>

                    </div>
                </div>
                <div class=""col-lg-3 col-sm-6 col-xs-12"">
                    <div class=""r4_counter db_box"">

                        <div class=""stats1"">
                            <h3 class=""mb-5"">
                                ");
#nullable restore
#line 34 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Dashboard\Index.cshtml"
                           Write(ViewBag.MonthlyIncome);

#line default
#line hidden
#nullable disable
            WriteLiteral(" <small>MMK</small>\r\n                                <i class=\"pull-right ico-icon icon-md icon-primary mt-10\">\r\n                                    <img src=\"../data/icons/AdminDashboard/color/monthly-income.svg\" class=\"ico-icon-o\"");
            BeginWriteAttribute("alt", " alt=\"", 1497, "\"", 1503, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                                </i>
                            </h3>
                            <span>Monthly Income</span>
                        </div>
                    </div>
                </div>
                <div class=""col-lg-3 col-sm-6 col-xs-12"">
                    <div class=""r4_counter db_box"">

                        <div class=""stats1"">
                            <h3 class=""mb-5"">
                                ");
#nullable restore
#line 48 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Dashboard\Index.cshtml"
                           Write(ViewBag.TotalPatient);

#line default
#line hidden
#nullable disable
            WriteLiteral("  <small></small>\r\n                                <i class=\"pull-right ico-icon icon-md icon-primary mt-10\">\r\n                                    <img src=\"../data/icons/AdminDashboard/color/total-patient.svg\" class=\"ico-icon-o\"");
            BeginWriteAttribute("alt", " alt=\"", 2210, "\"", 2216, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                                </i>
                            </h3>
                            <span>Total Patients</span>
                        </div>
                    </div>
                </div>
                <div class=""col-lg-3 col-sm-6 col-xs-12"">
                    <div class=""r4_counter db_box"">

                        <div class=""stats1"">
                            <h3 class=""mb-5"">
                                ");
#nullable restore
#line 62 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Dashboard\Index.cshtml"
                           Write(ViewBag.TodayOrder);

#line default
#line hidden
#nullable disable
            WriteLiteral(" <small>Orders</small>\r\n                                <i class=\"pull-right ico-icon icon-md icon-primary mt-10\">\r\n                                    <img src=\"../data/icons/AdminDashboard/color/order.svg\" class=\"ico-icon-o\"");
            BeginWriteAttribute("alt", " alt=\"", 2918, "\"", 2924, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                                </i>
                            </h3>
                            <span>In Pharmacy Today</span>
                        </div>
                    </div>
                </div>
                <div class=""col-lg-3 col-sm-6 col-xs-12"">
                    <div class=""r4_counter db_box"">

                        <div class=""stats1"">
                            <h3 class=""mb-5"">
                                ");
#nullable restore
#line 76 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Dashboard\Index.cshtml"
                           Write(ViewBag.TodayVisit);

#line default
#line hidden
#nullable disable
            WriteLiteral(" <small>Visits</small>\r\n                                <i class=\"pull-right ico-icon icon-md icon-primary mt-10\">\r\n                                    <img src=\"../data/icons/AdminDashboard/color/visit.svg\" class=\"ico-icon-o\"");
            BeginWriteAttribute("alt", " alt=\"", 3629, "\"", 3635, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                                </i>
                            </h3>
                            <span>In Out Patient Today</span>
                        </div>
                    </div>
                </div>
                <div class=""col-lg-3 col-sm-6 col-xs-12"">
                    <div class=""r4_counter db_box"">

                        <div class=""stats1"">
                            <h3 class=""mb-5"">
                                ");
#nullable restore
#line 90 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Dashboard\Index.cshtml"
                           Write(ViewBag.TodayAdmitted);

#line default
#line hidden
#nullable disable
            WriteLiteral(" <small>Patients</small>\r\n                                <i class=\"pull-right ico-icon icon-md icon-primary mt-10\">\r\n                                    <img src=\"../data/icons/AdminDashboard/color/admit-patient.svg\" class=\"ico-icon-o\"");
            BeginWriteAttribute("alt", " alt=\"", 4356, "\"", 4362, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                                </i>
                            </h3>
                            <span>Admitted Today</span>
                        </div>
                    </div>
                </div>
                <div class=""col-lg-3 col-sm-6 col-xs-12"">
                    <div class=""r4_counter db_box"">

                        <div class=""stats1"">
                            <h3 class=""mb-5"">
                                ");
#nullable restore
#line 104 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Dashboard\Index.cshtml"
                           Write(ViewBag.TodayDischarged);

#line default
#line hidden
#nullable disable
            WriteLiteral(" <small>Patients</small>\r\n                                <i class=\"pull-right ico-icon icon-md icon-primary mt-10\">\r\n                                    <img src=\"../data/icons/AdminDashboard/color/discharge-patient.svg\" class=\"ico-icon-o\"");
            BeginWriteAttribute("alt", " alt=\"", 5083, "\"", 5089, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                                </i>
                            </h3>
                            <span>Discharged Today</span>
                        </div>
                    </div>
                </div>
                <div class=""col-lg-3 col-sm-6 col-xs-12"">
                    <div class=""r4_counter db_box"">

                        <div class=""stats1"">
                            <h3 class=""mb-5"">
                                ");
#nullable restore
#line 118 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Dashboard\Index.cshtml"
                           Write(ViewBag.TodayOT);

#line default
#line hidden
#nullable disable
            WriteLiteral(" <small></small>\r\n                                <i class=\"pull-right ico-icon icon-md icon-primary mt-10\">\r\n                                    <img src=\"../data/icons/AdminDashboard/color/operation.svg\" class=\"ico-icon-o\"");
            BeginWriteAttribute("alt", " alt=\"", 5788, "\"", 5794, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                                </i>
                            </h3>
                            <span>Today Operations</span>
                        </div>
                    </div>
                </div>
            </div>
            <!-- End .row -->
        </div>
    </section>
</div>

<div class=""clearfix""></div>
<!-- MAIN CONTENT AREA STARTS -->

<div class=""col-lg-8"">
    <section class=""box"">
        <header class=""panel_header"">
            <h2 class=""title pull-left"">statistics <small>(");
#nullable restore
#line 139 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Dashboard\Index.cshtml"
                                                      Write(ViewBag.CurrentMonth);

#line default
#line hidden
#nullable disable
            WriteLiteral(@")</small></h2>
            <div class=""actions panel_actions pull-right"">
                <a class=""box_toggle fa fa-chevron-down""></a>
                <a class=""box_setting fa fa-cog"" data-toggle=""modal"" href=""#section-settings""></a>
                <a class=""box_close fa fa-times""></a>
            </div>
        </header>
        <div class=""content-body"">
            <div class=""row"">
                <div class=""col-xs-12"">

                    <div class=""table-responsive"" data-pattern=""priority-columns"">
                        <table id=""tech-companies-1"" class=""table vm table-small-font no-mb table-bordered table-striped"">
                            <thead>
                                <tr>
                                    <th>No.</th>
                                    <th>Module</th>
                                    <th class=""money"">Daily Income</th>
                                    <th class=""money"">Monthly Income</th>
                                </tr>
        ");
            WriteLiteral("                    </thead>\r\n                            <tbody>\r\n\r\n");
#nullable restore
#line 162 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Dashboard\Index.cshtml"
                                 foreach (var amt in ViewBag.DailyAndMonthly)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <tr>\r\n                                        <td>");
#nullable restore
#line 165 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Dashboard\Index.cshtml"
                                       Write(amt.No);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                        <td>");
#nullable restore
#line 166 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Dashboard\Index.cshtml"
                                       Write(amt.Module);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                        <td class=\"money\">");
#nullable restore
#line 167 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Dashboard\Index.cshtml"
                                                     Write(amt.DailyIncome);

#line default
#line hidden
#nullable disable
            WriteLiteral(" MMK</td>\r\n                                        <td class=\"money\">");
#nullable restore
#line 168 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Dashboard\Index.cshtml"
                                                     Write(amt.MonthlyIncome);

#line default
#line hidden
#nullable disable
            WriteLiteral(" MMK</td>\r\n                                    </tr>\r\n");
#nullable restore
#line 170 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Dashboard\Index.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

                            </tbody>
                            <tfoot>
                                <tr>
                                    <td colspan=""2"" class=""money""><b>Total</b></td>
                                    <td class=""money""><b>");
#nullable restore
#line 177 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Dashboard\Index.cshtml"
                                                    Write(ViewBag.TotalDailyAmt);

#line default
#line hidden
#nullable disable
            WriteLiteral(" MMK</b></td>\r\n                                    <td class=\"money\"><b>");
#nullable restore
#line 178 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Dashboard\Index.cshtml"
                                                    Write(ViewBag.TotalMonthlyAmt);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" MMK</b></td>
                                </tr>
                            </tfoot>
                        </table>
                    </div>

                </div>
            </div>
        </div>
    </section>
</div>

<div class=""col-xs-12 col-md-6 col-lg-4"">
    <section class=""box "">
        <header class=""panel_header"">
            <h2 class=""title pull-left"">Monthly Income</h2>
            <div class=""actions panel_actions pull-right"">
                <a class=""box_toggle fa fa-chevron-down""></a>
                <a class=""box_setting fa fa-cog"" data-toggle=""modal"" href=""#section-settings""></a>
                <a class=""box_close fa fa-times""></a>
            </div>
        </header>
        <div class=""content-body"">
            <div class=""row"">
                <div class=""col-xs-12"">
                    <div class=""chart-container"">
                        <div");
            BeginWriteAttribute("class", " class=\"", 9257, "\"", 9265, 0);
            EndWriteAttribute();
            WriteLiteral(" style=\"height:200px\" id=\"browser_type_admin\"></div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </section>\r\n</div>\r\n\r\n<div class=\"clearfix\"></div>\r\n\r\n<!-- MAIN CONTENT AREA ENDS -->");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
