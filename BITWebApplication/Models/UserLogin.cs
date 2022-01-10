using BITWebApplication.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BITWebApplication.Models
{
    public class UserLogin
    {
        private SQLHelper _db;
        private int _userId;
        private string _email;
        private string _password;
        private bool _isEmployee;
        private string _userType;
        public int UserId
        {
            get { return _userId; }
            set { this._userId = value; }
        }
        public string Email
        {
            get { return _email; }
            set { this._email = value; }
        }
        public string Password
        {
            get { return _password; }
            set { this._password = value; }
        }
        public bool IsEmployee
        {
            get { return _isEmployee; }
            set { this._isEmployee = value; }
        }
        public string UserType
        {
            get { return _userType; }
            set { this._userType = value; }
        }
        public UserLogin()
        {
            _db = new SQLHelper();
        }
        public string GetUserType()
        {
            string sql = "SELECT userId, employee FROM LOGIN WHERE archived = 0 AND username = '" + Email + "' AND [password] = '" + Password + "'";
            _db = new SQLHelper();
            DataTable data = _db.ExecuteSQL(sql);
            if (data.Rows.Count != 1)
            {
                return "error";
            }
            else
            {
                this.UserId = Convert.ToInt32(data.Rows[0][0]);
                this.IsEmployee = Convert.ToBoolean(data.Rows[0][1]);
                if (IsEmployee == false)
                {
                    return "Client";
                }
                else
                {
                    return GetEmployeeType();
                }
            }
        }

        public string GetEmployeeType()
        {
            string sql = "SELECT roleName FROM STAFF WHERE staffId = " + UserId;
            _db = new SQLHelper();
            DataTable data = _db.ExecuteSQL(sql);
            return data.Rows[0][0].ToString();
        }
    }
}