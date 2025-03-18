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

        if (_viewModel.NewMacroLog != null)
        {
            _viewModel.NewMacroLog.calories = _viewModel.NewMacroLog.food.calories * _viewModel.NewMacroLog.weight / 100;
            _viewModel.NewMacroLog.protein = _viewModel.NewMacroLog.food.protein * _viewModel.NewMacroLog.weight / 100;
            _viewModel.NewMacroLog.fat = _viewModel.NewMacroLog.food.fat * _viewModel.NewMacroLog.weight / 100;
            _viewModel.NewMacroLog.carbonhydrates = _viewModel.NewMacroLog.food.carbonhydrates * _viewModel.NewMacroLog.weight / 100;
            _viewModel.MacroLogs.Add(_viewModel.NewMacroLog);
        }
        if (_viewModel.IsBusy || _viewModel.MacroLogs.Any()) return;

        _viewModel.GetMacroLogs.Execute(null);
    }
}