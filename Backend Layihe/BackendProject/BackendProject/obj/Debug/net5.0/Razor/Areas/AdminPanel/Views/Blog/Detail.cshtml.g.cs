#pragma checksum "C:\Users\Admin\Desktop\BackendProject\Backend Layihe\BackendProject\BackendProject\Areas\AdminPanel\Views\Blog\Detail.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b824a30680b6e67a15ee30ae1eec80447a4b829e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_AdminPanel_Views_Blog_Detail), @"mvc.1.0.view", @"/Areas/AdminPanel/Views/Blog/Detail.cshtml")]
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
#line 1 "C:\Users\Admin\Desktop\BackendProject\Backend Layihe\BackendProject\BackendProject\Areas\AdminPanel\_ViewImports.cshtml"
using BackendProject;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Admin\Desktop\BackendProject\Backend Layihe\BackendProject\BackendProject\Areas\AdminPanel\_ViewImports.cshtml"
using BackendProject.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Admin\Desktop\BackendProject\Backend Layihe\BackendProject\BackendProject\Areas\AdminPanel\_ViewImports.cshtml"
using BackendProject.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b824a30680b6e67a15ee30ae1eec80447a4b829e", @"/Areas/AdminPanel/Views/Blog/Detail.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"34361eeca8f809117d889d92f2a2f5a8d728906c", @"/Areas/AdminPanel/_ViewImports.cshtml")]
    public class Areas_AdminPanel_Views_Blog_Detail : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("width:200px !important ; height:100px !important"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\Users\Admin\Desktop\BackendProject\Backend Layihe\BackendProject\BackendProject\Areas\AdminPanel\Views\Blog\Detail.cshtml"
  
    ViewData["Title"] = "Detail";
    Blog blog = Model;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n<div class=\"row\">\r\n    <div class=\"col-md-12 grid-margin stretch-card\">\r\n        <div class=\"card\">\r\n            <div class=\"card-body\">\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "b824a30680b6e67a15ee30ae1eec80447a4b829e5191", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 236, "~/img/blog/", 236, 11, true);
#nullable restore
#line 12 "C:\Users\Admin\Desktop\BackendProject\Backend Layihe\BackendProject\BackendProject\Areas\AdminPanel\Views\Blog\Detail.cshtml"
AddHtmlAttributeValue("", 247, blog.Image, 247, 11, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                <h4 class=\"card-title\">Name : ");
#nullable restore
#line 13 "C:\Users\Admin\Desktop\BackendProject\Backend Layihe\BackendProject\BackendProject\Areas\AdminPanel\Views\Blog\Detail.cshtml"
                                         Write(blog.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n                <p>Time : ");
#nullable restore
#line 14 "C:\Users\Admin\Desktop\BackendProject\Backend Layihe\BackendProject\BackendProject\Areas\AdminPanel\Views\Blog\Detail.cshtml"
                     Write(blog.Time);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                <p>MessageCount : ");
#nullable restore
#line 15 "C:\Users\Admin\Desktop\BackendProject\Backend Layihe\BackendProject\BackendProject\Areas\AdminPanel\Views\Blog\Detail.cshtml"
                             Write(blog.MessageCount);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                <p>\r\n                    MainDescription : ");
#nullable restore
#line 17 "C:\Users\Admin\Desktop\BackendProject\Backend Layihe\BackendProject\BackendProject\Areas\AdminPanel\Views\Blog\Detail.cshtml"
                                 Write(Html.Raw(blog.MainDescription));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </p>\r\n                <p>Description1 : ");
#nullable restore
#line 19 "C:\Users\Admin\Desktop\BackendProject\Backend Layihe\BackendProject\BackendProject\Areas\AdminPanel\Views\Blog\Detail.cshtml"
                             Write(blog.BlogDetail.Description1);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                <p>Description2 : ");
#nullable restore
#line 20 "C:\Users\Admin\Desktop\BackendProject\Backend Layihe\BackendProject\BackendProject\Areas\AdminPanel\Views\Blog\Detail.cshtml"
                             Write(blog.BlogDetail.Description2);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                <p>Description3 : ");
#nullable restore
#line 21 "C:\Users\Admin\Desktop\BackendProject\Backend Layihe\BackendProject\BackendProject\Areas\AdminPanel\Views\Blog\Detail.cshtml"
                             Write(blog.BlogDetail.Description3);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                <p>Description4 : ");
#nullable restore
#line 22 "C:\Users\Admin\Desktop\BackendProject\Backend Layihe\BackendProject\BackendProject\Areas\AdminPanel\Views\Blog\Detail.cshtml"
                             Write(blog.BlogDetail.Description4);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                <p>Title : ");
#nullable restore
#line 23 "C:\Users\Admin\Desktop\BackendProject\Backend Layihe\BackendProject\BackendProject\Areas\AdminPanel\Views\Blog\Detail.cshtml"
                      Write(blog.BlogDetail.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                <p>LastDescription : ");
#nullable restore
#line 24 "C:\Users\Admin\Desktop\BackendProject\Backend Layihe\BackendProject\BackendProject\Areas\AdminPanel\Views\Blog\Detail.cshtml"
                                Write(blog.BlogDetail.LastDescription);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n<div class=\"row\">\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "b824a30680b6e67a15ee30ae1eec80447a4b829e10351", async() => {
                WriteLiteral("Go Back");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n\r\n");
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
