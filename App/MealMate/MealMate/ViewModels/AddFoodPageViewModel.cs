using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MealMate.ViewModels;

// ViewModel for managing food-related data and operations
public partial class AddFoodPageViewModel : BaseViewModel
{
    // Observable property to control visibility of single items
    [ObservableProperty]
    bool enkeltVarerSynlighed = true;

    // Observable property to control visibility of my dishes
    [ObservableProperty]
    bool mineRetterSynlighed = false;

    // Observable property for the selected color of single items
    [ObservableProperty]
    private Color enkeltVarerValgt = (Color)Application.Current.Resources["CustomBlaa"];

    // Observable property for the selected color of my dishes
    [ObservableProperty]
    private Color mineRetterValgt = (Color)Application.Current.Resources["CustomGraa"];

    // Observable property for the text color of single items
    [ObservableProperty]
    private Color tekstEnkeltvarerValgt = (Color)Application.Current.Resources["CustomHvid"];

    // Observable property for the text color of my dishes
    [ObservableProperty]
    private Color tekstMineRetterValgt = (Color)Application.Current.Resources["CustomTekstHvidereGraa"];

    public ObservableCollection<Dish> Dish { get; } = new();


    DishService dishService;


    // Observable property for search text
    [ObservableProperty]
    string searchText;

    // Observable property for food details
    [ObservableProperty]
    Food food;

    // Collection to hold food items
    public ObservableCollection<Food> Foods { get; } = new();

    // Commands for various operations
    public ICommand GetAllFood { get; }
    public ICommand SearchFood { get; }
    public ICommand GetFood { get; }

    // Service for managing food items
    FoodService FoodService;

    public AddFoodPageViewModel(FoodService FoodService, DishService dishService)
    {
        this.FoodService = FoodService;
        GetAllFood = new AsyncRelayCommand(GetFoods);
        SearchFood = new AsyncRelayCommand(SearchFoods);
        GetFood = new AsyncRelayCommand<string>(GetFoodByBarcode);

        this.dishService = dishService;
        getAllRetter();
    }

    [RelayCommand]
    async Task OpretKnap()
    {
        if (EnkeltVarerSynlighed)
        {
            Food Food = new();



            await Shell.Current.GoToAsync(nameof(CreateFoodPage), true, new Dictionary<string, object>
            {
            { "SelectedFood", Food }

            });
        }
        else
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(CreateDishPage), true);
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
                throw;
            }
            
            
        }
    }

    // Command to navigate to the Add Dish page with the selected dish
    [RelayCommand]
    async Task TilfoejRetKnap(Dish selectedRet)
    {
        await Shell.Current.GoToAsync(nameof(AddDishPage), false, new Dictionary<string, object>
        {
            {"Objekt", selectedRet}
        });
    }

    // Async method to get all food items
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

    // Async method to search for food items
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

    // Async method to get food details by barcode
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

            Food = food;
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

    // Command to switch to the My Dishes view
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

    // Command to switch to the Single Items view
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

    // Method to get all dishes
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
            return;
        }
        finally
        {
            IsBusy = false;
        }
    }
}

