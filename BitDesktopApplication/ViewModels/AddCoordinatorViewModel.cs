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
    class AddCoordinatorViewModel
    {
        private Coordinator _coordinator;

        private RelayCommand _saveCommand;
        private ObservableCollection<State> _states;
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
        public ObservableCollection<State> States
        {
            get { return _states; }
            set { this._states = value;  }
        }
        public ObservableCollection<Title> Titles
        {
            get { return _titles; }
            set { this._titles = value; }
        }

        public void SaveMethod()
        {
            if (Coordinator.AddCoordinator() != -1)
            {

                MessageBox.Show("Save Successful", "Success");
            }
            else
            {
                //MessageBox.Show("Unable to save Coordinator", "Error");
                
            }
        }

        public Coordinator Coordinator
        {
            get { return _coordinator; }
            set { _coordinator = value; }
        }

        public AddCoordinatorViewModel()
        {
            this.States = null;
            this.Titles = null;
            

            Coordinator = new Coordinator();
            States stateList = new States();
            this.States = new ObservableCollection<State>(stateList);
            Titles titleList = new Titles();
            this.Titles = new ObservableCollection<Title>(titleList);
        }
    }
}
