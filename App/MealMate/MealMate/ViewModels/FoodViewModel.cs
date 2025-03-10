using System.Collections.ObjectModel;

namespace MealMate.ViewModels;

public partial class FoodViewModel : BaseViewModel
{
    public ObservableCollection<Food> Foods { get; } = new();
    public Command GetFoodCmd { get; }
    FoodService FoodService;

    public FoodViewModel(FoodService FoodService)
    {
        this.FoodService = FoodService;
        GetFoodCmd = new Command(async () => await GetFoodsAsync());
    }

    async Task GetFoodsAsync()
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
            Debug.WriteLine($"StackTrace: {ex.StackTrace}");
            await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
