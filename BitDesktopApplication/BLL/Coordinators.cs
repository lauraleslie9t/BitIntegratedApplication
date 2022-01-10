using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitDesktopApplication.DAL;
using BitDesktopApplication.Models;

namespace BitDesktopApplication.BLL
{
    class Coordinators : List<Coordinator>
    {
        public Coordinators()
        {
            SQLHelper helper = new SQLHelper();

        }
        public void GetAllCoordinators()
        {
            SQLHelper helper = new SQLHelper();
            string sql = "set dateformat dmy; select staffId, title, firstName, lastName, dob, email, mobile, phone, street, suburb, state, postcode, country, roleName  from STAFF WHERE roleName='Coordinator' AND archived=0 ";

            DataTable coordinatorTable = new DataTable();

            coordinatorTable = helper.ExecuteSQL(sql);
            foreach (DataRow dr in coordinatorTable.Rows)
            {
                Coordinator newCoordinator = new Coordinator(dr);
                this.Add(newCoordinator);
            }
        }
        public void GetCoordinatorsById(int staffId)
        {
            SQLHelper helper = new SQLHelper();
            string sql = "set dateformat dmy; select staffId, title, firstName, lastName, dob, email, mobile, phone, street, suburb, state, postcode, country, roleName  from STAFF WHERE roleName='Coordinator' AND archived=0 AND staffId = " + staffId;

            DataTable coordinatorTable = new DataTable();

            coordinatorTable = helper.ExecuteSQL(sql);
            foreach (DataRow dr in coordinatorTable.Rows)
            {
                Coordinator newCoordinator = new Coordinator(dr);
                this.Add(newCoordinator);
            }
        }
        public void GetCoordinatorsByFirstName(string firstName)
        {
            SQLHelper helper = new SQLHelper();
            string sql = "set dateformat dmy; select staffId, title, firstName, lastName, dob, email, mobile, phone, street, suburb, state, postcode, country, roleName  from STAFF WHERE roleName='Coordinator' AND archived=0 AND firstName LIKE '%' + '" + firstName + "' + '%'";

            DataTable coordinatorTable = new DataTable();

            coordinatorTable = helper.ExecuteSQL(sql);
            foreach (DataRow dr in coordinatorTable.Rows)
            {
                Coordinator newCoordinator = new Coordinator(dr);
                this.Add(newCoordinator);
            }
        }
        public void GetCoordinatorsByLastName(string lastName)
        {
            SQLHelper helper = new SQLHelper();
            string sql = "set dateformat dmy; select staffId, title, firstName, lastName, dob, email, mobile, phone, street, suburb, state, postcode, country, roleName  " +
                " from STAFF WHERE roleName='Coordinator' AND archived=0 " +
                " AND lastName LIKE '%' + '" + lastName + "' + '%'";

            DataTable coordinatorTable = new DataTable();

            coordinatorTable = helper.ExecuteSQL(sql);
            foreach (DataRow dr in coordinatorTable.Rows)
            {
                Coordinator newCoordinator = new Coordinator(dr);
                this.Add(newCoordinator);
            }
        }
        public void GetCoordinatorsByDob(DateTime dob)
        {
            SQLHelper helper = new SQLHelper();
            string sql = "set dateformat dmy; select staffId, title, firstName, lastName, dob, email, mobile, phone, street, suburb, state, postcode, country, roleName  from STAFF WHERE roleName='Coordinator' AND archived=0 " +
                " and dob = '" + dob.ToString("yyyy-MM-dd") + "'";

            DataTable coordinatorTable = new DataTable();

            coordinatorTable = helper.ExecuteSQL(sql);
            foreach (DataRow dr in coordinatorTable.Rows)
            {
                Coordinator newCoordinator = new Coordinator(dr);
                this.Add(newCoordinator);
            }
        }

    }
}
