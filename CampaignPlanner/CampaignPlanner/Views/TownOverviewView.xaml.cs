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
    public partial class TownOverviewView : ContentPage
    {
        TownOverviewViewModel _townOverviewViewModel;
        public TownOverviewView()
        {
            InitializeComponent();
            BindingContext = _townOverviewViewModel = ViewModelLocator.TownOverviewViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _townOverviewViewModel.OnAppearing();
        }
    }
}