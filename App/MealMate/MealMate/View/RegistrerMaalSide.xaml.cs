namespace MealMate.View;

public partial class RegistrerMaalSide : ContentPage
{
	public RegistrerMaalSide(RegistrerMaalSideViewModel ViewModel)
	{
		InitializeComponent();
        BindingContext = ViewModel;
        
    }
}