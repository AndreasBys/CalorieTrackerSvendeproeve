namespace MealMate.View;

public partial class HomePage : ContentPage
{
    private readonly HomePageViewModel _viewModel;
    public HomePage(HomePageViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = _viewModel = viewModel;

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (_viewModel.IsBusy) return;

        _viewModel.GetMacroLogs.Execute(null);
    }
}