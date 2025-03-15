namespace MealMate.View;

[QueryProperty(nameof(SelectedFood), "SelectedFood")]
public partial class AdminSelectedFood : ContentPage
{
    private Food _food;
    private readonly FoodService _foodService;
    private Entry _nameEntry;
    private Entry _barcodeEntry;
    private Entry _caloriesEntry;
    private Entry _proteinEntry;
    private Entry _carbsEntry;
    private Entry _fatEntry;
    private CheckBox _isApprovedEntry;


    public AdminSelectedFood(FoodService foodService)
	{
		InitializeComponent();
        _foodService = foodService;

        _nameEntry = this.FindByName<Entry>("nameEntry");
        _barcodeEntry = this.FindByName<Entry>("barcodeEntry");
        _caloriesEntry = this.FindByName<Entry>("caloriesEntry");
        _proteinEntry = this.FindByName<Entry>("proteinEntry");
        _carbsEntry = this.FindByName<Entry>("carbsEntry");
        _fatEntry = this.FindByName<Entry>("fatEntry");
        _isApprovedEntry = this.FindByName<CheckBox>("isApprovedEntry");
    }

    public Food SelectedFood
    {
        get => _food;
        set
        {
            BindingContext = value;
            _food = value;
        }
    }

    private async void Gem(object sender, EventArgs e)
    {
        var food = new Food
        {
            _id = _food._id,
            name = _nameEntry.Text,
            barcode = _barcodeEntry.Text,
            calories = Convert.ToInt32(_caloriesEntry.Text),
            protein = Convert.ToInt32(_proteinEntry.Text),
            carbonhydrates = Convert.ToInt32(_carbsEntry.Text),
            fat = Convert.ToInt32(_fatEntry.Text),
            approved = _isApprovedEntry.IsChecked,
            user = _food.user
        };

        try
        {
            var foodResponse = await _foodService.UpdateFood(food, food._id);
            await DisplayAlert("Success", "Food updated!", "OK");
            await Shell.Current.GoToAsync($"{nameof(AdminHomePage)}");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"error: {ex.Message}", "OK");
        }

    }

    private async void Anuller(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(AdminHomePage)}");
    }
}