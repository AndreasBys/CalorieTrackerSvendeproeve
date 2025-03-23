using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MealMate.ViewModels
{
    
    public partial class CreateDishPageViewModel : BaseViewModel
    {
        

        // Observable property for food details
        [ObservableProperty]
        Food food;

        [ObservableProperty]
        string searchText;

        // Collection to hold food items
        public ObservableCollection<Food> Foods { get; } = new();

        public ObservableCollection<Food> FoodInDish { get; } = new();

        FoodService FoodService;

        public ICommand SearchFood { get; }

        public ICommand AddToDishCommand { get; }

        public ICommand RemoveFromDishCommand { get; }

        // Constructor
        public CreateDishPageViewModel(FoodService FoodService)
        {
            this.FoodService = FoodService;
            GetAllFood();

            AddToDishCommand = new Command<Food>(AddToDish);
            SearchFood = new AsyncRelayCommand(SearchFoods);
            RemoveFromDishCommand = new Command<Food>(RemoveFromDish);
        }


        private async void GetAllFood()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;

                var foods = await FoodService.GetAllFoods();

                if (Foods.Count != 0)
                    Foods.Clear();

                foreach (var food in foods)
                    Foods.Add(food);
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

        async Task SearchFoods()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                var foods = await FoodService.SearchFoods(SearchText);

                if (Foods.Count != 0)
                    Foods.Clear();

                foreach (var food in foods)
                    Foods.Add(food);
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

        private void AddToDish(Food food)
        {
            if (food == null || FoodInDish.Contains(food))
            {
                return;
            }
            FoodInDish.Add(food);

        }

        private void RemoveFromDish(Food food)
        {
            if (food == null)
            {
                return;
            }
            FoodInDish.Remove(food);
        }



    }
}

