namespace MealMate.View;

public partial class ProfilSide : ContentPage
{
	public ProfilSide()
	{
		InitializeComponent();
	}

    private async void logud_knap(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//StartSkaermSide", true);
    }

    private async void saetMaal_knap(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(RegistrerMaalSide), true);
    }

    private async void saetProfildata_knap(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(RegistrerProfildataSide), true);
    }
}