using MealMate.ViewModels;

namespace MealMate.View;

public partial class AdminHomePage : ContentPage
{
    private readonly FoodViewModel _foodViewModel;

    // Constructor to initialize the page and set the BindingContext to the provided FoodViewModel
    public AdminHomePage(FoodViewModel foodViewModel)
    {
        InitializeComponent();
        BindingContext = foodViewModel;
        _foodViewModel = foodViewModel;
        OnPropertyChanged(nameof(_foodViewModel.Foods));
    }

    // Override OnAppearing to load the food list when the page appears
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (_foodViewModel.Foods.Count == 0)
            _foodViewModel.GetFoods();
    }

    // Event handler for the logout button click
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
        await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
    }

    // Event handler for selecting a food item
    private async void SelectFood_knap(object sender, EventArgs e)
    {
        if (sender is ImageButton button && button.BindingContext is Food selectedFood)
        {
            // Navigate to the AdminSelectedFood page, passing the selected food item
            await Shell.Current.GoToAsync($"{nameof(AdminSelectedFood)}", true, new Dictionary<string, object> { { "SelectedFood", selectedFood } });
        }
    }

    // Event handler for creating a new food item
    private async void OpretNy_knap(object sender, EventArgs e)
    {
        // Navigate to the OpretFoedevareSide page
        await Shell.Current.GoToAsync($"{nameof(OpretFoedevareSide)}");
    }
}