namespace MealMate.View;

public partial class OpretFoedevareSide : ContentPage
{
	public OpretFoedevareSide()
	{
		InitializeComponent();
	}

    private async void oprettetFoedevare_knap(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync(nameof(TilfoejFoedvareSide), true);
    }

    private async void annullerFoedevareOprettelse_knap(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(TilfoejFoedvareSide), true);
    }
}