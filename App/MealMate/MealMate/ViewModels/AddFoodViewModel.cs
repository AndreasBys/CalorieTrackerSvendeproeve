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
    public AddFoodViewModel(FoodService FoodService, MacroLogService macroLogService)
    {
        this.FoodService = FoodService;
        this.MacroLogService = macroLogService;
        CreateFood = new AsyncRelayCommand(CreateFoodAsync);
        CreateMacroLog = new AsyncRelayCommand(CreateMacroLogAsync);
        MacroLogService = macroLogService;
    }

    async Task CreateFoodAsync()
    {
        if (IsBusy)
            return;
        try
        {
            IsBusy = true;

            var food = await FoodService.CreateFood(foodDetails);

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
            return;
        try
        {
            IsBusy = true;

            NewMacroLog newMacroLog = new(FoodDetails._id, Convert.ToInt32(MacroWeight));
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
