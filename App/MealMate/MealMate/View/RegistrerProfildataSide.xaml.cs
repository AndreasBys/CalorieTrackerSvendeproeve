using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace MealMate.View;

[QueryProperty(nameof(User), "user")]
public partial class RegistrerProfildataSide : ContentPage
{
	public RegistrerProfildataSide(RegistrerProfildataViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    // Event handler for the save profile data button click
    private async void gemProfilData_knap(object sender, EventArgs e)
    {
        // Get the input values from the entries and update the User object
        _user.weight = Convert.ToInt32(_weightEntry.Text);
        _user.height = Convert.ToInt32(_heightEntry.Text);
        _user.birthdate = _birthdateEntry.Date.ToString("dd-MM-yyyy");
        _user.gender = _genderEntry.SelectedItem as string;

        try
        {
            // Register the user with the provided profile data
            var registeredUser = await _loginService.Register(_user);
            await DisplayAlert("Success", "Profile created - logging in", "OK");

            // Log in the user after successful registration
            var loggedInUser = await _loginService.Login(_user.email, _user.password);
            await Shell.Current.GoToAsync(nameof(HjemmeskaermSide), true);
        }
        catch (Exception ex)
        {
            // Display an alert if an error occurs during registration or login
            await DisplayAlert("Error", $"error: {ex.Message}", "OK");
        }
    }
}