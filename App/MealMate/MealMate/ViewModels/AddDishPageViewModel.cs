using System.Collections.ObjectModel;

namespace MealMate.ViewModels;

[QueryProperty(nameof(Dish), "SelectedDish")]
public partial class AddDishPageViewModel : BaseViewModel
{
    // Observable property for the selected dish
    [ObservableProperty]
    public Dish retter;

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


    public AddDishPageViewModel()
    {
        



    }

    

}
