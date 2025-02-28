namespace MealMate
{
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NMaF1cXmhKYVRpR2Nbek55flRFalxQVAciSV9jS3tTdEdrWXZfc3RURWBYWA==");
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
