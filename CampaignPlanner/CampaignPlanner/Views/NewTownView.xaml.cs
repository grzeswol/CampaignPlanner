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
    public partial class NewTownView : ContentPage
    {
        public NewTownView()
        {
            InitializeComponent();
            BindingContext = ViewModelLocator.NewTownViewModel;
        }
    }
}