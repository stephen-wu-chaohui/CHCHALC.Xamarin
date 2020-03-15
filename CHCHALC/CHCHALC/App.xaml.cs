using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CHCHALC.Services;
using CHCHALC.Views;

namespace CHCHALC
{
    public partial class App : Application
    {
        public static string AzureBackendUrl = "https://breadapi.azurewebsites.net";
        public static bool Developing = true;

        public App()
        {
            InitializeComponent();
            DependencyService.Register<MockDataStore>();
            DependencyService.Register<GroupStore>();
            DependencyService.Register<DiscipleStore>();
            DependencyService.Register<ParticipateStore>();
            DependencyService.Register<PostStore>();
            DependencyService.Register<YoutubeReader>();
            DependencyService.Register<AzureBlogStorage>();
            DependencyService.Register<ContextService>();
            DependencyService.Register<DataService>();

            MainPage = new MainTabbedPage();
        }

        protected override void OnStart()
        {
            var profileService = DependencyService.Get<IContextService>();
            profileService.LoginAsync();
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
