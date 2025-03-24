using System.Collections.ObjectModel;

namespace MealMate.ViewModels;

[QueryProperty(nameof(Dish), "SelectedDish")]
public partial class AddDishPageViewModel : BaseViewModel
{
    // Observable property for the selected dish
    [ObservableProperty]
    private DishResponse dish;

    // Observable properties for the dish's nutritional information
    [ObservableProperty]
    string rettensNavn;
    [ObservableProperty]
    double rettensKalorier;
    [ObservableProperty]
    double rettensProtein;
    [ObservableProperty]
    double rettensKulhydrater;
    [ObservableProperty]
    double rettensFedt;
    [ObservableProperty]
    string dishWeight;

    MacroLogService _macroService;

    public AddDishPageViewModel(MacroLogService macroService)
    {
        _macroService = macroService;
    }



    [RelayCommand]
    async Task CreateDishKnap()
    {
        if (IsBusy)
        {
            return;
        }
        try
        {
            IsBusy = true;

            MacroLogDishRequest macroLog = new MacroLogDishRequest(Dish.dish._id, Convert.ToInt32(DishWeight));

            var log = await _macroService.CreateMacroLog(macroLog);

            if (log == null)
            {
                throw new Exception($"MacroLog couldn't be added");
            }


            await Shell.Current.GoToAsync(nameof(HomePage));

        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
            return;
        }
        finally
        {
            IsBusy = false;
        }


    }

    partial void OnDishChanged(DishResponse value)
    {
        RettensNavn = Dish.dish.name;

        foreach (var item in Dish.foods)
        {
            RettensKalorier += item.food.calories;
            RettensProtein += item.food.protein;
            RettensKulhydrater += item.food.carbonhydrates;
            RettensFedt += item.food.fat;
        }
    }


}
