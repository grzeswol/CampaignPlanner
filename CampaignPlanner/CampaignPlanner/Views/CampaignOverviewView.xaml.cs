using CampaignPlanner.Utility;
using CampaignPlanner.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CampaignPlanner.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CampaignOverviewView : ContentPage
    {
        CampaignOverviewViewModel _campaignOverviewViewModel;
        public CampaignOverviewView()
        {
            InitializeComponent();
            BindingContext = _campaignOverviewViewModel = ViewModelLocator.CampaignOverviewViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _campaignOverviewViewModel.OnAppearing();
        }
    }
}