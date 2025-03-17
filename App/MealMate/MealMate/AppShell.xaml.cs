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
            Routing.RegisterRoute(nameof(HjemmeskaermSide), typeof(HjemmeskaermSide));
            Routing.RegisterRoute(nameof(OpretFoedevareSide), typeof(OpretFoedevareSide));
            Routing.RegisterRoute(nameof(ProfilSide), typeof(ProfilSide));
            Routing.RegisterRoute(nameof(RegistrerMaalSide), typeof(RegistrerMaalSide));
            Routing.RegisterRoute(nameof(TilfoejFoedvareSide), typeof(TilfoejFoedvareSide));
            Routing.RegisterRoute(nameof(OpretRetSide), typeof(OpretRetSide));
            Routing.RegisterRoute(nameof(AdminHomePage), typeof(AdminHomePage));
            Routing.RegisterRoute(nameof(AdminSelectedFood), typeof(AdminSelectedFood));
        }
    }
}
