namespace MealMate.View;

public partial class RegistrerSide : ContentPage
{
    private readonly UserService _userService;
    private Entry _emailEntry;
    private Entry _usernameEntry;
    private Entry _passwordEntry;

    // Constructor to initialize the page and find UI elements by their names
    public RegistrerSide(UserService userService)
    {
        InitializeComponent();
        _userService = userService;

        // Find UI elements by their names
        _usernameEntry = this.FindByName<Entry>("usernameRegistrerEntry");
        _emailEntry = this.FindByName<Entry>("emailRegistrerEntry");
        _passwordEntry = this.FindByName<Entry>("passwordRegistrerEntry");
    }

    // Event handler for the register button click
    private async void Registrer_knap(object sender, EventArgs e)
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
            admin = false // Default to non-admin user
        };

        // Navigate to the RegistrerProfildataSide page, passing the user object
        await Shell.Current.GoToAsync($"{nameof(RegistrerProfildataSide)}", true, new Dictionary<string, object> { { "user", user } });
    }

    // Event handler for the login button tap
    private async void logindForside_knap(object sender, TappedEventArgs e)
    {
        // Navigate back to the StartSkaermSide page
        await Shell.Current.GoToAsync("///" + nameof(StartSkaermSide), true);
    }
}