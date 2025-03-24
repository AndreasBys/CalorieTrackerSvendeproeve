using System.Collections.ObjectModel;

namespace MealMate.ViewModels;

[QueryProperty(nameof(Dish), "SelectedDish")]
public partial class AddDishPageViewModel : BaseViewModel
{
    // Observable property for the selected dish
    [ObservableProperty]
    private Dish dish;

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



    partial void OnDishChanged(Dish value)
    {
        RettensNavn = Dish.name;

        foreach (var item in Dish.foods)
        {
            RettensKalorier += item.calories;
            RettensProtein += item.protein;
            RettensKulhydrater += item.carbonhydrates;
            RettensFedt += item.fat;
        }
    }


}
