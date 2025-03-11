namespace MealMate.View;

public partial class AdminHomePage : ContentPage
{
	public AdminHomePage()
	{
		InitializeComponent();

        
    }
    void GetFood()
    {

    }

    void OnTextChanged(object sender, EventArgs e)
    {
        SearchBar searchBar = (SearchBar)sender;
        //searchResults.ItemsSource = DataService.GetSearchResults(searchBar.Text);
    }

    void foodListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {

    }
}