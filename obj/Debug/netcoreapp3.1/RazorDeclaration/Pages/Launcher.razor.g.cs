#pragma checksum "C:\Users\i00018\source\repos\BlazorApp\Pages\Launcher.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "959d8cdd0f319273630e68378d709775d37311ec"
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
#line 2 "C:\Users\i00018\source\repos\BlazorApp\Pages\Launcher.razor"
using System.Configuration;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\i00018\source\repos\BlazorApp\Pages\Launcher.razor"
using BlazorApp.Services;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\i00018\source\repos\BlazorApp\Pages\Launcher.razor"
using System.Threading;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/launcher")]
    public partial class Launcher : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 59 "C:\Users\i00018\source\repos\BlazorApp\Pages\Launcher.razor"
      
    #region fields
    private DataEntry entry = new DataEntry();
    public bool IsTaskRunning = false;
    public string testingprogress = "";
    public string ButtonName1 = "Basic Test Scenerio";
    public string ButtonName2 = "Advanced Test Scenerio";
    public string ButtonName3 = "Full Test Scenerio";
    public string ButtonName4 = "Four Part Test Scenerio";
    public string ButtonName5 = "Regression Test Scenerio";
    #endregion

    #region button actions
    public void ButtonAction1()
    {
        string tests = ConfigurationManager.AppSettings.Get("Basic Test");
        WaitForResults(tests, 60000);
    }
    public void ButtonAction2()
    {
        string tests = ConfigurationManager.AppSettings.Get("Advanced Test");
        WaitForResults(tests);
    }
    public void ButtonAction3()
    {
        string tests = ConfigurationManager.AppSettings.Get("Full Test");
        WaitForResults(tests, 120000);
    }
    public void ButtonAction4()
    {
        string tests = ConfigurationManager.AppSettings.Get("Four Part Test");
        WaitForResults(tests, 120000);
    }
    public void ButtonAction5()
    {
        string tests = ConfigurationManager.AppSettings.Get("Regression Test");
        WaitForResults(tests, 60000);
    }
    public void SearchButtonAction()
    {
        WaitForResults(entry.Text);
    }
    #endregion
    public void SetTimer()
    {
        Timer t = new Timer(new TimerCallback(TimerAction));
        t.Change(int.Parse(entry.Time), 0);
    }
    private void TimerAction(object state)
    {
        // The state object is the Timer object.
        Timer t = (Timer)state;
        t.Dispose();
        WaitForResults(entry.Text, 25000);
    }
    public async void WaitForResults(string tests, int estimatedTime = 25000)
    {
        IsTaskRunning = true;
        testingprogress = "Running Tests ...";
        try
        {
            StateHasChanged();
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e.StackTrace);
        }
        Runner.Runner(tests);
        await Task.Delay(estimatedTime);
        IsTaskRunning = false;
        testingprogress = "";
        try
        {
            StateHasChanged();
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e.StackTrace);
        }


    }
    public void ValidSubmit()
    {
        Console.WriteLine("Valid Submit " + entry.Text);
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private TestLauncher Runner { get; set; }
    }
}
#pragma warning restore 1591
