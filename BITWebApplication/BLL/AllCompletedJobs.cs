using BITWebApplication.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BITWebApplication.BLL
{
    public class AllCompletedJobs
    {
        public AllCompletedJobs()
        {
            SQLHelper helper = new SQLHelper();
        }

        public DataTable GetAllJobs()
        {
            SQLHelper helper = new SQLHelper();
            string sql =
                        " SELECT j.jobNo [Job No], cl.clientId [Client Id], cl.firstName [Client First Name], cl.lastName [Client Last Name], " +
                        " s.staffId [Contractor Id], s.firstName [Contractor First Name], s.lastName [Contractor Last Name], " +
                        " Convert(varchar(12), a.date, 103)  [Job Date], Convert(varchar(12), requiredCompDate, 103)   [Required Completion Date], j.jobDescription Description, " +
                        " j.kmTravelled [Km Travelled], j.hoursTaken [Hours Taken]" +

                        " FROM JOB j, CLIENT cl, JOB_LOCATION l, AVAILABILITY a, CONTRACTOR c, STAFF  s " +
                        " WHERE l.locationId = j.locationIdRef " +
                        " AND cl.clientId = l.clientIdRef " +
                        " AND a.availabilityId = j.availabilityIdRef " +
                        " AND a.contractorIdRef = c.contractorId " +
                        " AND c.contractorId = s.StaffId " +
                        " AND availabilityIdRef is not null " +
                        //" AND GETDATE() >= a.date " +
                        " AND j.kmTravelled IS NOT NULL " +
                        " AND j.hoursTaken IS NOT NULL" +
                        " AND paymentStatus = 'unpaid' " +
                        " Order by Convert(DateTime, a.date, 103) ";

            return helper.ExecuteSQL(sql);
        }
    }
}