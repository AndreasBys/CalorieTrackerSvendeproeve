using Microsoft.Maui.Layouts;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MealMate.ViewModels;

// This attribute allows the NewMacroLog property to be set via query parameters
[QueryProperty(nameof(NewMacroLog), "NewMacroLog")]
public partial class HomePageViewModel : BaseViewModel
{
    [ObservableProperty]
    private double calories;
    [ObservableProperty]
    private double carbonhydrates;
    [ObservableProperty]
    private double protein;
    [ObservableProperty]
    private double fat;
    [ObservableProperty]
    private double caloriesProgress;
    [ObservableProperty]
    private double carbonhydratesProgress;
    [ObservableProperty]
    private double proteinProgress;
    [ObservableProperty]
    private double fatProgress;

    // Property to hold the newly created MacroLog
    public MacroLog NewMacroLog { get; set; }

    // Collection to hold macro logs
    public ObservableCollection<MacroLog> MacroLogs { get; } = new();

    // Service for managing macro logs
    MacroLogService MacroLogService;

    // Command to get today's macro logs
    public ICommand GetMacroLogs { get; }

    public MacroGoal MacroGoal { get; set; }
    // Service for managing macro goal
    MacroGoalService MacroGoalService;
    public ICommand GetMacroGoal { get; }

    // Constructor to initialize services and commands
    public HomePageViewModel(MacroLogService MacroLogService, MacroGoalService MacroGoalService)
    {
        this.MacroLogService = MacroLogService;
        this.MacroGoalService = MacroGoalService;
        GetMacroLogs = new AsyncRelayCommand(GetTodaysMacroLogs);
        GetMacroGoal = new AsyncRelayCommand(GetCurrentMacroGoal);
    }

    // Async method to get today's macro logs
    async Task GetTodaysMacroLogs()
    {
        if (IsBusy)
            return;
        try
        {
            IsBusy = true;

            if (MacroLogs.Count != 0 && NewMacroLog != null)
            {
                MacroLogs.Add(CalcMacros(
                    NewMacroLog.food.calories,
                    NewMacroLog.food.protein,
                    NewMacroLog.food.carbonhydrates,
                    NewMacroLog.food.fat,
                    NewMacroLog.weight));
                NewMacroLog = null;
                UpdateProgress();
                return;
            }

            if (MacroLogs.Count != 0)
                return;

            // Get today's macro logs from the service
            var macroLogs = await MacroLogService.GetTodaysMacroLogs();

            Calories = 0;
            Carbonhydrates = 0;
            Protein = 0;
            Fat = 0;

            // Calculate and add each macro log to the collection
            foreach (var macroLog in macroLogs)
            {
                Calories += macroLog.calories = macroLog.food.calories * macroLog.weight / 100;
                Carbonhydrates += macroLog.carbonhydrates = macroLog.food.carbonhydrates * macroLog.weight / 100;
                Protein += macroLog.protein = macroLog.food.protein * macroLog.weight / 100;
                Fat += macroLog.fat = macroLog.food.fat * macroLog.weight / 100;
                MacroLogs.Add(macroLog);
            }

            UpdateProgress();
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

    async Task GetCurrentMacroGoal()
    {
        if (IsBusy)
            return;
        try
        {
            IsBusy = true;
            MacroGoal = await MacroGoalService.GetCurrentMacroGoal();
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

    private MacroLog CalcMacros(double calories, double protein, double carbonhydrates, double fats, int weight)
    {
        MacroLog macroLog = new();
        Calories       += macroLog.calories       = (int)(calories * weight / 100);
        Carbonhydrates += macroLog.carbonhydrates = (int)(protein * weight / 100);
        Protein        += macroLog.protein        = (int)(carbonhydrates * weight / 100);
        Fat            += macroLog.fat            = (int)(fats * weight / 100);
        return macroLog;
    }

    private void UpdateProgress()
    {
        if (MacroGoal.calories != null)
            CaloriesProgress = (double)(Calories / MacroGoal.calories * 100);
        if (MacroGoal.proteins != null)
            ProteinProgress = (double)(Protein / MacroGoal.proteins * 100);
        if (MacroGoal.carbonhydrates != null)
            CarbonhydratesProgress = (double)(Carbonhydrates / MacroGoal.carbonhydrates * 100);
        if (MacroGoal.fats != null)
            FatProgress = (double)(Fat / MacroGoal.fats * 100);
    }
}