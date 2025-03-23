namespace MealMate.View;

public partial class NavBar : ContentView	
{

    // Constructor to initialize the navigation bar
    public NavBar()
    {
        InitializeComponent();
    }

    // Event handler for the home screen button click
    private async void hjemmesideSkearm_knap(object sender, EventArgs e)
    {
        string currentPage = Shell.Current.CurrentState.Location.ToString();
        var targetPage = nameof(HomePage);

        // Navigate to the home screen if not already on it
        if (!currentPage.EndsWith(targetPage))
        {
            await Shell.Current.GoToAsync(targetPage, true);
        }
    }

    // Event handler for the add food button click
    private async void tilfoejMad_knap(object sender, EventArgs e)
    {
        string currentPage = Shell.Current.CurrentState.Location.ToString();
        var targetPage = nameof(AddFoodPage);

        // Navigate to the add food screen if not already on it
        if (!currentPage.EndsWith(targetPage))
        {
            await Shell.Current.GoToAsync(targetPage, true);
        }
    }

    // Event handler for the profile screen button click
    private async void profilside_knap(object sender, EventArgs e)
    {
        string currentPage = Shell.Current.CurrentState.Location.ToString();
        var targetPage = nameof(ProfilePage);

        // Navigate to the profile screen if not already on it
        if (!currentPage.EndsWith(targetPage))
        {
            await Shell.Current.GoToAsync(targetPage, true);
        }
    }
}



