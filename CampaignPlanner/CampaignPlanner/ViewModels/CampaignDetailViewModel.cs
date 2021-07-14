using CampaignPlanner.Models;
using CampaignPlanner.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Linq;
using CampaignPlanner.Utility;

namespace CampaignPlanner.ViewModels
{
    [QueryProperty(nameof(CampaignId), nameof(CampaignId))]
    public class CampaignDetailViewModel : BaseViewModel
    {
        private int _campaignId;

        private IDataService<Campaign> _campaignDataService;
        private IDataService<Town> _townDataService;
        private IDataService<Keyword> _keywordDataService;
        private ObservableCollection<Town> _towns;
        private ObservableCollection<Keyword> _selectedKeywords;
        private Town _selectedTown;

        private string _name;
        private ObservableCollection<Keyword> _keywords;
        private double _bidAmount;
        private double _campaignFund;
        private bool _status;
        private Town _town;
        private int _radius;
        private double _emeraldAccountFunds;

        public ObservableCollection<Keyword> SelectedKeywords
        {
            get => _selectedKeywords;
            set
            {
                _selectedKeywords = value;
                SetProperty(ref _selectedKeywords, value);
            }
        }
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

        public ObservableCollection<Keyword> Keywords
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
                LoadCampaignId(value);
            }
        }

        public Command DeleteCommand { get; }
        public Command UpdateCommand { get; }
        public Command CampaignFundCommand { get; }

        public CampaignDetailViewModel(IDataService<Campaign> campaignDataService, IDataService<Town> townDataService, IDataService<Keyword> keywordDataService)
        {
            _campaignDataService = campaignDataService;
            _townDataService = townDataService;
            _keywordDataService = keywordDataService;
            DeleteCommand = new Command(OnDelete);
            UpdateCommand = new Command(OnUpdate);
            CampaignFundCommand = new Command(OnCampaignFund);
            _towns = new ObservableCollection<Town>();
            _keywords = new ObservableCollection<Keyword>();
            _selectedKeywords = new ObservableCollection<Keyword>();
            SelectedKeywords = new ObservableCollection<Keyword>();
            SelectedTown = new Town();
            PopulateTowns();
            PopulateKeywords();
        }


        private void PopulateTowns()
        {
            try
            {
                Towns = new ObservableCollection<Town>();
                var dbTowns = _townDataService.GetItems();
                foreach (var town in dbTowns)
                {
                    Towns.Add(town);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to Populate Towns", ex.Message);
            }
        }

        private void PopulateKeywords()
        {
            try
            {
                Keywords = new ObservableCollection<Keyword>();
                var dbKeywords = _keywordDataService.GetItems();
                foreach (var keyword in dbKeywords)
                {
                    Keywords.Add(keyword);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to Populate Keywords", ex.Message);
            }
        }

        private void OnCampaignFund()
        {
            EmeraldAccountFunds = App.EmeraldAccountFunds - CampaignFund;
        }

        public void LoadCampaignId(int campaignId)
        {
            try
            {                
                var campaign = _campaignDataService.GetItem(campaignId);
                Id = campaign.Id;
                Name = campaign.Name;
                BidAmount = campaign.BidAmount;
                CampaignFund = campaign.CampaignFund;
                Status = campaign.Status;
                Town = campaign.Town;
                Radius = campaign.Radius;
                SelectedTown = Towns.FirstOrDefault(t => t.Id == campaign.Town.Id);
                if (SelectedKeywords == null)
                {
                    SelectedKeywords = new ObservableCollection<Keyword>();
                }
                else
                {
                    SelectedKeywords.SafeClear();
                }
                foreach (var keyword in campaign.Keywords)
                {
                    var listKeyword = Keywords.FirstOrDefault(k => k.Id == keyword.Id);
                    SelectedKeywords.Add(keyword);
                }
                EmeraldAccountFunds = App.EmeraldAccountFunds - CampaignFund;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to Load Campaign", ex.Message);
            }
        }

        private async void OnDelete()
        {
            try
            {
                await _campaignDataService.DeleteItemAsync(CampaignId);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to Delete Campaign", ex.Message);

            }
            finally
            {
                await Shell.Current.GoToAsync("..");
            }
        }

        private async void OnUpdate()
        {
            try
            {
                var campaign = new Campaign();
                campaign.Id = CampaignId;
                campaign.Name = Name;
                campaign.BidAmount = BidAmount;
                campaign.CampaignFund = CampaignFund;
                campaign.Radius = Radius;
                campaign.Status = Status;
                campaign.Town = SelectedTown;
                campaign.Keywords = SelectedKeywords.ToList();
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
