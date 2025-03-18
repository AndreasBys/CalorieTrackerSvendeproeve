namespace MealMate.View;

public partial class Navigationsbar : ContentView	
{
    string baseUrl = "//" + nameof(LoginPage) + "/";

    public Navigationsbar()
	{
		InitializeComponent();

    }

    private async void hjemmesideSkearm_knap(object sender, EventArgs e)
    {
        string currentPage = Shell.Current.CurrentState.Location.ToString();
        var targetPage = nameof(HjemmeskaermSide);

        if (!currentPage.EndsWith(targetPage))
        {
            await Shell.Current.GoToAsync(baseUrl + targetPage, true);
        }
    }

    private async void tilfoejMad_knap(object sender, EventArgs e)
    {
        string currentPage = Shell.Current.CurrentState.Location.ToString();
        var targetPage = nameof(TilfoejFoedvareSide);

        if (!currentPage.EndsWith(targetPage))
        {
            await Shell.Current.GoToAsync(baseUrl + nameof(HjemmeskaermSide) + "/" + targetPage, true);
        }
    }

    private async void profilside_knap(object sender, EventArgs e)
    {
        string currentPage = Shell.Current.CurrentState.Location.ToString();
        var targetPage = nameof(ProfilSide);

        if (!currentPage.EndsWith(targetPage))
        {
            await Shell.Current.GoToAsync(baseUrl + nameof(HjemmeskaermSide) + "/" + targetPage, true);
        }
    }
}