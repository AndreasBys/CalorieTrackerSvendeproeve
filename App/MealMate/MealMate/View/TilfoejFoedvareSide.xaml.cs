namespace MealMate.View;

public partial class TilfoejFoedvareSide : ContentPage
{
	public TilfoejFoedvareSide()
	{
		InitializeComponent();
	}

    private async void opretFoedevare_knap(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync(nameof(OpretFoedevareSide), true);
    }

    private async void aendrerFoedevare_knap(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(OpretFoedevareSide), true);
    }
}