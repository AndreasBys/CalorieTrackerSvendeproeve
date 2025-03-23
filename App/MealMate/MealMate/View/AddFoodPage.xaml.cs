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

    private async void aendrerFoedevare_knap(object sender, EventArgs e)
    {
        var food = ((VisualElement)sender).BindingContext as Food;

        await Shell.Current.GoToAsync(nameof(CreateFoodPage), false, new Dictionary<string, object>
        {
            { "SelectedFood", food }
        });
    }
    
    private async void AddDishKnap(object sender, EventArgs e)
    {
        var ret = ((VisualElement)sender).BindingContext as Dish;

        await Shell.Current.GoToAsync(nameof(AddDishPage), false, new Dictionary<string, object>
        {
            {"SelectedDish", ret }


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