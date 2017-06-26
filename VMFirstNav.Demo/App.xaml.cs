using Xamarin.Forms;

namespace VMFirstNav.Demo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();


			// ******
            // MasterDetail Pages
			 MainPage = new MasterDetailRootPage();
			// ******

			// ******
            // Tab pages
			// MainPage = new RootTabPage();
			// ******


			// ******
			// Sets up a normal navigation stack

			//var normalVM = new NormalOneViewModel();
   //         var normalPage = new NormalOnePage { ViewModel = normalVM };
			
			//MainPage = new NavigationPage(normalPage);
			// ******
		}

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
