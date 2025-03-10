using MealMate.Services;
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
        //FoodService fS = new FoodService();

        //var foods = await fS.getFoodList("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI2N2NiMjFiNzg4ZjU1ZmZlMTliMjM5YTYiLCJpYXQiOjE3NDEzNjU2ODcsImV4cCI6MTc0MTM2OTI4N30.BSJST4FZ8OGwMvS3q8CCCdaEDN2NQxrAbLjQz4ZjObU");

        await Shell.Current.GoToAsync(nameof(HjemmeskaermSide), true);
    }

    private async void registrerBruger_knap(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(RegistrerSide), true);
    }
}