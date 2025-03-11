namespace MealMate.View;

public partial class AdminSelectedFood : ContentPage
{
	public AdminSelectedFood()
	{
		InitializeComponent();

    }

    public void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        // Perform required operation after examining e.Value
    }

    void Anuller(object sender, EventArgs e)
    {
;       App.Current.MainPage = new NavigationPage(new AdminHomePage());
    }
}