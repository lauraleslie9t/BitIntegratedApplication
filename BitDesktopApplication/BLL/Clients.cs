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
    class Clients : List<Client>
    {
        public Clients()
        {
            SQLHelper helper = new SQLHelper();
        }
        public void GetAllClients()
        {
            string sql = "SELECT  * " +
                        " FROM CLIENT" +
                        " WHERE archived = 0";
            SQLHelper helper = new SQLHelper();
            DataTable allCustomers = helper.ExecuteSQL(sql);

            foreach (DataRow dr in allCustomers.Rows)
            {
                Client contractorRow = new Client(dr);
                this.Add(contractorRow);
            }
        }
        public void GetClientsById(int clientId)
        {   //TODO
            string sql = " SELECT * FROM CLIENT WHERE archived = 0 AND clientId = " + clientId;
            SQLHelper helper = new SQLHelper();
            DataTable allCustomers = helper.ExecuteSQL(sql);

            foreach (DataRow dr in allCustomers.Rows)
            {
                Client contractorRow = new Client(dr);
                this.Add(contractorRow);
            }
        }
        public void GetClientsByFirstName(string FirstName)
        {   //TODO
            string sql = "SELECT * FROM CLIENT WHERE archived = 0 AND firstName LIKE '%' + '" + FirstName + "' + '%'"; 
            SQLHelper helper = new SQLHelper();
            DataTable allCustomers = helper.ExecuteSQL(sql);

            foreach (DataRow dr in allCustomers.Rows)
            {
                Client contractorRow = new Client(dr);
                this.Add(contractorRow);
            }
        }
        public void GetClientsByLastName(string LastName)
        {   //TODO
            string sql = "SELECT * FROM CLIENT WHERE archived = 0 AND lastName LIKE '%' + '" + LastName + "' + '%'";
            SQLHelper helper = new SQLHelper();
            DataTable allCustomers = helper.ExecuteSQL(sql);

            foreach (DataRow dr in allCustomers.Rows)
            {
                Client contractorRow = new Client(dr);
                this.Add(contractorRow);
            }
        }
        public void GetClientsByDob(DateTime dob)
        {   //TODO
            string sql = " SELECT * FROM CLIENT WHERE archived = 0 AND dob = '" + dob.ToString("yyyy-MM-dd") + "'";
            SQLHelper helper = new SQLHelper();
            DataTable allCustomers = helper.ExecuteSQL(sql);

            foreach (DataRow dr in allCustomers.Rows)
            {
                Client contractorRow = new Client(dr);
                this.Add(contractorRow);
            }
        }
    }
}
