using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MealMate.ViewModels;

public partial class FoodViewModel : BaseViewModel
{
    [ObservableProperty]
    string searchText;
    [ObservableProperty]
    Food food;
    public ObservableCollection<Food> Foods { get; } = new();
    public ICommand GetAllFood { get; }
    public ICommand SearchFood { get; }
    public ICommand GetFood { get; }

    FoodService FoodService;

    public FoodViewModel(FoodService FoodService)
    {
        this.FoodService = FoodService;
        GetAllFood = new AsyncRelayCommand(GetFoods);
        SearchFood = new AsyncRelayCommand(SearchFoods);
        GetFood = new AsyncRelayCommand<string>(GetFoodByBarcode);
    }

    public async Task GetFoods()
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

    async Task SearchFoods()
    {
        if (IsBusy)
            return;
        try
        {
            IsBusy = true;

            var foods = await FoodService.SearchFoods(SearchText);

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

    async Task GetFoodByBarcode(string? scanText)
    {
        if (scanText == null)
            return;
        if (IsBusy)
            return;
        try
        {
            IsBusy = true;

            var food = await FoodService.GetFoodByBarcode(scanText);

            if (food == null) 
                throw new Exception($"Barcode: {scanText} is invalid");

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
