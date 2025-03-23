namespace MealMate.ViewModels
{
    // ViewModel for managing the registration of user profile data
    public partial class CreateUserdataViewModel : BaseViewModel
    {
        // Observable properties for user input
        [ObservableProperty]
        DateTime foedselsdato;

        [ObservableProperty]
        string hoejde = "";

        [ObservableProperty]
        string vaegt = "";

        [ObservableProperty]
        string aktivitetsniveau;

        [ObservableProperty]
        string maal;

        [ObservableProperty]
        string koen;

        // Service for managing user data
        UserService userService;

        public CreateUserdataViewModel(UserService userService)
        {
            this.userService = userService;
        }

        // Command to save the user profile data
        [RelayCommand]
        async Task gemProfildataKnap()
        {
            if (NullorWhitespace())
            {
                try
                {
                    // Validate the input height and weight
                    if (Convert.ToInt32(Hoejde) > 400 || Convert.ToInt32(Hoejde) < 10)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error!", "Udfyld højde mellem 10 og 400 cm!", "OK");
                        return;
                    }
                    if (Convert.ToInt32(Vaegt) > 500 || Convert.ToInt32(Vaegt) < 1)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error!", "Udfyld vægt mellem 1 og 500 kg!", "OK");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error!", $"Indtast venligst kun tal! {ex.Message}", "OK");
                    return;
                }

                User user = new User();


                user.birthdate = Foedselsdato.Date;
                user.height = Convert.ToInt32(Hoejde);
                user.weight = Convert.ToInt32(Vaegt);
                user.gender = Koen;

                try
                {
                    // Update the user data using the service
                    var us = await userService.UpdateUser(user);

                    await Application.Current.MainPage.DisplayAlert("Success", $"Bruger opdateret! {us.birthdate + us.gender + us.weight + us.gender}", "OK");

                    // Navigate to the goal registration screen
                    await Shell.Current.GoToAsync(nameof(CreateGoalPage), false);

                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error!", $"Fejl i serveren! {ex.Message}", "OK");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error!", "Ingen tomme felter tak!", "OK");
            }
        }

        // Method to check if any input fields are null or whitespace
        public bool NullorWhitespace()
        {
            return !string.IsNullOrWhiteSpace(Foedselsdato.ToString()) && !string.IsNullOrWhiteSpace(Hoejde) && !string.IsNullOrWhiteSpace(Vaegt) && !string.IsNullOrWhiteSpace(Koen);
        }
    }
}


