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
using System.Windows.Navigation;

namespace BitDesktopApplication.ViewModels
{
    class AddClientViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        private Client _client;

        private RelayCommand _saveCommand;
        private ObservableCollection<Title> _titles;

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
            if (this.Client.AddClient() != -1)
            {
                this.Client.GetLastInsertedClientId();
                MessageBox.Show("Save Successful", "Success");
                //NavigationService.Navigate()
            }
            else
            {
                MessageBox.Show("Unable to save client", "Error");
            }
            
        }

        public ObservableCollection<Title> Titles
        {
            get { return _titles; }
            set { this._titles = value; }
        }
        public Client Client
        {
            get { return _client; }
            set { this._client = value; OnPropertyChanged("Client"); }
        }



        public AddClientViewModel()
        {
            Client = new Client();
            Titles titleList = new Titles();
            this.Titles = new ObservableCollection<Title>(titleList);
        }
    }
}
