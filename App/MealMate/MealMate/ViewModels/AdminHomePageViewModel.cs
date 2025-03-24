using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MealMate.Models;
using MealMate.Services;
using Microsoft.Maui.Controls;

namespace MealMate.ViewModels
{
    public class AdminHomePageViewModel : BaseViewModel
    {
        private readonly FoodService _foodService;

        public ObservableCollection<Food> Foods { get; } = new ObservableCollection<Food>();

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                SearchFoodsCommand.Execute(null);
            }
        }

        private bool _isFilterApprovedActive;
        private bool _isFilterNotApprovedActive;

        public ICommand GetAllFoodsCommand { get; }
        public ICommand SearchFoodsCommand { get; }
        public ICommand FilterApprovedCommand { get; }
        public ICommand FilterNotApprovedCommand { get; }

        public AdminHomePageViewModel(FoodService foodService)
        {
            _foodService = foodService;

            GetAllFoodsCommand = new Command(async () => await GetAllFoods());
            SearchFoodsCommand = new Command(async () => await SearchFoods());
            FilterApprovedCommand = new Command(async () => await ToggleFilterApproved());
            FilterNotApprovedCommand = new Command(async () => await ToggleFilterNotApproved());

            GetAllFoodsCommand.Execute(null);
        }

        public async Task GetAllFoods()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var foods = await _foodService.GetAllFoods();
                Foods.Clear();
                foreach (var food in foods)
                {
                    Foods.Add(food);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading foods: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task SearchFoods()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var foods = await _foodService.SearchFoods(SearchText);

                if (_isFilterApprovedActive)
                {
                    foods = foods.Where(food => food.approved).ToList();
                }
                else if (_isFilterNotApprovedActive)
                {
                    foods = foods.Where(food => !food.approved).ToList();
                }

                Foods.Clear();
                foreach (var food in foods)
                {
                    Foods.Add(food);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching foods: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task ToggleFilterApproved()
        {
            if (_isFilterApprovedActive)
            {
                _isFilterApprovedActive = false;
                await GetAllFoods();
            }
            else
            {
                _isFilterApprovedActive = true;
                _isFilterNotApprovedActive = false;
                if (!string.IsNullOrEmpty(SearchText))
                {
                    await SearchFoods();
                }
                else
                {
                    await FilterApprovedFoods();
                }
            }
        }

        private async Task ToggleFilterNotApproved()
        {
            if (_isFilterNotApprovedActive)
            {
                _isFilterNotApprovedActive = false;
                await GetAllFoods();
            }
            else
            {
                _isFilterNotApprovedActive = true;
                _isFilterApprovedActive = false;
                if (!string.IsNullOrEmpty(SearchText))
                {
                    await SearchFoods();
                }
                else
                {
                    await FilterNotApprovedFoods();
                }
            }
        }

        private async Task FilterApprovedFoods()
        {
            var allFoods = await _foodService.GetAllFoods();
            Foods.Clear();
            foreach (var food in allFoods.Where(food => food.approved))
            {
                Foods.Add(food);
            }
        }

        private async Task FilterNotApprovedFoods()
        {
            var allFoods = await _foodService.GetAllFoods();
            Foods.Clear();
            foreach (var food in allFoods.Where(food => !food.approved))
            {
                Foods.Add(food);
            }
        }
    }
}
