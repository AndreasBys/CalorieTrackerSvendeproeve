using Microsoft.Maui.Controls.PlatformConfiguration;

namespace MealMate.View;

public partial class StartSkaermSide : ContentPage
{
	public StartSkaermSide()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);

        }
}