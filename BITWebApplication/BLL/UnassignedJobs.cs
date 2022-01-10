using BITWebApplication.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BITWebApplication.BLL
{
    public class UnassignedJobs
    {
        private SQLHelper _db;

        public UnassignedJobs()
        {
            _db = new SQLHelper();

        }

        public DataTable GetAllJobs()
        {
            string sql =
               " SELECT JobNo [Job No], c.firstName [Client First Name], c.lastName [Client Last Name], " +
               " l.region [Region], j.jobDescription [Description], j.priorityLevel [Priority Level], " +
               " Convert(varchar(12), j.creationDate, 103) [Creation Date], Convert(varchar(12), j.requiredCompDate, 103)  [Required Completion Date]" +
               " FROM JOB j, JOB_LOCATION l, CLIENT c WHERE l.locationId = j.locationIdRef " +
               " AND c.clientId = l.clientIdRef " +
               " AND j.availabilityIdRef IS NULL" +
               " AND jobNo NOT IN (SELECT jobNoRef from JOB_REJECTION)" +
               " ORDER BY requiredCompDate, priorityLevel";
            _db = new SQLHelper();
            return _db.ExecuteSQL(sql);
        }
    }
}