using System.Threading.Tasks;

namespace MealMate.View;

public partial class HomePage : ContentPage
{
    private readonly HomePageViewModel _viewModel;
    public HomePage(HomePageViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = _viewModel = viewModel;

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await ((AsyncRelayCommand)_viewModel.GetMacroGoal).ExecuteAsync(null);

        _viewModel.GetMacroLogs.Execute(null);
    }
}