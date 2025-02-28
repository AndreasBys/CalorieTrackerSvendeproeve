using Android.App;
using Android.Content.Res;
using Android.Runtime;

namespace MealMate
{
    [Application]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {

            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
            {
                if (view is Entry)
                {
                    // Remove underline
                    handler.PlatformView.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Transparent);

                    // Change placeholder text color
                    handler.PlatformView.SetHintTextColor(ColorStateList.ValueOf(Android.Graphics.Color.Red));
                }
            });

            Microsoft.Maui.Handlers.PickerHandler.Mapper.AppendToMapping(nameof(Picker), (handler, view) =>
            {
                if (view is Picker)
                {
                    // Remove underline
                    handler.PlatformView.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Transparent);

                    // Change placeholder text color
                    handler.PlatformView.SetHintTextColor(ColorStateList.ValueOf(Android.Graphics.Color.White));
                }
            });

            Microsoft.Maui.Handlers.DatePickerHandler.Mapper.AppendToMapping(nameof(DatePicker), (handler, view) =>
            {
                if (view is DatePicker)
                {
                    // Remove underline
                    handler.PlatformView.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Transparent);

                    // Change placeholder text color
                    handler.PlatformView.SetHintTextColor(ColorStateList.ValueOf(Android.Graphics.Color.White));
                }
            });


        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        

    }
}
