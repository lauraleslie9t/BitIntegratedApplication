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
    class JobManagementViewModel : INotifyPropertyChanged
    {

        private Job _selectedJob;
        private ObservableCollection<Job> _jobs;
        private ObservableCollection<Job> _allJobs;
        

        private RelayCommand _searchCommand;
        private RelayCommand _clearCommand;

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
            ObservableCollection<Job> alteredJobList = new ObservableCollection<Job>(Jobs);
            if (SearchOption == "Job No")
            {
                if (Int32.TryParse(SearchText, out int id))
                {
                    foreach (Job jobItem in alteredJobList)
                    {
                        if (jobItem.JobNo != id)
                        {
                            Jobs.Remove(jobItem);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Id must be a number");
                }
            }
            else if (this.SearchOption == "Job Date")
            {
                foreach (Job jobItem in alteredJobList)
                {
                    if (jobItem.AvailabilityDate != SearchDate)
                    {
                        Jobs.Remove(jobItem);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a search Option");
            }


        }
        public RelayCommand ClearCommand
        {
            get
            {
                if (_clearCommand == null)
                {
                    _clearCommand = new RelayCommand(this.ClearMethod, true);
                }
                return _clearCommand;
            }
            set
            {
                _clearCommand = value;
            }
        }
        private void ClearMethod()
        {
            this.Jobs = new ObservableCollection<Job>(_allJobs);
        }

        public Job SelectedJob
        {
            get { return _selectedJob; }
            set { this._selectedJob = value; OnPropertyChanged("SelectedJob"); }
        }
        public ObservableCollection<Job> Jobs
        {
            get { return _jobs; }
            set { this._jobs = value; OnPropertyChanged("Jobs"); }
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


        public JobManagementViewModel()
        {
            AllJobs jobList = new AllJobs();
            this._allJobs = new ObservableCollection<Job>(jobList);
            this.Jobs = new ObservableCollection<Job>(jobList);
        }
    }
}
