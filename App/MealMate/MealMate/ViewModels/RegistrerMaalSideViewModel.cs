using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealMate.ViewModels
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

        [ObservableProperty]
        private string proteinProcent = "15";

        [ObservableProperty]
        private string kulhydraterProcent = "50";

        [ObservableProperty]
        private string fedtProcent = "35";

        [ObservableProperty]
        private string proteinText = "g";

        [ObservableProperty]
        private string fedtProgressBar;



        partial void OnKalorieInputChanged(string kalorieValue)
        {
            try
            {

                int fedtprocentBar = Convert.ToInt32(KalorieInput) / Convert.ToInt32(proteinProcent);

                Console.WriteLine(fedtprocentBar);

                fedtprocentBar = fedtprocentBar / 100;

                fedtProgressBar = fedtprocentBar.ToString();

                



            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Text changed", "Enter numbers", "OK");
                throw;
            }


            



            //await Application.Current.MainPage.DisplayAlert("Text changed", "", "OK");
        }


    }
}
