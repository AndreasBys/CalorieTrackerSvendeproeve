namespace MealMate.View;

public partial class NavBar : ContentView	
{
    string baseUrl = "//" + nameof(LoginPage) + "/";

    public NavBar()
	{
		InitializeComponent();

    }

    private async void hjemmesideSkearm_knap(object sender, EventArgs e)
    {
        string currentPage = Shell.Current.CurrentState.Location.ToString();
        var targetPage = nameof(HomePage);

        if (!currentPage.EndsWith(targetPage))
        {
            await Shell.Current.GoToAsync(baseUrl + targetPage, true);
        }
    }

    private async void tilfoejMad_knap(object sender, EventArgs e)
    {
        string currentPage = Shell.Current.CurrentState.Location.ToString();
        var targetPage = nameof(AddFoodPage);

        if (!currentPage.EndsWith(targetPage))
        {
            await Shell.Current.GoToAsync(baseUrl + nameof(HomePage) + "/" + targetPage, true);
        }
    }

    private async void profilside_knap(object sender, EventArgs e)
    {
        string currentPage = Shell.Current.CurrentState.Location.ToString();
        var targetPage = nameof(ProfilePage);

        if (!currentPage.EndsWith(targetPage))
        {
            await Shell.Current.GoToAsync(baseUrl + nameof(HomePage) + "/" + targetPage, true);
        }
    }
}