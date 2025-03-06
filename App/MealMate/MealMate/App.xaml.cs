namespace MealMate
{
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mzc0NjU0MkAzMjM4MmUzMDJlMzBSNVYvbzM4K1BXejY3Y1JvMTVnaS9saHVqbWhDK2NPNUhldzd1K0tTV0cwPQ==");
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
