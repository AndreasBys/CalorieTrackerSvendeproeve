using MealMate.View;

namespace MealMate
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(FoedevareSide), typeof(FoedevareSide));
        }
    }
}
