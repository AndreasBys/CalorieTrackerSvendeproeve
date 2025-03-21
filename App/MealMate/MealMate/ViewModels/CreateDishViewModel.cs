using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealMate.ViewModels
{
    [QueryProperty(nameof(Dish), "Objekt")]
    public partial class CreateDishViewModel : BaseViewModel
    {
        [ObservableProperty]
        public Dish dish;


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


        private void Template()
        {

            int kalorier = 0;

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
