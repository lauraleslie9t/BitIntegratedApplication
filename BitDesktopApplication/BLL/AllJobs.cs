using BitDesktopApplication.DAL;
using BitDesktopApplication.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitDesktopApplication.BLL
{
    public class AllJobs : List<Job>
    {
        private SQLHelper _db = new SQLHelper();
        private int _id;
        public AllJobs()
        {
            _db = new SQLHelper();
            DataTable jobTable = _db.ExecuteStoredProc("usp_GetAllJobStatus");
            foreach (DataRow dr in jobTable.Rows)
            {
                Job newJob = new Job(dr, "all"); 
                this.Add(newJob);
            }
        }
        public AllJobs(int id, string type) //gets all jobs for clients with their status. 
        {
            this._id = id;

            switch (type)
            {
                case "Client":
                    LoadClientJobs();
                    break;

                case "Contractor":
                    LoadContractorJobs();
                    break;

                default:
                    break;
            }
        }

        private void LoadClientJobs()
        {
            SqlParameter[] parameters = { new SqlParameter("@ClientId", _id)};

            _db = new SQLHelper();
            DataTable jobTable = _db.ExecuteStoredProc("usp_GetAllCustomerJobStatus", parameters);
            foreach (DataRow dr in jobTable.Rows)
            {
                Job newJob = new Job(dr, "ClientStatus");
                this.Add(newJob);
            }
        }

        private void LoadContractorJobs()
        {
            SqlParameter[] parameters = { new SqlParameter("@ContractorId", _id) };

            _db = new SQLHelper();
            DataTable jobTable = _db.ExecuteStoredProc("usp_GetAllContractorJobStatus", parameters);
            foreach (DataRow dr in jobTable.Rows)
            {
                Job newJob = new Job(dr, "ContractorStatus");
                this.Add(newJob);
            }
        }
        
    }
}
