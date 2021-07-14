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
    public class TownOverviewViewModel : BaseViewModel
    {
        private ObservableCollection<Town> _towns;
        private IDataService<Town> _townDataService;
        private Town _selectedTown;

        public ObservableCollection<Town> Towns
        {
            get => _towns;
            set
            {
                _towns = value;
                SetProperty(ref _towns, value);
            }
        }

        public Command<Town> TownTapped { get; }


        public ICommand LoadCommand { get; }
        public ICommand AddTownCommand { get; }

        public Town SelectedTown
        {
            get => _selectedTown;
            set
            {
                SetProperty(ref _selectedTown, value);
                OnTownSelected(value);
            }
        }

        public TownOverviewViewModel(IDataService<Town> townDataService)
        {
            _townDataService = townDataService;
            LoadCommand = new Command(async () => await ExecuteLoadTownsCommand());
            AddTownCommand = new Command(OnAddTown);
            TownTapped = new Command<Town>(OnTownSelected);
            Towns = new ObservableCollection<Town>();
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedTown = null;
        }

        async Task ExecuteLoadTownsCommand()
        {
            IsBusy = true;

            try
            {
                Towns.Clear();
                var towns = await _townDataService.GetItemsAsync(true);
                foreach (var town in towns)
                {
                    Towns.Add(town);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to Load Towns", ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void OnAddTown(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewTownView));
        }

        async void OnTownSelected(Town town)
        {
            if (town == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(TownDetailView)}?{nameof(TownDetailViewModel.TownId)}={town.Id}");
        }

    }
}
