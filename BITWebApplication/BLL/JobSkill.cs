using BITWebApplication.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BITWebApplication.BLL
{
    public class JobSkill
    {
        private string _skillName;
        private string _skillDescription;
        private int _jobNo;
        SQLHelper _db;

        public string SkillName
        {
            get { return _skillName; }
            set { this._skillName = value; }
        }
        public string SkillDescription
        {
            get { return _skillDescription; }
            set { this._skillDescription = value; }
        }
        public int JobNo
        {
            get { return _jobNo; }
            set { this._jobNo = value; }
        }
        public JobSkill()
        {
            _db = new SQLHelper();
        }
        public int AddJobSkill()
        {
            _db = new SQLHelper();
            string sql = "INSERT INTO JOB_SKILLS VALUES(" + this.JobNo + ", '" + this.SkillName + "' )";
            try
            {
                return _db.ExecuteNonQuery(sql);
            }
            catch
            {
                return -1;
            }
        }
        public int RemoveJobSkill()
        {
            _db = new SQLHelper();
            string sql = "DELETE FROM JOB_SKILLS WHERE jobNo = " + this.JobNo + " AND jobSkillRef = '" + this.SkillName + "'";
            return _db.ExecuteNonQuery(sql);
        }
    }
}