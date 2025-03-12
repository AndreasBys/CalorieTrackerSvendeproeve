namespace MealMate.View;

public partial class ProfilSide : ContentPage
{
	public ProfilSide()
	{
		InitializeComponent();
	}

    private async void logud_knap(object sender, EventArgs e)
    {
        // Check if "Remember Me" is not checked
        if (!Preferences.Get("RememberMe", false))
        {
            // Clear stored user information
            Preferences.Remove("Email");
            Preferences.Remove("Password");
            Preferences.Set("RememberMe", false);
        }

        // Navigate back to the login page
        await Shell.Current.GoToAsync($"//{nameof(StartSkaermSide)}");
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