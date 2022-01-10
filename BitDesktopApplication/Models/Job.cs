using BitDesktopApplication.BLL;
using BitDesktopApplication.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitDesktopApplication.Models
{
    public class Job : INotifyPropertyChanged, IDataErrorInfo
    {
        //job creation params
        private SQLHelper _db;
        private int _jobNo;
        private ClientLocation _location;
        private DateTime _creationDate;
        private DateTime _requiredCompDate;
        private string _description;
        private string _priority;
        private string _region;

        //Job assignment params
        private Skills _jobSkills;
        private Availability _jobAvailability;
        private string _startTime;
        private int _expHours;

        //read only params
        private Client _client;
        private Contractor _contractor;
        private string _rejectionComment;
        private DateTime _rejectionDate;
        private DateTime _availabilityDate;
        private int _kmTravelled;
        private decimal _hoursTaken;
        private string _jobStatus;

        public int JobNo
        {
            get { return _jobNo; }
            set { this._jobNo = value; OnPropertyChanged("JobNo"); }
        }
        public ClientLocation Location
        {
            get { return _location; }
            set { this._location = value; OnPropertyChanged("Location"); }
        }
        public DateTime CreationDate
        {
            get { return _creationDate; }
            set { this._creationDate = value; OnPropertyChanged("CreationDate"); }
        }
        public DateTime RequiredCompDate
        {
            get { return _requiredCompDate; }
            set { this._requiredCompDate = value; OnPropertyChanged("RequiredCompDate"); }
        }
        public string Description
        {
            get { return _description; }
            set { this._description = value; OnPropertyChanged("Description"); }
        }
        public string Priority
        {
            get { return _priority; }
            set { this._priority = value; OnPropertyChanged("Priority"); }
        }
        public string Region
        {
            get { return _region; }
            set { this._region = value; OnPropertyChanged("Region"); }
        }

        public Skills JobSkills
        {
            get { return _jobSkills; }
            set { this._jobSkills = value; }
        }
        public Availability JobAvailability
        {
            get { return _jobAvailability; }
            set { this._jobAvailability = value; OnPropertyChanged("JobAvailability"); }
        }
        public string StartTime
        {
            get { return _startTime; }
            set { this._startTime = value; OnPropertyChanged("StartTime"); }
        }
        public int ExpHours
        {
            get { return _expHours; }
            set { this._expHours = value; OnPropertyChanged("ExpHours"); }
        }

        public Client Client
        {
            get { return _client; }
            set { this._client = value; }
        }
        public Contractor Contractor
        {
            get { return _contractor; }
            set { this._contractor = value; }
        }

        public string RejectionComment
        {  
            get { return _rejectionComment; }
            set { this._rejectionComment = value; }
        }
        public DateTime RejectionDate
        {
            get { return _rejectionDate; }
            set { this._rejectionDate = value; }
        }
        public DateTime AvailabilityDate
        {
            get { return _availabilityDate; }
            set 
            { 
                this._availabilityDate = value;
                this.JobDate = _availabilityDate.ToString("dd/MM/yyyy");
                
            }
        }
        public string JobDate { get; set; }
        public int KmTravelled
        {
            get { return _kmTravelled; }
            set { this._kmTravelled = value; }
        }
        public decimal HoursTaken
        {
            get { return _hoursTaken; }
            set { this._hoursTaken = value; }
        }
        public string JobStatus
        {
            get { return _jobStatus; }
            set { this._jobStatus = value; }
        }
        
        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();
        public string Error { get { return null; } }

        public string this[string propertyName]
        {
            get
            {
                string result = null;
                switch (propertyName)
                {
                    case "Priority":
                        if (string.IsNullOrWhiteSpace(Priority))
                        {
                            result = "Priority level cannot be empty.";

                        }
                        break;

                    case "RequiredCompDate":
                        if (RequiredCompDate < DateTime.Today.AddDays(1))
                        {
                            result = "Date must be set in the future. ";
                            break;
                        }
                        if (RequiredCompDate == null)
                        {
                            result = "Date must be selected. ";
                        }
                        break;

                   
                    case "Description":
                        if (string.IsNullOrWhiteSpace(Description))
                        {
                            result = "Description cannot be empty.";
                            break;

                        }
                        if (Description.Length > 500)
                        {
                            result = "Description must be under 500 characters. ";
                        }
                        break;

                    case "ExpHours":
                        if (ExpHours.Equals(null))
                        {
                            result = "Expected hours are required. ";
                            break;
                        }
                        if (ExpHours <= 0)
                        {
                            result = "More than 0 hours are expected";
                        }
                        break;

                    case "StartTime":
                        if (string.IsNullOrWhiteSpace(StartTime))
                        {
                            result = "A start time must be selected.";
                        }

                        break;


                    case "Region":
                        if (string.IsNullOrWhiteSpace(Region))
                        {
                            result = "Region must be selected.";

                        }
                        break;

                }
                if (result != null && !ErrorCollection.ContainsKey(propertyName))
                    ErrorCollection.Add(propertyName, result);
                OnPropertyChanged("ErrorCollection");
                return result;
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
        public Job()
        {
            _db = new SQLHelper();
        }

        public Job(DataRow dr, string jobListType)
        {
            this.Location = new ClientLocation();
            this.Client = new Client();
            this.Contractor = new Contractor();
           
            if (jobListType == "unassigned")
            {
                this.JobNo = Convert.ToInt32(dr["jobNo"].ToString());
                this.Client.ClientId = Convert.ToInt32(dr["clientId"].ToString());
                this.Client.FirstName = dr["firstName"].ToString();  //customer firstName
                this.Client.LastName = dr["lastName"].ToString();
                this.Region = dr["region"].ToString();
                this.Description = dr["jobDescription"].ToString();
                this.Priority = dr["priorityLevel"].ToString();
                this.CreationDate = Convert.ToDateTime(dr["creationDate"].ToString());
                this.RequiredCompDate = Convert.ToDateTime(dr["requiredCompDate"].ToString());
            }
            else if (jobListType == "rejected")
            { 
                this.JobNo = Convert.ToInt32(dr["jobNo"].ToString());
                this.Contractor.StaffId = Convert.ToInt32(dr["staffId"].ToString());
                this.Contractor.FirstName = dr["cfirstName"].ToString(); 
                this.Contractor.LastName = dr["clastName"].ToString();        
                this.RejectionComment = dr["rejectionComment"].ToString();
                this.RejectionDate = Convert.ToDateTime(dr["rejectionDate"].ToString());
                this.RequiredCompDate = Convert.ToDateTime(dr["requiredCompDate"].ToString());
                this.Priority = dr["priorityLevel"].ToString();
                this.Client.ClientId = Convert.ToInt32(dr["clientId"].ToString());
                this.Client.FirstName = dr["clfirstName"].ToString(); 
                this.Client.LastName = dr["cllastName"].ToString();       
                this.Description = dr["jobDescription"].ToString();
            }
            else if (jobListType == "completed")
            { 
                this.JobNo = Convert.ToInt32(dr["jobNo"].ToString());
                this.Client.ClientId = Convert.ToInt32(dr["clientId"].ToString());
                this.Client.FirstName = dr["clfirstName"].ToString(); 
                this.Client.LastName = dr["cllastName"].ToString();     
                this.Contractor.StaffId = Convert.ToInt32(dr["staffId"].ToString());
                this.Contractor.FirstName = dr["cfirstName"].ToString(); 
                this.Contractor.LastName = dr["clastName"].ToString();      
                this.AvailabilityDate = Convert.ToDateTime(dr["date"].ToString());
                this.RequiredCompDate = Convert.ToDateTime(dr["requiredCompDate"].ToString());
                this.Description = dr["jobDescription"].ToString();
                this.KmTravelled = Convert.ToInt32(dr["kmTravelled"].ToString());
                this.HoursTaken = Convert.ToDecimal(dr["hoursTaken"].ToString());
                
            }
            else if (jobListType == "all")
            { 
                this.JobNo = Convert.ToInt32(dr["jobNo"].ToString());
                this.Client.ClientId = Convert.ToInt32(dr["clientId"].ToString());
                this.Client.FirstName = dr["firstName"].ToString(); //client
                this.Client.LastName = dr["lastName"].ToString();      //client
                string availDate = dr["AssignedDate"].ToString();
                if (DateTime.TryParse(availDate, out DateTime availabilityDate))
                {
                    this.AvailabilityDate = availabilityDate;
                }
                this.RequiredCompDate = Convert.ToDateTime(dr["requiredCompDate"].ToString());
                this.Description = dr["jobDescription"].ToString();
                this.JobStatus = dr["jobStatus"].ToString();
            }
            else if (jobListType == "ClientStatus")
            {
                this.JobNo = Convert.ToInt32(dr["Job No"].ToString());
                this.JobStatus = dr["Job Status"].ToString();
                string availDate = dr["Job Date"].ToString();
                if (DateTime.TryParse(availDate, out DateTime availabilityDate))
                {
                    this.AvailabilityDate = availabilityDate;
                }
                this.RequiredCompDate = Convert.ToDateTime(dr["Required Completion Date"].ToString());
                this.Description = dr["Description"].ToString();
                this.Priority = dr["Priority"].ToString();
                this.Location.Street = dr["Street"].ToString();
                this.Location.State = dr["state"].ToString();
                this.Region = dr["Region"].ToString();
            }
            else if (jobListType == "ContractorStatus")
            {
                this.JobNo = Convert.ToInt32(dr["jobNo"].ToString());
                this.JobStatus = dr["jobStatus"].ToString();
                string availDate = dr["date"].ToString();
                if (DateTime.TryParse(availDate, out DateTime availabilityDate))
                {
                    this.AvailabilityDate = availabilityDate;
                }
                this.RequiredCompDate = Convert.ToDateTime(dr["requiredCompDate"].ToString());
                this.Description = dr["JobDescription"].ToString();
                this.Priority = dr["Prioritylevel"].ToString();
                this.Location.LocationName = dr["locationName"].ToString();
                this.Location.Street = dr["Street"].ToString();
                this.Location.State = dr["state"].ToString();
                this.Region = dr["Region"].ToString();
            }

        }
        public void GetLastInsertedJobId()
        {
            _db = new SQLHelper();
            string sql = "Select top 1 jobNo from JOB Order by jobNo desc";
            DataTable dataTable = _db.ExecuteSQL(sql);
            foreach (DataRow dr in dataTable.Rows)
            {
                JobNo = Convert.ToInt32(dr["jobNo"].ToString());
            }
        } 
        public int AddNewBooking()
        {
            _db = new SQLHelper();
            string insertSql =
            " set dateformat dmy ;INSERT INTO JOB(locationIdRef, creationDate,  requiredCompDate, jobDescription, priorityLevel, paymentStatus, contractorConfirmed) " +
            " VALUES(@locationIdRef, @creationDate,  @requiredCompDate, @jobDescription, @priorityLevel, 'unpaid', 0)";

            SqlParameter[] objParam = new SqlParameter[5];
            objParam[0] = new SqlParameter("@locationIdRef", DbType.Int32);
            objParam[0].Value = this.Location.LocationId;
            objParam[1] = new SqlParameter("@creationDate", DbType.DateTime);
            objParam[1].Value = DateTime.Today;
            objParam[2] = new SqlParameter("@requiredCompDate", DbType.DateTime);
            objParam[2].Value = this.RequiredCompDate;
            objParam[3] = new SqlParameter("@jobDescription", DbType.String);
            objParam[3].Value = this.Description;
            objParam[4] = new SqlParameter("@priorityLevel", DbType.String);
            objParam[4].Value = this.Priority;

            int rowsUpdated = _db.ExecuteNonQuery(insertSql, objParam);
            GetLastInsertedJobId();
            return rowsUpdated;
        }
        public int SaveJobChanges()
        {
            if (this.Description == "" | this.Description.Length > 500 | this.Priority == null | this.RequiredCompDate == DateTime.MinValue)
            {
                return -1;
            }

            string updateSql =
                " UPDATE Job " +
                " SET jobDescription = '" + Description +
                "', requiredCompDate = '" + RequiredCompDate.ToString("yyyy-MM-dd") + "'" +
                ", priorityLevel = '" + Priority +
                "' WHERE jobNo = " + JobNo;
            _db = new SQLHelper();
            int updatedRows = _db.ExecuteNonQuerySql(updateSql);
            return updatedRows;
        }
        public int SaveContractorToJob()
        {
            if (this.JobAvailability == null | this.StartTime == "" | this.StartTime == null | this.ExpHours == 0 | this.ExpHours.Equals(null))
            {
                return -1;
            }
            
            string updateSql =
                " UPDATE Job " +
                " SET availabilityIdRef = " + JobAvailability.AvailabilityId +
                ", startTime = '" + StartTime + "'" +
                ", expectedHours = " + ExpHours +
                " WHERE jobNo = " + JobNo + 
                " UPDATE Availability " +
                " SET slotBooked = " + 1 +
                " WHERE availabilityId = " + JobAvailability.AvailabilityId;
            _db = new SQLHelper();
            int updatedRows = _db.ExecuteNonQuerySql(updateSql);
            return updatedRows;
        }
        public int MarkVerified()
        {
            string updateSql =
                " UPDATE JOB " +
                " SET paymentStatus = 'Verified'" +
                " WHERE jobNo = " + JobNo;
            _db = new SQLHelper();
            int updatedRows = _db.ExecuteNonQuerySql(updateSql);
            return updatedRows;
        }
        public int MarkIncomplete()
        {
            string updateSql =
                " UPDATE JOB " +
                " SET paymentStatus = 'Incomplete'" +
                " WHERE jobNo = " + JobNo;
            _db = new SQLHelper();
            int updatedRows = _db.ExecuteNonQuerySql(updateSql);
            return updatedRows;
        }
    }
}
