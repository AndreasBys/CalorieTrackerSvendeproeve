using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace MealMate.ViewModels;

[QueryProperty(nameof(FoodDetails), "SelectedFood")]
public partial class AddFoodViewModel : BaseViewModel
{
    [ObservableProperty]
    Food foodDetails;
    [ObservableProperty]
    string macroWeight;
    MacroLog macroLog;
    public ICommand CreateFood { get; }
    public ICommand CreateMacroLog { get; }
    FoodService FoodService;
    MacroLogService MacroLogService;
    public AddFoodViewModel(FoodService FoodService, MacroLogService MacroLogService)
    {
        this.FoodService = FoodService;
        this.MacroLogService = MacroLogService;
        CreateFood = new AsyncRelayCommand(CreateFoodAsync);
        CreateMacroLog = new AsyncRelayCommand(CreateMacroLogAsync);
    }

    async Task CreateFoodAsync()
    {
        if (IsBusy)
            return;
        try
        {
            IsBusy = true;

            FoodRequest newFood = new(
                FoodDetails.name,
                FoodDetails.barcode,
                FoodDetails.calories,
                FoodDetails.carbonhydrates,
                FoodDetails.protein,
                FoodDetails.fat,
                FoodDetails.user);

            var food = await FoodService.CreateFood(newFood);

            if (food == null)
                throw new Exception($"Food couldn't be added");

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

    async Task CreateMacroLogAsync()
    {
        if (IsBusy)
        {
            return;
        }
        try
        {
            IsBusy = true;

            MacroLogRequest newMacroLog = new(FoodDetails._id, Convert.ToInt32(MacroWeight));
            var macroLog = await MacroLogService.CreateMacroLog(newMacroLog);

            if (macroLog == null)
                throw new Exception($"MacroLog couldn't be added");

            this.macroLog = macroLog;
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
