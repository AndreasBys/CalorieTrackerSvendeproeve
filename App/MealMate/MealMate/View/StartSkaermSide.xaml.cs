using MealMate.Services;
using Microsoft.Maui.Controls.PlatformConfiguration;

namespace MealMate.View;

public partial class StartSkaermSide : ContentPage
{
    private readonly LoginService _loginService;
    private bool _isPasswordHidden = true;

    private Entry _emailEntry;
    private Entry _passwordEntry;
    private CheckBox _rememberMeCheckBox;
    private ImageButton _togglePasswordButton;

    // Constructor to initialize the page and load saved login information if "Remember Me" was checked
    public StartSkaermSide(LoginService loginService)
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _loginService = loginService ?? throw new ArgumentNullException(nameof(loginService));

        // Find UI elements by their names
        _emailEntry = this.FindByName<Entry>("emailEntry");
        _passwordEntry = this.FindByName<Entry>("passwordEntry");
        _rememberMeCheckBox = this.FindByName<CheckBox>("rememberMeCheckBox");

        // Load saved login information if "Remember Me" was checked
        if (Preferences.Get("RememberMe", false))
        {
            _emailEntry.Text = Preferences.Get("Email", string.Empty);
            _passwordEntry.Text = Preferences.Get("Password", string.Empty);
            _rememberMeCheckBox.IsChecked = true;
        }
    }

    // Override OnAppearing to clear email and password entries if "Remember Me" is not checked
    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Clear email and password entries if "Remember Me" is not checked
        if (!Preferences.Get("RememberMe", false))
        {
            _emailEntry.Text = string.Empty;
            _passwordEntry.Text = string.Empty;
            //_rememberMeCheckBox.IsChecked = false;
        }
    }

    // Event handler for the login button click
    private async void Logind_knap(object sender, EventArgs e)
    {
        try
        {
            string email = _emailEntry.Text;
            string password = _passwordEntry.Text;

            // Attempt to log in using the provided email and password
            var user = await _loginService.Login(email, password);
            if (user != null)
            {
                // Save login information if "Remember Me" is checked
                if (_rememberMeCheckBox.IsChecked)
                {
                    Preferences.Set("Email", email);
                    Preferences.Set("Password", password);
                    Preferences.Set("RememberMe", true);
                }
                else
                {
                    Preferences.Remove("Email");
                    Preferences.Remove("Password");
                    Preferences.Set("RememberMe", false);
                }

                // Navigate to the appropriate page based on the user's admin status
                if (user.admin == true)
                    await Shell.Current.GoToAsync(nameof(AdminHomePage), true);
                else
                    await Shell.Current.GoToAsync(nameof(HjemmeskaermSide), true);
            }
            else
            {
                // Display an alert if login fails
                await Application.Current.MainPage.DisplayAlert("Login Failed", "Invalid username or password.", "OK");
            }
        }
        catch (HttpRequestException httpEx)
        {
            // Handle network errors
            Debug.WriteLine($"Login failed: {httpEx.Message}");
            await Application.Current.MainPage.DisplayAlert("Network Error", "Please check your internet connection and try again.", "OK");
        }
        catch (Exception ex)
        {
            // Handle other exceptions
            Debug.WriteLine($"Login failed: {ex.Message}");
            await Application.Current.MainPage.DisplayAlert("Could not login!", ex.Message, "OK");
        }
    }

    // Event handler for the register button tap
    private async void registrerBruger_knap(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(RegistrerSide), true);
    }

    // Event handler to toggle password visibility
    private void TogglePasswordVisibility(object sender, EventArgs e)
    {
        _isPasswordHidden = !_isPasswordHidden;
        //_passwordEntry.IsPassword = _isPasswordHidden;
        //if (togglePasswordButton != null)
        //    togglePasswordButton.Source = _isPasswordHidden ? "eye_show.png" : "eye_off.png";
    }
}