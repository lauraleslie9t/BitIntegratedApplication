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
    class RejectedJobManagementViewModel : INotifyPropertyChanged
    {
        private Job _selectedJob;
        private ObservableCollection<Job> _rejectedJobs;

        private RelayCommand _searchCommand;

        private string _searchOption;
        private DateTime _searchDate;
        private string _searchText;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
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
            if (SearchOption == "Job No")
            {
                if (Int32.TryParse(SearchText, out int id))
                {
                    RejectedJobs jobList = new RejectedJobs(id);
                    this.RejectedJobs = new ObservableCollection<Job>(jobList);
                }
                else
                {
                    MessageBox.Show("Id must be a number");
                }
            }
            else if (this.SearchOption == "Required Completion Date")
            {
                RejectedJobs jobList = new RejectedJobs(this.SearchDate);
                this.RejectedJobs = new ObservableCollection<Job>(jobList);
            }
            else
            {
                MessageBox.Show("Please select a search Option");
            }


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

        public ObservableCollection<Job> RejectedJobs
        {
            get { return _rejectedJobs; }
            set { this._rejectedJobs = value; OnPropertyChanged("RejectedJobs"); }
        }

        public RejectedJobManagementViewModel()
        {
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            RejectedJobs jobList = new RejectedJobs();
            this.RejectedJobs = new ObservableCollection<Job>(jobList);
        }
    }
}

