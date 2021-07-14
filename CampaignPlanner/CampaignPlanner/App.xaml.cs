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
        public static CampaignDataService CampaignDataService { get; } = new CampaignDataService();
        public static KeywordDataService KeywordDataService { get; } = new KeywordDataService();
        public static double EmeraldAccountFunds { get; set; } = 20000.00;
        public static double MIN_BID_AMOUNT = 4;

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            SeedData();
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
            await CampaignDataService.DeleteAllItemsAsync();
            await TownDataService.DeleteAllItemsAsync();
            await KeywordDataService.DeleteAllItemsAsync();

            await TownDataService.AddItemAsync(new Models.Town() { Name = "New York" });
            await TownDataService.AddItemAsync(new Models.Town() { Name = "London" });
            await TownDataService.AddItemAsync(new Models.Town() { Name = "Oxford" });
            await TownDataService.AddItemAsync(new Models.Town() { Name = "San Francisco" });
            await KeywordDataService.AddItemAsync(new Models.Keyword() { Name = "Weather" });
            await KeywordDataService.AddItemAsync(new Models.Keyword() { Name = "Architecture" });
            await KeywordDataService.AddItemAsync(new Models.Keyword() { Name = "Buildings" });
            await KeywordDataService.AddItemAsync(new Models.Keyword() { Name = "People" });
        }
    }
}
