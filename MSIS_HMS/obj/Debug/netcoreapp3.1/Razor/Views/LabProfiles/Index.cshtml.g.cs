#pragma checksum "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\LabProfiles\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "cd0a112aa37b9e8c9021c27394414998d80dfe10"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_LabProfiles_Index), @"mvc.1.0.view", @"/Views/LabProfiles/Index.cshtml")]
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
#line 1 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\LabProfiles\Index.cshtml"
using MSIS_HMS.Enums;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\LabProfiles\Index.cshtml"
using X.PagedList;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cd0a112aa37b9e8c9021c27394414998d80dfe10", @"/Views/LabProfiles/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4f8d343b1f1203f781b2225c85b76acba6afc043", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_LabProfiles_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IPagedList<MSIS_HMS.Core.Entities.LabProfile>>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "get", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "LabProfiles", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-xs btn-secondary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_Pagination", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 4 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\LabProfiles\Index.cshtml"
  
    ViewData["Title"] = "All Lab Profiles";
    var count = 1;
    var controller = "LabProfiles";
    var query = Context.Request.Query;
    var Name = query["Name"];


#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""col-xs-12 "">
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cd0a112aa37b9e8c9021c27394414998d80dfe107028", async() => {
                WriteLiteral(@"
                        <div class=""row"">
                            <div class=""col-md-3"">
                                <div class=""form-group"">
                                    <label class=""form-label"" for=""Name"">Name</label>
                                    <div class=""controls"">
                                        <input type=""text"" class=""form-control"" name=""Name""");
                BeginWriteAttribute("value", " value=\"", 1441, "\"", 1454, 1);
#nullable restore
#line 31 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\LabProfiles\Index.cshtml"
WriteAttributeValue("", 1449, Name, 1449, 5, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" id=""Name"">
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
#line 40 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\LabProfiles\Index.cshtml"
                                                                     Write(controller);

#line default
#line hidden
#nullable disable
                WriteLiteral("/index\" class=\"btn btn-click\">Cancel</button>\r\n                                    </div>\r\n                                </div>\r\n                            </div>\r\n                        </div>\r\n                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "action", 3, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 1028, "/", 1028, 1, true);
#nullable restore
#line 25 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\LabProfiles\Index.cshtml"
AddHtmlAttributeValue("", 1029, controller, 1029, 11, false);

#line default
#line hidden
#nullable disable
            AddHtmlAttributeValue("", 1040, "/Index", 1040, 6, true);
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

    <div class=""row"">
        <div class=""col-lg-12"">
            <section class=""box "">
                <header class=""panel_header"">
                    <h2 class=""title pull-left""></h2>
                    <div class=""actions panel_actions pull-right"">
                        <button data-href=""/labprofiles/create"" class=""btn btn-click btn-primary gradient-blue"">Create</button>
");
            WriteLiteral(@"                    </div>
                </header>
                <div class=""content-body"">
                    <div class=""row"">
                        <div class=""col-xs-12"">
                            
                            <table id=""example"" class=""display table table-hover table-condensed"">
                                <thead>
                                <tr>
                                    <th>No</th>
                                    <th>Name</th>
                                    <th>Description</th>
                                    <th></th>
                                </tr>
                                </thead>
                                <tbody>
");
#nullable restore
#line 75 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\LabProfiles\Index.cshtml"
                                 foreach (var item in Model)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <tr>\r\n                                        <td>");
#nullable restore
#line 78 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\LabProfiles\Index.cshtml"
                                        Write((((int)ViewBag.Page - 1) * (int)ViewBag.PageSize) + count);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td> \r\n                                        <td>");
#nullable restore
#line 79 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\LabProfiles\Index.cshtml"
                                       Write(item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                        <td>");
#nullable restore
#line 80 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\LabProfiles\Index.cshtml"
                                       Write(item.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td> \r\n                                        <td>");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cd0a112aa37b9e8c9021c27394414998d80dfe1013510", async() => {
                WriteLiteral("Edit");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-Id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 81 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\LabProfiles\Index.cshtml"
                                                                                                WriteLiteral(item.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["Id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-Id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["Id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("button", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cd0a112aa37b9e8c9021c27394414998d80dfe1016045", async() => {
                WriteLiteral("Delete");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-Id", "Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 82 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\LabProfiles\Index.cshtml"
                                                                                                       WriteLiteral(item.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.RouteValues["Id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-Id", __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.RouteValues["Id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "onclick", 3, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 4351, "DeleteConfirm(\'/LabProfiles/Delete/\',", 4351, 37, true);
#nullable restore
#line 83 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\LabProfiles\Index.cshtml"
AddHtmlAttributeValue("", 4388, item.Id, 4388, 8, false);

#line default
#line hidden
#nullable disable
            AddHtmlAttributeValue("", 4396, ")", 4396, 1, true);
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("</td>\r\n                                    </tr>\r\n");
#nullable restore
#line 85 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\LabProfiles\Index.cshtml"
                                    count++;
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                </tbody>\r\n                            </table>\r\n\r\n                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "cd0a112aa37b9e8c9021c27394414998d80dfe1019648", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
#nullable restore
#line 90 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\LabProfiles\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = Model;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("model", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#nullable restore
#line 90 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\LabProfiles\Index.cshtml"
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
            WriteLiteral("\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </section>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n     <script");
                BeginWriteAttribute("src", " src=\"", 4901, "\"", 4937, 1);
#nullable restore
#line 101 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\LabProfiles\Index.cshtml"
WriteAttributeValue("", 4907, Url.Content("~/js/common.js"), 4907, 30, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" type=\"text/javascript\"></script>\r\n        \r\n");
#nullable restore
#line 103 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\LabProfiles\Index.cshtml"
         if (TempData["notice"] != null)
        {
            switch ((int) TempData["notice"])
            {
                case (int)StatusEnum.NoticeStatus.Success :

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <script>SaveAlert(\'center\',\'success\',\'LabProfiles\');</script>\r\n");
#nullable restore
#line 109 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\LabProfiles\Index.cshtml"
                break;
                case (int)StatusEnum.NoticeStatus.Edit :

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <script>EditAlert(\'center\',\'success\',\'LabProfiles\');</script>\r\n");
#nullable restore
#line 112 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\LabProfiles\Index.cshtml"
                break;
                case (int)StatusEnum.NoticeStatus.Delete :

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <script>DeleteAlert(\'center\',\'success\',\'LabProfiles\');</script>\r\n");
#nullable restore
#line 115 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\LabProfiles\Index.cshtml"
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IPagedList<MSIS_HMS.Core.Entities.LabProfile>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
