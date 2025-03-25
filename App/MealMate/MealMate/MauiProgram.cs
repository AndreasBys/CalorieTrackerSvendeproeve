using MealMate.Services;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;
using ZXing.Net.Maui.Controls;

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
                })
                .UseBarcodeReader();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<AddFoodPage>();
            builder.Services.AddTransient<CreateUserPage>();
            builder.Services.AddTransient<CreateUserDataPage>();
            builder.Services.AddTransient<CreateGoalPage>();
            builder.Services.AddTransient<AddDishPage>();
            builder.Services.AddTransient<CreateFoodPage>();
            builder.Services.AddTransient<BarcodeReaderPage>();
            builder.Services.AddTransient<ProfilePage>();
            builder.Services.AddTransient<CreateDishPage>();
            builder.Services.AddTransient<AdminHomePage>();
            builder.Services.AddTransient<AdminSelectedFood>();


            // DI for ViewModels:

            builder.Services.AddSingleton<CreateFoodPageViewModel>();
            builder.Services.AddSingleton<AddDishPageViewModel>();
            builder.Services.AddSingleton<AddFoodPageViewModel>();
            builder.Services.AddSingleton<CreateGoalPageViewModel>();
            builder.Services.AddSingleton<HomePageViewModel>();
            builder.Services.AddSingleton<AdminHomePageViewModel>();
            builder.Services.AddSingleton<CreateDishPageViewModel>();

            // DI for Models:
            builder.Services.AddTransient<User>();

            // Services
#if ANDROID
            string baseUrl = "http://10.0.2.2:5000/";
            //string baseUrl = "http://192.168.8.52:5000/";
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

            builder.Services.AddHttpClient<DishService>(client =>
            {
                client.BaseAddress = new Uri(baseUrl + "api/dish/");
            });

            builder.Services.AddHttpClient<MacroLogService>(client =>
            {
                client.BaseAddress = new Uri(baseUrl + "api/macroLog/");
            });

            builder.Services.AddHttpClient<MacroGoalService>(client =>
            {
                client.BaseAddress = new Uri(baseUrl + "api/macroGoal/");
            });



            return builder.Build();
        }
    }
}
