using BITWebApplication.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BITWebApplication.BLL
{
    public class Availabilities
    {
        public Availabilities()
        {
            SQLHelper helper = new SQLHelper();
        }
        public DataTable GetAvailabilities(DateTime endDate, int jobNo)
        {
            string sql =
                    " SELECT a.availabilityId [Availability Id], a.contractorIdRef [Contractor Id], " +
                    " s.firstName [Contractor First Name], s.lastName [Contractor Last Name], Convert(varchar(12), a.date, 103) [Availability Date], " +
                    " DATENAME(weekday, date) [Day], a.start_time [Start Time], a.end_time [End Time], c.rating [Contractor Rating]" +
                    " FROM [AVAILABILITY] a, STAFF s, CONTRACTOR c, JOB_SKILLS js, CONTRACTOR_SKILLS cs, JOB j, JOB_LOCATION jl" +
                    
                    " WHERE a.contractorIdRef = s.staffId" +
                    " AND c.contractorId = s.staffId " +
                    " AND cs.staffId = c.contractorId" +
                    " AND js.jobNo = j.jobNo" +
                    " AND js.jobSkillRef = cs.staffSkillRef" +
                    " AND jl.locationId = j.locationIdRef" +
                    " AND jl.region = c.region" +
                    
                    " AND js.jobNo = " + jobNo +
                    " AND a.date >= GETDATE()" +
                    " AND a.date <= '" + endDate.ToString("yyyy-MM-dd") + "'" +
                    " AND s.archived = 0" +
                    " AND a.slotBooked = 0 " +

                    " AND a.contractorIdRef NOT IN " +
                    " (SELECT a.contractorIdRef FROM JOB_REJECTION jr, AVAILABILITY a WHERE jobNoRef = " + jobNo + " AND jr.availabilityIdRef = a.availabilityId)" +
                    
                    " ORDER BY date ASC, rating DESC";

            SQLHelper helper = new SQLHelper();

            return  helper.ExecuteSQL(sql);
            

        }
    }
}