namespace MealMate.View;

public partial class RegistrerSide : ContentPage
{
	public RegistrerSide()
	{
		InitializeComponent();
	}

    private async void Registrer_knap(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync(nameof(RegistrerProfildataSide), true);
    }

    private async void logindForside_knap(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("///" + nameof(StartSkaermSide), true);
    }
}