using CampaignPlanner.Models;
using CampaignPlanner.Services;
using CampaignPlanner.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace CampaignPlanner.ViewModels
{
    public class CampaignOverviewViewModel : BaseViewModel
    {
        private ObservableCollection<Campaign> _campaigns;
        private IDataService<Campaign> _campaignDataService;
        private Campaign _selectedCampaign;

        public ObservableCollection<Campaign> Campaigns
        {
            get => _campaigns;
            set
            {
                _campaigns = value;
                SetProperty(ref _campaigns, value);
            }
        }

        public Command<Campaign> CampaignTapped { get; }


        public ICommand LoadCommand { get; }
        public ICommand AddCommand { get; }

        public Campaign SelectedCampaign
        {
            get => _selectedCampaign;
            set
            {
                SetProperty(ref _selectedCampaign, value);
                OnCampaignSelected(value);
            }
        }

        public CampaignOverviewViewModel(IDataService<Campaign> campaignDataService)
        {
            _campaignDataService = campaignDataService;
            LoadCommand = new Command(async () => await ExecuteLoadCampaignsCommand());
            AddCommand = new Command(OnAddCampaign);
            CampaignTapped = new Command<Campaign>(OnCampaignSelected);
            Campaigns = new ObservableCollection<Campaign>();
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedCampaign = null;
        }

        async Task ExecuteLoadCampaignsCommand()
        {
            IsBusy = true;

            try
            {
                Campaigns.Clear();
                var campaigns = await _campaignDataService.GetItemsAsync(true);
                foreach (var campaign in campaigns)
                {
                    Campaigns.Add(campaign);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void OnAddCampaign(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewCampaignView));
        }

        async void OnCampaignSelected(Campaign campaign)
        {
            if (campaign == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(CampaignDetailView)}?{nameof(CampaignDetailViewModel.CampaignId)}={campaign.Id}");
        }

    }
}
