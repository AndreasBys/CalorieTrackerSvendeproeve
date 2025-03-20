namespace MealMate.View;

public partial class CreateGoalPage : ContentPage
{
	public CreateGoalPage(CreateGoalPageViewModel ViewModel)
	{
		InitializeComponent();
        BindingContext = ViewModel;
        
    }
}