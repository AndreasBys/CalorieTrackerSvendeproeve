using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealMate.ViewModels
{
    public partial class RegistrerProfildataViewModel : BaseViewModel
    {
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

        UserService userService;

        public RegistrerProfildataViewModel(UserService userService)
        {
            this.userService = userService;
        }



        [RelayCommand]
        async Task gemProfildataKnap()
        {
            if (NullorWhitespace())
            {
                try
                {
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
                    await Application.Current.MainPage.DisplayAlert("Error!", $"Indtast venligst kun tal! { ex.Message}", "OK");
                    return;
                }

                User user = new User();


                user.birthdate = Foedselsdato.ToString();
                user.height = Convert.ToInt32(Hoejde);
                user.weight = Convert.ToInt32(Vaegt);
                user.gender = Koen;

                try
                {
                    var us = await userService.UpdateUser(user);

                    await Application.Current.MainPage.DisplayAlert("Success", $"Bruger opdateret! {us.birthdate + us.gender + us.weight + us.gender}", "OK");

                    await Shell.Current.GoToAsync(nameof(RegistrerMaalSide), false);

                }
                catch (Exception ex )
                {
                    await Application.Current.MainPage.DisplayAlert("Error!", $"Fejl i serveren! {ex.Message}", "OK");
                    
                }

                

            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error!", "Ingen tomme felter tak!", "OK");
            }


        }

        public bool NullorWhitespace()
        {
            return !string.IsNullOrWhiteSpace(Foedselsdato.ToString()) && !string.IsNullOrWhiteSpace(Hoejde) && !string.IsNullOrWhiteSpace(Vaegt) && !string.IsNullOrWhiteSpace(Koen);
        }





    }
}
