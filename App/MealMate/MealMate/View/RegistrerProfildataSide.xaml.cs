using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace MealMate.View;

[QueryProperty(nameof(User), "user")]
public partial class RegistrerProfildataSide : ContentPage
{
	public RegistrerProfildataSide(RegistrerProfildataViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

}