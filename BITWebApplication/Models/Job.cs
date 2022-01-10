using BITWebApplication.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BITWebApplication.Models
{
    public class Job
    {
        //job creation params
        private SQLHelper _db;
        private int _jobNo;
        private int _locationId;
        private DateTime _creationDate;
        private DateTime _requiredCompDate;
        private string _description;
        private string _priority;
        private string _region;


        //Job assignment params
       
        //private Skills _jobSkills;
        private int     _availabilityId;
        private string  _startTime;
        private int     _expHours;

        //read only params
        private string _firstName;
        private string _lastName;
        private string _rejectionComment;
        private DateTime _rejectionDate;
        private DateTime _availabilityDate;
        private int _kmTravelled;
        private decimal _hoursTaken;
        private string _jobStatus;

        public int JobNo
        {
            get { return _jobNo; }
            set { this._jobNo = value; }
        }
        public int LocationId
        {
            get { return _locationId; }
            set { this._locationId = value; }

        }
        public DateTime CreationDate
        {
            get { return _creationDate; }
            set { this._creationDate = value; }
        }
        public DateTime RequiredCompDate
        {
            get { return _requiredCompDate; }
            set { this._requiredCompDate = value; }
        }
        public string Description
        {
            get { return _description; }
            set { this._description = value; }
        }
        public string Priority
        {
            get { return _priority; }
            set { this._priority = value; }
        }
        public string Region
        {
            get { return _region; }
            set { this._region = value; }
        }

        //public Skills JobSkills
        //{
        //    get { return _jobSkills; }
        //    set { this._jobSkills = value; }
        //}
        public int AvailabilityId
        {
            get { return _availabilityId; }
            set { this._availabilityId = value; }
        }
        public string StartTime
        {
            get { return _startTime; }
            set { this._startTime = value; }
        }
        public int ExpHours
        {
            get { return _expHours; }
            set { this._expHours = value; }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { this._firstName = value; }
        }
        public string LastName
        {
            get { return _lastName; }
            set { this._lastName = value; }
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
            set { this._availabilityDate = value; }
        }
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

        
        public Job()
        {
            _db = new SQLHelper();
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
            objParam[0].Value = this.LocationId;
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
        public int SaveContractorToJob()
        {
            string updateSql =
                " UPDATE Job " +
                " SET availabilityIdRef = " + AvailabilityId +
                ", startTime = '" + StartTime + "'" +
                ", expectedHours = " + ExpHours +
                " WHERE jobNo = " + JobNo +
                " UPDATE Availability " +
                " SET slotBooked = " + 1 +
                " WHERE availabilityId = " + AvailabilityId;
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
        public int ConfirmJob()
        {
            string updateSql =
                " UPDATE JOB " +
                " SET contractorConfirmed = 1 " +
                " WHERE jobNo = " + JobNo;
            _db = new SQLHelper();
            int updatedRows = _db.ExecuteNonQuerySql(updateSql);
            return updatedRows;
        }
        public int RejectJob()
        { 
            string updateSql =
                " UPDATE JOB " +
                " SET availabilityIdRef = null " +
                " WHERE jobNo = " + JobNo +
                " UPDATE AVAILABILITY " +
                " SET slotBooked = 0 " +
                " WHERE availabilityId = " + AvailabilityId +
                " INSERT into JOB_REJECTION(jobNoRef, availabilityIdRef, rejectionComment, rejectionDate) " +
                " VALUES("+ JobNo + "," + AvailabilityId +" , '" + RejectionComment + "', GETDATE())";
            _db = new SQLHelper();
            int updatedRows = _db.ExecuteNonQuerySql(updateSql);
            return updatedRows;
        }
        public int AddJobDetails()
        {
            string updateSql =
                " UPDATE JOB " +
                " SET kmTravelled = '" + KmTravelled + "'," +
                " hoursTaken = '" + HoursTaken + "'" +
                " WHERE jobNo = " + JobNo;
                
            _db = new SQLHelper();
            int updatedRows = _db.ExecuteNonQuerySql(updateSql);
            return updatedRows;
        }
    }
}