using BitDesktopApplication.BLL;
using BitDesktopApplication.Commands;
using BitDesktopApplication.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BitDesktopApplication.ViewModels
{
    class AddClientLocationViewModel
    {
        private ClientLocation _location;

        private RelayCommand _saveCommand;
        private ObservableCollection<State> _states;
        private ObservableCollection<Region> _regions;
        private Client _client;

        public RelayCommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(this.SaveMethod, true);
                }
                return _saveCommand;
            }
            set
            {
                _saveCommand = value;
            }
        }

        private void SaveMethod()
        { 
          if (Location.AddClientLocation() == 1)
            {
                MessageBox.Show("Save Successful", "Success");
            }
            else
            {
                MessageBox.Show("Unable to save location", "Error");
            }
        }

        public ObservableCollection<State> States
        {
            get { return _states; }
            set { this._states = value; }
        }
        public ClientLocation Location
        {
            get { return _location; }
            set { this._location = value; }
        }
        public ObservableCollection<Region> Regions
        {
            get { return _regions; }
            set { this._regions = value; }
        }
        public Client Client
        {
            get { return _client; }
            set { this._client = value; }
        }

        public AddClientLocationViewModel(Client client)
        {
            Client = client;
            Location = new ClientLocation();
            Location.ClientId = client.ClientId;
            States stateList = new States();
            this.States = new ObservableCollection<State>(stateList);
            Regions regionList = new Regions();
            this.Regions = new ObservableCollection<Region>(regionList);
        }
    }
}
