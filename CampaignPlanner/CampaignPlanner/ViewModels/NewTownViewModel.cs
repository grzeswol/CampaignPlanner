using CampaignPlanner.Models;
using CampaignPlanner.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CampaignPlanner.ViewModels
{
    public class NewTownViewModel : BaseViewModel
    {
        private IDataService<Town> _townDataService;

        private string name;

        public NewTownViewModel(IDataService<Town> townDataService)
        {
            _townDataService = townDataService;
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(name);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }


        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Town town = new Town()
            {
                Name = Name
            };


            await _townDataService.AddItemAsync(town);

            await Shell.Current.GoToAsync("..");
        }
    }
}
