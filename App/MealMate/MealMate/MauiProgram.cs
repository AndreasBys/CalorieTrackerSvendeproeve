using MealMate.View;
using MealMate.ViewModel;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;

namespace MealMate
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureSyncfusionCore()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddTransient<HjemmeskaermSide>();
            builder.Services.AddTransient<OpretFoedevareSide>();
            builder.Services.AddTransient<ProfilSide>();
            builder.Services.AddTransient<RegistrerMaalSide>();
            builder.Services.AddTransient<RegistrerProfildataSide>();
            builder.Services.AddTransient<RegistrerSide>();
            builder.Services.AddSingleton<StartSkaermSide>();
            builder.Services.AddTransient<TilfoejFoedvareSide>();
            builder.Services.AddTransient<OpretRetSide>();

            // DI for ViewModels:

            builder.Services.AddSingleton<RegistrerMaalSideViewModel>();



            return builder.Build();
        }
    }
}
