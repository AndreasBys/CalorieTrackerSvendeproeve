namespace MealMate.View;

public partial class CreateDishPage : ContentPage
{
	public CreateDishPage()
	{
		InitializeComponent();
        BindingContext = viewModel;
	}

    private async void annullerRetOprettelse_knap(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddFoodPage), true);
    }

    private async void oprettetRet_knap(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddFoodPage), true);
    }

    
}