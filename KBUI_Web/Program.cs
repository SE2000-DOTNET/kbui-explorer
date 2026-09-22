using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using KBUI_Explorer.Options;
using KBUI_Explorer.Services;
using KBUI_Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.Configure<QueryUiOptions>(options =>
{
    var config = builder.Configuration.GetSection(QueryUiOptions.SectionName);
    if (config.Exists())
    {
        config.Bind(options);
    }

    var apiBaseUrl = builder.Configuration["QueryUi:ApiBaseUrl"];
    if (!string.IsNullOrWhiteSpace(apiBaseUrl))
    {
        options.ApiBaseUrl = apiBaseUrl;
    }
});

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<QueryApiClient>();

await builder.Build().RunAsync();
