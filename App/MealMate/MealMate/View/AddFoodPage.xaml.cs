namespace MealMate.View;

public partial class AddFoodPage : ContentPage
{
    private readonly AddFoodPageViewModel _viewModel;
    private readonly BarcodeTaskCompletionService _taskCompletionService;
    public AddFoodPage(AddFoodPageViewModel viewModel, BarcodeTaskCompletionService barcodeTaskService)
	{
		InitializeComponent();
        BindingContext = _viewModel = viewModel;
        _taskCompletionService = barcodeTaskService;

    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

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
        await Shell.Current.GoToAsync(nameof(BarcodeReaderPage)); // Non-blocking

        string result = await _taskCompletionService.StartBarcodeTask(); 



        // Makes code work async
        if (_viewModel.GetFood is AsyncRelayCommand<string> getFoodCommand)
        {
            await getFoodCommand.ExecuteAsync(result);
        }

        if (_viewModel.Food == null) return;

        await Shell.Current.GoToAsync(nameof(CreateFoodPage), true, new Dictionary<string, object>
        {
            { "SelectedFood", _viewModel.Food }
        });
    }
}