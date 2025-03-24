using MealMate.ViewModels;
using Syncfusion.Maui.Core.Carousel;

namespace MealMate.View;

public partial class AdminHomePage : ContentPage
{
    private readonly AdminHomePageViewModel _viewModel;

    // Constructor to initialize the page and set the BindingContext to the provided FoodViewModel
    public AdminHomePage(AdminHomePageViewModel ViewModel)
    {
        InitializeComponent();
        BindingContext = ViewModel;
        _viewModel = ViewModel;
        OnPropertyChanged(nameof(_viewModel.Foods));
    }

    // Override OnAppearing to load the food list when the page appears
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.GetAllFoods();
    }

    private void OnSearch(object sender, TextChangedEventArgs e)
    {
        var viewModel = BindingContext as AddFoodPageViewModel;
        if (viewModel != null)
        {
            viewModel.SearchText = e.NewTextValue;
            viewModel.SearchFood.Execute(null);
        }
    }

    // Event handler for the logout button click
    private async void Logout_button(object sender, EventArgs e)
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
    private async void SelectFood_button(object sender, EventArgs e)
    {
        if (sender is ImageButton button && button.BindingContext is Food selectedFood)
        {
            // Navigate to the AdminSelectedFood page, passing the selected food item
            await Shell.Current.GoToAsync($"{nameof(AdminSelectedFood)}", true, new Dictionary<string, object> { { "SelectedFood", selectedFood } });
        }
    }

    // Event handler for creating a new food item
    private async void CreateNewFood_button(object sender, EventArgs e)
    {
        // Navigate to the OpretFoedevareSide page
        await Shell.Current.GoToAsync($"{nameof(CreateFoodPage)}");
    }
}