using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealMate.ViewModel
{
    public partial class RegistrerMaalSideViewModel : BaseViewModel
    {
        [RelayCommand]
        private async Task gemMaal_knap()
        {
            await Shell.Current.GoToAsync(nameof(HjemmeskaermSide), true);
        }


        [ObservableProperty]
        private string kalorieInput;



        partial void OnKalorieInputChanged(string kalorieValue)
        {
            Console.WriteLine($"InputTextÆndret  { kalorieValue}");

            //await Application.Current.MainPage.DisplayAlert("Text changed", "", "OK");
        }


    }
}
