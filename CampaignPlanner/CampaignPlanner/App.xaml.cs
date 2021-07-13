using CampaignPlanner.Services;
using CampaignPlanner.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CampaignPlanner
{
    public partial class App : Application
    {

        public static TownDataService TownDataService { get; } = new TownDataService();
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            //SeedData();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private async void SeedData()
        {
            await TownDataService.AddItemAsync(new Models.Town() { Name = "test" });
        }
    }
}
