#pragma checksum "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Payments\Create.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "cd83055f30cd5a89235afd14c5061b35608fac13"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Payments_Create), @"mvc.1.0.view", @"/Views/Payments/Create.cshtml")]
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
#line 1 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Payments\Create.cshtml"
using MSIS_HMS.Enums;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cd83055f30cd5a89235afd14c5061b35608fac13", @"/Views/Payments/Create.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4f8d343b1f1203f781b2225c85b76acba6afc043", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Payments_Create : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_PartialForm", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 2 "C:\Users\Wai\Desktop\thirisandar\hms\MSIS_HMS\Views\Payments\Create.cshtml"
  
    ViewData["Title"] = "Create Payment";
    ViewData["Action"] = ViewEnum.Action.Create.ToDescription();
    var FromDate = "";
    var ToDate = "";

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "cd83055f30cd5a89235afd14c5061b35608fac133864", async() => {
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
            WriteLiteral("\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
    <script>


        $(document).ready(function () {
            $(""#paymentCalculate"").modal(""hide"");
            var ipdRecordId = document.getElementById(""ipdRecordId"").value;
            getRecordData(ipdRecordId);

            $('#paymentTypeId').select2({
                dropdownParent: $('#paymentInput')
            });
         
        });

        function ShowModal() {

            document.getElementById(""paidBy"").value = """";
            $(""#paymentTypeId"").select2(""val"", ""0"");
            //document.getElementById(""paymentTypeId"").selectedIndex = """";
            document.getElementById(""amount"").value = """";
            document.getElementById(""Date"").value = """";
            $(""#paymentTypeId"").prop(""disabled"", false);
            $(""#amount"").prop(""disabled"", false);
            $(""#Date"").prop(""disabled"", false);
            $(""#paymentInput"").modal(""show"");
        }

        function ShowModalForEdit(id) {
            $.ajax({

                url: `/Payments/");
                WriteLiteral(@"GetPaymentById?Id=${id}`,
                type: 'get',
                success: function (res) {
                    if (res) {
                        document.getElementById(""id"").value = res.id;
                        document.getElementById(""paidBy"").value = res.paidBy;
                        //document.getElementById(""paymentTypeId"").selectedIndex = res.paymentType;
                        $(""#paymentTypeId"").select2(""val"", String(res.paymentType));
                        document.getElementById(""amount"").value = res.amount;
                        document.getElementById(""Date"").value = res.date;
                        $(""#paymentTypeId"").prop(""disabled"", true);
                        $(""#amount"").prop(""disabled"", true);
                        $(""#Date"").prop(""disabled"", true);


                    }

                }

            });
            $(""#paymentInput"").modal(""show"");

        }
        function CloseModal() {
            $(""#paymentInput"").modal(""hide"");
   ");
                WriteLiteral(@"     }

        function getRecordData(recordId) {
            $.ajax({

                url: `/Payments/GetRecordByRecordId?RecordId=${recordId}`,
                type: 'get',
                success: function (res) {
                    if (res) {
                        $(""#lblVoucherNo"").html(res.voucherNo);
                        $(""#lblDOA"").html(res.doa.toString().split(""T"")[0]);
                        $(""#lblPatient"").html(res.patientName);
                        $(""#lblDODC"").html(res.dodc.toString().split(""T"")[0]);
                        $(""#lblGuardian"").html(res.guardian);
                        $(""#lblPaymentType"").html(res.paymentTypeName);
                        $(""#lblAge"").html(res.age);
                        $(""#lblStatus"").html(res.status);
                        var img = document.createElement(""img"");

                        var div = document.getElementById('patientImg');
                        if (res.image == """") {
                            img.src = """);
                WriteLiteral(@"/images/add-image.png"";
                        }
                        else {
                            img.src = res.image;
                        }

                        img.style.width = ""150px"";
                        img.style.height = ""125px"";
                        /*img.style.borderRadius = ""50%"";*/
                        img.className = ""thumbnail"";
                        div.appendChild(img);


                    }

                }

            });
        }

        function getPaymentData() {
            $.ajax({

                url: `/Payments/GetPayments`,
                type: 'get',
                success: function (res) {
                    if (res) {

                    }

                }

            });
        }

        function GetDetail(ipdRecordDate) {
            //document.getElementById(""btnPrint"").style.display = ""block"";
            //document.getElementById(""recordDetail"").style.visibility = ""visible"";

            var");
                WriteLiteral(@" RecordId = document.getElementById(""ipdRecordId"").value;

            $.ajax({

                url: `/Payments/GetIPDRecordDetailByRecordId?ipdRecordId=${RecordId}` + `&date=${ipdRecordDate}`,
                type: 'get',
                success: function (res) {
                    if (res) {
                        var html = """";
                        var subTotal = 0;
                        if (res.roomChargesDTOs.length > 0) {
                            html += `<tr>
                                                <td></td>
                                                <td style=""font-weight:bold"">Room Charges</td>
                                                <td></td>
                                                <td></td>
                                                </tr>`
                            for (var i = 0; i < res.roomChargesDTOs.length; i++) {

                                html += ` <tr>
                                                <td>${i}</td>
    ");
                WriteLiteral(@"                                            <td>${res.roomChargesDTOs[i].roomName}/${res.roomChargesDTOs[i].bedName}</td>
                                                <td>${res.roomChargesDTOs[i].unitPrice} MMK</td>
                                                <td>${res.roomChargesDTOs[i].qty}</td>
                                                <td>${res.roomChargesDTOs[i].amount} MMK</td>
                                                </tr>`;
                                subTotal += res.roomChargesDTOs[i].amount;
                            }
                            html += `<tr>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td style=""font-weight:bold"">SubTotal</td>
                                                <td style=""font-weight:bold"">${subTotal} MMK</td>
                                             ");
                WriteLiteral(@"   </tr>`
                        }
                        if (res.medicationsDTOs.length > 0) {
                            subTotal = 0;
                            html += `<tr>
                                                <td></td>
                                                <td style=""font-weight:bold"">Medication</td>
                                                <td></td>
                                                <td></td>
                                                </tr>`
                            for (var i = 0; i < res.medicationsDTOs.length; i++) {

                                html += ` <tr>
                                                <td>${i + 1}</td>
                                                <td>${res.medicationsDTOs[i].name}</td>
                                                <td>${res.medicationsDTOs[i].unitPrice} MMK</td>
                                                <td>${res.medicationsDTOs[i].qty} ${res.medicationsDTOs[i].unitName}</td>");
                WriteLiteral(@"
                                                <td>${res.medicationsDTOs[i].amount} MMK</td>
                                                </tr>`;
                                subTotal += res.medicationsDTOs[i].amount;
                            }
                            html += `<tr>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td style=""font-weight:bold"">SubTotal</td>
                                                <td style=""font-weight:bold"">${subTotal} MMK</td>
                                                </tr>`
                        }
                        if (res.iPDOrderServiceDTOs.length > 0) {
                            subTotal = 0;
                            html += `<tr>
                                                <td></td>
                                                <td style=");
                WriteLiteral(@"""font-weight:bold"">Service</td>
                                                <td></td>
                                                <td></td>
                                                </tr>`
                            for (var i = 0; i < res.iPDOrderServiceDTOs.length; i++) {

                                html += ` <tr>
                                                <td>${i + 1}</td>
                                                <td>${res.iPDOrderServiceDTOs[i].name}</td>
                                                <td>${res.iPDOrderServiceDTOs[i].unitPrice} MMK</td>
                                                <td>${res.iPDOrderServiceDTOs[i].qty}</td>
                                                <td>${res.iPDOrderServiceDTOs[i].amount} MMK</td>
                                                </tr>`;
                                subTotal += res.iPDOrderServiceDTOs[i].amount;
                            }
                            html += `<tr>
               ");
                WriteLiteral(@"                                 <td></td>
                                                <td></td>
                                                <td></td>
                                                <td style=""font-weight:bold"">SubTotal</td>
                                                <td style=""font-weight:bold"">${subTotal} MMK</td>
                                                </tr>`
                        }
                        if (res.feesDTOs.length > 0) {
                            subTotal = 0;
                            html += `<tr>
                                                <td></td>
                                                <td style=""font-weight:bold"">Round & Other Fees</td>
                                                <td></td>
                                                <td></td>
                                                </tr>`
                            for (var i = 0; i < res.feesDTOs.length; i++) {

                                ");
                WriteLiteral(@"html += ` <tr>
                                                <td>${i + 1}</td>
                                                <td>${res.feesDTOs[i].feesName}</td>
                                                <td>${res.feesDTOs[i].unitPrice} MMK</td>
                                                <td>${res.feesDTOs[i].qty}</td>
                                                <td>${res.feesDTOs[i].amount} MMK</td>
                                                </tr>`;
                                subTotal += res.feesDTOs[i].amount;
                            }
                            html += `<tr>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td style=""font-weight:bold"">SubTotal</td>
                                                <td style=""font-weight:bold"">${subTotal} MMK</td>
                           ");
                WriteLiteral(@"                     </tr>`
                        }
                        if (res.foodDTOs.length > 0) {
                            subTotal = 0;
                            html += `<tr>
                                                <td></td>
                                                <td style=""font-weight:bold"">Food</td>
                                                <td></td>
                                                <td></td>
                                                </tr>`
                            for (var i = 0; i < res.foodDTOs.length; i++) {

                                html += ` <tr>
                                                <td>${i + 1}</td>
                                                <td>${res.foodDTOs[i].name}</td>
                                                <td>${res.foodDTOs[i].unitPrice} MMK</td>
                                                <td>${res.foodDTOs[i].qty}</td>
                                                <td>${res");
                WriteLiteral(@".foodDTOs[i].amount} MMK</td>
                                                </tr>`;
                                subTotal += res.foodDTOs[i].amount;
                            }
                            html += `<tr>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td style=""font-weight:bold"">SubTotal</td>
                                                <td style=""font-weight:bold"">${subTotal} MMK</td>
                                                </tr>`
                        }
                        if (res.labDTOs.length > 0) {
                            subTotal = 0;
                            html += `<tr>
                                                <td></td>
                                                <td style=""font-weight:bold"">Lab</td>
                                                <td></t");
                WriteLiteral(@"d>
                                                <td></td>
                                                </tr>`
                            for (var i = 0; i < res.labDTOs.length; i++) {

                                html += ` <tr>
                                                <td>${i + 1}</td>
                                                <td>${res.labDTOs[i].name}</td>
                                                <td>${res.labDTOs[i].unitPrice} MMK</td>
                                                <td>${res.labDTOs[i].qty}</td>
                                                <td>${res.labDTOs[i].amount} MMK</td>
                                                </tr>`;
                                subTotal += res.labDTOs[i].amount;
                            }
                            html += `<tr>
                                                <td></td>
                                                <td></td>
                                                <td></td>");
                WriteLiteral(@"
                                                <td style=""font-weight:bold"">SubTotal</td>
                                                <td style=""font-weight:bold"">${subTotal} MMK</td>
                                                </tr>`
                        }
                        if (res.imgDTOs.length > 0) {
                            subTotal = 0;
                            html += `<tr>
                                                <td></td>
                                                <td style=""font-weight:bold"">Imaging</td>
                                                <td></td>
                                                <td></td>
                                                </tr>`
                            for (var i = 0; i < res.imgDTOs.length; i++) {

                                html += ` <tr>
                                                <td>${i + 1}</td>
                                                <td>${res.imgDTOs[i].name}</td>
         ");
                WriteLiteral(@"                                       <td>${res.imgDTOs[i].unitPrice} MMK</td>
                                                <td>${res.imgDTOs[i].qty}</td>
                                                <td>${res.imgDTOs[i].amount} MMK</td>
                                                </tr>`;
                                subTotal += res.imgDTOs[i].amount;
                            }
                            html += `<tr>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td style=""font-weight:bold"">SubTotal</td>
                                                <td style=""font-weight:bold"">${subTotal} MMK</td>
                                                </tr>`
                        }
                        $(""#recordDetailBody"").html(html);

                    }

                }

            });
 ");
                WriteLiteral(@"       }

        function SaveDiscountAndTax() {
            var ipdRecordId = document.getElementById(""ipdRecordId"").value;
            var discount = document.getElementById(""discount"").value;
            var tax = document.getElementById(""tax"").value;
            $.ajax({
                url: `/Payments/DiscountAndTax`,
                type: 'get',
                data: { discount: discount, ipdRecordId: ipdRecordId, tax: tax },
                success: function (res) {
                    //alert(""Save Successfully"");
                    Swal.fire({
                        position: `center`,
                        icon: `success`,
                        showCloseButton: true,
                        title: `Save Successfully`,
                        showConfirmButton: false,
                        ConfirmButtonText: ""Close"",
                        timer: 2000
                    })
                }

            });
            //window.location.reload;
        }


     ");
                WriteLiteral(@"   function Calculatepayment(ipdRecordId) {
            debugger;
            $.ajax({
                url: `/Payments/DailyPaymentCalculate?IPdRecordId=${ipdRecordId}`,
                type: 'get',
                success: function (res) {
                    $(""#paymentCalculate"").modal(""hide"");
                    if (res == ""Not Enough Amount"") {
                        Swal.fire({
                            position: `center`,
                            icon: `warning`,
                            showCloseButton: true,
                            title: `Not Enough Amount`,
                            text: `You should enter deposit amount.`,
                            showConfirmButton: false,
                            ConfirmButtonText: ""Close"",
                            timer: 2000
                        })
                    }
                    else {
                        $(""#paymentCalculate"").modal(""show"");
                        var html = """";

              ");
                WriteLiteral(@"          html += `<tr>
                                  <td>${res.patientName}</td>
                                  <td>${res.amount}</td >
                                  <td>${res.date}</td>
                                 </tr>`

                        $(""#ipdPaymentBody"").html(html);
                    }

                }

            });
        }

        function PaidConfirm(url, id) {
            Swal.fire({
                title: 'Are you sure?',
                text: ""You won't be able to revert this!"",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, this Ipdrecord is paid.'
            }).then((result) => {
                if (result.isConfirmed) {
                    window.location.href = url + id;
                }
            })
        }
        function InsertData() {
            FromDate = document.");
                WriteLiteral(@"getElementById(""FromDate"").value;
            ToDate = document.getElementById(""ToDate"").value;
        }
        function GetPaymentDetail(ipdRecordId) {
            debugger;
            let FromDate = document.getElementById(""FromDate"").value;
            let ToDate = document.getElementById(""ToDate"").value;
            //window.location = `/Payments/PrintPaymentAmountDetail?ipdRecordId=${ipdRecordId}` + `&FromDate=${FromDate}&ToDate=${ToDate}`;
            var url = `/Payments/PrintPaymentAmountDetail?ipdRecordId=${ipdRecordId}` + `&FromDate=${FromDate}&ToDate=${ToDate}`;
            var xhr = new XMLHttpRequest();
            xhr.open('GET', url, true);
            xhr.responseType = 'arraybuffer';
            xhr.onload = function (e) {
                if (this.status == 200) {
                    console.log(""blob"", this.response);
                    var blob = new Blob([this.response], { type: ""application/pdf"" });
                    console.log(""file "", blob);
                    v");
                WriteLiteral(@"ar link = document.createElement('a');
                    link.href = window.URL.createObjectURL(blob);
                    link.download = ""DeliveryInvoice_"" + new Date() + "".pdf"";
                    link.click();
                }
            };
            xhr.send();


        }
      
    </script>

");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
