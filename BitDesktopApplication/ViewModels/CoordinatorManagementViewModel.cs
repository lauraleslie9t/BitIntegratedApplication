using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitDesktopApplication.ViewModels;
using BitDesktopApplication.Views;
using BitDesktopApplication.Models;
using System.Collections.ObjectModel;
using BitDesktopApplication.BLL;
using System.ComponentModel;
using BitDesktopApplication.Commands;
using System.Windows;

namespace BitDesktopApplication.ViewModels
{
    public class CoordinatorManagementViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Coordinator> _coordinators;
        private ObservableCollection<State> _states;
        private ObservableCollection<Title> _titles;

        private string _searchOption;
        private DateTime _searchDate;
        private string _searchText;


        private Coordinator _selectedCoordinator;

        private RelayCommand _deleteCoordinatorCommand;
        private RelayCommand _searchCommand;
        private RelayCommand _editCoordinatorCommand;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        public RelayCommand DeleteCoordinatorCommand
        {
            get
            {
                if (_deleteCoordinatorCommand == null)
                {
                    _deleteCoordinatorCommand = new RelayCommand(this.DeleteCoordinatorMethod, true);
                }
                return _deleteCoordinatorCommand;
            }
            set
            {
                _deleteCoordinatorCommand = value;
            }
        }

        private void DeleteCoordinatorMethod()
        {
            if (SelectedCoordinator == null)
            {
                MessageBox.Show("Please select a coordinator to delete. ", "Selection Required");
                
            }
            else
            {
                MessageBoxResult result =  MessageBox.Show("Are you sure you want to delete this coordinator?", "Delete Coordinator?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes) 
                {
                    if (SelectedCoordinator.DeleteCoordinator() == 2)
                    {
                        MessageBox.Show("Deletion Successful");
                        RefreshCoordinatorList();
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
                    Coordinators coordList = new Coordinators();
                    coordList.GetCoordinatorsById(id);
                    Coordinators = new ObservableCollection<Coordinator>(coordList);
                }
                else
                {
                    MessageBox.Show("Id must be a number");
                }
            }
            else if (SearchOption == "First Name")
            {
                Coordinators coordList = new Coordinators();
                coordList.GetCoordinatorsByFirstName(SearchText);
                Coordinators = new ObservableCollection<Coordinator>(coordList);
            }
            else if (SearchOption == "Last Name")
            {
                Coordinators coordList = new Coordinators();
                coordList.GetCoordinatorsByLastName(SearchText);
                Coordinators = new ObservableCollection<Coordinator>(coordList);
            }
            else if (SearchOption == "Date of Birth")
            {
                Coordinators coordList = new Coordinators();
                coordList.GetCoordinatorsByDob(_searchDate);
                Coordinators = new ObservableCollection<Coordinator>(coordList);
            }
            else
            {
                MessageBox.Show("Please select a search Option");
            }


        }
        public RelayCommand EditCoordinatorCommand
        {
            get
            {
                if (_editCoordinatorCommand == null)
                {
                    _editCoordinatorCommand = new RelayCommand(this.EditMethod, true);
                }
                return _editCoordinatorCommand;
            }
            set
            {
                _editCoordinatorCommand = value;
            }
        }
        private void EditMethod()
        {
            if (SelectedCoordinator == null)
            {
                MessageBox.Show("Please select a coordinator to edit. ", "Selection Required");

            }
            else
            {
                if (SelectedCoordinator.EditCoordinator() != -1)
                {
                    MessageBox.Show("Successfully edited coordinator.");
                }
                else
                {
                    MessageBox.Show("Unable to edit coordinator at this time. ");
                }
            }
        }

        public ObservableCollection<Coordinator> Coordinators
        {
            get { return _coordinators; }

            set { _coordinators = value; OnPropertyChanged("Coordinators"); }
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
        public Coordinator SelectedCoordinator
        {
            get { return _selectedCoordinator; }
            set { _selectedCoordinator = value; OnPropertyChanged("SelectedCoordinator"); }
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
        public virtual ObservableCollection<Coordinator> GetCoordinators()
        {
            RefreshCoordinatorList();
            States stateList = new States();
            this.States = new ObservableCollection<State>(stateList);
            Titles titleList = new Titles();
            this.Titles = new ObservableCollection<Title>(titleList);
            return Coordinators;
        }
        public CoordinatorManagementViewModel()
        {
            GetCoordinators();
        }

        public void RefreshCoordinatorList()
        {
            Coordinators allCoordinators = new Coordinators();
            allCoordinators.GetAllCoordinators();
            this.Coordinators = new ObservableCollection<Coordinator>(allCoordinators);
        }
    }
}
