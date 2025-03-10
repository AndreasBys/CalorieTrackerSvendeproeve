using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace MealMate.ViewModels;

public partial class FoodViewModel : BaseViewModel
{
    [ObservableProperty]
    string scanText;
    [ObservableProperty]
    Food food;
    public ObservableCollection<Food> Foods { get; } = new();
    public Command GetAllFood { get; }
    public Command GetFood { get; }
    FoodService FoodService;

    public FoodViewModel(FoodService FoodService)
    {
        this.FoodService = FoodService;
        GetAllFood = new Command(async () => await GetFoods());
        GetFood = new Command(async () => await GetFoodByBarcode());
    }

    async Task GetFoods()
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
    async Task GetFoodByBarcode()
    {
        if (IsBusy)
            return;
        try
        {
            IsBusy = true;

            var food = await FoodService.GetFoodByBarcode(ScanText);

            if (food == null) 
                throw new Exception($"Barcode: {ScanText} is invalid");

            this.Food = food;
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
}
