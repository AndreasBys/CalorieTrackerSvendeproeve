namespace MealMate.View;

public partial class ProfilePage : ContentPage
{
	public ProfilePage()
	{
		InitializeComponent();
	}

    private async void Logout_knap(object sender, EventArgs e)
    {
        // Check if "Remember Me" is not checked
        if (!Preferences.Get("RememberMe", false))
        {
            // Clear stored user information
            Preferences.Remove("Email");
            Preferences.Remove("Password");
            Preferences.Set("RememberMe", false);
        }

        // Clear the auth_token from SecureStorage
        SecureStorage.Remove("auth_token");

        // Navigate back to the Loginpage
        await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
    }

    private async void saetMaal_knap(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(CreateGoalPage), true);
    }

    private async void saetProfildata_knap(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(CreateUserDataPage), true);
    }
}