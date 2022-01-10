using BitDesktopApplication.BLL;
using BitDesktopApplication.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BitDesktopApplication.Models
{
    public class Client: INotifyPropertyChanged, IDataErrorInfo
    {
        private int _clientId;
        private string _title;
        private string _firstName;
        private string _lastName;
        private DateTime _dob;
        private string _companyName;
        private string _email;
        private string _phone;
        private string _mobile;
        private string _comments;
        private string _password;
        

        private SQLHelper _db;

        #region  Public Properties
        public int ClientId
        {
            get { return _clientId; }
            set
            {
                this._clientId = value; OnPropertyChanged("ClientId");
            }

        }
        public string Title
        {
            get { return _title; }
            set
            {
                this._title = value; OnPropertyChanged("Title");
            }

        }
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                this._firstName = value; OnPropertyChanged("FirstName");
            }

        }
        public string LastName
        {
            get { return _lastName; }
            set
            {
                this._lastName = value; OnPropertyChanged("LastName");
            }

        }
        public DateTime Dob
        {
            get { return _dob; }
            set
            {
                this._dob = value; OnPropertyChanged("Dob");
            }

        }
        public string CompanyName
        {
            get { return _companyName; }
            set
            {
                this._companyName = value; OnPropertyChanged("CompanyName");
            }

        }
        public string Email
        {
            get { return _email; }
            set
            {
                this._email = value; OnPropertyChanged("Email");
            }

        }
        public string Phone
        {
            get { return _phone; }
            set
            {
                this._phone = value; OnPropertyChanged("Phone");
            }

        }
        public string Mobile
        {
            get { return _mobile; }
            set
            {
                this._mobile = value; OnPropertyChanged("Mobile");
            }

        }
        public string Comments
        {
            get { return _comments; }
            set
            {
                this._comments = value; OnPropertyChanged("Comments");
            }

        }
        public string Password
        {
            get { return _password; }
            set
            {
                this._password = value; OnPropertyChanged("Password");
            }

        }
        #endregion Public Properties

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();
        public string Error { get { return null; } }

        //private bool NumericalChecker(string myNumber)
        //{
        //    foreach (char c in myNumber)
        //    {
        //        if (char.IsDigit(c))
        //        {
        //            return false;
        //        }

        //    }
        //    return true;
        //}
        Regex regex = new Regex("^[0-9]+$");
        public string this[string propertyName]
        {
            get
            {
                string result = null;
                switch (propertyName)
                {
                    case "CompanyName":
                        if (CompanyName != null)
                        {
                            if (CompanyName.Length > 50)
                            {
                                result = "Company name cannot be more than 50 characters.";
                            }

                        }
                        break;

                    case "Title":
                        if (string.IsNullOrWhiteSpace(Title))
                            result = "Title cannot be empty.";
                        break;

                    case "FirstName":
                        if (string.IsNullOrWhiteSpace(FirstName))
                        {
                            result = "First name cannot be empty. ";
                            break;
                        }
                        if (FirstName.Length > 50)
                        {
                            result = "First name details are too long.";
                        }
                        break;

                    case "LastName":
                        if (string.IsNullOrWhiteSpace(LastName))
                        {
                            result = "First name cannot be empty. ";
                            break;
                        }
                        if (LastName.Length > 50)
                        {
                            result = "Last name details are too long.";
                        }
                        break;

                    case "Dob":
                        if (Dob == null | Dob == Convert.ToDateTime("01/01/0001"))
                        {
                            result = "Date of Birth Required. ";
                            break;
                        }
                        break;

                    case "Email":
                        if (string.IsNullOrWhiteSpace(Email))
                        {
                            result = "Street cannot be empty. ";
                            break;
                        }
                        if (Email.Length > 254)
                        {
                            result = "Email address is too long.";
                        }
                        break;

                    case "Phone":
                        if (string.IsNullOrWhiteSpace(Phone))
                        {
                            result = "At least 1 phone number is required. ";
                            break;
                        }
                        if (!regex.IsMatch(Phone))
                        {
                            result = "Numerical digits only.";
                            break;
                        }
                        if (Phone.Length > 10)
                        {
                            result = "Phone number must be 10 characters.";
                            break;
                        }
                    
                        
                        break;

                    case "Mobile":
                        if (Mobile != "" && Mobile != null)
                        {
                            if (!regex.IsMatch(Mobile))
                            {
                                result = "Numerical digits only.";
                                break;
                            }
                            if (Mobile.Length > 10)
                            {
                                result = "Mobile number must be 10 characters.";
                                break;
                            }
  
                        }
                        break;

                    case "Comments":
                        if (Comments != null)
                        {
                            if (Comments.Length > 500)
                            {
                                result = "Comments cannot be more than 500 characters.";
                            }

                        }
                        break;
                }
                if (result != null && !ErrorCollection.ContainsKey(propertyName))
                    ErrorCollection.Add(propertyName, result);
                OnPropertyChanged("ErrorCollection");
                return result;
            }
        }

        public Client()
        {
            _db = new SQLHelper();
            //_db = null;
        }
        public Client(DataRow dr)
        {
            ClientId = Convert.ToInt32(dr["clientId"].ToString());
            Title = dr["title"].ToString();
            FirstName = dr["firstName"].ToString();
            LastName = dr["lastName"].ToString();
            Dob = Convert.ToDateTime(dr["dob"].ToString());
            CompanyName = dr["companyName"].ToString();
            Email = dr["email"].ToString();
            Phone = dr["phone"].ToString();
            Mobile = dr["mobile"].ToString();
            Comments = dr["comments"].ToString();
            _db = new SQLHelper();
        }
        
        public int AddClient()
        {
            // bug in validation. code behind blocks navigation but still inserts if not changed...
            if (Dob == Convert.ToDateTime("01-01-0001"))
            {
                return -1;
            }
            if (Phone != null)
            {
                if (!regex.IsMatch(Phone))
                {
                    return -1;
                }
            }
            if (Mobile != null)
            {
                if (!regex.IsMatch(Mobile))
                {
                    return -1;
                }
            }

            _db = new SQLHelper();

            GeneratePassword();
            string insertSql =
            " INSERT INTO LOGIN(username, password, employee, archived) " + 
            " VALUES('" + this.Email + "', '" +  this.Password + "', 0, 0) " +

            " set dateformat dmy; INSERT INTO " +
            " CLIENT(clientId, title, firstName, lastName, dob, companyName, email, mobile, phone, archived) " +
            " values(SCOPE_IDENTITY(), '" + this.Title + "', '" + this.FirstName + "', '" +
            this.LastName + "', '" + this.Dob.ToString() + "', '" + this.CompanyName + "', '" +
            this.Email + "', '" + this.Mobile + "', '" + this.Phone + "', 0)";


            return _db.ExecuteNonQuerySql(insertSql);

        }

        public int EditClient()
        {
            if (Dob == Convert.ToDateTime("01-01-0001"))
            {
                return -1;
            }
            if (ClientId.Equals(null))
            {
                return -1;
            }
            


            _db = new SQLHelper();
            string editSql =
                " UPDATE CLIENT " +
                " SET title = '" + this.Title + "', firstName = '" + this.FirstName + "', lastName = '" + this.LastName + "', dob = '" + this.Dob.ToString("yyyy-MM-dd") +
                "', email = '" + this.Email + "', mobile = '" + this.Mobile + "', phone = '" + this.Phone + 
                "', companyName = '" + this.CompanyName + "', comments = '" + this.Comments +
                "' WHERE clientId = " + ClientId.ToString() +
                " UPDATE LOGIN " +
                " Set username = '" + this.Email +
                "' WHERE userId = " + this.ClientId.ToString();

            return _db.ExecuteNonQuerySql(editSql);
        }

        public int DeleteClient()
        {
            _db = new SQLHelper();
            string updateSql =
                " UPDATE CLIENT " +
                " SET archived = 1 " +
                " WHERE clientId = " + ClientId +
                " Update LOGIN " +
                " SET archived = 1 " +
                " WHERE userId = " + ClientId;

            return _db.ExecuteNonQuerySql(updateSql);

        }

        public void GetLastInsertedClientId()
        {
            _db = new SQLHelper();
            string sql = "Select top 1 clientId from CLIENT Order by clientId desc";
            DataTable dataTable = _db.ExecuteSQL(sql);
            foreach (DataRow dr in dataTable.Rows)
            {
                ClientId = Convert.ToInt32(dr["clientId"].ToString());
            }
        }
        public void GetClientById()
        {
            _db = new SQLHelper();
            string sql = "Select * FROM CLIENT where clientId = " + ClientId;
            DataTable clientData =_db.ExecuteSQL(sql);
            DataRow dr = clientData.Rows[0];
            this.FirstName = dr["firstName"].ToString();
            this.LastName = dr["lastName"].ToString();
            this.Dob = Convert.ToDateTime(dr["dob"].ToString());
            this.CompanyName = dr["companyName"].ToString();
            this.Email = dr["email"].ToString();
            this.Mobile = dr["mobile"].ToString();
            this.Comments = dr["comments"].ToString();
            
        }

        private void GeneratePassword()
        {
            this.Password = "welcome";
        }
    }
}
