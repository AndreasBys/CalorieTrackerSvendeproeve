
//using Camera.MAUI;
//using Camera.MAUI.ZXingHelper;

using ZXing;

namespace MealMate.View;

public partial class BarcodeLaeserSide : ContentPage
{
	public BarcodeLaeserSide()
	{

        InitializeComponent();


        barcodeReader.Options = new ZXing.Net.Maui.BarcodeReaderOptions
        {
            Formats = ZXing.Net.Maui.BarcodeFormat.Ean13
        };
    }

    private void cameraView_barcodedetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
    {
        var first = e.Results[0];

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            BarkodeResultat.Text = first.Value;
        });


    }

    //protected override void OnAppearing()
    //{
    //    base.OnAppearing();

    //    if (cameraView == null)
    //    {
    //        BarkodeResultat.Text = "Cameraview null";
    //        return;
    //    }

    //    cameraView.BarcodeDetected += cameraView_barcodedetected; 
    //    cameraView.BarCodeDetectionEnabled = true; 
    //}


    //private void cameraView_CamerasLoaded(object sender, EventArgs e)
    //{
    //    if (cameraView.Cameras.Count != 0)
    //    {
    //        cameraView.Camera = cameraView.Cameras.First();
    //        MainThread.BeginInvokeOnMainThread(async () =>
    //        {
    //            await cameraView.StopCameraAsync();
    //            var resultat = await cameraView.StartCameraAsync();

    //            if (resultat == CameraResult.AccessDenied)
    //            {
    //                BarkodeResultat.Text = "fejl 1";
    //            }
    //            if (resultat == CameraResult.NoCameraSelected)
    //            {
    //                BarkodeResultat.Text = "fejl 2";
    //            }
    //            if (resultat == CameraResult.AccessError)
    //            {
    //                BarkodeResultat.Text = "fejl 3";
    //            }
    //            if (resultat == CameraResult.Success)
    //            {
    //                BarkodeResultat.Text = "sucess";
    //                cameraView.BarcodeDetected += cameraView_barcodedetected;
    //                cameraView.BarCodeDetectionEnabled = true;
    //            }
    //            if (resultat == CameraResult.NotInitiated)
    //            {
    //                BarkodeResultat.Text = "fejl 4";
    //            }


    //        });
    //    }

    //}

    //private void cameraView_barcodedetected(object sender, BarcodeEventArgs args)
    //{
    //    BarkodeResultat.Text = "scan";

    //    MainThread.BeginInvokeOnMainThread(() =>
    //    {
    //        BarkodeResultat.Text = args.Result[0].Text;
    //    });
    //}

}