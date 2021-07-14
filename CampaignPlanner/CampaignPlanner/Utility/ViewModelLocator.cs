using CampaignPlanner.Models;
using CampaignPlanner.Services;
using CampaignPlanner.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CampaignPlanner.Utility
{
    public static class ViewModelLocator
    {
        public static TownOverviewViewModel TownOverviewViewModel { get; set; } = new TownOverviewViewModel(App.TownDataService);
        public static NewTownViewModel NewTownViewModel { get; set; } = new NewTownViewModel(App.TownDataService);
        public static TownDetailViewModel TownDetailViewModel { get; set; } = new TownDetailViewModel(App.TownDataService);
        public static CampaignOverviewViewModel CampaignOverviewViewModel { get; set; } = new CampaignOverviewViewModel(App.CampaignDataService);
        public static NewCampaignViewModel NewCampaignViewModel { get; set; } = new NewCampaignViewModel(App.CampaignDataService, App.TownDataService, App.KeywordDataService);
        public static CampaignDetailViewModel CampaignDetailViewModel { get; set; } = new CampaignDetailViewModel(App.CampaignDataService, App.TownDataService, App.KeywordDataService);
    }
}

