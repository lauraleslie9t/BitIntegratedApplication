using BITWebApplication.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BITWebApplication.BLL
{
    public class ContractorConfirmedJobs
    {
        public ContractorConfirmedJobs()
        {
            SQLHelper helper = new SQLHelper();

        }
        public DataTable GetAllJobs(int contractorId)
        {
            string sql = " SELECT j.jobNo [Job No], cl.firstName [Client First Name], cl.lastName [Client Last Name], Convert(varchar(12), a.Date, 103) [Date], j.startTime [Start Time], j.expectedHours [Expected Hours], j.jobDescription [Description], priorityLevel [Priority Level], l.locationName [Location Name], l.Street, l.State, l.Phone " +
                            " FROM JOB j, CLIENT cl, JOB_LOCATION l, [AVAILABILITY] a " +
                            " WHERE l.locationId = j.locationIdRef " +
                            " AND cl.clientId = l.clientIdRef " +
                            " AND a.availabilityId = j.availabilityIdRef " +
                            " AND availabilityIdRef is not null " +
                            //" AND GETDATE() <= a.date " +
                            " AND j.contractorConfirmed = 1 " +
                            " and kmTravelled IS NULL " +
                            " AND hoursTaken IS NULL " +
                            " AND a.contractorIdRef = " + contractorId +
                            " ORDER BY Convert(DateTime, a.Date, 103) asc";

            SQLHelper helper = new SQLHelper();
            return helper.ExecuteSQL(sql);
        }
    }
}