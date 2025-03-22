using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MealMate.ViewModels;

public partial class FoodViewModel : BaseViewModel
{
    

    [ObservableProperty]
    bool enkeltVarerSynlighed = true;

    [ObservableProperty]
    bool mineRetterSynlighed = false;

    [ObservableProperty]
    private Color enkeltVarerValgt = (Color)Application.Current.Resources["CustomBlaa"];

    [ObservableProperty]
    private Color mineRetterValgt = (Color)Application.Current.Resources["CustomGraa"];

    [ObservableProperty]
    private Color tekstEnkeltvarerValgt = (Color)Application.Current.Resources["CustomTekstHvidereGraa"];

    [ObservableProperty]
    private Color tekstMineRetterValgt = (Color)Application.Current.Resources["CustomTekstHvidereGraa"];

    public ObservableCollection<Dish> Dish { get; } = new();

    DishService dishService;

    [ObservableProperty]
    string searchText;
    [ObservableProperty]
    Food food;
    public ObservableCollection<Food> Foods { get; } = new();
    public ICommand GetAllFood { get; }
    public ICommand SearchFood { get; }
    public ICommand GetFood { get; }

    FoodService FoodService;

    public FoodViewModel(FoodService FoodService, DishService dishService)
    {
        this.FoodService = FoodService;
        GetAllFood = new AsyncRelayCommand(GetFoods);
        SearchFood = new AsyncRelayCommand(SearchFoods);
        GetFood = new AsyncRelayCommand<string>(GetFoodByBarcode);

        this.dishService = dishService;
        getAllRetter();
    }

    public async Task GetFoods()
    {
        try
        {
            IsBusy = true;
            var foods = await FoodService.GetAllFoods();
            if (foods != null)
            {
                Foods.Clear();
                foreach (var food in foods)
                {
                    Foods.Add(food);
                }
                Console.WriteLine($"Loaded {Foods.Count} foods.");
            }
            else
            {
                Console.WriteLine("No foods found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading foods: {ex.Message}");
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

        if (EnkeltVarerSynlighed)
        {
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
        else if (MineRetterSynlighed)
        {
            try
            {
                IsBusy = true;

                var dishes = await dishService.SearchRetter(SearchText);

                if (Dish.Count != 0)
                    Dish.Clear();

                foreach (var item in dishes)
                    Dish.Add(item);
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


    [RelayCommand]
    async Task MineRetter()
    {
        EnkeltVarerSynlighed = false;
        MineRetterSynlighed = true;

        EnkeltVarerValgt = (Color)Application.Current.Resources["CustomGraa"];
        MineRetterValgt = (Color)Application.Current.Resources["CustomBlaa"];

        TekstMineRetterValgt = (Color)Application.Current.Resources["CustomHvid"];
        TekstEnkeltvarerValgt = (Color)Application.Current.Resources["CustomTekstHvidereGraa"];
    }

    [RelayCommand]
    async Task EnkeltVarer()
    {
        MineRetterSynlighed = false;
        EnkeltVarerSynlighed = true;

        MineRetterValgt = (Color)Application.Current.Resources["CustomGraa"];
        EnkeltVarerValgt = (Color)Application.Current.Resources["CustomBlaa"];

        TekstEnkeltvarerValgt = (Color)Application.Current.Resources["CustomHvid"];
        
        TekstMineRetterValgt = (Color)Application.Current.Resources["CustomTekstHvidereGraa"];
    }


    public async void getAllRetter()
    {
        if (IsBusy)
            return;
        try
        {
            IsBusy = true;

            List<Dish> dishList = await dishService.GetAllRetter();

            foreach (var item in dishList)
            {
                Dish.Add(item);
            }

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get Retter: {ex.Message}");
            await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
            throw;
        }
        finally
        {
            IsBusy = false;
        }
        



    }

}
