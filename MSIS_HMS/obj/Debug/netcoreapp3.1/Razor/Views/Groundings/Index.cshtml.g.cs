#pragma checksum "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Groundings\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b8ee7302a670509f78e5f3a416d873e44b487d54"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Groundings_Index), @"mvc.1.0.view", @"/Views/Groundings/Index.cshtml")]
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
#line 1 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Groundings\Index.cshtml"
using MSIS_HMS.Enums;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Groundings\Index.cshtml"
using X.PagedList.Mvc.Core;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Groundings\Index.cshtml"
using X.PagedList;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Groundings\Index.cshtml"
using static MSIS_HMS.Helpers.FormatHelper;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b8ee7302a670509f78e5f3a416d873e44b487d54", @"/Views/Groundings/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4f8d343b1f1203f781b2225c85b76acba6afc043", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Groundings_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IPagedList<MSIS_HMS.Core.Entities.Grounding>>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "WarehouseId", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-control"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "get", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 6 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Groundings\Index.cshtml"
  
    var controller = "Groundings";
    ViewData["Title"] = "All " + controller;
    var count = 1;
    var query = Context.Request.Query;
    var WarehouseId = query["WarehouseId"];
    var FromDate = query["FromDate"];
    var ToDate = query["ToDate"];
    ViewData["Query"] = query;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<div class=""col-xs-12 "">
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b8ee7302a670509f78e5f3a416d873e44b487d546905", async() => {
                WriteLiteral(@"
                                <div class=""row"">
                                    <div class=""col-md-3"">
                                        <div class=""form-group"">
                                            <label class=""form-label"" for=""field-2"">Warehouses</label>
                                            <span class=""desc""></span>
                                            <div class=""controls"">
                                                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("select", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b8ee7302a670509f78e5f3a416d873e44b487d547645", async() => {
                    WriteLiteral("\r\n                                                    ");
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b8ee7302a670509f78e5f3a416d873e44b487d547965", async() => {
                        WriteLiteral("Please Select Parent Menu");
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                    __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_0.Value;
                    __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    WriteLiteral("\r\n                                                ");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Name = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
#nullable restore
#line 37 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Groundings\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items = ViewBag.Warehouses;

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-items", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
                WriteLiteral(@"                                            </div>
                                        </div>
                                    </div>


                                    <div class=""col-md-3"">
                                        <div class=""form-group"">
                                            <label class=""form-label"" for=""FromDate"">From Date</label>
                                            <div class=""controls"">
                                                <input type=""date"" class=""form-control"" name=""FromDate""");
                BeginWriteAttribute("value", " value=\"", 2775, "\"", 2792, 1);
#nullable restore
#line 50 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Groundings\Index.cshtml"
WriteAttributeValue("", 2783, FromDate, 2783, 9, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@">
                                            </div>
                                        </div>
                                    </div>
                                    <div class=""col-md-3"">
                                        <div class=""form-group"">
                                            <label class=""form-label"" for=""ToDate"">To Date</label>
                                            <div class=""controls"">
                                                <input type=""date"" class=""form-control"" name=""ToDate""");
                BeginWriteAttribute("value", " value=\"", 3335, "\"", 3350, 1);
#nullable restore
#line 58 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Groundings\Index.cshtml"
WriteAttributeValue("", 3343, ToDate, 3343, 7, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@">
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
#line 67 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Groundings\Index.cshtml"
                                                                             Write(controller);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"/index"" class=""btn btn-click"">Cancel</button>
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
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "action", 3, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 1371, "/", 1371, 1, true);
#nullable restore
#line 30 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Groundings\Index.cshtml"
AddHtmlAttributeValue("", 1372, controller, 1372, 11, false);

#line default
#line hidden
#nullable disable
            AddHtmlAttributeValue("", 1383, "/Index", 1383, 6, true);
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
#line 82 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Groundings\Index.cshtml"
                                       Write(controller);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"/create"" class=""btn btn-click btn-primary gradient-blue"">Create</button>
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
                                        <th>Warehouse</th>
                                        <th>Remark</th>
                                       
                                    </tr>
                                </thead>
                                <tbody>
");
#nullable restore
#line 100 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Groundings\Index.cshtml"
                                     foreach (var item in Model)
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <tr>\r\n                                            <td>");
#nullable restore
#line 103 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Groundings\Index.cshtml"
                                           Write(count);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                            <td>");
#nullable restore
#line 104 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Groundings\Index.cshtml"
                                           Write(item.Date.ToString("dd-MM-yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                            <td>");
#nullable restore
#line 105 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Groundings\Index.cshtml"
                                           Write(item.WarehouseName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                            <td>");
#nullable restore
#line 106 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Groundings\Index.cshtml"
                                           Write(item.Remark);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                            \r\n                                        </tr>\r\n");
#nullable restore
#line 109 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Groundings\Index.cshtml"
                                         if (item.GroundingItems != null)
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                            <tr>
                                                <th></th>
                                                <th>Item</th>
                                                <th>Previous Qty</th>
                                                <th>Changed Qty</th>
                                            </tr>
");
#nullable restore
#line 117 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Groundings\Index.cshtml"
                                        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 118 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Groundings\Index.cshtml"
                                         foreach (var groundingItems in item.GroundingItems)
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <tr>\r\n                                                <td></td>\r\n                                                <td>");
#nullable restore
#line 122 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Groundings\Index.cshtml"
                                               Write(groundingItems.Item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                                <td>");
#nullable restore
#line 123 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Groundings\Index.cshtml"
                                               Write(groundingItems.PreviouseQty);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                                <td>");
#nullable restore
#line 124 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Groundings\Index.cshtml"
                                               Write(groundingItems.ChangedQty);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n\r\n                                            </tr>\r\n");
#nullable restore
#line 127 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Groundings\Index.cshtml"
                                        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 127 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Groundings\Index.cshtml"
                                         
                                        count++;
                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                </tbody>\r\n                            </table>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </section>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n\r\n\r\n");
#nullable restore
#line 143 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Groundings\Index.cshtml"
     if (TempData["notice"] != null)
    {
        switch ((int)TempData["notice"])
        {
            case (int)StatusEnum.NoticeStatus.Success:

#line default
#line hidden
#nullable disable
                WriteLiteral("                <script>SaveAlert(\'center\', \'success\', \'Grounding\');</script>\r\n");
#nullable restore
#line 149 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Groundings\Index.cshtml"
                break;
            case (int)StatusEnum.NoticeStatus.Edit:

#line default
#line hidden
#nullable disable
                WriteLiteral("                <script>EditAlert(\'center\', \'success\', \'Grounding\');</script>\r\n");
#nullable restore
#line 152 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Groundings\Index.cshtml"
                break;
            case (int)StatusEnum.NoticeStatus.Delete:

#line default
#line hidden
#nullable disable
                WriteLiteral("                <script>DeleteAlert(\'center\', \'success\', \'Grounding\');</script>\r\n");
#nullable restore
#line 155 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Groundings\Index.cshtml"
                break;
        }

    }

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IPagedList<MSIS_HMS.Core.Entities.Grounding>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591