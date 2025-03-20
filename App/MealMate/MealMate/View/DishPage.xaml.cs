namespace MealMate.View;

public partial class DishPage : ContentPage
{
	public DishPage()
	{
		InitializeComponent();
	}

    

    private async void tilfoejFoedevare_knap(object sender, EventArgs e)
    {

    }

    private async void annullerRetOprettelse_knap(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddFoodPage), true);
    }

    private async void oprettetRet_knap(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(CreateFoodPage), true);
    }
}