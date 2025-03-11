using MealMate.View;

namespace MealMate
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();



            Routing.RegisterRoute(nameof(HjemmeskaermSide), typeof(HjemmeskaermSide));
            Routing.RegisterRoute(nameof(OpretFoedevareSide), typeof(OpretFoedevareSide));
            Routing.RegisterRoute(nameof(ProfilSide), typeof(ProfilSide));
            Routing.RegisterRoute(nameof(RegistrerMaalSide), typeof(RegistrerMaalSide));
            Routing.RegisterRoute(nameof(RegistrerProfildataSide), typeof(RegistrerProfildataSide));
            Routing.RegisterRoute(nameof(RegistrerSide), typeof(RegistrerSide));
            //Routing.RegisterRoute(nameof(StartSkaermSide), typeof(StartSkaermSide));
            Routing.RegisterRoute(nameof(TilfoejFoedvareSide), typeof(TilfoejFoedvareSide));
            Routing.RegisterRoute(nameof(OpretRetSide), typeof(OpretRetSide));
        }
    }
}
