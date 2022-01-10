using BITWebApplication.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BITWebApplication.BLL
{
    public class ClientLocations
    {
        public ClientLocations()
        {
            SQLHelper helper = new SQLHelper();
        }
        public DataTable GetClientLocations(int clientId)
        {
            string sql = "SELECT locationId [Location Id], clientIdRef [Client Id], phone [Phone],  locationName [Location Name], Street , Suburb, Postcode, State, Country, Region " +
                        " FROM JOB_LOCATION " +
                        " WHERE clientIdRef = " + clientId + " AND archived = 0";
            SQLHelper helper = new SQLHelper();
            return helper.ExecuteSQL(sql);
        }
        
    }
}