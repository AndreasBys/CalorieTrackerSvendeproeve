namespace MealMate.View;

public partial class OpretRetSide : ContentPage
{
	public OpretRetSide(OpretRetViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
	}

    private async void annullerRetOprettelse_knap(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(TilfoejFoedvareSide), true);
    }

    private async void oprettetRet_knap(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(TilfoejFoedvareSide), true);
    }

    
}