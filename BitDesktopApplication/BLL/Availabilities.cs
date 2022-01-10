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
    class Availabilities : List<Availability>
    {
        //public Availabilities()
        //{
        //    SQLHelper helper = new SQLHelper();
        //    string sql =    " SELECT a.availabilityId, c.contractorId, s.firstName, s.lastName, a.date, DATENAME(weekday, a.date) as dayOfWeek, a.start_Time, a.end_time, c.rating, a.slotBooked " +
        //                    " FROM AVAILABILITY a, CONTRACTOR c, STAFF s" +
        //                    " WHERE s.staffId = c.contractorId " +
        //                    " AND c.contractorId = a.contractorIdRef " +
        //                    " AND date > GETDATE() AND slotBooked = 0";

        //    DataTable availabilityTable = new DataTable();

        //    availabilityTable = helper.ExecuteSQL(sql);
        //    foreach (DataRow dr in availabilityTable.Rows)
        //    {
        //        Availability availability = new Availability(dr);
        //        this.Add(availability);
        //    }
        //}
        public Availabilities(int instructorId)
        {
            SQLHelper helper = new SQLHelper();
            string sql = " SELECT a.availabilityId, a.contractorIdRef, s.firstName, s.lastName, a.date, DATENAME(weekday, a.date) as dayOfWeek, a.start_Time, a.end_time, c.rating, a.slotBooked " +
                            " FROM AVAILABILITY a, CONTRACTOR c, STAFF s" +
                            " WHERE s.staffId = c.contractorId " +
                            " AND c.contractorId = a.contractorIdRef " +
                            " AND date > GETDATE() " +
                            " AND slotBooked = 0" +
                            " AND contractorIdRef = " + instructorId;

            DataTable availabilityTable = new DataTable();

            availabilityTable = helper.ExecuteSQL(sql);
            foreach (DataRow dr in availabilityTable.Rows)
            {
                Availability availability = new Availability(dr);
                this.Add(availability);
            }
        }
        public Availabilities(DateTime endDate, int jobNo)
        {
            string sql =
                    " SELECT a.availabilityId, a.contractorIdRef, s.firstName, s.lastName, a.date, DATENAME(weekday, date) as dayOfWeek, a.start_time, a.end_time, c.rating" +
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
                    " ORDER BY date Asc, rating Desc";

            SQLHelper helper = new SQLHelper();

            DataTable availabilityTable = helper.ExecuteSQL(sql);
            foreach (DataRow dr in availabilityTable.Rows)
            {
                Availability availability = new Availability(dr);
                this.Add(availability);
            }

        }
    }
}
