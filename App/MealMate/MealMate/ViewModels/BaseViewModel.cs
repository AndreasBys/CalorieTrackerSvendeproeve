namespace MealMate.ViewModels;

// BaseViewModel class that provides common functionality for all view models
public partial class BaseViewModel : ObservableObject
{
    // Observable property to indicate if a service is busy
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;

    // Property to indicate if a service is not busy (inverse of IsBusy)
    public bool IsNotBusy => !IsBusy;
}

