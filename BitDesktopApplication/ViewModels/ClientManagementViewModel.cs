using BitDesktopApplication.BLL;
using BitDesktopApplication.Commands;
using BitDesktopApplication.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BitDesktopApplication.ViewModels
{
    public class ClientManagementViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Client> _clients;
        private ObservableCollection<ClientLocation> _clientLocations;
        private ObservableCollection<Job> _clientJobs;
        private ObservableCollection<State> _states;
        private ObservableCollection<Title> _titles;
        private ObservableCollection<Region> _regions;
        private Client _selectedClient;
        private ClientLocation _selectedLocation;
        private Job _selectedJob;
        private string _searchOption;
        private DateTime _searchDate;
        private string _searchText;
        

        private RelayCommand _deleteLocationCommand;
        private RelayCommand _deleteClientCommand;
        private RelayCommand _editClientCommand;
        private RelayCommand _searchCommand;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        public RelayCommand DeleteClientCommand
        {
            get
            {
                if (_deleteClientCommand == null)
                {
                    _deleteClientCommand = new RelayCommand(this.DeleteClientMethod, true);
                }
                return _deleteClientCommand;
            }
            set
            {
                _deleteClientCommand = value;
            }
        }
        private void DeleteClientMethod()
        {
            if (SelectedClient == null)
            {
                MessageBox.Show("Please select a coordiator to delete. ", "Selection Required");
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this client?", "Delete Client?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    if (SelectedClient.DeleteClient() == 2)
                    {
                        MessageBox.Show("Deletion Successful");
                        RefreshClientList();
                    }
                    else
                    {
                        MessageBox.Show("Deletion Failed!");
                        RefreshClientList();
                    }
                }
            }
        }
        public RelayCommand DeleteLocationCommand
        {
            get
            {
                if (_deleteLocationCommand == null)
                {
                    _deleteLocationCommand = new RelayCommand(this.DeleteLocationMethod, true);
                }
                return _deleteLocationCommand;
            }
            set
            {
                _deleteLocationCommand = value;
            }
        }
        private void DeleteLocationMethod()
        {
            if (SelectedLocation == null)
            {
                MessageBox.Show("Please select a coordiator to delete. ", "Selection Required");
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this location?", "Delete location?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    if (SelectedLocation.DeleteLocation() == 1)
                    {
                        MessageBox.Show("One Row Affected");
                        RefreshLocationList();
                    }
                    else
                    {
                        MessageBox.Show("Deletion Failed!");
                    }
                }
            }
        }
        public RelayCommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                {
                    _searchCommand = new RelayCommand(this.SearchMethod, true);
                }
                return _searchCommand;
            }
            set
            {
                _searchCommand = value;
            }
        }
        private void SearchMethod()
        {
            if (SearchOption == "Id")
            {
                if (Int32.TryParse(SearchText, out int id))
                {
                    Clients clientList = new Clients();
                    clientList.GetClientsById(id);
                    this.Clients = new ObservableCollection<Client>(clientList);
                }
                else
                {
                    MessageBox.Show("Id must be a number");
                }
            }
            else if (SearchOption == "First Name")
            {
                Clients clientList = new Clients();
                clientList.GetClientsByFirstName(SearchText);
                this.Clients = new ObservableCollection<Client>(clientList);
            }
            else if (SearchOption == "Last Name")
            {
                Clients clientList = new Clients();
                clientList.GetClientsByLastName(SearchText);
                this.Clients = new ObservableCollection<Client>(clientList);
            }
            else if (SearchOption == "Date of Birth")
            {
                Clients clientList = new Clients();
                clientList.GetClientsByDob(SearchDate);
                this.Clients = new ObservableCollection<Client>(clientList);
            }
            else 
            {
                MessageBox.Show("Please select a search Option");
            }


        }
        public RelayCommand EditClientCommand
        {
            get
            {
                if (_editClientCommand == null)
                {
                    _editClientCommand = new RelayCommand(this.EditMethod, true);
                }
                return _editClientCommand;
            }
            set
            {
                _editClientCommand = value;
            }
        }
        private void EditMethod()
        { 
            if (SelectedClient != null)
            {
                if (SelectedClient.EditClient() != -1)
                {
                    MessageBox.Show("Successfully edited client.");
                }
                else
                {
                    MessageBox.Show("Unable to edit client at this time. ");
                }
            }
            else
            {
                MessageBox.Show("Please select a client to edit.");
            }
        }
        public ObservableCollection<Client> Clients
        {
            get { return _clients; }

            set 
            {
                _clients = value;
                OnPropertyChanged("Clients");
            }
        }
        public ObservableCollection<ClientLocation> ClientLocations
        {
            get { return _clientLocations; }
            set 
            {
                this._clientLocations = value;
                OnPropertyChanged("ClientLocations");
            }
        }
        public ObservableCollection<Job> ClientJobs
        {
            get { return _clientJobs; }
            set
            {
                this._clientJobs = value;
                OnPropertyChanged("ClientJobs");
            }
        }
        public ObservableCollection<State> States
        {
            get { return _states; }
            set { this._states = value; OnPropertyChanged("States"); }
        }
        public ObservableCollection<Title> Titles
        {
            get { return _titles; }
            set { this._titles = value; OnPropertyChanged("Titles"); }
        }
        public ObservableCollection<Region> Regions
        {
            get { return _regions; }
            set { this._regions = value; OnPropertyChanged("Regions"); }
        }
        public Client SelectedClient
        {
            get { return _selectedClient; }
            set 
            { 
                _selectedClient = value;
                OnPropertyChanged("SelectedClient");
                RefreshLocationList();
                RefreshJobList();
            }
        }
        public ClientLocation SelectedLocation
        {
            get { return _selectedLocation; }
            set { this._selectedLocation = value; OnPropertyChanged("SelectedLocation"); }
        }
        public Job SelectedJob
        {
            get { return _selectedJob; }
            set { this._selectedJob = value; OnPropertyChanged("SelectedJob"); }
        }
        public string SearchOption
        {
            get { return _searchOption; }
            set { this._searchOption = value; OnPropertyChanged("SearchOption"); }
        }
        public DateTime SearchDate
        {
            get { return _searchDate; }
            set { this._searchDate = value; OnPropertyChanged("SearchDate"); }
        }
        public string SearchText
        {
            get { return _searchText; }
            set { this._searchText = value; OnPropertyChanged("SearchText"); }
        }
        //split up constructor for unit tests. 
        public virtual ObservableCollection<Client> GetClients()
        {
            RefreshClientList();
            SearchDate = DateTime.Today;
            //ClientLocations.Clear();
            //this.ClientLocations = new ObservableCollection<ClientLocation>();
            States stateList = new States();
            this.States = new ObservableCollection<State>(stateList);
            Regions regionList = new Regions();
            this.Regions = new ObservableCollection<Region>(regionList);
            Titles titleList = new Titles();
            this.Titles = new ObservableCollection<Title>(titleList);
            return Clients;
        }
        public ClientManagementViewModel()
        {
            GetClients();
        }

        private void RefreshClientList()
        {
            Clients allClients = new Clients();
            allClients.GetAllClients();
            this.Clients = new ObservableCollection<Client>(allClients);
        }
        private void RefreshLocationList()
        {
            if (SelectedClient != null)
            {
                //ClientLocations.Clear();
                ClientLocations clientlocations = new ClientLocations(SelectedClient.ClientId);
                this.ClientLocations = new ObservableCollection<ClientLocation>(clientlocations);
            }
            
        }
        private void RefreshJobList()
        {
            if (SelectedClient != null)
            {
                AllJobs clientJobs = new AllJobs(SelectedClient.ClientId, "Client");
                this.ClientJobs = new ObservableCollection<Job>(clientJobs);
            }

        }
    }
}
