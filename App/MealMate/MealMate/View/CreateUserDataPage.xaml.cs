using System.Threading.Tasks;
using MealMate.Services;
using Microsoft.Maui.Controls;

namespace MealMate.View;

public partial class CreateUserDataPage : ContentPage
{
    private User _user;
    private readonly UserService _userService;
    private DatePicker _birthdateEntry;
    private Entry _heightEntry;
    private Entry _weightEntry;
    private Picker _genderEntry;

    // Constructor to initialize the page and find UI elements by their names
    public CreateUserDataPage(UserService userService)
    {
        InitializeComponent();
        _userService = userService;

        // Find UI elements by their names
        _weightEntry = this.FindByName<Entry>("weightRegistrerEntry");
        _heightEntry = this.FindByName<Entry>("heightRegistrerEntry");
        _birthdateEntry = this.FindByName<DatePicker>("birthdateRegistrerEntry");
        _genderEntry = this.FindByName<Picker>("genderRegistrerEntry");
    }

    // Event handler for the CreateUser_Button click
    private async void UpdateUser_Button(object sender, EventArgs e)
    {
        // Get the user id from the SecureStorage
        var _userId = await SecureStorage.GetAsync("user_id");

        var user = await _userService.GetUserById(_userId);
        // Get the input values from the entries and update the User object
        var _user = new User
        {
            _id = user._id,
            username = user.username,
            email = user.email,
            password = user.password,
            weight = Convert.ToInt32(_weightEntry.Text),
            height = Convert.ToInt32(_heightEntry.Text),
            birthdate = _birthdateEntry.Date,
            gender = _genderEntry.SelectedItem as string
        };

        

        try
        {
            // Register the user with the provided profile data
            var updatedUser = await _userService.UpdateUser(_user, _userId);
            await DisplayAlert("Success", "Success", "OK");

            // Navigates user to the CreateGoalPage after successful update
            await Shell.Current.GoToAsync(nameof(CreateGoalPage), true);
        }
        catch (Exception ex)
        {
            // Display an alert if an error occurs during registration or login
            await DisplayAlert("Error", $"error: {ex.Message}", "OK");
        }


    }
}