#pragma checksum "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Shared\Components\TopbarMenu\Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "697d4469233f9c28d5e63c47582448c4d7b8387b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Components_TopbarMenu_Default), @"mvc.1.0.view", @"/Views/Shared/Components/TopbarMenu/Default.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"697d4469233f9c28d5e63c47582448c4d7b8387b", @"/Views/Shared/Components/TopbarMenu/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4f8d343b1f1203f781b2225c85b76acba6afc043", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Shared_Components_TopbarMenu_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/data/icons/msislog.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString("login-icon"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("max-width:64px"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("action", new global::Microsoft.AspNetCore.Html.HtmlString("/Account/Logout"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("logoutform"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<div class=\'page-topbar gradient-blue1\'>\r\n    <div class=\'logo-area crypto\'>\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "697d4469233f9c28d5e63c47582448c4d7b8387b5673", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
    </div>
    <div class='quick-area'>
        <div class='pull-left'>
            <ul class=""info-menu left-links list-inline list-unstyled"">
                <li class=""sidebar-toggle-wrap"">
                    <a href=""#"" data-toggle=""sidebar"" class=""sidebar_toggle"">
                        <i class=""fa fa-bars""></i>
                    </a>
                </li>
            </ul>
        </div>
      
        <div class='pull-right'>

            <ul class=""info-menu right-links list-inline list-unstyled"">
                <li class=""notify-toggle-wrapper spec"">
                    <a href=""#"" data-toggle=""dropdown"" class=""toggle"">
                        <i class=""fa fa-bell""></i>
");
#nullable restore
#line 23 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Shared\Components\TopbarMenu\Default.cshtml"
                         if (ViewBag.Role == "Lab")
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <span class=\"badge badge-accent\">");
#nullable restore
#line 25 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Shared\Components\TopbarMenu\Default.cshtml"
                                                        Write(ViewBag.labOrderCount);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n");
#nullable restore
#line 26 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Shared\Components\TopbarMenu\Default.cshtml"
                        }
                        

#line default
#line hidden
#nullable disable
#nullable restore
#line 30 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Shared\Components\TopbarMenu\Default.cshtml"
                           
                        else
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <span class=\"badge badge-accent\">");
#nullable restore
#line 33 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Shared\Components\TopbarMenu\Default.cshtml"
                                                        Write(ViewBag.ipdPayemntCount);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n");
#nullable restore
#line 34 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Shared\Components\TopbarMenu\Default.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </a>\r\n                    <ul class=\"dropdown-menu notifications animated fadeIn\">\r\n                        <li class=\"total\">\r\n                            <span class=\"small\">\r\n");
#nullable restore
#line 40 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Shared\Components\TopbarMenu\Default.cshtml"
                                 if (ViewBag.Role == "Lab")
                                {
                                    

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <strong>LabOrders list</strong>\r\n");
#nullable restore
#line 44 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Shared\Components\TopbarMenu\Default.cshtml"
                                }
                               

#line default
#line hidden
#nullable disable
#nullable restore
#line 48 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Shared\Components\TopbarMenu\Default.cshtml"
                                   
                                else
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <strong>Alert patient list under 20% of payment</strong>\r\n");
#nullable restore
#line 52 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Shared\Components\TopbarMenu\Default.cshtml"

                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                            </span>\r\n                        </li>\r\n                        <li class=\"list\">\r\n\r\n                            <ul class=\"dropdown-menu-list list-unstyled ps-scrollbar\">\r\n");
#nullable restore
#line 60 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Shared\Components\TopbarMenu\Default.cshtml"
                                 if (ViewBag.Role == "Lab")
                                {
                                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 62 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Shared\Components\TopbarMenu\Default.cshtml"
                                     foreach (var labOrder in ViewBag.labOrders)
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <li class=\"unread available\">\r\n                                            <div>\r\n                                                <span class=\"name\">\r\n                                                    <strong>");
#nullable restore
#line 67 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Shared\Components\TopbarMenu\Default.cshtml"
                                                       Write(labOrder.VoucherNo);

#line default
#line hidden
#nullable disable
            WriteLiteral("</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\r\n                                                    <span>");
#nullable restore
#line 68 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Shared\Components\TopbarMenu\Default.cshtml"
                                                     Write(labOrder.PatientName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                                                </span>\r\n                                            </div>\r\n\r\n                                        </li>\r\n");
#nullable restore
#line 73 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Shared\Components\TopbarMenu\Default.cshtml"
                                    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 73 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Shared\Components\TopbarMenu\Default.cshtml"
                                     
                                }
                                

#line default
#line hidden
#nullable disable
#nullable restore
#line 89 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Shared\Components\TopbarMenu\Default.cshtml"
                                   
                               
                                else
                                {
                                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 93 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Shared\Components\TopbarMenu\Default.cshtml"
                                     foreach (var payment in ViewBag.ipdPayments)
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <li class=\"unread available\">\r\n                                            <div>\r\n                                                <span class=\"name\">\r\n                                                    <strong>");
#nullable restore
#line 98 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Shared\Components\TopbarMenu\Default.cshtml"
                                                       Write(payment.PatientName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;\r\n                                                    <span>");
#nullable restore
#line 99 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Shared\Components\TopbarMenu\Default.cshtml"
                                                     Write(payment.Amount);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                                                </span>\r\n                                            </div>\r\n\r\n                                        </li>\r\n");
#nullable restore
#line 104 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Shared\Components\TopbarMenu\Default.cshtml"
                                    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 104 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Shared\Components\TopbarMenu\Default.cshtml"
                                     
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n\r\n\r\n                            </ul>\r\n\r\n                        </li>\r\n\r\n");
            WriteLiteral("                    </ul>\r\n                </li>\r\n                <li class=\"profile\">\r\n                    <a href=\"#\" data-toggle=\"dropdown\" class=\"toggle\">\r\n");
            WriteLiteral("                        <span");
            BeginWriteAttribute("value", " value=\"", 5886, "\"", 5894, 0);
            EndWriteAttribute();
            WriteLiteral("> ");
#nullable restore
#line 124 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Shared\Components\TopbarMenu\Default.cshtml"
                                   Write(ViewBag.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" <i class=""fa fa-angle-down""></i></span>
                    </a>
                    <ul class=""dropdown-menu profile animated fadeIn"">
                        <li>
                            <a href=""crypto-account-setting.html"">
                                <i class=""fa fa-wrench""></i> Settings
                            </a>
                        </li>
                        <li>
                            <a href=""crypto-profile.html"">
                                <i class=""fa fa-user""></i> Profile
                            </a>
                        </li>
                        <li>
                            <a href=""crypto-faq.html"">
                                <i class=""fa fa-info""></i> Help
                            </a>
                        </li>
                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "697d4469233f9c28d5e63c47582448c4d7b8387b17347", async() => {
                WriteLiteral(@"
                        <li>
                            <a href=""javascript:{}"" onclick=""document.getElementById('logoutform').submit();"" style=""padding:3px 20px;"">
                                <i class=""fa fa-lock""></i> Logout
                            </a>
                        </li>
                        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                    </ul>\r\n                </li>\r\n            </ul>\r\n        </div>\r\n    </div>\r\n</div>");
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