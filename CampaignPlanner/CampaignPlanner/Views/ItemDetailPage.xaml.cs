using CampaignPlanner.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace CampaignPlanner.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}