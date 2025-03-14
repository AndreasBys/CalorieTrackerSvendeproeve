namespace MealMate.View;

public partial class RegistrerSide : ContentPage
{
    private readonly UserService _userService;
    private Entry _emailEntry;
    private Entry _usernameEntry;
    private Entry _passwordEntry;

    public RegistrerSide(UserService userService)
	{
		InitializeComponent();
        _userService = userService;

        _usernameEntry = this.FindByName<Entry>("usernameRegistrerEntry");
        _emailEntry = this.FindByName<Entry>("emailRegistrerEntry");
        _passwordEntry = this.FindByName<Entry>("passwordRegistrerEntry");
    }

    private async void Registrer_knap(object sender, EventArgs e)
    {

        string username = _usernameEntry.Text;
        string email = _emailEntry.Text;
        string password = _passwordEntry.Text;

        var user = new User
        {
            username = username,
            email = email,
            password = password,
            admin = false 
        };

        //var registeredUser = await _loginService.Register(user);
        await Shell.Current.GoToAsync($"{nameof(RegistrerProfildataSide)}", true, new Dictionary<string, object>{ { "user", user } });

    }

    private async void logindForside_knap(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("///" + nameof(StartSkaermSide), true);
    }
}