#pragma checksum "C:\Users\Admin\Desktop\BackendProject\Backend Layihe\BackendProject\BackendProject\Views\Shared\Components\Notice\Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "87559a2c7c636fcb95ef72dfafde678c74b69735"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Components_Notice_Default), @"mvc.1.0.view", @"/Views/Shared/Components/Notice/Default.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"87559a2c7c636fcb95ef72dfafde678c74b69735", @"/Views/Shared/Components/Notice/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"34361eeca8f809117d889d92f2a2f5a8d728906c", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Components_Notice_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<NoticeViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n    <div class=\"col-md-6 col-sm-6 col-xs-12\">\r\n        <div class=\"notice-right-wrapper mb-25 pb-25\">\r\n            <h3>");
#nullable restore
#line 5 "C:\Users\Admin\Desktop\BackendProject\Backend Layihe\BackendProject\BackendProject\Views\Shared\Components\Notice\Default.cshtml"
           Write(Model.NoticeVideo.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n            <div class=\"notice-video\"");
            BeginWriteAttribute("style", " style=\"", 214, "\"", 312, 12);
            WriteAttributeValue("", 222, "background:", 222, 11, true);
            WriteAttributeValue(" ", 233, "rgba(0,", 234, 8, true);
            WriteAttributeValue(" ", 241, "0,", 242, 3, true);
            WriteAttributeValue(" ", 244, "0,", 245, 3, true);
            WriteAttributeValue(" ", 247, "0)", 248, 3, true);
            WriteAttributeValue(" ", 250, "url(../img/notice/", 251, 19, true);
#nullable restore
#line 6 "C:\Users\Admin\Desktop\BackendProject\Backend Layihe\BackendProject\BackendProject\Views\Shared\Components\Notice\Default.cshtml"
WriteAttributeValue("", 269, Model.NoticeVideo.Image, 269, 24, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 293, ")", 293, 1, true);
            WriteAttributeValue(" ", 294, "repeat", 295, 7, true);
            WriteAttributeValue(" ", 301, "scroll", 302, 7, true);
            WriteAttributeValue(" ", 308, "0", 309, 2, true);
            WriteAttributeValue(" ", 310, "0", 311, 2, true);
            EndWriteAttribute();
            WriteLiteral(@">
                <div class=""video-icon video-hover"">
                    <a class=""video-popup"" href=""https://www.youtube.com/watch?v=to6Ghf8UL7o"">
                        <i class=""zmdi zmdi-play""></i>
                    </a>
                </div>
            </div>
        </div>
    </div>
    <div class=""col-md-6 col-sm-6 col-xs-12"">
        <div class=""notice-left-wrapper"">
            <h3>notice board</h3>
            <div class=""notice-left"">
");
#nullable restore
#line 19 "C:\Users\Admin\Desktop\BackendProject\Backend Layihe\BackendProject\BackendProject\Views\Shared\Components\Notice\Default.cshtml"
                 foreach (NoticeBoard noticeBoard in Model.NoticeBoards)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div class=\"single-notice-left mb-23 pb-20\">\r\n                        <h4>");
#nullable restore
#line 22 "C:\Users\Admin\Desktop\BackendProject\Backend Layihe\BackendProject\BackendProject\Views\Shared\Components\Notice\Default.cshtml"
                       Write(noticeBoard.Time.ToString("dd MMMM , yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n                        <p>");
#nullable restore
#line 23 "C:\Users\Admin\Desktop\BackendProject\Backend Layihe\BackendProject\BackendProject\Views\Shared\Components\Notice\Default.cshtml"
                      Write(noticeBoard.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                    </div>\r\n");
#nullable restore
#line 25 "C:\Users\Admin\Desktop\BackendProject\Backend Layihe\BackendProject\BackendProject\Views\Shared\Components\Notice\Default.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            WriteLiteral("            </div>\r\n        </div>\r\n    </div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<NoticeViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
