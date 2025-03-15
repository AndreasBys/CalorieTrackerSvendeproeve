using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace MealMate.View;

[QueryProperty(nameof(User), "user")]
public partial class RegistrerProfildataSide : ContentPage
{
    private User _user;
    private readonly LoginService _loginService;
    private DatePicker _birthdateEntry;
    private Entry _heightEntry;
    private Entry _weightEntry;
    private Picker _genderEntry;

    public User User
    {
        get => _user;
        set
        {
            _user = value;
            OnPropertyChanged();
        }
    }

    public RegistrerProfildataSide(LoginService loginService)
    {
        InitializeComponent();
        _loginService = loginService;

        _weightEntry = this.FindByName<Entry>("weightRegistrerEntry");
        _heightEntry = this.FindByName<Entry>("heightRegistrerEntry");
        _birthdateEntry = this.FindByName<DatePicker>("birthdateRegistrerEntry");
        _genderEntry = this.FindByName<Picker>("genderRegistrerEntry");
    }

    private async void gemProfilData_knap(object sender, EventArgs e)
    {
        _user.weight = Convert.ToInt32(_weightEntry.Text);
        _user.height = Convert.ToInt32(_heightEntry.Text);
        _user.birthdate = _birthdateEntry.Date.ToString("dd-MM-yyyy");
        _user.gender = _genderEntry.SelectedItem as string;

        try
        {
            var registeredUser = await _loginService.Register(_user);
            await DisplayAlert("Success", "Profile created - logging in", "OK");

            // Logging in the user
            var loggedInUser = await _loginService.Login(_user.email, _user.password);
            await Shell.Current.GoToAsync(nameof(HjemmeskaermSide), true);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"error: {ex.Message}", "OK");
        }
    }
}