using BitDesktopApplication.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitDesktopApplication.Models
{
    public class Availability : INotifyPropertyChanged
    {
        private int _availabilityId;
        private int _contractorId;
        private DateTime _date;
        private string _dayOfWeek;
        private string _startTime;
        private string _endTime;
        private bool _slotBooked;

        private string _contractorFirstName;
        private string _contractorLastName;
        private decimal _rating;

        private SQLHelper _db;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        public int AvailabilityId
        {
            get { return _availabilityId; }
            set { this._availabilityId = value; }
        }
        public int ContractorId
        {
            get { return _contractorId; }
            set { this._contractorId = value; }
        }
        public DateTime Date
        {
            get { return _date; }
            set { this._date = value; OnPropertyChanged("Date"); }
        }
        public string DayOfWeek
        {
            get { return _dayOfWeek; }
            set { this._dayOfWeek = value;  }
        }
        public string StartTime
        {
            get { return _startTime; }
            set { this._startTime = value; OnPropertyChanged("StartTime"); }
        }
        public string EndTime
        {
            get { return _endTime; }
            set { this._endTime = value; OnPropertyChanged("EndTime"); }
        }
        public bool SlotBooked
        {
            get { return _slotBooked; }
            set { this._slotBooked = value; }
        }
        
        public string ContractorFirstName
        {
            get { return _contractorFirstName;  }
            set { this._contractorFirstName = value; OnPropertyChanged("ContractorFirstName"); }
        } 
        public string ContractorLastName
        {
            get { return _contractorLastName; }
            set { this._contractorLastName = value; OnPropertyChanged("ContractorLastName"); }
        }
        public decimal Rating
        {
            get { return _rating; }
            set { this._rating = value; OnPropertyChanged("Rating"); }
        }

        public Availability()
        {
            _db = new SQLHelper();
        }

        public Availability(DataRow dr)
        {
            //for contractor profile
            this.AvailabilityId = Convert.ToInt32(dr["availabilityId"].ToString());
            this.ContractorId = Convert.ToInt32(dr["contractorIdRef"].ToString());
            this.ContractorFirstName = dr["firstName"].ToString();
            this.ContractorLastName = dr["lastName"].ToString();
            this.Date = Convert.ToDateTime(dr["date"].ToString());
            this.DayOfWeek = dr["dayOfWeek"].ToString();
            this.StartTime = dr["start_time"].ToString();
            this.EndTime = dr["end_time"].ToString();
            this.Rating = Convert.ToDecimal(dr["rating"].ToString());
            //this.SlotBooked = Convert.ToBoolean(dr["slotBooked"].ToString());
        }
        //public Availability(DataRow dr, bool forJobBooking)
        //{
        //    // for booking jobs
        //    this._availabilityId = Convert.ToInt32(dr["availabilityId"].ToString());
        //    this._contractorId = Convert.ToInt32(dr["contractorIdRef"].ToString());
        //    this._date = Convert.ToDateTime(dr["date"].ToString());
        //    this.DayOfWeek = dr["dayOfWeek"].ToString();
        //    this._startTime = dr["start_time"].ToString();
        //    this._endTime = dr["end_time"].ToString();
        //    this.ContractorFirstName = dr["firstName"].ToString();
        //    this.ContractorLastName = dr["lastName"].ToString();
        //    this.Rating = Convert.ToDecimal(dr["rating"].ToString());
        //}

        public int AddAvailability()
        {
            SQLHelper helper = new SQLHelper();
            string insertSql = "set dateformat dmy; INSERT INTO AVAILABILITY(contractorIdRef, date, start_time, end_time, slotBooked) " +
                              " VALUES (@contractorIdRef, @date, @start_time, @end_time, @slotBooked ) ";

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@contractorIdRef", DbType.Int32);
            param[1] = new SqlParameter("@date", DbType.Date);
            param[2] = new SqlParameter("@start_time", DbType.Time);
            param[3] = new SqlParameter("@end_time", DbType.Time);
            param[4] = new SqlParameter("@slotBooked", DbType.Byte);
            param[0].Value = this.ContractorId;
            param[1].Value = this.Date;
            param[2].Value = TimeSpan.Parse(this.StartTime);
            param[3].Value = TimeSpan.Parse(this.EndTime);
            param[4].Value = 0;


            return helper.ExecuteNonQuery(insertSql, param);

        }

    }
}
