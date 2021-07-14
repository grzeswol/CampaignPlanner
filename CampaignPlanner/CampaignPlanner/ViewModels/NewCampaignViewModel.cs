using CampaignPlanner.Models;
using CampaignPlanner.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using System.Diagnostics;

namespace CampaignPlanner.ViewModels
{
    public class NewCampaignViewModel : BaseViewModel
    {
        private IDataService<Campaign> _campaignDataService;
        private IDataService<Town> _townDataService;
        private IDataService<Keyword> _keywordDataService;
        private List<Town> _towns = new List<Town>();
        private Town _selectedTown;

        private string _name;
        private ObservableCollection<Keyword> _keywords = new ObservableCollection<Keyword>();
        private ObservableCollection<Keyword> _selectedKeywords = new ObservableCollection<Keyword>();
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

        public List<Town> Towns
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
            set
            {
                SetProperty(ref _campaignFund, value);
                EmeraldAccountFunds = App.EmeraldAccountFunds - value;
            }

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


        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Command CampaignFundCommand { get; }

        public NewCampaignViewModel(IDataService<Campaign> campaignDataService, IDataService<Town> townDataService, IDataService<Keyword> keywordDataService)
        {
            _campaignDataService = campaignDataService;
            _townDataService = townDataService;
            _keywordDataService = keywordDataService;
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            CampaignFundCommand = new Command(OnCampaignFund);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            Towns = new List<Town>();
            Keywords = new ObservableCollection<Keyword>();
            PopulateTowns();
            PopulateKeywords();
            EmeraldAccountFunds = App.EmeraldAccountFunds - CampaignFund;
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(_name) &&
                _selectedKeywords.Count > 0 &&
                _bidAmount > App.MIN_BID_AMOUNT &&
                _campaignFund > 0 &&
                _radius > 0;
        }

        private async void PopulateTowns()
        {
            try
            {
                Towns.Clear();
                var dbTowns = await _townDataService.GetItemsAsync();
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

        private async void PopulateKeywords()
        {
            try
            {
                Keywords.Clear();
                var dbKeywords = await _keywordDataService.GetItemsAsync();
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


        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private void OnCampaignFund()
        {
            EmeraldAccountFunds = App.EmeraldAccountFunds - CampaignFund;
        }

        private async void OnSave()
        {

            Campaign campaign = new Campaign()
            {
                Name = Name,
                BidAmount = BidAmount,
                CampaignFund = CampaignFund,
                Radius = Radius,
                Status = Status,
                Town = SelectedTown,
                Keywords = SelectedKeywords.ToList()
            };

            try
            {
                await _campaignDataService.AddItemAsync(campaign);
                App.EmeraldAccountFunds -= CampaignFund;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to Save Campaign", ex.Message);
            }
            finally
            {
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}
