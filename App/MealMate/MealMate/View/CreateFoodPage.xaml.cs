namespace MealMate.View;

public partial class CreateFoodPage : ContentPage
{
    // ViewModel for managing the food item
    private readonly CreateFoodPageViewModel _viewModel;
    public CreateFoodPage(CreateFoodPageViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    // Event handler for the create food button click
    private async void oprettetFoedevare_knap(object sender, EventArgs e)
    {
        // Validate the food name
        if (_viewModel.FoodDetails.name == null || _viewModel.FoodDetails.name == "")
        {
            await Application.Current.MainPage.DisplayAlert("Error!", "Giv fødevaren et navn!", "OK");
            return;
        }

        // Create the food item if it doesn't already exist
        if (_viewModel.FoodDetails._id == null &&

            _viewModel.CreateFood is AsyncRelayCommand createFoodCommand)
        {
            await createFoodCommand.ExecuteAsync(null);
        }

        // Validate the macro weight
        if (_viewModel.MacroWeight == null || _viewModel.MacroWeight == "")
        {
            await Application.Current.MainPage.DisplayAlert("Error!", "Gram spist er tom!", "OK");
            return;
        }

        // Create the macro log
        if (_viewModel.CreateMacroLog is AsyncRelayCommand createMacroLogCommand)
        {
            await createMacroLogCommand.ExecuteAsync(null);
        }

        // Reset the ViewModel properties
        _viewModel.FoodDetails = new();
        _viewModel.MacroWeight = null;


        // Navigate back to the home page with the new macro log
        await Shell.Current.GoToAsync("../..", true, new Dictionary<string, object>
        {
            { "NewMacroLog", _viewModel.NewMacroLog }
        });
    }

    // Event handler for the cancel button click
    private async void annullerFoedevareOprettelse_knap(object sender, EventArgs e)
    {
        // Reset the ViewModel properties
        _viewModel.FoodDetails = new();
        _viewModel.MacroWeight = null;




        var kic = Shell.Current.CurrentState.Location;


        await Shell.Current.GoToAsync("..");

    }
}