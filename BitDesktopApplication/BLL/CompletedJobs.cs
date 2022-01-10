using BitDesktopApplication.DAL;
using BitDesktopApplication.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitDesktopApplication.BLL
{
    class CompletedJobs :List<Job>
    {
        private SQLHelper _db;
        public CompletedJobs()
        { 
            _db = new SQLHelper();
            string sql =
                        " SELECT j.jobNo, cl.clientId, cl.firstName clFirstName, cl.lastName clLastName, s.staffId, s.firstName cFirstName, s.lastName cLastName," +
                        " a.date, requiredCompDate, j.jobDescription, j.kmTravelled, j.hoursTaken " +
                        " FROM JOB j, CLIENT cl, JOB_LOCATION l, AVAILABILITY a, CONTRACTOR c, STAFF s " +
                        " WHERE l.locationId = j.locationIdRef " +
                        " AND cl.clientId = l.clientIdRef " +
                        " AND a.availabilityId = j.availabilityIdRef " +
                        " AND s.staffId = c.contractorId " +
                        " AND a.contractorIdRef = c.contractorId " +
                        " AND availabilityIdRef is not null " +
                        " AND j.kmTravelled IS NOT NULL " +
                        " AND j.hoursTaken IS NOT NULL" +
                        " AND paymentStatus = 'unpaid' ";

            DataTable jobTable = _db.ExecuteSQL(sql);
            foreach (DataRow dr in jobTable.Rows)
            {
                Job newJob = new Job(dr, "completed");
                this.Add(newJob);
            }
        }

        public CompletedJobs(int jobNo)
        {
            _db = new SQLHelper();
            string sql =
                        " SELECT j.jobNo, cl.clientId, cl.firstName clFirstName, cl.lastName clLastName, s.staffId, s.firstName cFirstName, s.lastName cLastName," +
                        " a.date, requiredCompDate, j.jobDescription, j.kmTravelled, j.hoursTaken " +
                        " FROM JOB j, CLIENT cl, JOB_LOCATION l, AVAILABILITY a, CONTRACTOR c, STAFF s " +
                        " WHERE l.locationId = j.locationIdRef " +
                        " AND cl.clientId = l.clientIdRef " +
                        " AND a.availabilityId = j.availabilityIdRef " +
                        " AND s.staffId = c.contractorId " +
                        " AND a.contractorIdRef = c.contractorId " +
                        " AND availabilityIdRef is not null " +
                        " AND j.kmTravelled IS NOT NULL " +
                        " AND j.hoursTaken IS NOT NULL" +
                        " AND j.jobNo = " + jobNo + 
                        " AND paymentStatus = 'unpaid' ";

            DataTable jobTable = _db.ExecuteSQL(sql);
            foreach (DataRow dr in jobTable.Rows)
            {
                Job newJob = new Job(dr, "completed");
                this.Add(newJob);
            }
        }

        public CompletedJobs(DateTime jobDate)
        {
            _db = new SQLHelper();
            string sql =
                        " SELECT j.jobNo, cl.clientId, cl.firstName clFirstName, cl.lastName clLastName, s.staffId, s.firstName cFirstName, s.lastName cLastName," +
                        " a.date, requiredCompDate, j.jobDescription, j.kmTravelled, j.hoursTaken " +
                        " FROM JOB j, CLIENT cl, JOB_LOCATION l, AVAILABILITY a, CONTRACTOR c, STAFF s " +
                        " WHERE l.locationId = j.locationIdRef " +
                        " AND cl.clientId = l.clientIdRef " +
                        " AND a.availabilityId = j.availabilityIdRef " +
                        " AND s.staffId = c.contractorId " +
                        " AND a.contractorIdRef = c.contractorId " +
                        " AND availabilityIdRef is not null " +
                        " AND j.kmTravelled IS NOT NULL " +
                        " AND j.hoursTaken IS NOT NULL" +
                        " AND a.date = '" + jobDate.ToString("yyyy-MM-dd") +  "'" +
                        " AND paymentStatus = 'unpaid' ";

            DataTable jobTable = _db.ExecuteSQL(sql);
            foreach (DataRow dr in jobTable.Rows)
            {
                Job newJob = new Job(dr, "completed");
                this.Add(newJob);
            }
        }
    }
}
