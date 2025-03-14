using MealMate.Services;
using Microsoft.Maui.Controls.PlatformConfiguration;

namespace MealMate.View;

public partial class StartSkaermSide : ContentPage
{
    private readonly LoginService _loginService;

    private Entry _emailEntry;
    private Entry _passwordEntry;

    public StartSkaermSide(LoginService loginService)
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _loginService = loginService ?? throw new ArgumentNullException(nameof(loginService));

        // Delay the initialization of the entries to ensure they are properly loaded
        this.Loaded += (s, e) =>
        {
            _emailEntry = this.FindByName<Entry>("emailEntry");
            _passwordEntry = this.FindByName<Entry>("passwordEntry");
        };
    }

    private async void Logind_knap(object sender, EventArgs e)
    {
        try
        {
            string email = _emailEntry.Text;
            string password = _passwordEntry.Text;

            var user = await _loginService.Login(email, password);
            if (user != null)
            {
                await Shell.Current.GoToAsync(nameof(HjemmeskaermSide), true);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Login Failed", "Invalid username or password.", "OK");
            }
        }
        catch (HttpRequestException httpEx)
        {
            Debug.WriteLine($"Login failed: {httpEx.Message}");
            await Application.Current.MainPage.DisplayAlert("Network Error", "Please check your internet connection and try again.", "OK");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Login failed: {ex.Message}");
            await Application.Current.MainPage.DisplayAlert("Could not login!", ex.Message, "OK");
        }
    }

    private async void registrerBruger_knap(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(RegistrerSide), true);
    }
}