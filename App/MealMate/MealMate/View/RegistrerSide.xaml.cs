namespace MealMate.View;

public partial class RegistrerSide : ContentPage
{
	public RegistrerSide(RegistrerViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;

	}

  //  private async void Registrer_knap(object sender, EventArgs e)
  //  {
		//await Shell.Current.GoToAsync(nameof(RegistrerProfildataSide), true);
  //  }

    // Event handler for the login button tap
    private async void logindForside_knap(object sender, TappedEventArgs e)
    {
        // Navigate back to the StartSkaermSide page
        await Shell.Current.GoToAsync("///" + nameof(StartSkaermSide), true);
    }
}