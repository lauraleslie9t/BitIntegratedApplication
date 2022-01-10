using BitDesktopApplication.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitDesktopApplication.Models
{
    public class Login
    {
        private SQLHelper _db;
        private int _userId;
        private string _username;
        private string _password;
        private string _userType;
        private string _firstName;
        private string _lastName;
        public int UserId
        {
            get { return _userId; }
            set { this._userId = value; }
        }
        public string Username
        {
            get { return _username; }
            set { this._username = value; }
        }
        public string Password
        {
            get { return _password; }
            set { this._password = value; }
        }
        public string UserType
        {
            get { return _userType; }
            set { this._userType = value; }
        }
        public string FirstName
        {
            get { return _firstName; }
            set { this._firstName = value; }
        }
        public string LastName
        {
            get { return _lastName; }
            set { this._lastName = value; }
        }
       
        public Login()
        {
            _db = new SQLHelper();
        }
        public bool LoginUser()
        {
            string sql = 
                " SELECT l.userId, s.firstName, s.lastName, s.roleName " +
                " FROM LOGIN l, STAFF s " +
                " WHERE l.userId = s.staffId" +
                " AND l.archived = 0 AND username = '" + Username + "' AND [password] = '" + Password + "'";
            _db = new SQLHelper();
            DataTable data = _db.ExecuteSQL(sql);
            if (data.Rows.Count != 1)
            {
                return false;
            }
            else
            {
                this.UserId = Convert.ToInt32(data.Rows[0][0]);
                this.FirstName = data.Rows[0][1].ToString();
                this.LastName = data.Rows[0][2].ToString();
                this.UserType = data.Rows[0][3].ToString();
                //if client
                if (UserType == "Administrator")
                {
                    return true;
                }
                //if contractor
                if(UserType == "Coordinator")
                {
                    return true;
                }
                //if admin or coord. 
                else
                {
                    return false;
                }
            }
        }
    }

}
