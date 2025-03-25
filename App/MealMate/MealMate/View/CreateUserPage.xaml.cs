namespace MealMate.View;

public partial class CreateUserPage : ContentPage
{
    private readonly LoginService _loginService;
    private Entry _emailEntry;
    private Entry _usernameEntry;
    private Entry _passwordEntry;

    // Constructor to initialize the page and find UI elements by their names
    public CreateUserPage(LoginService loginService)
    {
        InitializeComponent();
        _loginService = loginService;

        // Find UI elements by their names
        _usernameEntry = this.FindByName<Entry>("usernameRegistrerEntry");
        _emailEntry = this.FindByName<Entry>("emailRegistrerEntry");
        _passwordEntry = this.FindByName<Entry>("passwordRegistrerEntry");
    }

    // Event handler for the CreateUser_Button click
    private async void CreateUser_Button(object sender, EventArgs e)
    {
        // Get the input values from the entries
        string username = _usernameEntry.Text;
        string email = _emailEntry.Text;
        string password = _passwordEntry.Text;

        // Create a new User object with the input values
        var user = new User
        {
            username = username,
            email = email,
            password = password,
            admin = false, // Default to non-admin user

            // Default values for the user profile data
            weight = 0,
            height = 0,
            birthdate = DateTime.Now,
            gender = null
        };
        try
        {
            // Register the user with the provided profile data
            var createdUser = await _loginService.CreateUser(user);
            await DisplayAlert("Success", "", "OK");

            // Log in the user after successful registration
            var loggedInUser = await _loginService.Login(user.email, user.password);
            await Shell.Current.GoToAsync(nameof(CreateUserDataPage), true);
        }
        catch (Exception ex)
        {
            // Display an alert if an error occurs during registration or login
            await DisplayAlert("Error", $"error: {ex.Message}", "OK");
        }
    }

    // Event handler for the login button tap
    private async void GoToLoginpage_Button(object sender, TappedEventArgs e)
    {
        // Navigate back to the Loginpage
        await Shell.Current.GoToAsync("///" + nameof(LoginPage), true);
    }
}