using System.Threading.Tasks;

namespace MealMate.View;

public partial class RegistrerProfildataSide : ContentPage
{
	public RegistrerProfildataSide(RegistrerProfildataViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    private async void gemProfilData_knap(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync(nameof(RegistrerMaalSide), true);
    }
}