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
        private string kulhydraterText = "g";

        [ObservableProperty]
        private string fedtText = "g";

        [ObservableProperty]
        private string proteinProgressBar;

        [ObservableProperty]
        private string fedtProgressBar;

        [ObservableProperty]
        private string kulhydraterProgressBar;

        [RelayCommand]
        async Task GemMaalKnap()
        {
            

            await Shell.Current.GoToAsync(nameof(HjemmeskaermSide));
        }


        partial void OnKalorieInputChanged(string kalorieValue)
        {
            Console.WriteLine($"InputTextÆndret  { kalorieValue}");
            try
            {
                if (string.IsNullOrWhiteSpace(kalorieValue))
                {
                    return;
                }

                double kalorieValueDouble = Convert.ToDouble(kalorieValue);

                double fedtIGram = Convert.ToDouble(fedtProcent) / 100;
                double proteinIGram = Convert.ToDouble(proteinProcent) / 100;
                double kulhydraterIGram = Convert.ToDouble(kulhydraterProcent) / 100;

                proteinIGram = kalorieValueDouble * proteinIGram;
                kulhydraterIGram = kalorieValueDouble * kulhydraterIGram;
                fedtIGram = kalorieValueDouble * fedtIGram;

                fedtIGram = Convert.ToInt32(fedtIGram);
                proteinIGram = Convert.ToInt32(proteinIGram);
                kulhydraterIGram = Convert.ToInt32(kulhydraterIGram);

                FedtText = fedtIGram + "g";
                ProteinText = proteinIGram + "g";
                KulhydraterText = kulhydraterIGram + "g";

                KulhydraterProgressBar = KulhydraterProcent;
                ProteinProgressBar = proteinProcent;

                FedtProgressBar = fedtProcent;





            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Text changed", "Enter numbers", "OK");
                throw;
            }


        }

        partial void OnProteinProcentChanged(string ProteinValue)
        {
            if (string.IsNullOrWhiteSpace(ProteinValue))
            {
                return;
            }

            if (Convert.ToInt32(ProteinValue) > 100 || Convert.ToInt32(ProteinValue) < 0)
            {
                proteinProcent = "100";
            }

            OnKalorieInputChanged(kalorieInput);


        }

        partial void OnFedtProcentChanged(string FedtValue)
        {
            if (string.IsNullOrWhiteSpace(FedtValue))
            {
                return;
            }

            if (Convert.ToInt32(FedtValue) > 100 || Convert.ToInt32(FedtValue) < 0)
            {
                fedtProcent = "100";
            }

            OnKalorieInputChanged(kalorieInput);
        }

        partial void OnKulhydraterProcentChanged(string KulhydraterValue)
        {
            if (string.IsNullOrWhiteSpace(KulhydraterValue))
            {
                return;
            }

            if (Convert.ToInt32(KulhydraterValue) > 100 || Convert.ToInt32(KulhydraterValue) < 0)
            {
                kulhydraterProcent = "100";
            }

            OnKalorieInputChanged(kalorieInput);
        }



    }
}
