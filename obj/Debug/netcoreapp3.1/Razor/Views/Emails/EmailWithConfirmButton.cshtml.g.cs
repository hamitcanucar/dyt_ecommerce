#pragma checksum "C:\Proje\dyt_ecommerce\Views\Emails\EmailWithConfirmButton.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0e4c6f0b4bc31f197370455ddac446bc3f05e79d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Emails_EmailWithConfirmButton), @"mvc.1.0.view", @"/Views/Emails/EmailWithConfirmButton.cshtml")]
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
#line 1 "C:\Proje\dyt_ecommerce\Views\Emails\EmailWithConfirmButton.cshtml"
using dyt_ecommerce.Views.Emails;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Proje\dyt_ecommerce\Views\Emails\EmailWithConfirmButton.cshtml"
using dyt_ecommerce.Views.Shared;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0e4c6f0b4bc31f197370455ddac446bc3f05e79d", @"/Views/Emails/EmailWithConfirmButton.cshtml")]
    public class Views_Emails_EmailWithConfirmButton : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<EmailWithConfirmButtonModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 5 "C:\Proje\dyt_ecommerce\Views\Emails\EmailWithConfirmButton.cshtml"
  
    ViewData["logo"] = Model.LogoUrl;
    ViewData["EmailTitle"] = Model.Title;
    ViewData["EmailPreview"] = Model.Preview;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<p>\r\n    ");
#nullable restore
#line 12 "C:\Proje\dyt_ecommerce\Views\Emails\EmailWithConfirmButton.cshtml"
Write(Model.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</p>\r\n\r\n<br />\r\n\r\n");
#nullable restore
#line 17 "C:\Proje\dyt_ecommerce\Views\Emails\EmailWithConfirmButton.cshtml"
Write(await Html.PartialAsync("EmailButton", new EmailButtonViewModel(Model.ButtonText, Model.Url)));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n<p>\r\n    ");
#nullable restore
#line 20 "C:\Proje\dyt_ecommerce\Views\Emails\EmailWithConfirmButton.cshtml"
Write(Model.UrlDescription);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</p>\r\n\r\n<p>\r\n    ");
#nullable restore
#line 24 "C:\Proje\dyt_ecommerce\Views\Emails\EmailWithConfirmButton.cshtml"
Write(Model.Url);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</p>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<EmailWithConfirmButtonModel> Html { get; private set; }
    }
}
#pragma warning restore 1591