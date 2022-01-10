using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitDesktopApplication.Models
{
    public class Timeslot
    {
        private string _timeSlotTime;

        public string TimeslotTime
        {
            get { return _timeSlotTime; }
            set { this._timeSlotTime = value; }
        }


        public Timeslot()
        {

        }
        public Timeslot(DataRow dr)
        {
            this._timeSlotTime = dr["timeslot"].ToString();
        }
    }
}
