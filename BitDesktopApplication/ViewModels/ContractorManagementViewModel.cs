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
    class ContractorManagementViewModel :INotifyPropertyChanged
    {
        #region private properties
        private Contractor _selectedContractor;

        private Skill _selectedPotentialSkill;
        private Skill _selectedContractorSkill;
        private Skill _newSkill;
        private Job _selectedJob;

        private DateTime _newAvailDate;
        private Timeslot _newStartTime;
        private Timeslot _newEndTime;

        private ObservableCollection<Contractor> _contractors;
        private ObservableCollection<Availability> _availabilities;
        private ObservableCollection<Skill> _contractorSkills;
        private ObservableCollection<Skill> _potentialSkills;
        private ObservableCollection<State> _states;
        private ObservableCollection<Title> _titles;
        private ObservableCollection<Region> _regions;
        private ObservableCollection<Timeslot> _timeslots;
        private ObservableCollection<Job> _bookedJobs;

        private RelayCommand _addAvailabilityCommand;
        private RelayCommand _addSkillCommand;
        private RelayCommand _RemoveSkillCommand;
        private RelayCommand _AddNewSkillCommand;
        private RelayCommand _DeleteSkillCommand;
        private RelayCommand _deleteContractorCommand;
        private RelayCommand _editContractorCommand;
        private RelayCommand _searchCommand;

        private string _searchOption;
        private DateTime _searchDate;
        private string _searchText;
        #endregion private properties
        
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
        public RelayCommand AddAvailabilityCommand
        {
            get
            {
                if (_addAvailabilityCommand == null)
                {
                    _addAvailabilityCommand = new RelayCommand(this.AddAvailabilityMethod, true);
                }
                return _addAvailabilityCommand;
            }
            set
            {
                _addAvailabilityCommand = value;
            }
        }
        public RelayCommand AddSkillCommand
        {
            get
            {
                if (_addSkillCommand == null)
                {
                    _addSkillCommand = new RelayCommand(this.AddSkillMethod, true);
                }
                return _addSkillCommand;
            }
            set
            {
                _addSkillCommand = value;
            }
        }
        public RelayCommand RemoveSkillCommand
        {
            get
            {
                if (_RemoveSkillCommand == null)
                {
                    _RemoveSkillCommand = new RelayCommand(this.RemoveSkillMethod, true);
                }
                return _RemoveSkillCommand;
            }
            set
            {
                _RemoveSkillCommand = value;
            }
        }
        public RelayCommand AddNewSkillCommand
        {
            get
            {
                if (_AddNewSkillCommand == null)
                {
                    _AddNewSkillCommand = new RelayCommand(this.AddNewSkillMethod, true);
                }
                return _AddNewSkillCommand;
            }
            set
            {
                _addAvailabilityCommand = value;
            }
        }
        public RelayCommand DeleteSkillCommand
        {
            get
            {
                if (_DeleteSkillCommand == null)
                {
                    _DeleteSkillCommand = new RelayCommand(this.DeleteSkillMethod, true);
                }
                return _DeleteSkillCommand;
            }
            set
            {
                _DeleteSkillCommand = value;
            }
        }
        public RelayCommand DeleteContractorCommand
        {
            get
            {
                if (_deleteContractorCommand == null)
                {
                    _deleteContractorCommand = new RelayCommand(this.DeleteContractorMethod, true);
                }
                return _deleteContractorCommand;
            }
            set
            {
                _deleteContractorCommand = value;
            }
        }
        public RelayCommand EditContractorCommand
        {
            get
            {
                if (_editContractorCommand == null)
                {
                    _editContractorCommand = new RelayCommand(this.EditContractorMethod, true);
                }
                return _editContractorCommand;
            }
            set
            {
                _editContractorCommand = value;
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
                    Contractors contList = new Contractors();
                    contList.GetContractorsById(id);
                    Contractors = new ObservableCollection<Contractor>(contList);
                }
                else
                {
                    MessageBox.Show("Id must be a number");
                }
            }
            else if (SearchOption == "First Name")
            {
                Contractors contList = new Contractors();
                contList.GetContractorsByFirstName(SearchText);
                Contractors = new ObservableCollection<Contractor>(contList);
            }
            else if (SearchOption == "Last Name")
            {
                Contractors contList = new Contractors();
                contList.GetContractorsByLastName(SearchText);
                Contractors = new ObservableCollection<Contractor>(contList);
            }
            else if (SearchOption == "Date of Birth")
            {
                Contractors contList = new Contractors();
                contList.GetContractorsByDob(SearchDate);
                Contractors = new ObservableCollection<Contractor>(contList);
            }
            else
            {
                MessageBox.Show("Please select a search option.");
            }


        }
        public ObservableCollection<Timeslot> Timeslots
        {
            get { return _timeslots; }
            set { this._timeslots = value; OnPropertyChanged("Timeslots"); }
        }
        public ObservableCollection<Contractor> Contractors
        {
            get { return _contractors; }
            set { this._contractors = value; OnPropertyChanged("Contractors"); }
        }
        public ObservableCollection<Availability> Availabilities
        {
            get { return _availabilities; }
            set { this._availabilities = value; OnPropertyChanged("Availabilities"); }
        }
        public ObservableCollection<Skill> ContractorSkills
        {
            get { return _contractorSkills; }
            set { this._contractorSkills = value; OnPropertyChanged("ContractorSkills"); }
        }
        public ObservableCollection<Skill> PotentialSkills
        {
            get { return _potentialSkills; }
            set { this._potentialSkills = value; OnPropertyChanged("PotentialSkills"); }
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
        public ObservableCollection<Job> BookedJobs
        {
            get { return _bookedJobs; }
            set { this._bookedJobs = value; OnPropertyChanged("BookedJobs"); }
        }
        public Contractor SelectedContractor
        {
            get { return _selectedContractor; }
            set 
            { 
                this._selectedContractor = value; 
                OnPropertyChanged("SelectedContractor");
                if (SelectedContractor != null)
                {
                    Availabilities contractorAvailability = new Availabilities(SelectedContractor.StaffId);
                    this.Availabilities = new ObservableCollection<Availability>(contractorAvailability);
                    LoadSkills();
                    LoadJobs();

                }
            }
        }
        public Skill SelectedPotentialSkill
        {
            get { return _selectedPotentialSkill; }
            set { this._selectedPotentialSkill = value; OnPropertyChanged("SelectedPotentialSkill"); }
        }
        public Skill SelectedContractorSkill
        {
            get { return _selectedContractorSkill; }
            set { this._selectedContractorSkill = value; OnPropertyChanged("SelectedContractorSkill"); }
        }
        public Job SelectedJob
        {
            get { return _selectedJob; }
            set { this._selectedJob = value; OnPropertyChanged("SelectedJob"); }
        }
        public Skill NewSkill
        {
            get { return _newSkill; }
            set { this._newSkill = value; OnPropertyChanged("NewSkill"); }
        }
        public Timeslot NewStartTime
        {
            get { return _newStartTime; }
            set { this._newStartTime = value; OnPropertyChanged("NewStartTime"); }
        }
        public Timeslot NewEndTime
        {
            get { return _newEndTime; }
            set { this._newEndTime = value; OnPropertyChanged("NewEndTime"); }
        }
        public DateTime NewAvailDate
        {
            get { return _newAvailDate; }
            set { this._newAvailDate = value; OnPropertyChanged("NewAvailDate"); }
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
        public ContractorManagementViewModel()
        {
            if (NewSkill == null)
            {
                NewSkill = new Skill();
            }
            RefreshContractorList();
            States stateList = new States();
            this.States = new ObservableCollection<State>(stateList);
            Regions regionList = new Regions();
            this.Regions = new ObservableCollection<Region>(regionList);
            Titles titleList = new Titles();
            this.Titles = new ObservableCollection<Title>(titleList);
            Timeslots newTimeslots = new Timeslots();
            this.Timeslots = new ObservableCollection<Timeslot>(newTimeslots);
            NewAvailDate = DateTime.Today;
            NewStartTime = Timeslots[0];
            NewEndTime = Timeslots[0];
        }

        private void RefreshContractorList()
        {
            Contractors allContractors = new Contractors();
            allContractors.GetAllContractors();
            this.Contractors = new ObservableCollection<Contractor>(allContractors);
        }
        private void LoadJobs()
        {
            AllJobs jobList = new AllJobs(SelectedContractor.StaffId, "Contractor");
            BookedJobs = new ObservableCollection<Job>(jobList);
        }
        private void LoadSkills()
        {
            Skills potentialSkills = new Skills();
            potentialSkills.GetMissingContractorSkills(SelectedContractor.StaffId);
            this.PotentialSkills = new ObservableCollection<Skill>(potentialSkills);
            Skills contractorSkills = new Skills();
            contractorSkills.GetContractorSKills(SelectedContractor.StaffId);
            this.ContractorSkills = new ObservableCollection<Skill>(contractorSkills);
        }
        private void AddAvailabilityMethod()
        {

            if (ValidateAvailability())
            {
                Availability newAvailability = new Availability();
                newAvailability.ContractorId = SelectedContractor.StaffId;
                newAvailability.Date = NewAvailDate.Date;
                newAvailability.StartTime = NewStartTime.TimeslotTime;
                newAvailability.EndTime = NewEndTime.TimeslotTime;
                int rowsUpdated = newAvailability.AddAvailability();
                if (rowsUpdated < 1)
                {
                    MessageBox.Show("Insert Failed.");
                }
                else
                {
                    Availabilities contractorAvailability = new Availabilities(SelectedContractor.StaffId);
                    this.Availabilities = new ObservableCollection<Availability>(contractorAvailability);
                }
            }
            
        }
        private bool ValidateAvailability()
        {
            if (SelectedContractor == null)
            {
                MessageBox.Show("Please select a contractor to add availability. ", "Selection Required");
                return false;
            }
            if (NewAvailDate.Date == null)
            {
                MessageBox.Show("Date must be selected.");
                return false;
            }
            if (NewAvailDate.Date < DateTime.Today )
            {
                MessageBox.Show("Date cannot be set in the past.");
                return false;
            }
            if (NewStartTime.TimeslotTime == NewEndTime.TimeslotTime)
            {
                MessageBox.Show("Start time cannot be the same as end time. ");
                return false;
            }
            if (Convert.ToInt32(NewStartTime.TimeslotTime.Split(':')[0]) > Convert.ToInt32(NewEndTime.TimeslotTime.Split(':')[0]))
            {
                MessageBox.Show("End time cannot be before start time. ");
                return false;
            }


            return true;

        }
        private void DeleteContractorMethod()
        {
            if (SelectedContractor == null)
            {
                MessageBox.Show("Please select a contractor to delete. ", "Selection Required");
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this contractor?", "Delete Contractor?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    if (SelectedContractor.DeleteContractor() == 2)
                    {
                        MessageBox.Show("Deletion Successful");
                        RefreshContractorList();
                    }
                    else
                    {
                        MessageBox.Show("Deletion Failed!");

                    }
                }
            }
        }
        private void EditContractorMethod()
        {
            if (SelectedContractor == null)
            {
                MessageBox.Show("Please select a contractor to edit. ", "Selection Required");

            }
            else
            {
                if (SelectedContractor.EditContractor() != -1)
                {
                    MessageBox.Show("Successfully edited contractor.");
                }
                else
                {
                    MessageBox.Show("Unable to edit contractor at this time. ");
                }
            }
        }
        #region Skill CRUD
        private void AddSkillMethod()
        {
            if (SelectedPotentialSkill == null)
            {
                MessageBox.Show("Please select a skill to delete. ", "Selection Required");

            }
            else
            {
                if (SelectedPotentialSkill.AddContractorSkill(SelectedContractor.StaffId) > -1)
                {
                    LoadSkills();
                }
                else
                {
                    MessageBox.Show("Failed to add skill");
                }
            }
        }
        private void RemoveSkillMethod()
        {
            if (SelectedContractorSkill == null)
            {
                MessageBox.Show("Please select a skill to delete. ", "Selection Required");

            }
            else
            {
                if (SelectedContractorSkill.RemoveContractorSkill(SelectedContractor.StaffId) > -1)
                {
                    LoadSkills();
                }
                else
                {
                    MessageBox.Show("Failed to add skill");
                }
            }
        }
        private void AddNewSkillMethod()
        {
            if (NewSkill.SkillName == null)
            {
                MessageBox.Show("Please type the skill name before adding a skill. ", "Input Required");

            }
            else
            {
                if (NewSkill.AddNewSkill() > -1)
                {
                    //MessageBox.Show("New skill added successfully");
                    LoadSkills();
                }
                else
                {
                    MessageBox.Show("Failed to add skill");
                }
            }
        }
        private void DeleteSkillMethod()
        {
            if (SelectedPotentialSkill == null)
            {
                MessageBox.Show("Please select a skill to delete. ", "Selection Required");

            }
            else
            {
                var result = MessageBox.Show("You are about to delete the skill " + SelectedPotentialSkill.SkillName + " completely. Are you sure?", "Delete Record", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    if (SelectedPotentialSkill.DeleteSkill(SelectedContractor.StaffId) > -1)
                    {
                        MessageBox.Show("Delete Successful.", "Delete Record");
                        LoadSkills();
                    }
                    else
                    {
                        MessageBox.Show("Delete Unsuccessful.", "Delete Record");
                    }


                }
                else
                {
                    MessageBox.Show(SelectedPotentialSkill.SkillName + " has not been deleted.", "Delete Record");

                }
            }
            
        }

        #endregion Skill CRUD
    }
}
