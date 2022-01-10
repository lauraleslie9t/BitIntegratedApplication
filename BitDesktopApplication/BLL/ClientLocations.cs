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
    class ClientLocations : List<ClientLocation>
    {
        public ClientLocations()
        {
            SQLHelper helper = new SQLHelper();
        }
        public ClientLocations(int clientID)
        {
            string sql = "SELECT locationId, clientIdRef, phone,  locationName, street, suburb, postcode, state, country, region " +
                        " FROM JOB_LOCATION " +
                        " WHERE clientIdRef = '" + clientID + "' AND archived = 0";
            SQLHelper helper = new SQLHelper();
            DataTable clientLocations = helper.ExecuteSQL(sql);

            foreach (DataRow dr in clientLocations.Rows)
            {
                ClientLocation clientLocation = new ClientLocation(dr);
                this.Add(clientLocation);
            }
        }
    }   
}
