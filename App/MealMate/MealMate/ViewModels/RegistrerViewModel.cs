using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MealMate.ViewModels
{
    public partial class RegistrerViewModel : BaseViewModel
    {
        [ObservableProperty]
        string username = "";
        [ObservableProperty]
        string email = "";
        [ObservableProperty]
        string password = "";
        [ObservableProperty]
        string usernameLabel = "Username:";
        [ObservableProperty]
        string emailLabel = "Email:";

        LoginService LoginService;

        public RegistrerViewModel(LoginService loginService)
        {
            LoginService = loginService;
        }


        //[RelayCommand]
        //async Task Registrer()
        //{

        //    // 8-20 karaterer, A-Z i bogstaver, må gerne indholde numre og "_","-". Må ikke indeholde mellemrum og specielle karatere

        //    bool emailCorrect = false;
        //    bool usernameCorrect = false;

        //    if (Regex.IsMatch(Email, @"^(([^<>()[\]\\.,;:\s@']+(\.[^<>()[\]\\.,;:\s@']+)*)|.('.+'))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$"))
        //    {
        //        emailCorrect = true;
        //        EmailLabel = "Email:";
        //    }
        //    else
        //    {
        //        EmailLabel = "Email: skal være mindst 6 tegn, indeholde et @ og et punktum *";

        //    }
        //    if (Regex.IsMatch(Username, @"^[a-zA-Z][a-zA-Z0-9_-]{7,19}$"))
        //    {
        //        usernameCorrect = true;
        //        UsernameLabel = "Username:";

        //    }
        //    else
        //    {
        //        UsernameLabel = "Username: 8-20 tegn & ingen speciale karatere*";
        //    }
        //    if (emailCorrect && usernameCorrect)
        //    {

        //        try
        //        {
        //            User nyBruger = new User { username = Username, email = Email, password = Password };
        //            await LoginService.Register(nyBruger);

        //            await Shell.Current.GoToAsync(nameof(RegistrerProfildataSide), false);

        //        }
        //        catch (Exception ex)
        //        {
        //            await Application.Current.MainPage.DisplayAlert("Could not register!", ex.Message, "OK");
        //        }
                

        //    }

            





        //}

        //[RelayCommand]
        //async Task Tap()
        //{
        //    await Shell.Current.GoToAsync(nameof(StartSkaermSide),false);
        //}



    }
}
