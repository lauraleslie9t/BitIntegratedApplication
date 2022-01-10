using BITWebApplication.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BITWebApplication.BLL
{
    public class JobSkills
    {
        private SQLHelper _db;
        public JobSkills()
        {
            _db = new SQLHelper();
        }
        public DataTable GetJobSKills(int jobNo)
        {
            string sql = "SELECT jobSkillRef [Selected Skills] from JOB_SKILLS WHERE jobNo = " + jobNo;
            _db = new SQLHelper();
            return _db.ExecuteSQL(sql);
        }
        public DataTable GetMissingJobSKills(int jobNo)
        {
            string sql = "Select Skill as [Possible Skills] From SKILLS WHERE skill not in (SELECT jobSkillRef from JOB_SKILLS WHERE jobNo = " + jobNo + ")";
            _db = new SQLHelper();
            return _db.ExecuteSQL(sql);
        }
    }
}