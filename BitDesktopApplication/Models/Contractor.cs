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
    public class Contractor : INotifyPropertyChanged, IDataErrorInfo
    {
        private int _staffId;
        private string _title;
        private string _firstName;
        private string _lastName;
        private DateTime _dob;
        private string _email;
        private string _mobile;
        private string _phone;
        private string _street;
        private string _suburb;
        private string _postcode;
        private string _state;
        private string _country;

        private decimal _hourlyRate;
        private decimal _rating;
        private string _region;


        private string _password;


        private SQLHelper _db;

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
                        if (Phone != null && Phone != "")
                        {
                            if (Phone.Length > 10)
                            {
                                result = "Phone number must be 10 characters.";
                                break;
                            }
                            if (!regex.IsMatch(Phone))
                            {
                                result = "Numerical digits only.";
                                break;
                            }

                        }
                        break;

                    case "Mobile":
                        if (Mobile != null && Mobile != "")
                        {
                            if (Mobile.Length > 10)
                            {
                                result = "Mobile number must be 10 characters.";
                                break;
                            }
                            if (!regex.IsMatch(Mobile))
                            {
                                result = "Numerical digits only.";
                                break;
                            }
                        }
                        break;
                    case "Street":
                        if (string.IsNullOrWhiteSpace(Street))
                        {
                            result = "Street cannot be empty. ";
                            break;
                        }
                        if (Street.Length > 50)
                        {
                            result = "Street details are too long.";
                        }
                        break;

                    case "Suburb":
                        if (string.IsNullOrWhiteSpace(Suburb))
                        {
                            result = "Suburb cannot be empty.";
                            break;
                        }
                        if (Suburb.Length > 20)
                        {
                            result = "Suburb name is too long.";
                            break;
                        }
                        break;

                    case "Postcode":
                        if (string.IsNullOrWhiteSpace(Postcode))
                        {
                            result = "Postcode cannot be empty.";
                            break;
                        }
                        if (!regex.IsMatch(Postcode))
                        {
                            result = "Numerical digits only.";
                            break;
                        }
                        if (Postcode.Length > 4)
                        {
                            result = "Postcode is too long.";
                            break;
                        }
                        
                        break;

                    case "State":
                        if (string.IsNullOrWhiteSpace(State))
                        {
                            result = "State cannot be empty.";

                        }
                        break;

                    case "Country":
                        if (string.IsNullOrWhiteSpace(Country))
                        {
                            result = "Country cannot be empty.";
                            break;

                        }
                        if (Country.Length > 74)
                        {
                            result = "Country name is too long.";
                            break;
                        }
                        break;
                    case "HourlyRate":
                        if (HourlyRate.Equals(null))
                        {
                            
                            result = "Hourly rate is required.";
                            break;
                        }
                        break;

                    case "Region":
                        if (string.IsNullOrWhiteSpace(Region))
                        {
                            result = "Region must be selected.";

                        }
                        break;

                }
                if (result != null && !ErrorCollection.ContainsKey(propertyName))
                    ErrorCollection.Add(propertyName, result);
                OnPropertyChanged("ErrorCollection");
                return result;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        #region  Public Properties
        public int StaffId
        {
            get { return _staffId; }
            set
            {
                this._staffId = value; OnPropertyChanged("StaffId");
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
        public string Email
        {
            get { return _email; }
            set
            {
                this._email = value; OnPropertyChanged("Email");
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
        public string Phone
        {
            get { return _phone; }
            set
            {
                this._phone = value; OnPropertyChanged("Phone");
            }

        }
        public string Street
        {
            get { return _street; }
            set
            {
                this._street = value; OnPropertyChanged("Street");
            }

        }
        public string Suburb
        {
            get { return _suburb; }
            set
            {
                this._suburb = value; OnPropertyChanged("Suburb");
            }

        }
        public string Postcode
        {
            get { return _postcode; }
            set
            {
                this._postcode = value; OnPropertyChanged("PostCode");
            }

        }
        public string State
        {
            get { return _state; }
            set
            {
                this._state = value; OnPropertyChanged("State");
            }

        }
        public string Country
        {
            get { return _country; }
            set
            {
                this._country = value; OnPropertyChanged("Country");
            }

        }
        public decimal HourlyRate
        {
            get { return _hourlyRate; }
            set
            {
                this._hourlyRate = value; OnPropertyChanged("HourlyRate");
            }
        }
        public decimal Rating
        {
            get { return _rating; }
            set { this._rating = value; OnPropertyChanged("Rating"); }
        }
        public string Region
        {
            get { return _region;  }
            set
            {
                this._region = value; OnPropertyChanged("Region");
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

        public Contractor()
        {
            _db = new SQLHelper();
        }
        public Contractor(DataRow dr)
        {
            this.StaffId = Convert.ToInt32(dr["staffId"].ToString());
            this.Title = dr["title"].ToString();
            this.FirstName = dr["firstName"].ToString();
            this.LastName = dr["lastName"].ToString();
            this.Dob = Convert.ToDateTime(dr["dob"].ToString());
            this.Email = dr["email"].ToString();
            this.Phone = dr["phone"].ToString();
            this.Mobile = dr["mobile"].ToString();
            this.Street = dr["street"].ToString();
            this.Suburb = dr["suburb"].ToString();
            this.Postcode = dr["postcode"].ToString();
            this.State = dr["state"].ToString();
            this.Country = dr["country"].ToString();

            this.HourlyRate = Convert.ToDecimal(dr["hourlyRate"].ToString());
            this.Rating = Convert.ToDecimal(dr["rating"].ToString());
            this.Region = dr["region"].ToString();

            _db = new SQLHelper();
        }

        public int AddContractor()
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
            if (Postcode != null)
            {
                if (!regex.IsMatch(Postcode))
                {
                    return -1;
                }
            }


            GeneratePassword();
            GenerateRating();
            _db = new SQLHelper();
            string sql =
            " DECLARE @staffId INT " +
            " DECLARE @contractorId INT " +
            " BEGIN TRAN T1 " +
            " INSERT INTO LOGIN(username, password, employee, archived) " +
            " values('" + this.Email + "', '" + this.Password +"', 1, 0)" +
            " set dateformat dmy; " +
            " set @staffId = SCOPE_IDENTITY(); set @contractorId = SCOPE_IDENTITY(); " +
            " INSERT INTO STAFF(staffId, title, firstName, lastName, dob, email, mobile, phone, street, suburb, state, postcode, country, roleName, archived) " +
            " values(@staffId, '" + this.Title + "', '" + this.FirstName + "', '" + this.LastName + "', '" + this.Dob + "', '" + 
            this.Email + "', '" + this.Mobile + "', '" +  this.Phone + "', '" + this.Street + "', '" + this.Suburb + "', '" + 
            this.State + "', '" + this.Postcode + "', '" + this.Country + "', 'Contractor', '0') " +
            " INSERT INTO CONTRACTOR(contractorId, hourlyrate, rating, region) " +
            " VALUES(@contractorId, " + this.HourlyRate + ", " + this.Rating + ", '" + this.Region + "') " +
            " COMMIT TRAN T1";

            return _db.ExecuteNonQuerySql(sql);

          

        }

        public int EditContractor()
        {
            if (Dob == Convert.ToDateTime("01-01-0001"))
            {
                return -1;
            }
            if (StaffId.Equals(null))
            {
                return -1;
            }


            _db = new SQLHelper();
            string editSql =
                " UPDATE STAFF " +
                " SET title = '" + this.Title + "', firstName = '" + this.FirstName + "', lastName = '" + this.LastName + "', dob = '" + this.Dob.ToString("yyyy-MM-dd") +
                "', email = '" + this.Email + "', mobile = '" + this.Mobile + "', phone = '" + this.Phone + "', street = '" + this.Street + "', suburb = '" +
                this.Suburb + "', state = '" + this.State + "', postcode = '" + this.Postcode + "', country = '" + this.Country + "'" +
                " WHERE staffId = " + StaffId.ToString() +
                " UPDATE Contractor " +
                " Set region = '" + this.Region + "', hourlyRate = " + HourlyRate.ToString() +
                " WHERE contractorId = " + this.StaffId.ToString() +
                " UPDATE LOGIN " +
                " Set username = '" + this.Email +
                "' WHERE userId = " + this.StaffId.ToString();

            return _db.ExecuteNonQuerySql(editSql);
        }

        public int DeleteContractor()
        {
            _db = new SQLHelper();
            string updateSql =
                " UPDATE STAFF " +
                " SET archived = 1 " +
                " WHERE staffId = " + StaffId +
                " Update LOGIN " +
                " SET archived = 1 " +
                " WHERE userId = " + StaffId;

            return _db.ExecuteNonQuerySql(updateSql);
        }
        private void GeneratePassword()
        {
            this.Password = "welcome";
        }
        private void GenerateRating()
        {
            Random rnd = new Random();
            this.Rating = Convert.ToDecimal(rnd.Next(3, 5) + "." + rnd.Next(0,9) + rnd.Next(0,9));
        }

    }
}
