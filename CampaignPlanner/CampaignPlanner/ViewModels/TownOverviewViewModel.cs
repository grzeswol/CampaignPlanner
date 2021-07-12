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
        private ITownDataService<Town> _townDataService;
        public ObservableCollection<Town> Towns
        {
            get => _towns;
            set
            {
                _towns = value;
                SetProperty(ref _towns, value);
            }
        }


        public ICommand LoadCommand { get; }
        public ICommand AddTownCommand { get; }

        public TownOverviewViewModel(ITownDataService<Town> townDataService)
        {
            _townDataService = townDataService;
            LoadCommand = new Command(async () => await ExecuteLoadTownsCommand());
            AddTownCommand = new Command(OnAddTown);
            Towns = new ObservableCollection<Town>();
        }

        public void OnAppearing()
        {
            IsBusy = true;
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
                Debug.WriteLine(ex);
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

    }
}
