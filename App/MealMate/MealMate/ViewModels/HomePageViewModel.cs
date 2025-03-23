using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MealMate.ViewModels;

// This attribute allows the NewMacroLog property to be set via query parameters
[QueryProperty(nameof(NewMacroLog), "NewMacroLog")]
public partial class HomePageViewModel : BaseViewModel
{
    // Property to hold the newly created MacroLog
    public MacroLog NewMacroLog { get; set; }

    // Collection to hold macro logs
    public ObservableCollection<MacroLog> MacroLogs { get; } = new();

    // Service for managing macro logs
    MacroLogService MacroLogService;

    // Command to get today's macro logs
    public ICommand GetMacroLogs { get; }

    // Constructor to initialize services and commands
    public HomePageViewModel(MacroLogService MacroLogService)
    {
        this.MacroLogService = MacroLogService;
        GetMacroLogs = new AsyncRelayCommand(GetTodaysMacroLogs);
    }

    // Async method to get today's macro logs
    async Task GetTodaysMacroLogs()
    {
        if (IsBusy)
            return;
        try
        {
            IsBusy = true;

            // Get today's macro logs from the service
            var macroLogs = await MacroLogService.GetTodaysMacroLogs();

            // Clear the existing macro logs if any
            if (MacroLogs.Count != 0)
                MacroLogs.Clear();

            // Calculate and add each macro log to the collection
            foreach (var macroLog in macroLogs)
            {
                macroLog.calories = macroLog.food.calories * macroLog.weight / 100;
                macroLog.carbonhydrates = macroLog.food.carbonhydrates * macroLog.weight / 100;
                macroLog.protein = macroLog.food.protein * macroLog.weight / 100;
                macroLog.fat = macroLog.food.fat * macroLog.weight / 100;
                MacroLogs.Add(macroLog);
            }
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