﻿using Microsoft.AspNetCore.Components.WebView.Maui;
using FinPorfolioAnylizer.Data;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinPorfolioAnylizer;

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

        builder.Configuration.AddUserSecrets<MauiApp>();
#if DEBUG
        AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
        {
            // Log the exception, show an error dialog, etc.
            Console.WriteLine($"Unhandled exception: {e.ExceptionObject}");
        };
        builder.Services.AddBlazorWebViewDeveloperTools();
#endif
		
		builder.Services.AddSingleton<WeatherForecastService>();
        builder.Services.AddScoped<ChatbotService>();
        builder.Services.AddHttpClient<ChatbotService>((serviceProvider, client) =>
		{
			var config = serviceProvider.GetRequiredService<IConfiguration>();
            client.BaseAddress = new Uri("https://api.openai.com");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });

        return builder.Build();
	}
}