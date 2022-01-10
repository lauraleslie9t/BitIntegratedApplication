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
    public class PriorityLevels : List<Priority>
    {
        public PriorityLevels()
        {
            SQLHelper helper = new SQLHelper();
            string sql = "Select * from Priority";
            DataTable priorityList = helper.ExecuteSQL(sql);

            foreach (DataRow dr in priorityList.Rows)
            {
                Priority newPriority = new Priority(dr);
                this.Add(newPriority);
            }
        }
    }
}
