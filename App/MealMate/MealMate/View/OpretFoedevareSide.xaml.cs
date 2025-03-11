namespace MealMate.View;

public partial class OpretFoedevareSide : ContentPage
{
    private readonly AddFoodViewModel _viewModel;
    public OpretFoedevareSide(AddFoodViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = _viewModel = viewModel;
	}

    private async void oprettetFoedevare_knap(object sender, EventArgs e)
    {
        // Makes code work async
        if (_viewModel.FoodDetails._id == null && 
            _viewModel.CreateFood is AsyncRelayCommand createFoodCommand)
        {
            await createFoodCommand.ExecuteAsync(null);
        }
        // Skal sende den oprettede macroLog til home page
        await Shell.Current.GoToAsync(nameof(HjemmeskaermSide), true);
    }

    private async void annullerFoedevareOprettelse_knap(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}