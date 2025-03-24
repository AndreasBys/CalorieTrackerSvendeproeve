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



    //[RelayCommand]
    //partial void CreateDishKnap()
    //{

    //}

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
