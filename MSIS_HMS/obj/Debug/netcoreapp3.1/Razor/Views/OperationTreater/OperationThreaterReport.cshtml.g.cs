#pragma checksum "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\OperationTreater\OperationThreaterReport.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4f712ee40d416d2af0f410c8231a62cd15ea91b2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_OperationTreater_OperationThreaterReport), @"mvc.1.0.view", @"/Views/OperationTreater/OperationThreaterReport.cshtml")]
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
#line 1 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\OperationTreater\OperationThreaterReport.cshtml"
using MSIS_HMS.Enums;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\OperationTreater\OperationThreaterReport.cshtml"
using X.PagedList.Mvc.Core;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\OperationTreater\OperationThreaterReport.cshtml"
using X.PagedList;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\OperationTreater\OperationThreaterReport.cshtml"
using static MSIS_HMS.Helpers.FormatHelper;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\OperationTreater\OperationThreaterReport.cshtml"
using static MSIS_HMS.Infrastructure.Helpers.DateTimeExtensions;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4f712ee40d416d2af0f410c8231a62cd15ea91b2", @"/Views/OperationTreater/OperationThreaterReport.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4f8d343b1f1203f781b2225c85b76acba6afc043", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_OperationTreater_OperationThreaterReport : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IPagedList<MSIS_HMS.Core.Entities.OperationTreater>>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "date", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-control"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "get", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_Pagination", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 7 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\OperationTreater\OperationThreaterReport.cshtml"
  
    var controller = "OperationTreater";
    ViewData["Title"] = "All " + "Operation Threater Reports";
    var count = 1;
    var query = Context.Request.Query;
    var FromDate = query["FromDate"];
    var ToDate = query["ToDate"];
    ViewData["Query"] = query;


#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""col-xs-12 "">
    <div class="" bg-w"">
        <div class=""col-lg-12"">
            <div class=""panel-group"" id=""accordion"" role=""tablist"" aria-multiselectable=""true"">
                <div class=""panel panel-default"">
                    <div class=""panel-heading"" role=""tab"" id=""headingOne1"">
                        <h4 class=""panel-title"">
                            <a data-toggle=""collapse"" data-parent=""#accordion"" href=""#collapseOne"" aria-expanded=""true"" aria-controls=""collapseOne"">
                                <i class='fa fa-search'></i> Search
                            </a>
                        </h4>
                    </div>
                    <div id=""collapseOne"" class=""panel-collapse collapse in"" role=""tabpanel"" aria-labelledby=""headingOne1"">
                        <div class=""panel-body"">
                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4f712ee40d416d2af0f410c8231a62cd15ea91b27384", async() => {
                WriteLiteral(@"
                                <div class=""row"">

                                    <div class=""col-md-3"">
                                        <div class=""form-group"">
                                            <label class=""form-label"" for=""FromDate"">From Date</label>
                                            <div class=""controls"">
                                                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "4f712ee40d416d2af0f410c8231a62cd15ea91b28052", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
#nullable restore
#line 39 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\OperationTreater\OperationThreaterReport.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => FromDate);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                BeginWriteTagHelperAttribute();
#nullable restore
#line 39 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\OperationTreater\OperationThreaterReport.cshtml"
                                                                                                       WriteLiteral(DateTime.Now.ToString("yyyy-MM-dd"));

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.Value = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
                                            </div>
                                        </div>
                                    </div>
                                    <div class=""col-md-3"">
                                        <div class=""form-group"">
                                            <label class=""form-label"" for=""ToDate"">To Date</label>
                                            <div class=""controls"">
                                                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "4f712ee40d416d2af0f410c8231a62cd15ea91b211142", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
#nullable restore
#line 47 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\OperationTreater\OperationThreaterReport.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => ToDate);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                BeginWriteTagHelperAttribute();
#nullable restore
#line 47 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\OperationTreater\OperationThreaterReport.cshtml"
                                                                                                     WriteLiteral(DateTime.Now.ToString("yyyy-MM-dd"));

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.Value = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class=""col-md-3"">
                                        <div class=""form-group"">
                                            <label class=""form-label"">&nbsp;</label>
                                            <div class=""controls"">
                                                <button type=""submit"" class=""btn btn-primary gradient-blue"">Search</button>
                                                <button type=""button"" data-href=""/");
#nullable restore
#line 57 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\OperationTreater\OperationThreaterReport.cshtml"
                                                                             Write(controller);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"/OperationThreaterReport"" class=""btn btn-click"">Cancel</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "action", 3, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 1428, "/", 1428, 1, true);
#nullable restore
#line 32 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\OperationTreater\OperationThreaterReport.cshtml"
AddHtmlAttributeValue("", 1429, controller, 1429, 11, false);

#line default
#line hidden
#nullable disable
            AddHtmlAttributeValue("", 1440, "/OperationThreaterReport", 1440, 24, true);
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                        </div>
                    </div>
                </div>
            </div>
            <section class=""box "">
                <header class=""panel_header"">
                    <h2 class=""title pull-left""></h2>
                    <div class=""actions panel_actions pull-right"">
                        <button data-href=""/");
#nullable restore
#line 72 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\OperationTreater\OperationThreaterReport.cshtml"
                                       Write(controller);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"/DownloadReport"" class=""btn btn-click btn-primary gradient-blue"">Download Report</button>
                    </div>
                </header>
                <div class=""content-body"">
                    <div class=""row"">
                        <div class=""col-xs-12"">

                            <table id=""example"" class=""display table table-hover table-condensed"">
                                <thead>
                                    <tr>
                                        <th>No</th>
                                        <th>Date</th>

                                        <th>Patient</th>
                                        <th>Operation Type</th>
                                        <th>Operation Room</th>
                                        <th>Chief Surgeon</th>
                                        <th>Total</th>
                                        
                                    </tr>
                                </thead>
                ");
            WriteLiteral("                <tbody>\r\n");
#nullable restore
#line 94 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\OperationTreater\OperationThreaterReport.cshtml"
                                     foreach (var item in Model)
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <tr>\r\n                                            <td>");
#nullable restore
#line 97 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\OperationTreater\OperationThreaterReport.cshtml"
                                            Write((((int)ViewBag.Page - 1) * (int)ViewBag.PageSize) + count);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                            <td>");
#nullable restore
#line 98 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\OperationTreater\OperationThreaterReport.cshtml"
                                           Write(item.OperationDate.ToDateString());

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n\r\n                                            <td>");
#nullable restore
#line 100 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\OperationTreater\OperationThreaterReport.cshtml"
                                           Write(item.PatientName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                            <td>");
#nullable restore
#line 101 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\OperationTreater\OperationThreaterReport.cshtml"
                                           Write(item.OperationTypeName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                            <td>");
#nullable restore
#line 102 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\OperationTreater\OperationThreaterReport.cshtml"
                                           Write(item.RoomNo);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                            <td>");
#nullable restore
#line 103 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\OperationTreater\OperationThreaterReport.cshtml"
                                           Write(item.DoctorName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                            <td>");
#nullable restore
#line 104 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\OperationTreater\OperationThreaterReport.cshtml"
                                           Write(item.Total.FormatMoney());

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                         \r\n                                            \r\n                                        </tr>\r\n");
#nullable restore
#line 108 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\OperationTreater\OperationThreaterReport.cshtml"
                                        count++;
                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                </tbody>\r\n                            </table>\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "4f712ee40d416d2af0f410c8231a62cd15ea91b221915", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
#nullable restore
#line 112 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\OperationTreater\OperationThreaterReport.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = Model;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("model", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#nullable restore
#line 112 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\OperationTreater\OperationThreaterReport.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.ViewData = ViewData;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("view-data", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.ViewData, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </section>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n \r\n");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IPagedList<MSIS_HMS.Core.Entities.OperationTreater>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
