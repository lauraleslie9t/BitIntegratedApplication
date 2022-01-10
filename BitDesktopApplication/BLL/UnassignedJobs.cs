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
    public class UnassignedJobs : List<Job>
    {
        private SQLHelper _db;
        public UnassignedJobs()
        {
            _db = new SQLHelper();
            string sql =
                " SELECT JobNo,c.ClientId,  c.firstName, c.lastName, l.region, j.jobDescription, j.priorityLevel, j.creationDate, j.requiredCompDate" +
                " FROM JOB j, JOB_LOCATION l, CLIENT c WHERE l.locationId = j.locationIdRef " +
                " AND c.clientId = l.clientIdRef " +
                " AND j.availabilityIdRef IS NULL" +
                " AND jobNo NOT IN (SELECT jobNoRef from JOB_REJECTION)" +
                " ORDER BY requiredCompDate, priorityLevel";

            DataTable jobTable = _db.ExecuteSQL(sql);
            foreach (DataRow dr in jobTable.Rows)
            {
                Job newJob = new Job(dr, "unassigned");
                this.Add(newJob);
            }
        }

        public UnassignedJobs(int jobNo)
        {
            _db = new SQLHelper();
            string sql =
                " SELECT JobNo,c.ClientId,  c.firstName, c.lastName, l.region, j.jobDescription, j.priorityLevel, j.creationDate, j.requiredCompDate" +
                " FROM JOB j, JOB_LOCATION l, CLIENT c WHERE l.locationId = j.locationIdRef " +
                " AND c.clientId = l.clientIdRef " +
                " AND j.availabilityIdRef IS NULL" +
                " AND jobNo NOT IN (SELECT jobNoRef from JOB_REJECTION)" +
                " AND jobNo = " + jobNo + 
                " ORDER BY requiredCompDate, priorityLevel";

            DataTable jobTable = _db.ExecuteSQL(sql);
            foreach (DataRow dr in jobTable.Rows)
            {
                Job newJob = new Job(dr, "unassigned");
                this.Add(newJob);
            }
        }
        public UnassignedJobs(DateTime compDate)
        {
            _db = new SQLHelper();
            string sql =
                " SELECT JobNo,c.ClientId,  c.firstName, c.lastName, l.region, j.jobDescription, j.priorityLevel, j.creationDate, j.requiredCompDate" +
                " FROM JOB j, JOB_LOCATION l, CLIENT c WHERE l.locationId = j.locationIdRef " +
                " AND c.clientId = l.clientIdRef " +
                " AND j.availabilityIdRef IS NULL" +
                " AND jobNo NOT IN (SELECT jobNoRef from JOB_REJECTION)" +
                " AND j.requiredCompDate = '" + compDate.ToString("yyyy-MM-dd") + "' " +
                " ORDER BY requiredCompDate, priorityLevel";

            DataTable jobTable = _db.ExecuteSQL(sql);
            foreach (DataRow dr in jobTable.Rows)
            {
                Job newJob = new Job(dr, "unassigned");
                this.Add(newJob);
            }
        }
    }
}
