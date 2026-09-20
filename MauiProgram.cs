using KBUI_Explorer.Options;
using KBUI_Explorer.Services;
using Microsoft.Extensions.Logging;

namespace KBUI_Explorer;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();

        const string defaultApiBase = "https://app-q-cpy-hud-dev.azurewebsites.net";
        var savedBase = Preferences.Default.Get("QueryUi.ApiBaseUrl", defaultApiBase);
        // Migrate prior local-API default so existing installs hit Azure without a manual reset.
        if (string.Equals(savedBase.TrimEnd('/'), "http://127.0.0.1:5088", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(savedBase.TrimEnd('/'), "http://localhost:5088", StringComparison.OrdinalIgnoreCase))
        {
            savedBase = defaultApiBase;
            Preferences.Default.Set("QueryUi.ApiBaseUrl", savedBase);
        }

        var options = new QueryUiOptions
        {
            ApiBaseUrl = savedBase,
            ApiKey = Preferences.Default.Get("QueryUi.ApiKey", ""),
            MaxQuestionLength = 500,
            MaxTopK = 10
        };
        builder.Services.AddSingleton(Microsoft.Extensions.Options.Options.Create(options));
        builder.Services.AddSingleton<QueryApiClient>();

        builder.Services.AddHttpClient("KbQuery", client =>
        {
            client.Timeout = TimeSpan.FromMinutes(2);
        });

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
