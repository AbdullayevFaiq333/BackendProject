#pragma checksum "C:\Users\Admin\Desktop\BackendProject\Backend Layihe\BackendProject\BackendProject\Views\About\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e004d4412a04bc00a246c5301bdeeed26febb2ac"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_About_Index), @"mvc.1.0.view", @"/Views/About/Index.cshtml")]
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
#line 1 "C:\Users\Admin\Desktop\BackendProject\Backend Layihe\BackendProject\BackendProject\Views\_ViewImports.cshtml"
using BackendProject;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Admin\Desktop\BackendProject\Backend Layihe\BackendProject\BackendProject\Views\_ViewImports.cshtml"
using BackendProject.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Admin\Desktop\BackendProject\Backend Layihe\BackendProject\BackendProject\Views\_ViewImports.cshtml"
using BackendProject.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e004d4412a04bc00a246c5301bdeeed26febb2ac", @"/Views/About/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"34361eeca8f809117d889d92f2a2f5a8d728906c", @"/Views/_ViewImports.cshtml")]
    public class Views_About_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\Users\Admin\Desktop\BackendProject\Backend Layihe\BackendProject\BackendProject\Views\About\Index.cshtml"
  
    ViewData["Title"] = "About";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<!-- Banner Area Start -->\r\n");
#nullable restore
#line 7 "C:\Users\Admin\Desktop\BackendProject\Backend Layihe\BackendProject\BackendProject\Views\About\Index.cshtml"
Write(await Component.InvokeAsync("Banner", "about us"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<!-- Banner Area End -->\r\n<!-- About Start -->\r\n<div class=\"about-area pt-145 pb-155\">\r\n    <div class=\"container\">\r\n        <div class=\"row\">\r\n            ");
#nullable restore
#line 13 "C:\Users\Admin\Desktop\BackendProject\Backend Layihe\BackendProject\BackendProject\Views\About\Index.cshtml"
       Write(await Component.InvokeAsync("About"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n</div>\r\n<!-- About End -->\r\n<!-- Teacher Start -->\r\n<div class=\"teacher-area pt-150 pb-105\">\r\n    <div class=\"container\">\r\n        <div class=\"row\">\r\n            ");
#nullable restore
#line 22 "C:\Users\Admin\Desktop\BackendProject\Backend Layihe\BackendProject\BackendProject\Views\About\Index.cshtml"
       Write(await Component.InvokeAsync("Teacher"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n<!-- Teacher End -->\r\n<!-- Testimonial Area Start -->\r\n");
#nullable restore
#line 29 "C:\Users\Admin\Desktop\BackendProject\Backend Layihe\BackendProject\BackendProject\Views\About\Index.cshtml"
Write(await Component.InvokeAsync("Testimonial"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<!-- Testimonial Area End -->\r\n<!-- Notice Start -->\r\n<section class=\"notice-area two pt-140 pb-100\">\r\n    <div class=\"container\">\r\n        <div class=\"row\">\r\n            ");
#nullable restore
#line 35 "C:\Users\Admin\Desktop\BackendProject\Backend Layihe\BackendProject\BackendProject\Views\About\Index.cshtml"
       Write(await Component.InvokeAsync("Notice"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n</section>\r\n<!-- Notice End -->\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
