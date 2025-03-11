using CommunityToolkit.Mvvm.ComponentModel;

namespace MealMate.ViewModels;

[QueryProperty(nameof(FoodDetails), "SelectedFood")]
public partial class AddFoodViewModel : BaseViewModel
{
    [ObservableProperty]
    Food foodDetails;
}
