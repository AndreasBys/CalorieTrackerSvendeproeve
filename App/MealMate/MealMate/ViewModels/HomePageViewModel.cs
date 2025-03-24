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
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(NoProgress))]
    private bool progress;
    public bool NoProgress => !Progress;

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

            if (NewMacroLog != null)
            {
                // With the following line
                MacroLog newMacroLog = await CalcMacros(NewMacroLog);
                MacroLogs.Add(newMacroLog);
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
                MacroLog newMacroLog = await CalcMacros(macroLog);
                MacroLogs.Add(newMacroLog);
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
    private async Task<MacroLog> CalcMacros(MacroLog ml)
    {
        Calories       += ml.calories       = (int)(ml.food.calories * ml.weight / 100);
        Protein        += ml.protein        = (int)(ml.food.protein * ml.weight / 100);
        Carbonhydrates += ml.carbonhydrates = (int)(ml.food.carbonhydrates * ml.weight / 100);
        Fat            += ml.fat            = (int)(ml.food.fat * ml.weight / 100);
        return ml;
    }

    private void UpdateProgress()
    {
        if (Protein <= 0 && Carbonhydrates <= 0 && Fat <= 0)
        {
            Progress = false;
            return;
        }
        Progress = true;
        if (MacroGoal == null)
        {
            double proteinsCalories = Protein * 4;
            double carbsCalories    = Carbonhydrates * 4;
            double fatsCalories     = Fat * 9;
            double totalCalories    = proteinsCalories + carbsCalories + fatsCalories;
            CaloriesProgress        = 0;
            ProteinProgress         = (proteinsCalories / totalCalories) * 100;
            CarbonhydratesProgress  = (carbsCalories    / totalCalories) * 100;
            FatProgress             = (fatsCalories     / totalCalories) * 100;
            return;
        }
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