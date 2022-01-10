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
    public class AddJobContractorViewModel : INotifyPropertyChanged
    {
        private Job _job;

        private Skill _selectedPotentialSkill;
        private Skill _selectedJobSkill;
        private Availability _selectedAvailability;

        private ObservableCollection<Availability> _availabilities;
        private ObservableCollection<Skill> _jobSkills;
        private ObservableCollection<Skill> _potentialSkills;
        private ObservableCollection<Timeslot> _timeslots;
        private ObservableCollection<Priority> _priorities;

        private RelayCommand _addSkillCommand;
        private RelayCommand _RemoveSkillCommand;
        private RelayCommand _getavailabilityCommand;
        private RelayCommand _saveContractorCommand;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
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
        public RelayCommand GetAvailabilityCommand
        {
            get
            {
                if (_getavailabilityCommand == null)
                {
                    _getavailabilityCommand = new RelayCommand(this.GetAvailabilityMethod, true);
                }
                return _getavailabilityCommand;
            }
            set
            {
                _getavailabilityCommand = value;
            }
        }
        public RelayCommand SaveContractorCommand
        {
            get
            {
                if (_saveContractorCommand == null)
                {
                    _saveContractorCommand = new RelayCommand(this.SaveContractorMethod, true);
                }
                return _saveContractorCommand;
            }
            set
            {
                _saveContractorCommand = value;
            }
        }

        private void AddSkillMethod()
        {
            if (SelectedPotentialSkill == null)
            {
                MessageBox.Show("Please select a skill to add. ", "Selection Required");

            }
            else
            {
                if (SelectedPotentialSkill.AddJobSkill(_job.JobNo) > -1)
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
            if (SelectedJobSkill == null)
            {
                MessageBox.Show("Please select a skill to remove. ", "Selection Required");

            }
            else
            {
                if (SelectedJobSkill.RemoveJobSkill(_job.JobNo) > -1)
                {
                    LoadSkills();
                }
                else
                {
                    MessageBox.Show("Failed to remove skill");
                }
            }
        }
        private void SaveContractorMethod()
        {
            
            if (CheckStartTime())
            {
                if (Job.SaveContractorToJob() != -1)
                {
                    MessageBox.Show("Job has been added", "Success");
                }
            }
        }

        public bool CheckStartTime()
        {
            Job.JobAvailability = SelectedAvailability;
            if (Job.JobAvailability != null)
            {
                string[] timeSplit = SelectedAvailability.StartTime.Split(':');
                int startTime = Convert.ToInt32(timeSplit[0]);
                if (Job.StartTime != null)
                {
                    string[] selectedTimeSplit = Job.StartTime.Split(':');
                    int selectedStartTime = Convert.ToInt32(selectedTimeSplit[0]);
                    if (selectedStartTime >= startTime)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            return false;
        }

        private void GetAvailabilityMethod()
        {
            if (JobSkills.Count == 0)
            {
                MessageBox.Show("Job skills are required to match a contractor. ");
            }
            else
            {
                Job.SaveJobChanges();
                Availabilities availabilityList = new Availabilities(_job.RequiredCompDate, _job.JobNo);
                Availabilities = new ObservableCollection<Availability>(availabilityList);
                if (Availabilities.Count == 0)
                {
                    MessageBox.Show("No sessions are available for the skills and completion date set.");
                }
            }
        }
        public Job Job
        {
            get { return _job; }
            set { this._job = value; }
        } 
        public ObservableCollection<Availability> Availabilities
        {
            get { return _availabilities; }
            set { this._availabilities = value; OnPropertyChanged("Availabilities"); }
        }
        public ObservableCollection<Timeslot> Timeslots
        {
            get { return _timeslots; }
            set { this._timeslots = value; OnPropertyChanged("Timeslots"); }
        }
        public ObservableCollection<Priority> Priorities
        {
            get { return _priorities; }
            set { this._priorities = value; OnPropertyChanged("Priorities"); }
        }
        public Skill SelectedPotentialSkill
        {
            get { return _selectedPotentialSkill; }
            set { this._selectedPotentialSkill = value; OnPropertyChanged("SelectedPotentialSkill"); }
        }
        public Skill SelectedJobSkill
        {
            get { return _selectedJobSkill; }
            set { this._selectedJobSkill = value; OnPropertyChanged("SelectedJobSkill"); }
        }
        public Availability SelectedAvailability
        {
            get { return _selectedAvailability; }
            set { this._selectedAvailability = value; OnPropertyChanged("SelectdAvailability"); }
        }

        public ObservableCollection<Skill> JobSkills
        {
            get { return _jobSkills; }
            set { this._jobSkills = value; OnPropertyChanged("JobSkills"); }
        }
        public ObservableCollection<Skill> PotentialSkills
        {
            get { return _potentialSkills; }
            set { this._potentialSkills = value; OnPropertyChanged("PotentialSkills"); }
        }


        public AddJobContractorViewModel(Job job)
        {
            Timeslots timeslots = new Timeslots();
            Timeslots = new ObservableCollection<Timeslot>(timeslots);
            PriorityLevels priorities = new PriorityLevels();
            Priorities = new ObservableCollection<Priority>(priorities);

            Job = job;
            this.Availabilities = new ObservableCollection<Availability>();
            LoadSkills();
        }


        private void LoadSkills()
        {
            Skills potentialSkills = new Skills();
            potentialSkills.GetMissingJobSkills(Job.JobNo);
            this.PotentialSkills = new ObservableCollection<Skill>(potentialSkills);
            Skills jobSkills = new Skills();
            jobSkills.GetJobSKills(Job.JobNo);
            this.JobSkills = new ObservableCollection<Skill>(jobSkills);
        }
    }
}
