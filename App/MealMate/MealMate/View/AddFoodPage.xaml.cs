

namespace MealMate.View;

public partial class AddFoodPage : ContentPage
{
    private readonly AddFoodPageViewModel _viewModel;
    public AddFoodPage(AddFoodPageViewModel viewModel)
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


        await Application.Current.MainPage.DisplayAlert("Error!", Shell.Current.CurrentState.Location.ToString(), "OK");

        await Shell.Current.GoToAsync(nameof(CreateFoodPage), true, new Dictionary<string, object>
        {
            { "SelectedFood", Food }
        });
    }

    private async void aendrerFoedevare_knap(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//" + nameof(CreateFoodPage), true);
    }
    
    private async void AddDishKnap(object sender, EventArgs e)
    {
        var ret = ((VisualElement)sender).BindingContext as Dish;

        await Shell.Current.GoToAsync(nameof(AddDishPage), false, new Dictionary<string, object>
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