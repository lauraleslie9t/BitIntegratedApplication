using BITWebApplication.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BITWebApplication.BLL
{
    public class Skills
    {
        private SQLHelper _db;

        public Skills()
        {
            _db = new SQLHelper();

        }

        public DataTable GetAllSkills()
        {
            string sql = "SELECT Skill from SKILLS";
            _db = new SQLHelper();
            return _db.ExecuteSQL(sql);
        } 
    }
}