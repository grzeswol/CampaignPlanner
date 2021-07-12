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
    }
}

