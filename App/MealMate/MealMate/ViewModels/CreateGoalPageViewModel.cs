namespace MealMate.ViewModels
{
    public partial class CreateGoalPageViewModel : BaseViewModel
    {
        // Observable properties for user input and calculated values

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

        // Service for managing macro goals
        MacroGoalService macroGoalService;

        // Constructor to initialize the service
        public CreateGoalPageViewModel(MacroGoalService macroGoalService)
        {
            this.macroGoalService = macroGoalService;
        }

        // Command to save the macro goal
        [RelayCommand]
        async Task GemMaalKnap()
        {
            if (NullorWhitespace())
            {
                try
                {
                    // Validate the input percentages
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

                    // Create a new MacroGoal object with the input values
                    MacroGoal macroGoal = new MacroGoal
                    {
                        calories = Convert.ToInt32(kalorieInput),
                        proteins = Convert.ToInt32(proteinProcent),
                        carbonhydrates = Convert.ToInt32(kulhydraterProcent),
                        fats = Convert.ToInt32(fedtProcent),
                        margin = Convert.ToInt32(marginProcent)
                    };

                    try
                    {
                        // Save the macro goal using the service
                        await macroGoalService.CreateMacroGoal(macroGoal);

                        // Navigate to the home screen
                        await Shell.Current.GoToAsync(nameof(HomePage));
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

        // Method to check if any input fields are null or whitespace
        public bool NullorWhitespace()
        {
            return !string.IsNullOrWhiteSpace(KalorieInput) && !string.IsNullOrWhiteSpace(ProteinProcent) && !string.IsNullOrWhiteSpace(KulhydraterProcent) && !string.IsNullOrWhiteSpace(FedtProcent);
        }

        // Method called when the KalorieInput property changes
        partial void OnKalorieInputChanged(string kalorieValue)
        {

            try
            {
                if (string.IsNullOrWhiteSpace(kalorieValue))
                {
                    return;
                }

                // Calculates percentages and updates progress bars

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

        // Method called when the ProteinProcent property changes
        partial void OnProteinProcentChanged(string ProteinValue)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ProteinValue))
                {
                    return;
                }

                // Insures that u can't exceed 0-100%
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

        // Method called when the FedtProcent property changes
        partial void OnFedtProcentChanged(string FedtValue)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(FedtValue))
                {
                    return;
                }

                // Insures that u can't exceed 0-100%
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

        // Method called when the KulhydraterProcent property changes
        partial void OnKulhydraterProcentChanged(string KulhydraterValue)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(KulhydraterValue))
                {
                    return;
                }

                // Insures that u can't exceed 0-100%
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