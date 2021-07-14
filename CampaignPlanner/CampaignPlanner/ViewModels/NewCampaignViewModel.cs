using CampaignPlanner.Models;
using CampaignPlanner.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CampaignPlanner.ViewModels
{
    public class NewCampaignViewModel : BaseViewModel
    {
        private IDataService<Campaign> _campaignDataService;
        private IDataService<Town> _townDataService;
        private List<Town> _towns = new List<Town>();
        private Town _selectedTown;

        private string _name;
        private List<Keyword> _keywords = new List<Keyword>();
        private double _bidAmount;
        private double _campaignFund;
        private bool _status;
        private Town _town;
        private int _radius;
        private double _emeraldAccountFunds;

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

        public List<Keyword> Keywords
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

        public NewCampaignViewModel(IDataService<Campaign> campaignDataService, IDataService<Town> townDataService)
        {
            _campaignDataService = campaignDataService;
            _townDataService = townDataService;
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            CampaignFundCommand = new Command(OnCampaignFund);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            PopulateTowns();
            EmeraldAccountFunds = App.EmeraldAccountFunds - CampaignFund;
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(_name) &&
                //_keywords.Count > 0 &&
                _bidAmount > App.MIN_BID_AMOUNT &&
                _campaignFund > 0 &&
                _radius > 0;
        }

        private async void PopulateTowns()
        {
            var dbTowns = await _townDataService.GetItemsAsync();
            foreach (var town in dbTowns)
            {
                Towns.Add(town);
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
            Keywords.Add(new Keyword() { Name = "test1" });
            Campaign campaign = new Campaign()
            {
                Name = Name,
                BidAmount = BidAmount,
                CampaignFund = CampaignFund,
                Radius = Radius,
                Status = Status,
                Town = SelectedTown,
                Keywords = Keywords
            };


            await _campaignDataService.AddItemAsync(campaign);
            App.EmeraldAccountFunds -= CampaignFund;

            await Shell.Current.GoToAsync("..");
        }
    }
}
