using BitDesktopApplication.DAL;
using BitDesktopApplication.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitDesktopApplication.BLL
{
    class Timeslots : List<Timeslot>
    {
        public Timeslots()
        {
            SQLHelper helper = new SQLHelper();
            string sql = "Select * from Timeslot";
            DataTable timeslotList = helper.ExecuteSQL(sql);

            foreach (DataRow dr in timeslotList.Rows)
            {
                Timeslot newSlot = new Timeslot(dr);
                this.Add(newSlot);
            }
        }
    }
}
