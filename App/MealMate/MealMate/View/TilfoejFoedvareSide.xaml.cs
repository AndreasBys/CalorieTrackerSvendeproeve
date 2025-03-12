namespace MealMate.View;

public partial class TilfoejFoedvareSide : ContentPage
{
    private readonly FoodViewModel _viewModel;
    public TilfoejFoedvareSide(FoodViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = _viewModel = viewModel;

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (_viewModel.IsBusy) return;

        _viewModel.GetAllFood.Execute(null);
    }

    private async void opretFoedevare_knap(object sender, EventArgs e)
    {
        Food Food = new();
        await Shell.Current.GoToAsync(nameof(OpretFoedevareSide), true, new Dictionary<string, object>
        {
            { "SelectedFood", Food }
        });
    }

    private async void aendrerFoedevare_knap(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(OpretFoedevareSide), true);
    }

    private void OnSearch(object sender, EventArgs e)
    {
        _viewModel.SearchFood.Execute(null);
    }

    private async void OnScan(object sender, EventArgs e)
    {
        // Makes code work async
        if (_viewModel.GetFood is AsyncRelayCommand<string> getFoodCommand)
        {
            await getFoodCommand.ExecuteAsync("test");
        }

        if (_viewModel.Food == null) return;

        await Shell.Current.GoToAsync(nameof(OpretFoedevareSide), true, new Dictionary<string, object>
        {
            { "SelectedFood", _viewModel.Food }
        });
    }
}