

namespace MealMate.View;

public partial class AddFoodPage : ContentPage
{
    private readonly FoodViewModel _viewModel;
    public AddFoodPage(FoodViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = _viewModel = viewModel;

    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        if (_viewModel.IsBusy || _viewModel.Foods.Any()) return;

        _viewModel.GetAllFood.Execute(null);
    }

    private async void opretFoedevare_knap(object sender, EventArgs e)
    {
        Food Food = new();
        await Shell.Current.GoToAsync(nameof(CreateFoodPage), true, new Dictionary<string, object>
        {
            { "SelectedFood", Food }
        });
    }

    private async void aendrerFoedevare_knap(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(CreateFoodPage), true);
    }
    // Command="{Binding Source={x:Reference FoedevareSide}, Path=BindingContext.TilfoejRetKnapCommand}" CommandParameter="{Binding}"
    private async void Testknap(object sender, EventArgs e)
    {
        var ret = ((VisualElement)sender).BindingContext as Dish;

        await Shell.Current.GoToAsync(nameof(CreateDishPage), false, new Dictionary<string, object>
        {
            {"Objekt", ret }


        });
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

        await Shell.Current.GoToAsync(nameof(CreateFoodPage), true, new Dictionary<string, object>
        {
            { "SelectedFood", _viewModel.Food }
        });
    }

    
}