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
    class RejectedJobs : List<Job>
    {
        private SQLHelper _db;
        public RejectedJobs()
        {  
            _db = new SQLHelper();
            string sql =
                        " SELECT j.jobNo, s.staffId, s.firstName cFirstName, s.lastName cLastName, r.rejectionComment, r.rejectionDate, j.requiredCompDate, j.priorityLevel, " +
                        " cl.clientId, cl.firstName clFirstName, cl.lastName clLastName,  j.jobDescription " +
                        " FROM JOB j, JOB_REJECTION r, AVAILABILITY a, CONTRACTOR c, STAFF s, JOB_LOCATION l, CLIENT cl " +
                        " WHERE j.jobNo = r.jobNoRef " +
                        " AND r.availabilityIdRef = a.availabilityId " +
                        " AND a.contractorIdRef = c.contractorId " +
                        " AND c.contractorId = s.staffId " +
                        " AND l.locationId = j.locationIdRef " +
                        " AND cl.clientId = l.clientIdRef " +
                        " AND j.availabilityIdRef is null " +
                        " ORDER BY j.requiredCompDate, j.jobNo asc ";

            DataTable jobTable = _db.ExecuteSQL(sql);
            foreach (DataRow dr in jobTable.Rows)
            {
                Job newJob = new Job(dr, "rejected");
                this.Add(newJob);
            }
        }
        public RejectedJobs(int jobNo)
        {
            _db = new SQLHelper();
            string sql =
                        " SELECT j.jobNo, s.staffId, s.firstName cFirstName, s.lastName cLastName, r.rejectionComment, r.rejectionDate, j.requiredCompDate, j.priorityLevel, " +
                        " cl.clientId, cl.firstName clFirstName, cl.lastName clLastName,  j.jobDescription " +
                        " FROM JOB j, JOB_REJECTION r, AVAILABILITY a, CONTRACTOR c, STAFF s, JOB_LOCATION l, CLIENT cl " +
                        " WHERE j.jobNo = r.jobNoRef " +
                        " AND r.availabilityIdRef = a.availabilityId " +
                        " AND a.contractorIdRef = c.contractorId " +
                        " AND c.contractorId = s.staffId " +
                        " AND l.locationId = j.locationIdRef " +
                        " AND cl.clientId = l.clientIdRef " +
                        " AND j.availabilityIdRef is null " +
                        " AND j.jobNo = " + jobNo + 
                        " ORDER BY j.requiredCompDate, j.jobNo asc ";

            DataTable jobTable = _db.ExecuteSQL(sql);
            foreach (DataRow dr in jobTable.Rows)
            {
                Job newJob = new Job(dr, "rejected");
                this.Add(newJob);
            }
        }
        public RejectedJobs(DateTime JobDate)
        {
            _db = new SQLHelper();
            string sql =
                        " SELECT j.jobNo, s.staffId, s.firstName cFirstName, s.lastName cLastName, r.rejectionComment, r.rejectionDate, j.requiredCompDate, j.priorityLevel, " +
                        " cl.clientId, cl.firstName clFirstName, cl.lastName clLastName,  j.jobDescription " +
                        " FROM JOB j, JOB_REJECTION r, AVAILABILITY a, CONTRACTOR c, STAFF s, JOB_LOCATION l, CLIENT cl " +
                        " WHERE j.jobNo = r.jobNoRef " +
                        " AND r.availabilityIdRef = a.availabilityId " +
                        " AND a.contractorIdRef = c.contractorId " +
                        " AND c.contractorId = s.staffId " +
                        " AND l.locationId = j.locationIdRef " +
                        " AND cl.clientId = l.clientIdRef " +
                        " AND j.availabilityIdRef is null " +
                        " AND j.requiredCompDate = '" + JobDate.ToString("yyyy-MM-dd") + "' " +
                        " ORDER BY j.requiredCompDate, j.jobNo asc ";

            DataTable jobTable = _db.ExecuteSQL(sql);
            foreach (DataRow dr in jobTable.Rows)
            {
                Job newJob = new Job(dr, "rejected");
                this.Add(newJob);
            }
        }

    }
}
