using Microsoft.Maui.Controls.PlatformConfiguration;

namespace MealMate.View;

public partial class StartSkaermSide : ContentPage
{
    public StartSkaermSide()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);

    }


    private  async void Logind_knap(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(HjemmeskaermSide), true);
    }

    private async void registrerBruger_knap(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(RegistrerSide), true);
    }
}