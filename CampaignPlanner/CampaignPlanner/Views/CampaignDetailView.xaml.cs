﻿using CampaignPlanner.Utility;
using CampaignPlanner.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace CampaignPlanner.Views
{
    public partial class CampaignDetailView : ContentPage
    {
        public CampaignDetailView()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.CampaignDetailViewModel;
        }
    }
}