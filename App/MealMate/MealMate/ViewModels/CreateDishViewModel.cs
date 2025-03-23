namespace MealMate.ViewModels
{
    [QueryProperty(nameof(Dish), "Objekt")]
    public partial class CreateDishViewModel : BaseViewModel
    {
        // Observable property for the selected dish
        [ObservableProperty]
        public Dish dish;

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
            Template();
        }


        public CreateDishViewModel()
        {

        }

        // Method to calculate the total nutritional information for the dish
        private void Template()
        {
            int kalorier = 0;
            // Iterate through each food item in the dish and sum up the nutritional values
            Dish.foods.ForEach(food =>
            {
                RettensKalorier += food.food.calories;
                RettensFedt += food.food.fat;
                RettensProtein += food.food.protein;
                RettensKulhydrater += food.food.carbonhydrates;
            });
        }
    }
}

