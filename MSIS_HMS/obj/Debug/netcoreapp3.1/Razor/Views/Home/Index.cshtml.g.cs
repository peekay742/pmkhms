#pragma checksum "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "95e715070cd5116f05d2956f2fa1790abd9f8d9d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"95e715070cd5116f05d2956f2fa1790abd9f8d9d", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4f8d343b1f1203f781b2225c85b76acba6afc043", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Home\Index.cshtml"
  
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
                        <i class=""pull-left ico-icon icon-md icon-primary mt-10"">
                            <img src=""../data/icons/laborder.png"" class=""ico-icon-o""");
            BeginWriteAttribute("alt", " alt=\"", 477, "\"", 483, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                        </i>\r\n                        <div class=\"stats\">\r\n                            <h3 class=\"mb-5\">");
#nullable restore
#line 16 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Home\Index.cshtml"
                                        Write(ViewBag.LabOrder);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h3>
                            <span>Total Lab Order </span>
                        </div>
                    </div>
                </div>
                <div class=""col-lg-3 col-sm-6 col-xs-12"">
                    <div class=""r4_counter db_box"">
                        <i class=""pull-left ico-icon icon-md icon-primary mt-10"">
                            <img src=""../data/icons/money.png"" class=""ico-icon-o""");
            BeginWriteAttribute("alt", " alt=\"", 1050, "\"", 1056, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                        </i>\r\n                        <div class=\"stats\">\r\n                            <h3 class=\"mb-5\">");
#nullable restore
#line 27 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Home\Index.cshtml"
                                        Write(ViewBag.LabOrderAmt);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" MMK</h3>
                            <span>Lab Order Amount For Today</span>
                        </div>
                    </div>
                </div>
                <div class=""col-lg-3 col-sm-6 col-xs-12"">
                    <div class=""r4_counter db_box"">
                        <i class=""pull-left ico-icon icon-md icon-primary mt-10"">
                            <img src=""../data/icons/money.png"" class=""ico-icon-o""");
            BeginWriteAttribute("alt", " alt=\"", 1640, "\"", 1646, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                        </i>\r\n                        <div class=\"stats\">\r\n                            <h3 class=\"mb-5\">");
#nullable restore
#line 38 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Home\Index.cshtml"
                                        Write(ViewBag.OrderAmt);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" MMK</h3>
                            <span>Pharmacy Amount For Today</span>
                        </div>
                    </div>
                </div>
                <div class=""col-lg-3 col-sm-6 col-xs-12"">
                    <div class=""r4_counter db_box"">
                        <i class=""pull-left ico-icon icon-md icon-primary mt-10"">
                            <img src=""../data/icons/purchase.png"" class=""ico-icon-o""");
            BeginWriteAttribute("alt", " alt=\"", 2229, "\"", 2235, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                        </i>\r\n                        <div class=\"stats\">\r\n                            <h3 class=\"mb-5\">");
#nullable restore
#line 49 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Home\Index.cshtml"
                                        Write(ViewBag.PurchaseAmt);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" MMK</h3>
                            <span>Purchase Amount For Today</span>
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
    <section class=""box"" style=""overflow:hidden"">
        <header class=""panel_header"">
            <h2 class=""title pull-left"">Hospital Visits Statistics</h2>
            <div class=""actions panel_actions pull-right"">
                <a class=""box_toggle fa fa-chevron-down""></a>
                <a class=""box_setting fa fa-cog"" data-toggle=""modal"" href=""#section-settings""></a>
                <a class=""box_close fa fa-times""></a>
            </div>
        </header>
        <div class=""content-body"">
            <div class=""row"">
                <div class=""col-xs-12"">
                    <div id=""demoarea-chart"">
                        <div id=""demoare");
            WriteLiteral(@"a-container"" style=""width: 100%;height:330px; text-align: center; margin:0 auto;""></div>
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
            BeginWriteAttribute("class", " class=\"", 4283, "\"", 4291, 0);
            EndWriteAttribute();
            WriteLiteral(@" style=""height:200px"" id=""browser_type""></div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</div>

<div class=""clearfix""></div>
<div class=""col-lg-8"">
    <section class=""box"">
        <header class=""panel_header"">
            <h2 class=""title pull-left"">Patient Queue</h2>
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
            WriteLiteral(@"                      <tr>
                                    <th>Patient</th>
                                    <th>Dr Name</th>
                                    <th>Visit Type</th>

                                </tr>
                            </thead>
                            <tbody>
");
#nullable restore
#line 134 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Home\Index.cshtml"
                                 foreach (var vp in ViewBag.VisitPatient)
                                {


#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                    <tr>
                                        <td>
                                            <div class=""round"">S</div>
                                            <div class=""designer-info"">
                                                <h6>");
#nullable restore
#line 141 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Home\Index.cshtml"
                                               Write(vp.PatientName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h6>\r\n                                                <small class=\"text-muted\">");
#nullable restore
#line 142 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Home\Index.cshtml"
                                                                     Write(vp.PatientGender);

#line default
#line hidden
#nullable disable
            WriteLiteral(", ");
#nullable restore
#line 142 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Home\Index.cshtml"
                                                                                        Write(vp.PatientAge);

#line default
#line hidden
#nullable disable
            WriteLiteral(" Years</small>\r\n                                            </div>\r\n                                        </td>\r\n                                        <td>");
#nullable restore
#line 145 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Home\Index.cshtml"
                                       Write(vp.DoctorName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                        <td>");
#nullable restore
#line 146 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Home\Index.cshtml"
                                       Write(vp.VisitTypeDesc);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n\r\n                                    </tr>\r\n");
#nullable restore
#line 149 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Home\Index.cshtml"

                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n                            </tbody>\r\n                        </table>\r\n                    </div>\r\n\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </section>\r\n");
            WriteLiteral(@"

</div>
<div class=""col-xs-12 col-md-6 col-lg-4"">
        <section class=""box "">
            <div class=""content-body p"">
                <div class=""row"">
                    <div class=""doctors-list relative"">
                        <div class=""doctors-head text-center"">
                            <h3 class=""header w-text relative bold"">Doctors List</h3>
");
            WriteLiteral("                        </div>\r\n");
#nullable restore
#line 229 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Home\Index.cshtml"
                         foreach (var doc in ViewBag.Doctors)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <div class=\"doctor-card has-shadow\">\r\n                                <div class=\"doc-info-wrap\">\r\n                                    <div class=\"doctor-img\">\r\n                                        <img");
            BeginWriteAttribute("src", " src=", 10712, "", 10755, 1);
#nullable restore
#line 234 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Home\Index.cshtml"
WriteAttributeValue("", 10717, Url.Action("Images",Path= @doc.Image), 10717, 38, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("alt", " alt=\"", 10755, "\"", 10761, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                                    </div>\r\n                                    <div class=\"doc-info\">\r\n                                        <h4 class=\"bold\">");
#nullable restore
#line 237 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Home\Index.cshtml"
                                                    Write(doc.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n                                        <h5>");
#nullable restore
#line 238 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Home\Index.cshtml"
                                       Write(doc.SpecialityName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n\r\n                                    </div>\r\n                                </div>\r\n                            </div>\r\n");
#nullable restore
#line 261 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Home\Index.cshtml"
                                    
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

                        <div class=""form-group no-mb"">
                            <button type=""button"" class=""btn btn-primary btn-lg mt-20 gradient-blue"" style=""width:100%""> View all doctors</button>
                        </div>

                    </div>

                </div>
            </div>
        </section>
    </div>
<!--<div class=""col-xs-12 col-md-6 col-lg-4"">
    <section class=""box "">
        <header class=""panel_header"">
            <h2 class=""title pull-left"">Recent Visits</h2>
            <div class=""actions panel_actions pull-right"">
                <a class=""box_toggle fa fa-chevron-down""></a>
                <a class=""box_setting fa fa-cog"" data-toggle=""modal"" href=""#section-settings""></a>
                <a class=""box_close fa fa-times""></a>
            </div>
        </header>
        <div class=""content-body"">
            <div class=""row"">
                <div class=""col-xs-12"">
                    <ul class=""project-activity list-unstyled mb-0"">
       ");
            WriteLiteral(@"                 <li class=""activity-list warning"">
                            <div class=""detail-info"">
                                <div class=""doc-img-con pull-left mr-10"">
                                    <img src=""../data/hos-dash/doc1.jpg"" width=""80"" alt="""">
                                </div>
                                <div class=""visit-doc"">
                                    <p class=""message"">
                                        <span class=""text-info bold"">Dr : </span> Mutten Sultan
                                    </p>
                                    <small class=""text-muted"">
                                        Blood Check
                                    </small>
                                </div>
                                <div class=""visit-date pull-right"">
                                    <p class=""mb-0"">25 Nov</p>
                                </div>
                            </div>
                        </li>
            ");
            WriteLiteral(@"            <li class=""clearfix""></li>
                        <li class=""activity-list info"">
                            <div class=""detail-info"">
                                <div class=""doc-img-con pull-left mr-10"">
                                    <img src=""../data/hos-dash/doc2.jpg"" width=""80"" alt="""">
                                </div>
                                <div class=""visit-doc"">
                                    <p class=""message "">
                                        <span class=""text-info bold"">Dr : </span> Smith Watson
                                    </p>
                                    <small class=""text-muted"">
                                        Thryoid Test
                                    </small>
                                </div>
                                <div class=""visit-date pull-right"">
                                    <p class=""mb-0"">25 Nov</p>
                                </div>
                            </div>");
            WriteLiteral(@"
                        </li>
                        <li class=""activity-list success"">
                            <div class=""detail-info"">
                                <div class=""doc-img-con pull-left mr-10"">
                                    <img src=""../data/hos-dash/doc3.jpg"" width=""80"" alt="""">
                                </div>
                                <div class=""visit-doc"">
                                    <p class=""message "">
                                        <span class=""text-info bold"">Dr : </span> Sarah Mutten
                                    </p>
                                    <small class=""text-muted"">
                                        Full Blood image
                                    </small>
                                </div>
                                <div class=""visit-date pull-right"">
                                    <p class=""mb-0"">25 Nov</p>
                                </div>
                            </div>");
            WriteLiteral(@"
                        </li>
                        <li class=""activity-list danger"">
                            <div class=""detail-info pb0"">
                                <div class=""doc-img-con pull-left mr-10"">
                                    <img src=""../data/hos-dash/doc1.jpg"" width=""80"" alt="""">
                                </div>
                                <div class=""visit-doc"">
                                    <p class=""message "">
                                        <span class=""text-info bold"">Dr : </span> Morese Sharpe
                                    </p>
                                    <small class=""text-muted"">
                                        Full Body Test
                                    </small>
                                </div>
                                <div class=""visit-date pull-right"">
                                    <p class=""mb-0"">25 Nov</p>
                                </div>
                            </di");
            WriteLiteral(@"v>
                        </li>
                    </ul>
                </div>
            </div>-->
 <!-- End .row -->
<!--</div>
    </section>
</div>-->
<!--<div class=""col-xs-12 col-md-6 col-lg-4"">
    <section class=""box "">
        <header class=""panel_header"">
            <h2 class=""title pull-left"">Operation Success Rate</h2>
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
                        <div class="""" style=""height:200px"" id=""user_type""></div>
                    </div>
                </div>
            </div>-->
 <!-- End .row -->
<!--</div>
    </se");
            WriteLiteral("ction>\r\n</div>-->\r\n");
            WriteLiteral("\r\n<div class=\"clearfix\"></div>\r\n\r\n");
            WriteLiteral(@"
<!--<div class=""col-xs-12 col-md-6 col-lg-4"">
    <section class=""box"">
        <div class=""hos-promo"">
            <div class=""promo-info"">
                <h3 class=""w-text bold"">Get full free <br> medical Check up!</h3>
                <h4 class=""g-text bold"">29 Nov .... 20:00 PM</h4>
            </div>
        </div>
    </section>
    <section class=""box "">
        <header class=""panel_header"">
            <h2 class=""title pull-left"">Patients Notes</h2>
            <div class=""actions panel_actions pull-right"">
                <a class=""box_toggle fa fa-chevron-down""></a>
                <a class=""box_setting fa fa-cog"" data-toggle=""modal"" href=""#section-settings""></a>
                <a class=""box_close fa fa-times""></a>
            </div>
        </header>
        <div class=""content-body"">
            <div class=""row"">
                <div class=""col-xs-12"">
                    <ul class=""project-activity list-unstyled mb-0"">
                        <li class=""activity-list war");
            WriteLiteral(@"ning"">
                            <div class=""detail-info"">
                                <div class=""visit-doc"">
                                    <small class=""text-muted"">
                                        I feel better Now :)
                                    </small>
                                    <p class=""message"">
                                        Meditation
                                    </p>

                                </div>
                                <div class=""visit-date visit-stat pull-right"">
                                    <p class=""mb-0"">OPENED</p>
                                </div>
                            </div>
                        </li>
                        <li class=""clearfix""></li>
                        <li class=""activity-list info"">
                            <div class=""detail-info"">
                                <div class=""visit-doc"">
                                    <small class=""text-muted"">
   ");
            WriteLiteral(@"                                     Treatment was good!
                                    </small>
                                    <p class=""message"">
                                        Thyroid Test
                                    </p>
                                </div>
                                <div class=""visit-date visit-stat pull-right"">
                                    <p class=""mb-0 uppercase"">closed</p>
                                </div>
                            </div>
                        </li>
                        <li class=""activity-list success"">
                            <div class=""detail-info"">
                                <div class=""visit-doc"">
                                    <small class=""text-muted"">
                                        My hair is gone!
                                    </small>
                                    <p class=""message"">
                                        Unhappy
                    ");
            WriteLiteral(@"                </p>
                                </div>
                                <div class=""visit-date visit-stat pull-right"">
                                    <p class=""mb-0 uppercase"">OPENED</p>
                                </div>
                            </div>
                        </li>
                        <li class=""activity-list warning"">
                            <div class=""detail-info"">
                                <div class=""visit-doc"">
                                    <small class=""text-muted"">
                                        My hair is gone!
                                    </small>
                                    <p class=""message"">
                                        Unhappy
                                    </p>
                                </div>
                                <div class=""visit-date visit-stat pull-right"">
                                    <p class=""mb-0 uppercase"">closed</p>
                   ");
            WriteLiteral(@"             </div>
                            </div>
                        </li>
                        <li class=""activity-list danger"">
                            <div class=""detail-info pb0"">
                                <div class=""visit-doc"">
                                    <small class=""text-muted"">
                                        Great Mediacal Care
                                    </small>
                                    <p class=""message"">
                                        Join Pain
                                    </p>
                                </div>
                                <div class=""visit-date visit-stat pull-right"">
                                    <p class=""mb-0 uppercase"">OPENED</p>
                                </div>
                            </div>
                        </li>


                    </ul>
                </div>
            </div>-->
 <!-- End .row -->
<!--</div>
    </section>

</div>-->
");
            WriteLiteral("\r\n\r\n<div class=\"clearfix\"></div>\r\n\r\n<!-- MAIN CONTENT AREA ENDS -->");
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