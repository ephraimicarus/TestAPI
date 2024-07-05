using BaseApp.Services;
using BaseApp.ViewModels;
using BaseApp.Views;
using Microsoft.Extensions.Logging;

namespace BaseApp
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
			builder.Services.AddSingleton<HomePageViewModel>();
			builder.Services.AddSingleton<CustomerPageViewModel>();
			builder.Services.AddTransient<HttpClient>();
			builder.Services.AddTransient<IClientApiService, ClientApiService>();
			builder.Services.AddTransient<HomePage>();
			builder.Services.AddTransient<CustomerPage>();

			//	builder.Services.AddTransient<HomePage>(serviceProvider => new HomePage()
			//{
			//	BindingContext = serviceProvider.GetRequiredService<HomePageViewModel>()
			//});
			//builder.Services.AddTransient<CustomerPage>(serviceProvider => new CustomerPage()
			//{
			//	BindingContext = serviceProvider.GetRequiredService<CustomerPageViewModel>()
			//});

			return builder.Build();
		}
	}
}
