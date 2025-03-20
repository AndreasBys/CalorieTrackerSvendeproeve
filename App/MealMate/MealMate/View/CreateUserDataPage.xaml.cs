using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace MealMate.View;

[QueryProperty(nameof(User), "user")]
public partial class CreateUserDataPage : ContentPage
{
    private User _user;
    private readonly LoginService _loginService;
    private DatePicker _birthdateEntry;
    private Entry _heightEntry;
    private Entry _weightEntry;
    private Picker _genderEntry;

    // Property to get and set the User object passed from the previous page
    public User User
    {
        get => _user;
        set
        {
            _user = value;
            OnPropertyChanged();
        }
    }

    // Constructor to initialize the page and find UI elements by their names
    public CreateUserDataPage(LoginService loginService)
    {
        InitializeComponent();
        _loginService = loginService;

        // Find UI elements by their names
        _weightEntry = this.FindByName<Entry>("weightRegistrerEntry");
        _heightEntry = this.FindByName<Entry>("heightRegistrerEntry");
        _birthdateEntry = this.FindByName<DatePicker>("birthdateRegistrerEntry");
        _genderEntry = this.FindByName<Picker>("genderRegistrerEntry");
    }

    // Event handler for the CreateUser_Button click
    private async void CreateUser_Button(object sender, EventArgs e)
    {
        // Get the input values from the entries and update the User object
        _user.weight = Convert.ToInt32(_weightEntry.Text);
        _user.height = Convert.ToInt32(_heightEntry.Text);
        _user.birthdate = _birthdateEntry.Date.ToString("dd-MM-yyyy");
        _user.gender = _genderEntry.SelectedItem as string;

        try
        {
            // Register the user with the provided profile data
            var registeredUser = await _loginService.CreateUser(_user);
            await DisplayAlert("Success", "Profile created - logging in", "OK");

            // Log in the user after successful registration
            var loggedInUser = await _loginService.Login(_user.email, _user.password);
            await Shell.Current.GoToAsync(nameof(HomePage), true);
        }
        catch (Exception ex)
        {
            // Display an alert if an error occurs during registration or login
            await DisplayAlert("Error", $"error: {ex.Message}", "OK");
        }
    }
}