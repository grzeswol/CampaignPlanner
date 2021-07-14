using CampaignPlanner.Models;
using CampaignPlanner.Services;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CampaignPlanner.ViewModels
{
    [QueryProperty(nameof(TownId), nameof(TownId))]
    public class TownDetailViewModel : BaseViewModel
    {
        private int townId;
        private string name;
        private IDataService<Town> _townDataService;

        public int Id { get; set; }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }


        public int TownId
        {
            get
            {
                return townId;
            }
            set
            {
                townId = value;
                LoadTownId(value);
            }
        }

        public Command DeleteTownCommand { get; }
        public Command UpdateTownCommand { get; }


        public TownDetailViewModel(IDataService<Town> townDataService)
        {
            _townDataService = townDataService;
            DeleteTownCommand = new Command(OnDelete);
            UpdateTownCommand = new Command(OnUpdate);
        }

        public async void LoadTownId(int townId)
        {
            try
            {
                var town = await _townDataService.GetItemAsync(townId);
                Id = town.Id;
                Name = town.Name;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Town");
            }
        }

        private async void OnDelete()
        {
            await _townDataService.DeleteItemAsync(TownId);
            await Shell.Current.GoToAsync("..");
        }

        private async void OnUpdate()
        {
            try
            {
                var town = new Town()
                {
                    Id = TownId,
                    Name = Name
                };
                await _townDataService.UpdateItemAsync(town);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to Update Town", ex.Message);
            }
            finally
            {
                await Shell.Current.GoToAsync("..");
            }

        }
    }
}
