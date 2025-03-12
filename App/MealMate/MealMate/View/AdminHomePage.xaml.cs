using MealMate.ViewModels;

namespace MealMate.View;

public partial class AdminHomePage : ContentPage
{
	public AdminHomePage(FoodViewModel foodViewModel)
	{
		InitializeComponent();
		BindingContext = foodViewModel;
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

        // Navigate back to the login page
        await Shell.Current.GoToAsync($"//{nameof(StartSkaermSide)}");
    }

}