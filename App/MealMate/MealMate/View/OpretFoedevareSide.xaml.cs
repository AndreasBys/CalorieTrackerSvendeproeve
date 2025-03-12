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
        if (_viewModel.FoodDetails.name == null || _viewModel.FoodDetails.name == "")
        {
            await Application.Current.MainPage.DisplayAlert("Error!", "Giv fødevaren et navn!", "OK");
            return;
        }
        // Makes code work async
        if (_viewModel.FoodDetails._id == null && 
            _viewModel.CreateFood is AsyncRelayCommand createFoodCommand)
        {
            await createFoodCommand.ExecuteAsync(null);
        }
        if (_viewModel.MacroWeight == null || _viewModel.MacroWeight == "")
        {
            await Application.Current.MainPage.DisplayAlert("Error!", "Gram spist er tom!", "OK");
            return;
        }
        if (_viewModel.CreateMacroLog is AsyncRelayCommand createMacroLogCommand)
        {
            await createMacroLogCommand.ExecuteAsync(null);
        }
        _viewModel.FoodDetails = new();
        _viewModel.MacroWeight = null;
        // Skal sende den oprettede macroLog til home page
        await Shell.Current.GoToAsync("..", true);
    }

    private async void annullerFoedevareOprettelse_knap(object sender, EventArgs e)
    {
        _viewModel.FoodDetails = new();
        _viewModel.MacroWeight = null;
        await Shell.Current.GoToAsync("..", true);
    }
}