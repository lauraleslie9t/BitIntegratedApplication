using BitDesktopApplication.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitDesktopApplication.Models
{
    public class Skill
    {
        private string _skillName;
        private string _skillDescription;
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

        public Skill()
        {
            _db = new SQLHelper();
        }
        public Skill(DataRow dr, string colName)
        {
            this._skillName = dr[colName].ToString();
        }
        public int AddContractorSkill(int contractorId)
        {
            _db = new SQLHelper();
            string sql = "INSERT INTO CONTRACTOR_SKILLS VALUES(" + contractorId + ", '" + this.SkillName + "' )";
            try
            {
                return _db.ExecuteNonQuery(sql);
            }
            catch
            {
                return -1;
            }
        } 
        public int RemoveContractorSkill(int contractorID)
        {
            _db = new SQLHelper();
            string sql = "DELETE FROM CONTRACTOR_SKILLS WHERE staffId = " + contractorID + " AND staffSkillRef = '" + this.SkillName + "'";
            return _db.ExecuteNonQuery(sql);
        }
        public int AddJobSkill(int jobNo)
        {
            _db = new SQLHelper();
            string sql = "INSERT INTO JOB_SKILLS VALUES(" + jobNo + ", '" + this.SkillName + "' )";
            try
            {
                return _db.ExecuteNonQuery(sql);
            }
            catch
            {
                return -1;
            }
        }
        public int RemoveJobSkill(int jobNo)
        {
            _db = new SQLHelper();
            string sql = "DELETE FROM JOB_SKILLS WHERE jobNo = " + jobNo + " AND jobSkillRef = '" + this.SkillName + "'";
            return _db.ExecuteNonQuery(sql);
        }

        public int AddNewSkill()
        {
            _db = new SQLHelper();
            string sql = "INSERT INTO SKILLS VALUES('" + this.SkillName + "', 'null') ";
            return _db.ExecuteNonQuery(sql);
        }
        public int DeleteSkill(int contractorID)
        {
            _db = new SQLHelper();
            string sql = "DELETE FROM SKILLS WHERE skill = '" + this.SkillName + "'";
            return _db.ExecuteNonQuery(sql);
        }

    }
}
