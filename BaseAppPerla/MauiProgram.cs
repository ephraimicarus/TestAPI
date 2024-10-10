using BaseAppPerla.Interfaces;
using BaseAppPerla.Services;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;

namespace BaseAppPerla
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
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif
            builder.Services.AddHttpClient();
            builder.Services.AddMudServices();

            builder.Services.AddTransient<ICustomerDueService, CustomerDueService>();
            builder.Services.AddTransient<ICustomerService, CustomerService>();
            builder.Services.AddTransient<IInventoryService, InventoryService>();
            builder.Services.AddTransient<IItemService, ItemService>();
            builder.Services.AddTransient<ITransactionService, TransactionService>();
            return builder.Build();
        }
    }
}
