using MealMate.ViewModels;

namespace MealMate.View;

public partial class AdminHomePage : ContentPage
{

    private readonly FoodViewModel _foodViewModel;

    public AdminHomePage(FoodViewModel foodViewModel)
	{
		InitializeComponent();
        BindingContext = foodViewModel;
        _foodViewModel = foodViewModel;
        OnPropertyChanged(nameof(_foodViewModel.Foods));
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _foodViewModel.GetFoods();
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

        // Navigate back to the login page
        await Shell.Current.GoToAsync($"//{nameof(StartSkaermSide)}");
    }

    private async void SelectFood_knap(object sender, EventArgs e)
    {
        if (sender is ImageButton button && button.BindingContext is Food selectedFood)
        {
            await Shell.Current.GoToAsync($"{nameof(AdminSelectedFood)}", true, new Dictionary<string, object> { { "SelectedFood", selectedFood } });
        }
    }

    private async void OpretNy_knap(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(OpretFoedevareSide)}");
    }

}