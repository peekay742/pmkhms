#pragma checksum "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\ItemTypes\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "303ff4ae6d21b29f1b38f6c0200df5d6f7e71bba"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ItemTypes_Index), @"mvc.1.0.view", @"/Views/ItemTypes/Index.cshtml")]
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
#line 1 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\ItemTypes\Index.cshtml"
using MSIS_HMS.Enums;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\ItemTypes\Index.cshtml"
using X.PagedList;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"303ff4ae6d21b29f1b38f6c0200df5d6f7e71bba", @"/Views/ItemTypes/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4f8d343b1f1203f781b2225c85b76acba6afc043", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_ItemTypes_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IPagedList<MSIS_HMS.Core.Entities.ItemType>>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "get", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-xs btn-secondary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_Pagination", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 4 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\ItemTypes\Index.cshtml"
  
    var controller = "ItemTypes";
    ViewData["Title"] = "All "+controller;
    var count = 1;
    var query = Context.Request.Query;
    var ItemTypeName = query["Itemtypename"];
    ViewData["Query"] = query;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    
<div class=""col-xs-12 "">
    <div class="" bg-w"">
        <div class=""col-lg-12"">
            <div class=""panel-group"" id=""accordion"" role=""tablist"" aria -multiselectable=""true"">
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "303ff4ae6d21b29f1b38f6c0200df5d6f7e71bba6906", async() => {
                WriteLiteral(@"
                                <div class=""row"">
                                    <div class=""col-md-3"">
                                        <div class=""form-group"">
                                            <label class=""form-label"" for=""Name"">Name</label>
                                            <div class=""controls"">
                                                <input type=""text"" class=""form-control"" name=""Itemtypename""");
                BeginWriteAttribute("value", " value=\"", 1697, "\"", 1718, 1);
#nullable restore
#line 33 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\ItemTypes\Index.cshtml"
WriteAttributeValue("", 1705, ItemTypeName, 1705, 13, false);

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
#line 42 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\ItemTypes\Index.cshtml"
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
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "action", 3, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 1228, "/", 1228, 1, true);
#nullable restore
#line 27 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\ItemTypes\Index.cshtml"
AddHtmlAttributeValue("", 1229, controller, 1229, 11, false);

#line default
#line hidden
#nullable disable
            AddHtmlAttributeValue("", 1240, "/Index", 1240, 6, true);
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
#line 56 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\ItemTypes\Index.cshtml"
                                       Write(controller);

#line default
#line hidden
#nullable disable
            WriteLiteral("/create\" class=\"btn btn-click btn-primary gradient-blue\">Create</button>\r\n                        <button data-href=\"/");
#nullable restore
#line 57 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\ItemTypes\Index.cshtml"
                                       Write(controller);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"/import"" class=""btn btn-click btn-primary gradient-blue"">Excel Import</button>
                    </div>
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
                                    <th>Branch</th>
                                    <th></th>
                                </tr>
                                </thead>
                                <tbody>
");
#nullable restore
#line 75 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\ItemTypes\Index.cshtml"
                                 foreach (var item in Model)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <tr>\r\n                                        <td>");
#nullable restore
#line 78 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\ItemTypes\Index.cshtml"
                                        Write((((int)ViewBag.Page - 1) * (int)ViewBag.PageSize) + count);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                        <td>");
#nullable restore
#line 79 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\ItemTypes\Index.cshtml"
                                       Write(item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                        <td>");
#nullable restore
#line 80 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\ItemTypes\Index.cshtml"
                                       Write(item.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                        <td>");
#nullable restore
#line 81 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\ItemTypes\Index.cshtml"
                                       Write(item.Branch.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                        <td>");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "303ff4ae6d21b29f1b38f6c0200df5d6f7e71bba14427", async() => {
                WriteLiteral("Edit");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 82 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\ItemTypes\Index.cshtml"
                                                   WriteLiteral(controller);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-controller", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-Id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 82 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\ItemTypes\Index.cshtml"
                                                                                                WriteLiteral(item.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["Id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-Id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["Id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("button", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "303ff4ae6d21b29f1b38f6c0200df5d6f7e71bba17449", async() => {
                WriteLiteral("Delete");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 83 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\ItemTypes\Index.cshtml"
                                                        WriteLiteral(controller);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.Controller = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-controller", __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.Controller, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-Id", "Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 83 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\ItemTypes\Index.cshtml"
                                                                                                       WriteLiteral(item.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.RouteValues["Id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-Id", __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.RouteValues["Id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "onclick", 5, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 4792, "DeleteConfirm(\'/", 4792, 16, true);
#nullable restore
#line 84 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\ItemTypes\Index.cshtml"
AddHtmlAttributeValue("", 4808, controller, 4808, 11, false);

#line default
#line hidden
#nullable disable
            AddHtmlAttributeValue("", 4819, "/Delete/\',", 4819, 10, true);
#nullable restore
#line 84 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\ItemTypes\Index.cshtml"
AddHtmlAttributeValue("", 4829, item.Id, 4829, 8, false);

#line default
#line hidden
#nullable disable
            AddHtmlAttributeValue("", 4837, ")", 4837, 1, true);
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
#line 86 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\ItemTypes\Index.cshtml"
                                    count++;
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                </tbody>\r\n                            </table>\r\n                             ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "303ff4ae6d21b29f1b38f6c0200df5d6f7e71bba21815", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
#nullable restore
#line 90 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\ItemTypes\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model = Model;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("model", __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Model, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#nullable restore
#line 90 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\ItemTypes\Index.cshtml"
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
                WriteLiteral("\r\n     \r\n        \r\n");
#nullable restore
#line 102 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\ItemTypes\Index.cshtml"
         if (TempData["notice"] != null)
        {
            switch ((int) TempData["notice"])
            {
                case (int)StatusEnum.NoticeStatus.Success :

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <script>SaveAlert(\'center\',\'success\',\'ItemType\');</script>\r\n");
#nullable restore
#line 108 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\ItemTypes\Index.cshtml"
                break;
                case (int)StatusEnum.NoticeStatus.Edit :

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <script>EditAlert(\'center\',\'success\',\'ItemType\');</script>\r\n");
#nullable restore
#line 111 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\ItemTypes\Index.cshtml"
                break;
                case (int)StatusEnum.NoticeStatus.Delete :

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <script>DeleteAlert(\'center\',\'success\',\'ItemType\');</script>\r\n");
#nullable restore
#line 114 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\ItemTypes\Index.cshtml"
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IPagedList<MSIS_HMS.Core.Entities.ItemType>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
