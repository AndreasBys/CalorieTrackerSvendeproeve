using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;

namespace MealMate.ViewModels;

[QueryProperty(nameof(FoodDetails), "SelectedFood")]
public partial class AddFoodViewModel : BaseViewModel
{
    [ObservableProperty]
    Food foodDetails;
    public ICommand CreateFood { get; }
    FoodService FoodService;
    public AddFoodViewModel(FoodService FoodService)
    {
        this.FoodService = FoodService;
        CreateFood = new AsyncRelayCommand(CreateFoodAsync);
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
}
