using BITWebApplication.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BITWebApplication.BLL
{
    public class AllClientJobs
    {
        public AllClientJobs()
        {
            SQLHelper helper = new SQLHelper();
        }
        public DataTable GetAllClientJobs(int clientId)
        {
            SQLHelper helper = new SQLHelper();


            try
            {
                SqlParameter[] parameters =
                {
                new SqlParameter("@ClientId", clientId),
                };


                DataTable jobList = helper.ExecuteStoredProc("usp_GetAllCustomerJobStatus", parameters);

                return jobList;
            }
            catch (Exception ex)
            {

                throw new Exception("Failed to read data.", ex);
            }

        }
    }
}