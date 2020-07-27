#pragma checksum "C:\Users\i00018\source\repos\BlazorApp\Pages\Previous.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "415806442e3d85ec7175cde88ca63e8a410aee6d"
// <auto-generated/>
#pragma warning disable 1591
namespace BlazorApp.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\i00018\source\repos\BlazorApp\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\i00018\source\repos\BlazorApp\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\i00018\source\repos\BlazorApp\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\i00018\source\repos\BlazorApp\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\i00018\source\repos\BlazorApp\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\i00018\source\repos\BlazorApp\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\i00018\source\repos\BlazorApp\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\i00018\source\repos\BlazorApp\_Imports.razor"
using BlazorApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\i00018\source\repos\BlazorApp\_Imports.razor"
using BlazorApp.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\i00018\source\repos\BlazorApp\_Imports.razor"
using ChartJs.Blazor;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\i00018\source\repos\BlazorApp\Pages\Previous.razor"
using BlazorApp.Data;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/previous")]
    public partial class Previous : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<h1>Previous Test Run Outcomes</h1>\r\n\r\n");
#nullable restore
#line 7 "C:\Users\i00018\source\repos\BlazorApp\Pages\Previous.razor"
 if (testdata == null)
{

#line default
#line hidden
#nullable disable
            __builder.AddContent(1, "    ");
            __builder.AddMarkupContent(2, "<p><em>Loading...</em></p>\r\n");
#nullable restore
#line 10 "C:\Users\i00018\source\repos\BlazorApp\Pages\Previous.razor"
}
else
{

#line default
#line hidden
#nullable disable
            __builder.AddContent(3, "    ");
            __builder.OpenElement(4, "table");
            __builder.AddAttribute(5, "class", "table");
            __builder.AddMarkupContent(6, "\r\n        ");
            __builder.AddMarkupContent(7, @"<thead>
            <tr>
                <th>Test Run Id</th>
                <th>Created Date</th>
                <th>Tests Passed</th>
                <th>Tests Failed</th>
                <th>Created By</th>
                <th>Application Name</th>
                <th>Application Version</th>
            </tr>
        </thead>
        ");
            __builder.OpenElement(8, "tbody");
            __builder.AddMarkupContent(9, "\r\n");
#nullable restore
#line 26 "C:\Users\i00018\source\repos\BlazorApp\Pages\Previous.razor"
             foreach (var data in testdata)
            {

#line default
#line hidden
#nullable disable
            __builder.AddContent(10, "                ");
            __builder.OpenElement(11, "tr");
            __builder.AddMarkupContent(12, "\r\n                    ");
            __builder.OpenElement(13, "td");
            __builder.AddContent(14, 
#nullable restore
#line 29 "C:\Users\i00018\source\repos\BlazorApp\Pages\Previous.razor"
                         data.TestRunId

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(15, "\r\n                    ");
            __builder.OpenElement(16, "td");
            __builder.AddContent(17, 
#nullable restore
#line 30 "C:\Users\i00018\source\repos\BlazorApp\Pages\Previous.razor"
                         data.CreatedDate

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(18, "\r\n                    ");
            __builder.OpenElement(19, "td");
            __builder.AddContent(20, 
#nullable restore
#line 31 "C:\Users\i00018\source\repos\BlazorApp\Pages\Previous.razor"
                         data.TestsFailed

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(21, "\r\n                    ");
            __builder.OpenElement(22, "td");
            __builder.AddContent(23, 
#nullable restore
#line 32 "C:\Users\i00018\source\repos\BlazorApp\Pages\Previous.razor"
                         data.TestsPassed

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(24, "\r\n                    ");
            __builder.OpenElement(25, "td");
            __builder.AddContent(26, 
#nullable restore
#line 33 "C:\Users\i00018\source\repos\BlazorApp\Pages\Previous.razor"
                         data.CreatedBy

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(27, "\r\n                    ");
            __builder.OpenElement(28, "td");
            __builder.AddContent(29, 
#nullable restore
#line 34 "C:\Users\i00018\source\repos\BlazorApp\Pages\Previous.razor"
                         data.ApplicationName

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(30, "\r\n                    ");
            __builder.OpenElement(31, "td");
            __builder.AddContent(32, 
#nullable restore
#line 35 "C:\Users\i00018\source\repos\BlazorApp\Pages\Previous.razor"
                         data.ApplicationVersion

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(33, " \r\n                ");
            __builder.CloseElement();
            __builder.AddMarkupContent(34, "\r\n");
#nullable restore
#line 37 "C:\Users\i00018\source\repos\BlazorApp\Pages\Previous.razor"
            }

#line default
#line hidden
#nullable disable
            __builder.AddContent(35, "        ");
            __builder.CloseElement();
            __builder.AddMarkupContent(36, "\r\n    ");
            __builder.CloseElement();
            __builder.AddMarkupContent(37, "\r\n");
#nullable restore
#line 40 "C:\Users\i00018\source\repos\BlazorApp\Pages\Previous.razor"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
#nullable restore
#line 42 "C:\Users\i00018\source\repos\BlazorApp\Pages\Previous.razor"
       
    private TestData[] testdata;

    protected override async Task OnInitializedAsync()
    {
        testdata = await DataRetrieval.GetPreviousRunsDataAsync(); 
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private TestDataRetrieval DataRetrieval { get; set; }
    }
}
#pragma warning restore 1591
