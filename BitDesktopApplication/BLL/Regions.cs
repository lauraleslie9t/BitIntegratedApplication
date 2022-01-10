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
    class Regions : List<Region>
    {
        public Regions()
        {
            SQLHelper helper = new SQLHelper();
            string sql = "Select * from REGION";
            DataTable regionTable = helper.ExecuteSQL(sql);

            foreach (DataRow dr in regionTable.Rows)
            {
                Region newRegion = new Region(dr);
                this.Add(newRegion);
            }
        }
    }
}
