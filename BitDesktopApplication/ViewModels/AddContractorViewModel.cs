using BitDesktopApplication.Commands;
using BitDesktopApplication.Models;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitDesktopApplication.BLL;
using System.Windows;

namespace BitDesktopApplication.ViewModels
{
    class AddContractorViewModel
    {
        private Contractor _contractor;

        private RelayCommand _saveCommand;
        private ObservableCollection<State> _states;
        private ObservableCollection<Title> _titles;
        private ObservableCollection<Region> _regions;

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
            if (this.Contractor.AddContractor() != -1)
            {
                MessageBox.Show("Save Successful", "Success");
            }
            else
            {
                MessageBox.Show("Unable to save contractor", "Error");
            }

        }

        public ObservableCollection<State> States
        {
            get { return _states; }
            set { this._states = value; }
        }
        public ObservableCollection<Title> Titles
        {
            get { return _titles; }
            set { this._titles = value; }
        }
        public Contractor Contractor
        {
            get { return _contractor; }
            set { this._contractor = value; }
        }
        public ObservableCollection<Region> Regions
        {
            get { return _regions; }
            set { this._regions = value; }
        }


        public AddContractorViewModel()
        {
            Contractor = new Contractor();
            States stateList = new States();
            this.States = new ObservableCollection<State>(stateList);
            Titles titleList = new Titles();
            this.Titles = new ObservableCollection<Title>(titleList);
            Regions regionList = new Regions();
            this.Regions = new ObservableCollection<Region>(regionList);
        }


    }
}
