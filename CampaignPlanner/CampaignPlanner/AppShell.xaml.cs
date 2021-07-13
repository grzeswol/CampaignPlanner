using CampaignPlanner.ViewModels;
using CampaignPlanner.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CampaignPlanner
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(NewTownView), typeof(NewTownView));
            Routing.RegisterRoute(nameof(TownDetailView), typeof(TownDetailView));
        }

    }
}
