using BITWebApplication.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BITWebApplication.BLL
{
    public class RejectedJobs
    {
        private SQLHelper _db;

        public RejectedJobs()
        {
            _db = new SQLHelper();

        }

        public DataTable GetAllJobs()
        {
            string sql = 
                        " SELECT j.jobNo [Job No], s.firstName [Contractor First Name], s.lastName [Contractor Last Name], " +
                        " cl.firstName [Client First Name], cl.lastName [Client Last Name], " +
                        " r.rejectionComment [Rejection Comment],  Convert(varchar(12), r.rejectionDate, 103) [Rejection Date], " +
                        " Convert(varchar(12), j.requiredCompDate, 103) [Required Completion Date], j.priorityLevel [Priority Level], " +
                        " cl.clientId [Client Id],  j.jobDescription [Job Description]" +
                        " FROM JOB j, JOB_REJECTION r, AVAILABILITY a, CONTRACTOR c, STAFF s, JOB_LOCATION l, CLIENT cl " +
                        " WHERE j.jobNo = r.jobNoRef " +
                        " AND r.availabilityIdRef = a.availabilityId " +
                        " AND a.contractorIdRef = c.contractorId " +
                        " AND c.contractorId = s.staffId " +
                        " AND l.locationId = j.locationIdRef " +
                        " AND cl.clientId = l.clientIdRef " +
                        " AND j.availabilityIdRef is null " +
                        " ORDER BY j.requiredCompDate, j.jobNo asc ";
            _db = new SQLHelper();
            return _db.ExecuteSQL(sql);
        }
    }
}