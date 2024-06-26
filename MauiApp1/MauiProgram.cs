﻿using MauiApp1.Services;
using Microsoft.Extensions.Logging;

namespace MauiApp1
{
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
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				});

#if DEBUG
			builder.Logging.AddDebug();
#endif
			//resolve httpClient
			builder.Services.AddSingleton<HttpClient>();

			builder.Services.AddTransient<IClientApiService, ClientApiService>();
			builder.Services.AddTransient<MainPage>();

			return builder.Build();
		}
	}
}
