using MealMate.Services;
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

            builder.Services.AddSingleton<HjemmeskaermSide>();
            builder.Services.AddTransient<OpretFoedevareSide>();
            builder.Services.AddTransient<ProfilSide>();
            builder.Services.AddTransient<RegistrerMaalSide>();
            builder.Services.AddTransient<RegistrerProfildataSide>();
            builder.Services.AddTransient<RegistrerSide>();
            builder.Services.AddSingleton<StartSkaermSide>();
            builder.Services.AddTransient<TilfoejFoedvareSide>();
            builder.Services.AddTransient<OpretRetSide>();
            builder.Services.AddSingleton<AdminHomePage>();
            builder.Services.AddTransient<AdminSelectedFood>();


            // DI for ViewModels:

            builder.Services.AddSingleton<RegistrerMaalSideViewModel>();
            builder.Services.AddSingleton<FoodViewModel>();
            builder.Services.AddSingleton<AddFoodViewModel>();
            builder.Services.AddSingleton<HomePageViewModel>();

            // DI for Models:
            builder.Services.AddTransient<User>();

            // Services
#if ANDROID
            string baseUrl = "http://10.0.2.2:5000/";
#else
            string baseUrl = "http://localhost:5000/"; // or use the machine's IP address
#endif

            builder.Services.AddHttpClient<LoginService>(client =>
            {
                client.BaseAddress = new Uri(baseUrl + "api/auth/");
            });

            builder.Services.AddHttpClient<UserService>(client =>
            {
                client.BaseAddress = new Uri(baseUrl + "api/users/");
            });

            builder.Services.AddHttpClient<FoodService>(client =>
            {
                client.BaseAddress = new Uri(baseUrl + "api/food/");
            });

            builder.Services.AddHttpClient<RetterService>(client =>
            {
                client.BaseAddress = new Uri(baseUrl + "api/dish/");
            });

            builder.Services.AddHttpClient<MacroLogService>(client =>
            {
                client.BaseAddress = new Uri(baseUrl + "api/macroLog/");
            });



            return builder.Build();
        }
    }
}
