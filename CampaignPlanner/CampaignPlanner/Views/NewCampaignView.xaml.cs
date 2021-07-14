using CampaignPlanner.Models;
using CampaignPlanner.Utility;
using CampaignPlanner.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CampaignPlanner.Views
{
    public partial class NewCampaignView : ContentPage
    {
        public Campaign Campaign { get; set; }

        public NewCampaignView()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.NewCampaignViewModel;
        }
    }
}