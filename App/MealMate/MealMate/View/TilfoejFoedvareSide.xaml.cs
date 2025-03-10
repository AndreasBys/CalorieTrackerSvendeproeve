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
		await Shell.Current.GoToAsync(nameof(OpretFoedevareSide), true);
    }

    private async void aendrerFoedevare_knap(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(OpretFoedevareSide), true);
    }
}