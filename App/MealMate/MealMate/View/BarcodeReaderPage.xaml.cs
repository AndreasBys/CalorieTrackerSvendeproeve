using ZXing;

namespace MealMate.View;

// Page for barcode scanning functionality
public partial class BarcodeReaderPage : ContentPage
{
    public TaskCompletionSource<string> BarcodeScanTaskCompletionSource { get; set; }

    // Constructor to initialize the page
    public BarcodeReaderPage()
    {

        InitializeComponent();

        // Configure the barcode reader options
        barcodeReader.Options = new ZXing.Net.Maui.BarcodeReaderOptions
        {
            Formats = ZXing.Net.Maui.BarcodeFormat.Ean13 // Set the barcode format to Ean13
        };
    }

    // Event handler for barcode detection
    public async void cameraView_barcodedetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
    {
        var barcode = e.Results.FirstOrDefault()?.Value; // Get the first barcode text

        if (!string.IsNullOrEmpty(barcode))
        {
            BarcodeScanTaskCompletionSource?.SetResult(barcode);

            await Shell.Current.Navigation.PopAsync(false);
        }
    }
}


