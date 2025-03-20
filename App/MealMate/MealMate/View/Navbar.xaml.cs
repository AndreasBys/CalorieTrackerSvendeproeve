namespace MealMate.View;

public partial class NavBar : ContentView	
{
	public NavBar()
	{
		InitializeComponent();
	}
    // Base URL for navigation
    string baseUrl = "//" + nameof(LoginPage) + "/";

    // Constructor to initialize the navigation bar
    public Navigationsbar()
    {
        InitializeComponent();
    }

    // Event handler for the home screen button click
    private async void hjemmesideSkearm_knap(object sender, EventArgs e)
    {
        string currentPage = Shell.Current.CurrentState.Location.ToString();
        var targetPage = nameof(HjemmeskaermSide);

        // Navigate to the home screen if not already on it
        if (!currentPage.EndsWith(targetPage))
        {
            await Shell.Current.GoToAsync(baseUrl + targetPage, true);
        }
    }

    // Event handler for the add food button click
    private async void tilfoejMad_knap(object sender, EventArgs e)
    {
        string currentPage = Shell.Current.CurrentState.Location.ToString();
        var targetPage = nameof(TilfoejFoedvareSide);

        // Navigate to the add food screen if not already on it
        if (!currentPage.EndsWith(targetPage))
        {
            await Shell.Current.GoToAsync(baseUrl + nameof(HjemmeskaermSide) + "/" + targetPage, true);
        }
    }

    // Event handler for the profile screen button click
    private async void profilside_knap(object sender, EventArgs e)
    {
        string currentPage = Shell.Current.CurrentState.Location.ToString();
        var targetPage = nameof(ProfilSide);

        // Navigate to the profile screen if not already on it
        if (!currentPage.EndsWith(targetPage))
        {
            await Shell.Current.GoToAsync(baseUrl + nameof(HjemmeskaermSide) + "/" + targetPage, true);
        }
    }
}



