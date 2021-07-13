using CampaignPlanner.Utility;
using CampaignPlanner.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace CampaignPlanner.Views
{
    public partial class TownDetailView : ContentPage
    {
        public TownDetailView()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.TownDetailViewModel;
        }
    }
}