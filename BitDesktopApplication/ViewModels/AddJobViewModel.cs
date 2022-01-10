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
    class AddJobViewModel
    {
        private Client _client;
        private string _clientName;
        private Job _job;

        private RelayCommand _saveCommand;

        private ObservableCollection<Priority> _priorities;
        private ObservableCollection<ClientLocation> _clientLocations;

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
            if (Job.Location != null)
            {
                    Job.AddNewBooking(); 
            }

        }


        public Job Job
        {
            get { return _job; }
            set { this._job = value; }
        }
        public ObservableCollection<ClientLocation> ClientLocations
        {
            get { return _clientLocations; }
            set { this._clientLocations = value; }
        }
        public ObservableCollection<Priority> Priorities
        {
            get { return _priorities; }
            set { this._priorities = value; }
        }
        public Client Client
        {
            get { return _client; }
            set { this._client = value; }
        }
        public string ClientName
        {
            get { return _clientName; }
            set { this._clientName = value; }
        }
        public AddJobViewModel(Client client)
        {
            Client = client;
            ClientName = Client.FirstName + " " + Client.LastName;
            LoadComboboxes();
            Job = new Job();
            Job.RequiredCompDate = DateTime.Today;
        }

        //public AddJobViewModel(Job existingJob)
        //{
        //    this.Client = new Client();
        //    Client.ClientId = existingJob.Client.ClientId;
        //    Client.GetClientById();
        //    ClientName = Client.FirstName + " " + Client.LastName;
        //    LoadComboboxes();
        //    Job = existingJob;
        //}

        public void LoadComboboxes()
        {
            ClientLocations clientLocations = new ClientLocations(Client.ClientId);
            ClientLocations = new ObservableCollection<ClientLocation>(clientLocations);

            PriorityLevels priorities = new PriorityLevels();
            Priorities = new ObservableCollection<Priority>(priorities);
        }
    }
}
