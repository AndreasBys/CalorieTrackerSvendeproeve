namespace MealMate.ViewModels
{
    // This attribute allows the Retter property to be set via query parameters
    [QueryProperty(nameof(Retter), "Objekt")]
    public partial class CreateDishPageViewModel : BaseViewModel
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


        // Constructor
        public CreateDishPageViewModel()
        {

        }

        // Method to calculate the total nutritional information for the dish
        private void Template()
        {
            int kalorier = 0;

            // Iterate through each food item in the dish and sum up the nutritional values
            Retter.foods.ForEach(food =>
            {
                RettensKalorier += food.food.calories;
                RettensFedt += food.food.fat;
                RettensProtein += food.food.protein;
                RettensKulhydrater += food.food.carbonhydrates;
            });
        }
    }
}

