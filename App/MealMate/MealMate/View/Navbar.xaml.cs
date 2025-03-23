namespace MealMate.View;

public partial class NavBar : ContentView	
{
	public NavBar()
	{
		InitializeComponent();
	}

    private async void hjemmesideSkearm_knap(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(HomePage), true);
    }

    private async void tilfoejMad_knap(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddFoodPage), true);
    }

    private async void profilside_knap(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ProfilePage), true);
    }
}