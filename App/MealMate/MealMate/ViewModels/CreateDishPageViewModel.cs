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
        string searchText = "";

        [ObservableProperty]
        string dishname;

        [ObservableProperty]
        string weight;

        // Collection to hold food items
        public ObservableCollection<Food> Foods { get; } = new();

        public ObservableCollection<Food> foodsInDish { get; } = new();

        public ObservableCollection<FoodInDish> FoodRequestForDish { get; } = new();

        FoodService FoodService;

        DishService DishService;

        public ICommand SearchFood { get; }

        public ICommand AddToDishCommand { get; }

        public ICommand RemoveFromDishCommand { get; }

        // Constructor
        public CreateDishPageViewModel(FoodService FoodService, DishService dishService)
        {
            this.FoodService = FoodService;
            GetAllFood();

            AddToDishCommand = new Command<Food>(AddToDish);
            SearchFood = new AsyncRelayCommand(SearchFoods);
            RemoveFromDishCommand = new Command<FoodInDish>(RemoveFromDish);
            DishService = dishService;
        }

        [RelayCommand]
        async Task Opret()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Dishname))
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Retten mangler et navn", "OK");
                    return;
                }
                if (FoodRequestForDish.Count == 0)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Retten har ingen ingredienser", "OK");
                    return;
                }

                DishRequest dish = new DishRequest { name = Dishname, foods = new List<FoodInDishRequest>() };



                foreach (var item in FoodRequestForDish)
                {
                    FoodInDishRequest newFoodInDish = new FoodInDishRequest { id = item._id, weight = item.weight };
                    dish.foods.Add(newFoodInDish);
                }


                DishResponse responseDish = await DishService.CreateDish(dish);

                await Shell.Current.GoToAsync(nameof(AddDishPage), false, new Dictionary<string, object>
                {
                    { "SelectedDish", responseDish }
                });

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                return;
            }
        }

        private async void GetAllFood()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;

                List<Food> foods = new List<Food>();

                foods = await FoodService.GetAllFoods();

                if (Foods.Count != 0)
                    Foods.Clear();

                foreach (var food in foods)
                {
                    if (!FoodRequestForDish.Any(x => x._id == food._id))
                    {
                        Foods.Add(food);
                    }

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

        async Task SearchFoods()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;


                var foods = Foods.Where(x => x.name.Contains(SearchText)).ToList();

                if (Foods.Count != 0)
                    Foods.Clear();

                if (SearchText == "")
                {
                    IsBusy = false;
                    GetAllFood();
                    return;
                }

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

            if (food == null)
            {
                return;
            }

            FoodInDish x = new FoodInDish { weight = 0, _id = food._id, name = food.name };

            FoodRequestForDish.Add(x);

            Foods.Remove(food);

        }

        private void RemoveFromDish(FoodInDish food)
        {
            if (food == null)
            {
                return;
            }

            FoodRequestForDish.Remove(food);
            Foods.Add(new Food { _id = food._id, name = food.name, calories = food.food.calories });
        }



    }
}

