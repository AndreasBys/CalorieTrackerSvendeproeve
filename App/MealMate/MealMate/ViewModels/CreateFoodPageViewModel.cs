using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace MealMate.ViewModels;

// This attribute allows the FoodDetails property to be set via query parameters
[QueryProperty(nameof(FoodDetails), "SelectedFood")]

public partial class CreateFoodPageViewModel : BaseViewModel
{
    // Observable property for food details
    [ObservableProperty]
    Food foodDetails;

    // Observable property for macro weight
    [ObservableProperty]
    string macroWeight;

    // Property to hold the newly created MacroLog
    public MacroLog NewMacroLog;

    // Command to create a new food item
    public ICommand CreateFood { get; }

    // Command to create a new macro log
    public ICommand CreateMacroLog { get; }

    // Services for food and macro log operations
    FoodService FoodService;
    MacroLogService MacroLogService;

    // Constructor to initialize services and commands
    public CreateFoodPageViewModel(FoodService FoodService, MacroLogService MacroLogService)
    {
        this.FoodService = FoodService;
        this.MacroLogService = MacroLogService;
        CreateFood = new AsyncRelayCommand(CreateFoodAsync);
        CreateMacroLog = new AsyncRelayCommand(CreateMacroLogAsync);
    }



    // Async method to create a new food item
    async Task CreateFoodAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            // Create a new FoodRequest object with the food details
            FoodRequest newFood = new(
                FoodDetails.name,
                FoodDetails.barcode,
                FoodDetails.calories,
                FoodDetails.carbonhydrates,
                FoodDetails.protein,
                FoodDetails.fat,
                FoodDetails.user);

            // Call the FoodService to create the new food item
            var food = await FoodService.CreateFood(newFood);

            if (food == null)
                throw new Exception($"Food couldn't be added");

            // Update the FoodDetails property with the newly created food item
            FoodDetails = food;
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

    // Async method to create a new macro log
    async Task CreateMacroLogAsync()
    {
        if (IsBusy)
        {
            return;
        }

        try
        {
            IsBusy = true;

            // Create a new MacroLogRequest object with the food ID and macro weight
            MacroLogRequest newMacroLog = new(FoodDetails._id, Convert.ToInt32(MacroWeight));

            // Call the MacroLogService to create the new macro log
            var macroLog = await MacroLogService.CreateMacroLog(newMacroLog);

            if (macroLog == null)
                throw new Exception($"MacroLog couldn't be added");

            // Update the NewMacroLog property with the newly created macro log
            NewMacroLog = macroLog;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get MacroLog: {ex.Message}");
            await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
