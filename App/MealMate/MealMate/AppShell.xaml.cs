using MealMate.View;

namespace MealMate
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();



            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(CreateUserPage), typeof(CreateUserPage));
            Routing.RegisterRoute(nameof(CreateUserDataPage), typeof(CreateUserDataPage));
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(CreateFoodPage), typeof(CreateFoodPage));
            Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
            Routing.RegisterRoute(nameof(CreateGoalPage), typeof(CreateGoalPage));
            Routing.RegisterRoute(nameof(AddFoodPage), typeof(AddFoodPage));
            Routing.RegisterRoute(nameof(CreateDishPage), typeof(CreateDishPage));
            Routing.RegisterRoute(nameof(AdminHomePage), typeof(AdminHomePage));
            Routing.RegisterRoute(nameof(AdminSelectedFood), typeof(AdminSelectedFood));
        }
    }
}
