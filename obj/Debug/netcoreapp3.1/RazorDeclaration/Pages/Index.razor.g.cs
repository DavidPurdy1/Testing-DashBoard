#pragma checksum "C:\Users\i00018\source\repos\BlazorApp\Pages\Index.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e684dedf9b1e07002ec32c013a3ca55372c816b4"
// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

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
#line 3 "C:\Users\i00018\source\repos\BlazorApp\Pages\Index.razor"
using ChartJs.Blazor.Charts;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\i00018\source\repos\BlazorApp\Pages\Index.razor"
using ChartJs.Blazor.ChartJS.PieChart;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\i00018\source\repos\BlazorApp\Pages\Index.razor"
using ChartJs.Blazor.ChartJS.Common.Properties;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\i00018\source\repos\BlazorApp\Pages\Index.razor"
using ChartJs.Blazor.Util;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/")]
    public partial class Index : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 31 "C:\Users\i00018\source\repos\BlazorApp\Pages\Index.razor"
      
private PieConfig _config;
private ChartJsPieChart _pieChartJs;
protected override void OnInitialized() //Pie chart
{
    _config = new PieConfig
    {
        Options = new PieOptions
        {
            MaintainAspectRatio = false,
            
            Title = new OptionsTitle
            {
                Display = true,
                Text = "Last Assembly Results"
            },
            Responsive = true, 
             Animation = new ArcAnimation
            {
                AnimateRotate = true,
                AnimateScale = true
            } 
        }
    };

    _config.Data.Labels.AddRange(new[] { "Passed", "Failed"  });

    var pieSet = new PieDataset
    {
        BackgroundColor = new[] { ColorUtil.RandomColorString(), ColorUtil.RandomColorString(), ColorUtil.RandomColorString(), ColorUtil.RandomColorString() },
        BorderWidth = 0,
        HoverBackgroundColor = ColorUtil.RandomColorString(),
        HoverBorderColor = ColorUtil.RandomColorString(),
        HoverBorderWidth = 1,
        BorderColor = "#ffffff",
    };
    //This is where you put in the data
    pieSet.Data.AddRange(new double[] { 4, 5 });
    _config.Data.Datasets.Add(pieSet);
}

#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
