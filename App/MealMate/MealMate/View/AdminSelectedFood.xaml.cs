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

    // Constructor to initialize the page and find UI elements by their names
    public AdminSelectedFood(FoodService foodService)
    {
        InitializeComponent();
        _foodService = foodService;

        // Find UI elements by their names
        _nameEntry = this.FindByName<Entry>("nameEntry");
        _barcodeEntry = this.FindByName<Entry>("barcodeEntry");
        _caloriesEntry = this.FindByName<Entry>("caloriesEntry");
        _proteinEntry = this.FindByName<Entry>("proteinEntry");
        _carbsEntry = this.FindByName<Entry>("carbsEntry");
        _fatEntry = this.FindByName<Entry>("fatEntry");
        _isApprovedEntry = this.FindByName<CheckBox>("isApprovedEntry");
    }

    // Property to get and set the selected Food object passed from the previous page
    public Food SelectedFood
    {
        get => _food;
        set
        {
            BindingContext = value;
            _food = value;
        }
    }

    // Event handler for the save button click
    private async void Save(object sender, EventArgs e)
    {
        // Create a new Food object with the updated values from the entries
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
            // Update the food item using the FoodService
            var foodResponse = await _foodService.UpdateFood(food, food._id);
            await DisplayAlert("Success", "Food updated!", "OK");

            // Navigate back to the AdminHomePage
            await Shell.Current.GoToAsync($"{nameof(AdminHomePage)}");
        }
        catch (Exception ex)
        {
            // Display an alert if an error occurs during the update
            await DisplayAlert("Error", $"error: {ex.Message}", "OK");
        }
    }

    // Event handler for the cancel button click
    private async void Cancel(object sender, EventArgs e)
    {
        // Navigate back to the AdminHomePage
        await Shell.Current.GoToAsync($"{nameof(AdminHomePage)}");
    }


    // Event handler for the delete button click
    private async void DeleteFood(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Confirm Delete", "Are you sure you want to delete this food item?", "Yes", "No");
        if (confirm)
        {
            await _foodService.DeleteFood(_food._id);
            // Navigate back to the AdminHomePage
            await Shell.Current.GoToAsync($"{nameof(AdminHomePage)}");
        }
    }

}