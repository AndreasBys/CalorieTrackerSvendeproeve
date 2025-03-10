namespace MealMate.View;

public partial class Navigationsbar : ContentView	
{
	public Navigationsbar()
	{
		InitializeComponent();
	}

    private async void hjemmesideSkearm_knap(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(HjemmeskaermSide), true);
    }

    private async void tilfoejMad_knap(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(TilfoejFoedvareSide), true);
    }

    private async void profilside_knap(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ProfilSide), true);
    }
}