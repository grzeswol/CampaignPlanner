using CampaignPlanner.Models;
using CampaignPlanner.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;

namespace CampaignPlanner.ViewModels
{
    [QueryProperty(nameof(CampaignId), nameof(CampaignId))]
    public class CampaignDetailViewModel : BaseViewModel
    {
        private int _campaignId;

        private IDataService<Campaign> _campaignDataService;
        private IDataService<Town> _townDataService;
        private ObservableCollection<Town> _towns = new ObservableCollection<Town>();
        private Town _selectedTown;

        private string _name;
        private List<Keyword> _keywords = new List<Keyword>();
        private double _bidAmount;
        private double _campaignFund;
        private bool _status;
        private Town _town;
        private int _radius;
        private double _emeraldAccountFunds;

        public ObservableCollection<Town> Towns
        {
            get => _towns;
            set
            {
                _towns = value;
                SetProperty(ref _towns, value);
            }
        }
        public Town SelectedTown
        {
            get => _selectedTown;
            set => SetProperty(ref _selectedTown, value);
        }
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public double BidAmount
        {
            get => _bidAmount;
            set => SetProperty(ref _bidAmount, value);
        }

        public List<Keyword> Keywords
        {
            get => _keywords;
            set => SetProperty(ref _keywords, value);
        }

        public double CampaignFund
        {
            get => _campaignFund;
            set => SetProperty(ref _campaignFund, value);

        }

        public bool Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        public Town Town
        {
            get => _town;
            set => SetProperty(ref _town, value);
        }

        public int Radius
        {
            get => _radius;
            set => SetProperty(ref _radius, value);
        }

        public double EmeraldAccountFunds
        {
            get => _emeraldAccountFunds;
            set => SetProperty(ref _emeraldAccountFunds, value);

        }

        public int Id { get; set; }


        public int CampaignId
        {
            get
            {
                return _campaignId;
            }
            set
            {
                _campaignId = value;
                PopulateTowns();
                LoadCampaignId(value);
            }
        }

        public Command DeleteCommand { get; }
        public Command UpdateCommand { get; }
        public Command CampaignFundCommand { get; }

        public CampaignDetailViewModel(IDataService<Campaign> campaignDataService, IDataService<Town> townDataService)
        {
            _campaignDataService = campaignDataService;
            _townDataService = townDataService;
            DeleteCommand = new Command(OnDelete);
            UpdateCommand = new Command(OnUpdate);
            CampaignFundCommand = new Command(OnCampaignFund);
        }


        private async void PopulateTowns()
        {
            Towns.Clear();
            var dbTowns = await _townDataService.GetItemsAsync();
            foreach (var town in dbTowns)
            {
                Towns.Add(town);
            }
        }

        private void OnCampaignFund()
        {
            EmeraldAccountFunds = App.EmeraldAccountFunds - CampaignFund;
        }

        public async void LoadCampaignId(int campaignId)
        {
            try
            {
                var campaign = await _campaignDataService.GetItemAsync(campaignId);
                Id = campaign.Id;
                Name = campaign.Name;
                Keywords = campaign.Keywords;
                BidAmount = campaign.BidAmount;
                CampaignFund = campaign.CampaignFund;
                Status = campaign.Status;
                Town = campaign.Town;
                Radius = campaign.Radius;
                SelectedTown = Towns.FirstOrDefault(t => t.Id == campaign.Town.Id);
                EmeraldAccountFunds = App.EmeraldAccountFunds - CampaignFund;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to Load Campaign", ex.Message);
            }
        }

        private async void OnDelete()
        {
            await _campaignDataService.DeleteItemAsync(CampaignId);
            await Shell.Current.GoToAsync("..");
        }

        private async void OnUpdate()
        {
            try
            {
                var campaign = new Campaign();
                Keywords.Add(new Keyword() { Name = "test1" });
                campaign.Id = CampaignId;
                campaign.Name = Name;
                campaign.BidAmount = BidAmount;
                campaign.CampaignFund = CampaignFund;
                campaign.Radius = Radius;
                campaign.Status = Status;
                campaign.Town = SelectedTown;
                campaign.Keywords = Keywords;
                await _campaignDataService.UpdateItemAsync(campaign);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to Update Campaign", ex.Message);
            }
            finally
            {
                await Shell.Current.GoToAsync("..");
            }

        }
    }
}
