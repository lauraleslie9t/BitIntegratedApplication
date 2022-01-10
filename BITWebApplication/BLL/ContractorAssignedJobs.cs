using BITWebApplication.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BITWebApplication.BLL
{
    public class ContractorAssignedJobs
    { 
        public ContractorAssignedJobs()
        {
            SQLHelper helper = new SQLHelper();

        }
        public DataTable GetAllJobs(int contractorId)
        {
            string sql =
                            " SELECT j.jobNo, a.availabilityId [Availability Id], cl.firstName [Client First Name], cl.lastName [Client Last Name], " +
                            " Convert(varchar(12), a.Date, 103) [Date] , j.startTime [Start Time], " +
                            " j.expectedHours [Expected Hours], j.jobDescription [Description], priorityLevel [Prority Level], " +
                            " l.locationName [Location Name], l.Street , l.State " +
                            " FROM JOB j, CLIENT cl, JOB_LOCATION l, [AVAILABILITY] a " +
                            " WHERE l.locationId = j.locationIdRef " +
                            " AND cl.clientId = l.clientIdRef " +
                            " AND a.availabilityId = j.availabilityIdRef " +
                            " AND j.availabilityIdRef is not null " +
                            //" AND GETDATE() < a.date " +
                            " AND j.contractorConfirmed = 0 " +
                            " AND a.contractorIdRef = " + contractorId +
                            " ORDER BY Convert(DateTime, a.Date, 103) asc";

            SQLHelper helper = new SQLHelper();
            return helper.ExecuteSQL(sql);
        }
    }
}