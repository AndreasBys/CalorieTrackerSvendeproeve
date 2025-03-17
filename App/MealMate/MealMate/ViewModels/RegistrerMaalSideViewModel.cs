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



        [ObservableProperty]
        private string kalorieInput;

        [ObservableProperty]
        private string proteinProcent = "15";

        [ObservableProperty]
        private string kulhydraterProcent = "50";

        [ObservableProperty]
        private string fedtProcent = "35";

        [ObservableProperty]
        private string marginProcent;

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

        MacroGoalService macroGoalService;

        public RegistrerMaalSideViewModel(MacroGoalService macroGoalService)
        {
            this.macroGoalService = macroGoalService;
        }



        [RelayCommand]
        async Task GemMaalKnap()
        {
            if (NullorWhitespace())
            {
                try
                {
                    if (Convert.ToInt32(fedtProcent) > 100 || Convert.ToInt32(FedtProcent) < 0)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error!", "Udfyld fedt procent mellem 0-100!", "OK");
                        return;
                    }

                    if (Convert.ToInt32(proteinProcent) > 100 || Convert.ToInt32(ProteinProcent) < 0)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error!", "Udfyld protein procent mellem 0-100!", "OK");
                        return;
                    }

                    if (Convert.ToInt32(kulhydraterProcent) > 100 || Convert.ToInt32(KulhydraterProcent) < 0)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error!", "Udfyld kulhydrater procent mellem 0-100!", "OK");
                        return;
                    }


                    MacroGoal macroGoal = new MacroGoal();

                    macroGoal.calories = Convert.ToInt32(kalorieInput);
                    macroGoal.proteins = Convert.ToInt32(proteinProcent);
                    macroGoal.carbohydrates = Convert.ToInt32(kulhydraterProcent);
                    macroGoal.fats = Convert.ToInt32(fedtProcent);
                    macroGoal.Margin = Convert.ToInt32(marginProcent);

                    try
                    {
                        await macroGoalService.CreateMacroGoal(macroGoal);

                        await Shell.Current.GoToAsync(nameof(HjemmeskaermSide));

                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error!", $"Fejl i serveren! {ex.Message}", "OK");
                        return;
                    }


                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error!", $"Indtast venligst kun tal! {ex.Message}", "OK");
                    return;
                }
            }


        }
        public bool NullorWhitespace()
        {
            return !string.IsNullOrWhiteSpace(KalorieInput) && !string.IsNullOrWhiteSpace(ProteinProcent) && !string.IsNullOrWhiteSpace(KulhydraterProcent) && !string.IsNullOrWhiteSpace(FedtProcent);
        }

        partial void OnKalorieInputChanged(string kalorieValue)
        {
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
                Application.Current.MainPage.DisplayAlert("Error!", $"Indtast venligst kun tal! {ex.Message}", "OK");
                return;
            }


        }

        partial void OnProteinProcentChanged(string ProteinValue)
        {
            try
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
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Error!", $"Indtast venligst kun tal! {ex.Message}", "OK");
                return;
            }



        }

        partial void OnFedtProcentChanged(string FedtValue)
        {

            try
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
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Error!", $"Indtast venligst kun tal! {ex.Message}", "OK");
                return;
            }

        }

        partial void OnKulhydraterProcentChanged(string KulhydraterValue)
        {

            try
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
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Error!", $"Indtast venligst kun tal! {ex.Message}", "OK");
                return;
            }


        }



    }
}
