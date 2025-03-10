using System.Globalization;
using System.Threading.Tasks;

namespace MealMate.View;

public partial class RegistrerMaalSide : ContentPage
{
	public RegistrerMaalSide()
	{
		InitializeComponent();

        
    }

    private async void gemMaal_kmaå(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(HjemmeskaermSide), true);
    }


}