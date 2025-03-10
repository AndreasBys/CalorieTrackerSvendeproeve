namespace MealMate.View;

public partial class RegistrerMaalSide : ContentPage
{
	public RegistrerMaalSide(RegistrerMaalSideViewModel ViewModel)
	{
		InitializeComponent();
        BindingContext = ViewModel;
        
    }

    private void gemMaal_knap(object sender, EventArgs e)
    {

    }
}