#pragma checksum "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Dashboard\Pharmacy.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7dfa8d55cde1f16e7f991ad234cf80e966349e0e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Dashboard_Pharmacy), @"mvc.1.0.view", @"/Views/Dashboard/Pharmacy.cshtml")]
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
#line 1 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Dashboard\Pharmacy.cshtml"
using MSIS_HMS.Core.Entities;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7dfa8d55cde1f16e7f991ad234cf80e966349e0e", @"/Views/Dashboard/Pharmacy.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4f8d343b1f1203f781b2225c85b76acba6afc043", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Dashboard_Pharmacy : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Dashboard\Pharmacy.cshtml"
  
    ViewData["Title"] = "Pharmacy Dashboard";
    int i = 0;

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

                        <div class=""stats1"">
                            <h3 class=""mb-5"">
                                ");
#nullable restore
#line 17 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Dashboard\Pharmacy.cshtml"
                           Write(ViewBag.DailyOrderIncome);

#line default
#line hidden
#nullable disable
            WriteLiteral(" <small>MMK</small>\r\n                                <i class=\"pull-right ico-icon icon-md icon-primary mt-10\">\r\n                                    <img src=\"../data/icons/PharmacyDashboard/color/daily-income.svg\" class=\"ico-icon-o\"");
            BeginWriteAttribute("alt", " alt=\"", 751, "\"", 757, 0);
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
#line 31 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Dashboard\Pharmacy.cshtml"
                           Write(ViewBag.MonthlyOrderIncome);

#line default
#line hidden
#nullable disable
            WriteLiteral(" <small>MMK</small>\r\n                                <i class=\"pull-right ico-icon icon-md icon-primary mt-10\">\r\n                                    <img src=\"../data/icons/PharmacyDashboard/color/monthly-income.svg\" class=\"ico-icon-o\"");
            BeginWriteAttribute("alt", " alt=\"", 1474, "\"", 1480, 0);
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
#line 45 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Dashboard\Pharmacy.cshtml"
                           Write(ViewBag.TodayOrder);

#line default
#line hidden
#nullable disable
            WriteLiteral("  <small></small>\r\n                                <i class=\"pull-right ico-icon icon-md icon-primary mt-10\">\r\n                                    <img src=\"../data/icons/PharmacyDashboard/color/order.svg\" class=\"ico-icon-o\"");
            BeginWriteAttribute("alt", " alt=\"", 2180, "\"", 2186, 0);
            EndWriteAttribute();
            WriteLiteral(@" style=""margin-top:-11px;"">
                                </i>
                            </h3>
                            <span>Today Orders</span>
                        </div>
                    </div>
                </div>
                <div class=""col-lg-3 col-sm-6 col-xs-12"">
                    <div class=""r4_counter db_box"">

                        <div class=""stats1"">
                            <h3 class=""mb-5"">
                                ");
#nullable restore
#line 59 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Dashboard\Pharmacy.cshtml"
                           Write(ViewBag.TotalOutlet);

#line default
#line hidden
#nullable disable
            WriteLiteral(" <small></small>\r\n                                <i class=\"pull-right ico-icon icon-md icon-primary mt-10\">\r\n                                    <img src=\"../data/icons/PharmacyDashboard/color/outlet.svg\" class=\"ico-icon-o\"");
            BeginWriteAttribute("alt", " alt=\"", 2911, "\"", 2917, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                                </i>
                            </h3>
                            <span>Outlets</span>
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
            <h2 class=""title pull-left"">Outlet Wise</h2>
            <div class=""actions panel_actions pull-right"">
                <a class=""box_toggle fa fa-chevron-down""></a>
                <a class=""box_setting fa fa-cog"" data-toggle=""modal"" href=""#section-settings""></a>
                <a class=""box_close fa fa-times""></a>
            </div>
        </header>
        <div class=""content-body"">
            <div class=""row"">
                <div class=""col-xs-12"">

                    <div class=""table-responsive"" data-pattern=""priority-column");
            WriteLiteral(@"s"">
                        <table id=""tech-companies-1"" class=""table vm table-small-font no-mb table-bordered table-striped"">
                            <thead>
                                <tr>
                                    <th>No.</th>
                                    <th>Outlet</th>
                                    <th class=""text-center"">Today's Orders</th>
                                    <th class=""money"">Daily Income</th>
                                    <th class=""money"">Monthly Income</th>
                                </tr>
                            </thead>
                            <tbody>
");
#nullable restore
#line 103 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Dashboard\Pharmacy.cshtml"
                                 foreach (var amt in ViewBag.OutletIncome)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <tr>\r\n                                        <td>");
#nullable restore
#line 106 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Dashboard\Pharmacy.cshtml"
                                       Write(amt.No);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                        <td>");
#nullable restore
#line 107 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Dashboard\Pharmacy.cshtml"
                                       Write(amt.OutletName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                        <td class=\"text-center\">");
#nullable restore
#line 108 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Dashboard\Pharmacy.cshtml"
                                                           Write(amt.DailyOrder);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                        <td class=\"money\">");
#nullable restore
#line 109 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Dashboard\Pharmacy.cshtml"
                                                     Write(amt.DailyIncome);

#line default
#line hidden
#nullable disable
            WriteLiteral(" MMK</td>\r\n                                        <td class=\"money\">");
#nullable restore
#line 110 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Dashboard\Pharmacy.cshtml"
                                                     Write(amt.MonthlyIncome);

#line default
#line hidden
#nullable disable
            WriteLiteral(" MMK</td>\r\n                                    </tr>\r\n");
#nullable restore
#line 112 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Dashboard\Pharmacy.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

                            </tbody>
                        </table>
                    </div>

                </div>
            </div>
        </div>
    </section>

    <section class=""box"">
        <header class=""panel_header"">
            <h2 class=""title pull-left"">Best Selling Items <small>(This Month)</small></h2>
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
         ");
            WriteLiteral(@"                       <tr>
                                    <th>No.</th>
                                    <th>Item</th>
                                    <th>Qty</th>
                                    <th class=""money"">Amount</th>
                                </tr>
                            </thead>
                            <tbody>

");
#nullable restore
#line 149 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Dashboard\Pharmacy.cshtml"
                                 foreach (var amt in ViewBag.OutletItemFormonth)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <tr>\r\n                                        <td>");
#nullable restore
#line 152 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Dashboard\Pharmacy.cshtml"
                                       Write(amt.No);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                        <td>");
#nullable restore
#line 153 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Dashboard\Pharmacy.cshtml"
                                       Write(amt.ItemName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                        <td>");
#nullable restore
#line 154 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Dashboard\Pharmacy.cshtml"
                                       Write(amt.QtyString);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                        <td class=\"money\">");
#nullable restore
#line 155 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Dashboard\Pharmacy.cshtml"
                                                     Write(amt.Amount);

#line default
#line hidden
#nullable disable
            WriteLiteral(" MMK</td>\r\n                                    </tr>\r\n");
#nullable restore
#line 157 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Dashboard\Pharmacy.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                            </tbody>
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
            <h2 class=""title pull-left"">Pharmacy Orders</h2>
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
            BeginWriteAttribute("class", " class=\"", 7962, "\"", 7970, 0);
            EndWriteAttribute();
            WriteLiteral(" style=\"height:200px\" id=\"browser_type\"></div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </section>\r\n</div>\r\n\r\n<div class=\"clearfix\"></div>\r\n\r\n<!-- MAIN CONTENT AREA ENDS -->");
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