using System.Collections.ObjectModel;

namespace MealMate.ViewModels;

public partial class AddDishPageViewModel : BaseViewModel
{
    // Observable property for food details
    [ObservableProperty]
    Food food;

    // Collection to hold food items
    public ObservableCollection<Food> Foods { get; } = new();

    FoodService FoodService;


    public AddDishPageViewModel(FoodService FoodService)
    {
        this.FoodService = FoodService;
        GetAllFood();
        
    }

    private async void GetAllFood()
    {
        if (IsBusy)
            return;
        try
        {
            IsBusy = true;

            var foods = await FoodService.GetAllFoods();

            if (Foods.Count != 0)
                Foods.Clear();

            foreach (var food in foods)
                Foods.Add(food);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get Foods: {ex.Message}");
            await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    async Task SearchFood()
    {

    }

}
