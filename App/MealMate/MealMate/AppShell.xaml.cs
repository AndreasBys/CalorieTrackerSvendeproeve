using MealMate.View;

namespace MealMate
{
    public partial class AppShell : Shell
    {
        private readonly List<string> _registeredRoutes = new();
        public AppShell()
        {
            InitializeComponent();

            RegisterRoutes();
            // Log registered routes
            LogRegisteredRoutes();

            // Create a new ShellContent instance
            var loginShellContent = new ShellContent
            {
                Title = "Home",
                ContentTemplate = new DataTemplate(typeof(LoginPage)),
                Route = "LoginPage"
            };

            // Add the ShellContent to the Shell
            Items.Add(loginShellContent);

        }

        private void RegisterRoutes()
        {
            RegisterRoute(nameof(CreateUserPage), typeof(CreateUserPage));
            RegisterRoute(nameof(CreateUserDataPage), typeof(CreateUserDataPage));
            RegisterRoute(nameof(HomePage), typeof(HomePage));
            RegisterRoute(nameof(AddFoodPage), typeof(AddFoodPage));
            RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
            RegisterRoute(nameof(CreateGoalPage), typeof(CreateGoalPage));
            RegisterRoute(nameof(CreateFoodPage), typeof(CreateFoodPage));

            RegisterRoute(nameof(AddDishPage), typeof(AddDishPage));
            RegisterRoute(nameof(CreateDishPage), typeof(CreateDishPage));

            RegisterRoute(nameof(AdminHomePage), typeof(AdminHomePage));
            RegisterRoute(nameof(AdminSelectedFood), typeof(AdminSelectedFood));
            RegisterRoute(nameof(BarcodeLaeserSide), typeof(BarcodeLaeserSide));
        }

        private void RegisterRoute(string route, Type type)
        {
            if (!_registeredRoutes.Contains(route))
            {
                Routing.RegisterRoute(route, type);
                _registeredRoutes.Add(route);
            }
            else
            {
                Debug.WriteLine($"Route '{route}' is already registered.");
            }
        }

        private void LogRegisteredRoutes()
        {
            foreach (var route in _registeredRoutes)
            {
                Debug.WriteLine($"Registered Route: {route}");
            }
        }

    }
}
