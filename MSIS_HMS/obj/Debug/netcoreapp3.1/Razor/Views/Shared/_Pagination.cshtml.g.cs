#pragma checksum "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Shared\_Pagination.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ece6a70322659b36141b3dc6efb13f2620f62b29"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__Pagination), @"mvc.1.0.view", @"/Views/Shared/_Pagination.cshtml")]
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
#line 1 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Shared\_Pagination.cshtml"
using X.PagedList;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Shared\_Pagination.cshtml"
using X.PagedList.Mvc.Core;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Shared\_Pagination.cshtml"
using X.PagedList.Web.Common;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Shared\_Pagination.cshtml"
using System.Dynamic;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Shared\_Pagination.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ece6a70322659b36141b3dc6efb13f2620f62b29", @"/Views/Shared/_Pagination.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4f8d343b1f1203f781b2225c85b76acba6afc043", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Shared__Pagination : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 7 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Shared\_Pagination.cshtml"
Write(Html.PagedListPager((IPagedList)Model, page =>
{
    var query = ViewData["Query"];
    var actionName = ViewData["ActionName"] != null ? ViewData["ActionName"].ToString() : "Index";
    if (query != null)
    {
        var obj = new Dictionary<string, object>();
        obj.Add("page", page);
        foreach (var item in (QueryCollection)query)
        {
            if (!obj.ContainsKey(item.Key))
            {
                obj.Add(item.Key, item.Value);
            }
        }
        return Url.Action(actionName, (object)obj);
    }
    else
    {
        return Url.Action(actionName, new { page });
    }
}, new PagedListRenderOptions { ContainerDivClasses = new String[] { "text-center" }, UlElementClasses = new String[] { "pagination", "pagination-sm" }, LinkToPreviousPageFormat = "«", LinkToNextPageFormat = "»" }));

#line default
#line hidden
#nullable disable
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