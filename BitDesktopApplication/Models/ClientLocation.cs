using BitDesktopApplication.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BitDesktopApplication.Models
{
    public class ClientLocation :INotifyPropertyChanged, IDataErrorInfo
    {
        

        private int _locationId;
        private int _clientId;
        private string _locationName;
        private string _street;
        private string _phone;
        private string _suburb;
        private string _postcode;
        private string _state;
        private string _country;
        private string _region;
        private SQLHelper _db;

        #region Public Properties
        public int LocationId
        {
            get { return _locationId; }
            set
            {
                this._locationId = value; OnPropertyChanged("LocationId");
            }

        }
        public int ClientId
        {
            get { return _clientId; }
            set
            {
                this._clientId = value; OnPropertyChanged("ClientId");
            }

        }
        public string LocationName
        {
            get { return _locationName; }
            set
            {
                this._locationName = value; OnPropertyChanged("LocationName");
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
                this._postcode = value; OnPropertyChanged("Postcode");
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
        public string Region
        {
            get { return _region; }
            set
            {
                this._region = value; OnPropertyChanged("Region");
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
        //private static readonly Regex regex = new Regex(@"^d+$");
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
                    case "LocationName":
                        if (LocationName != null)
                        {
                            if (LocationName.Length > 100)
                            {
                                result = "Location name cannot be More than 10.";
                            }

                        }
                        break;

                    case "Phone":
                        if (Phone != null)
                        {
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

                    case "Region":
                        if (string.IsNullOrWhiteSpace(Region))
                            result = "Region cannot be empty.";
                        break;
                }
                if (result != null && !ErrorCollection.ContainsKey(propertyName))
                    ErrorCollection.Add(propertyName, result);
                OnPropertyChanged("ErrorCollection");
                return result;
            }
        }


        public ClientLocation()
        {
            _db = new SQLHelper();
        }
        public ClientLocation(DataRow dr)
        {
            this.LocationId = Convert.ToInt32(dr["locationId"].ToString());
            this.ClientId = Convert.ToInt32(dr["clientIdRef"].ToString());
            this.LocationName = dr["locationName"].ToString();
            this.Phone = dr["Phone"].ToString();
            this.Street = dr["street"].ToString();
            this.Suburb = dr["suburb"].ToString();
            this.Postcode = dr["postcode"].ToString();
            this.State = dr["state"].ToString();
            this.Country = dr["country"].ToString();
            this.Region = dr["region"].ToString();
            _db = new SQLHelper();
        }


        public int AddClientLocation()
        {
            if (!regex.IsMatch(Phone) | !regex.IsMatch(Postcode))
            {
                return -1;
            }
            _db = new SQLHelper();
            string insertSql =
           " INSERT INTO JOB_LOCATION(clientIdRef, locationName,  phone, street, suburb, postcode, state, country, region, archived) " +
           " VALUES(" + this.ClientId.ToString() + ", '" + this.LocationName + "', '" +  this.Phone + "', '" + this.Street + "', '" + this.Suburb + "', '" + this.Postcode + "', '" + this.State + "', '" + this.Country + "', '" + this.Region + "', 0)";

            return _db.ExecuteNonQuerySql(insertSql);
        }

        public int DeleteLocation()
        {
            _db = new SQLHelper();
            string updateSql = 
                " UPDATE JOB_LOCATION " +
                " SET archived = 1 " +
                " WHERE locationId = " + LocationId;

            return _db.ExecuteNonQuerySql(updateSql);

        }

    }
}
