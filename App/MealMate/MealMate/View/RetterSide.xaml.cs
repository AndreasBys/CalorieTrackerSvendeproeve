namespace MealMate.View;

public partial class RetterSide : ContentPage
{
	public RetterSide()
	{
		InitializeComponent();
	}

    

    private async void tilfoejFoedevare_knap(object sender, EventArgs e)
    {

    }

    private async void annullerRetOprettelse_knap(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(TilfoejFoedvareSide), true);
    }

    private async void oprettetRet_knap(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(OpretFoedevareSide), true);
    }
}