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
    public class CompletedJobManagementViewModel : INotifyPropertyChanged
    {
        private Job _selectedJob;
        private ObservableCollection<Job> _completedJobs;


        private string _searchOption;
        private DateTime _searchDate;
        private string _searchText;

        private RelayCommand _searchCommand;
        private RelayCommand _markVerifiedCommand;
        private RelayCommand _markIncompleteCommand;

        public RelayCommand MarkVerifiedCommand
        {
            get
            {
                if (_markVerifiedCommand == null)
                {
                    _markVerifiedCommand = new RelayCommand(this.MarkVerifiedMethod, true);
                }
                return _markVerifiedCommand;
            }
            set
            {
                _markVerifiedCommand = value;
            }
        }
        private void MarkVerifiedMethod()
        {
            if (SelectedJob == null)
            {
                MessageBox.Show("Please select a Job. ", "Selection Required");

            }
            else
            {
                if (SelectedJob.MarkVerified() == -1)
                {
                    MessageBox.Show("Unable to make changes to the job.");
                }
                else
                {
                    MessageBox.Show("Changes successful.");
                    RefreshGrid();
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
            if (SearchOption == "Job No")
            {
                if (Int32.TryParse(SearchText, out int id))
                {
                    CompletedJobs jobList = new CompletedJobs(id);
                    this.CompletedJobs = new ObservableCollection<Job>(jobList);
                }
                else
                {
                    MessageBox.Show("Id must be a number");
                }
            }
            else if (this.SearchOption == "Job Date")
            {
                CompletedJobs jobList = new CompletedJobs(SearchDate);
                this.CompletedJobs = new ObservableCollection<Job>(jobList);
            }
            else
            {
                MessageBox.Show("Please select a search Option");
            }


        }
        public RelayCommand MarkIncompleteCommand
        {
            get
            {
                if (_markIncompleteCommand == null)
                {
                    _markIncompleteCommand = new RelayCommand(this.MarkIncompleteMethod, true);
                }
                return _markIncompleteCommand;
            }
            set
            {
                _markIncompleteCommand = value;
            }
        }

        private void MarkIncompleteMethod()
        {
            if (SelectedJob == null)
            {
                MessageBox.Show("Please select a Job. ", "Selection Required");

            }
            else
            {
                if (SelectedJob.MarkIncomplete() == -1)
                {
                    MessageBox.Show("Unable to make changes to the job.");
                }
                else
                {
                    MessageBox.Show("Changes successful.");
                    RefreshGrid();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        public Job SelectedJob
        {
            get { return _selectedJob; }
            set { this._selectedJob = value; OnPropertyChanged("SelectedJob"); }
        }
        public ObservableCollection<Job> CompletedJobs
        {
            get { return _completedJobs; }
            set { this._completedJobs = value; OnPropertyChanged("CompletedJobs"); }
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
        public CompletedJobManagementViewModel()
        {
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            CompletedJobs jobList = new CompletedJobs();
            this.CompletedJobs = new ObservableCollection<Job>(jobList);
        }
    }
}
