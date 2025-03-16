using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealMate.ViewModels
{
    [QueryProperty(nameof(Retter), "Objekt")]
    public partial class OpretRetViewModel : BaseViewModel
    {
        [ObservableProperty]
        public Retter retter;


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


        partial void OnRetterChanged(Retter value)
        {
            Template();   
        }


        public OpretRetViewModel()
        {
            
        }


        private void Template()
        {
            System.Diagnostics.Debug.WriteLine($"Retter property set: {Retter?.name}");

            int kalorier = 0;
            
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
