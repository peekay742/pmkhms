#pragma checksum "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Visits\VisitDetail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "048ba5359129a3b2025c08d778e5c6e621193162"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Visits_VisitDetail), @"mvc.1.0.view", @"/Views/Visits/VisitDetail.cshtml")]
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
#line 1 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Visits\VisitDetail.cshtml"
using MSIS_HMS.Enums;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"048ba5359129a3b2025c08d778e5c6e621193162", @"/Views/Visits/VisitDetail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4f8d343b1f1203f781b2225c85b76acba6afc043", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Visits_VisitDetail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_VisitDetailPartial", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Visits\VisitDetail.cshtml"
  
    ViewData["Title"] = "Visit Detail";
    ViewData["Action"] = "VisitDetail";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "048ba5359129a3b2025c08d778e5c6e6211931623854", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    <script>\r\n        $(document).ready(function () {\r\n            $(\"#Status\").change(function (e) {\r\n                var val = $(\"#Status\").val();\r\n                if (val == \"");
#nullable restore
#line 15 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Visits\VisitDetail.cshtml"
                        Write((int)MSIS_HMS.Core.Enums.VisitStatusEnum.Cancel);

#line default
#line hidden
#nullable disable
                WriteLiteral(@""") {
                    $("".reasonForCancellation"").show();
                } else {
                    $("".reasonForCancellation"").hide();
                }
            });
        });

        function onSelectDoctor(id) {
            $('#AvailableDoctors').modal('hide');
            $(""#DoctorId"").val(id).trigger('change');
        }

        $('#AvailableDoctors').on('show.bs.modal', function (e) {
            $.ajax({
                url: `/Doctors/GetAvailableDoctors?DepartmentType=");
#nullable restore
#line 30 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Visits\VisitDetail.cshtml"
                                                              Write((int)MSIS_HMS.Core.Enums.DepartmentTypeEnum.EnumDepartmentType.OPD);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"&DepartmentId=${$(""#DepartmentId"").val()}&SpecialityId=${$(""#SpecialityId"").val()}`,
                type: 'get',
                success: function (res) {
                    var html = """";
                    for (var i = 0; i < res.length; i++) {
                        html += `
                                <tr>
                                    <td>${i+1}</td>
                                    <td>${res[i].name}</td>
                                    <td>${tConvert(res[i].fromTime)}</td>
                                    <td>${tConvert(res[i].toTime)}</td>
                                    <td>${res[i].patientInQueue}</td>
                                    <td>${res[i].estWaitingTime}</td>
                                    <td><button class=""btn btn-xs btn-secondary"" onclick=""onSelectDoctor(${res[i].id})"">Select</button></td>
                                </tr>
                                `;
                    }
                    $(""#AvailableDoctorsTableBody"").");
                WriteLiteral(@"html(html);
                },
                error: function (jqXhr, textStatus, errorMessage) {

                }
            });
        })

        $('#AvailableDoctors').on('hidden.bs.modal', function (e) {
            $(""#AvailableDoctorsTableBody"").html("""");
        })
    </script>
");
            }
            );
            WriteLiteral(" ");
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
