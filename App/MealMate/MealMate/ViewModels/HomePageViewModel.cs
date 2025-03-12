
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MealMate.ViewModels;

public partial class HomePageViewModel : BaseViewModel
{
    public ObservableCollection<MacroLog> MacroLogs { get; } = new();
    MacroLogService MacroLogService;
    public ICommand GetMacroLogs { get; }
    public HomePageViewModel(MacroLogService MacroLogService)
    {
        this.MacroLogService = MacroLogService;
        GetMacroLogs = new AsyncRelayCommand(GetTodaysMacroLogs);
    }
    async Task GetTodaysMacroLogs()
    {
        if (IsBusy)
            return;
        try
        {
            IsBusy = true;

            var macroLogs = await MacroLogService.GetTodaysMacroLogs();

            if (MacroLogs.Count != 0)
                MacroLogs.Clear();

            foreach (var macroLog in macroLogs)
            {
                macroLog.calories       = macroLog.food.calories       * macroLog.weight / 100;
                macroLog.carbonhydrates = macroLog.food.carbonhydrates * macroLog.weight / 100;
                macroLog.protein        = macroLog.food.protein        * macroLog.weight / 100;
                macroLog.fat            = macroLog.food.fat            * macroLog.weight / 100;
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
