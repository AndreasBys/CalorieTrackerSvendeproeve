namespace MealMate.View;

public partial class CreateDishPage : ContentPage
{
    private readonly CreateDishPageViewModel _viewModel;
    public CreateDishPage(CreateDishPageViewModel vm)
=========
	public CreateDishPage()
>>>>>>>>> Temporary merge branch 2:App/MealMate/MealMate/View/CreateDishPage.xaml.cs
	{
		InitializeComponent();
        BindingContext = _viewModel = vm;
    }

    


    private async void annullerRetOprettelse_knap(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddFoodPage), true);
    }

    private async void oprettetRet_knap(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddDishPage), true);
    }

    private void OnSearch(object sender, EventArgs e)
    {
        _viewModel.SearchFood.Execute(null);
    }

}