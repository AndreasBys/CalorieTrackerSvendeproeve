using Microsoft.Maui.Controls.PlatformConfiguration;

namespace MealMate.View;

public partial class HjemmeSkaermSide : ContentPage
{
	public HjemmeSkaermSide()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);

        }
}