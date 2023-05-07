using Microsoft.Extensions.Configuration;
using System.Reflection;
using MudBlazor.Services;
using FinPortfolioAnalyzer.Data;

namespace FinPortfolioAnalyzer;

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

        builder.Configuration.AddConfiguration(GetJSONFileConfig("FinPortfolioAnalyzer.appsettings.json"));
        builder.Configuration.AddConfiguration(GetJSONFileConfig("FinPortfolioAnalyzer.appsettings.development.json"));
#if DEBUG

        builder.Services.AddBlazorWebViewDeveloperTools();
#endif

        builder.Services.AddSingleton<WeatherForecastService>();
        builder.Services.AddScoped<ChatbotService>();
        builder.Services.AddScoped<FinanceAnalyzer>();
        builder.Services.AddMudServices();

        return builder.Build();
    }

    private static IConfiguration GetJSONFileConfig(string configName)
    {
        var a = Assembly.GetExecutingAssembly();
        using var stream = a.GetManifestResourceStream(configName);

        return new ConfigurationBuilder()
                    .AddJsonStream(stream)
                    .Build();
    }
}
