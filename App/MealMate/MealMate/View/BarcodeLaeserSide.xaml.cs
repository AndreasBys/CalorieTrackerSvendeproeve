using ZXing;

namespace MealMate.View;

// Page for barcode scanning functionality
public partial class BarcodeLaeserSide : ContentPage
{
    // Constructor to initialize the page
    public BarcodeLaeserSide()
    {
        InitializeComponent();

        // Configure the barcode reader options
        barcodeReader.Options = new ZXing.Net.Maui.BarcodeReaderOptions
        {
            Formats = ZXing.Net.Maui.BarcodeFormat.Ean13 // Set the barcode format to Ean13
        };
    }

    // Event handler for barcode detection
    private void cameraView_barcodedetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
    {
        // Get the first detected barcode result
        var first = e.Results[0];

        // Update the UI with the detected barcode value on the main thread
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            BarkodeResultat.Text = first.Value;
        });
    }
}


