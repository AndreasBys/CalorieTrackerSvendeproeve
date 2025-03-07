namespace MealMate.View;

public partial class RetterSide : ContentPage
{
	public RetterSide()
	{
		InitializeComponent();
	}

    private async void oprettetFoedevare_knap(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(FoedevareSide), true);
    }

    private async void annullerFoedevareOprettelse_knap(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(FoedevareSide), true);
    }
}