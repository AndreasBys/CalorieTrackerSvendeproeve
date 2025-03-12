namespace MealMate.View;

public partial class HjemmeskaermSide : ContentPage
{
    private readonly HomePageViewModel _viewModel;
    public HjemmeskaermSide(HomePageViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = _viewModel = viewModel;

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (_viewModel.IsBusy || _viewModel.MacroLogs.Any()) return;

        _viewModel.GetMacroLogs.Execute(null);
    }
}