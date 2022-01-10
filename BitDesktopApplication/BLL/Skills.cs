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
    public class Skills: List<Skill>
    {
        private SQLHelper _db;
        private string _sql;

        public Skills()
        {
            _db = new SQLHelper();
            
        }

        public void GetAllSkills()
        {
            _sql = "SELECT skill from SKILLS";
            ExecuteQuery("skill");
            _sql = string.Empty;
        }
        public void GetContractorSKills(int contractorId)
        {
            _sql = "SELECT staffSkillRef from Contractor_SKILLS WHERE staffId = " + contractorId;
            ExecuteQuery("staffSkillRef");
        }
        public void GetMissingContractorSkills(int contractorId)
        {
            _sql = "Select * From SKILLS WHERE skill not in (SELECT staffSkillRef from Contractor_SKILLS WHERE staffId =" + contractorId + ")";
            ExecuteQuery("skill");
        }
        public void GetJobSKills(int jobNo)
        {
            _sql = "SELECT jobSkillRef from JOB_SKILLS WHERE jobNo = " + jobNo;
            ExecuteQuery("jobSkillRef");
        }
        public void GetMissingJobSkills(int jobNo)
        {
            _sql = "Select * From SKILLS WHERE skill not in (SELECT jobSkillRef from JOB_SKILLS WHERE jobNo = " + jobNo + ")";
            ExecuteQuery("skill");
        }

        public void ExecuteQuery( string colName)
        {
            SQLHelper helper = new SQLHelper();
            DataTable skillsTable = helper.ExecuteSQL(_sql);
            foreach (DataRow dr in skillsTable.Rows)
            {
                Skill newSkill = new Skill(dr, colName);
                this.Add(newSkill);
            }
            _sql = string.Empty;
        }



        
    }
}
