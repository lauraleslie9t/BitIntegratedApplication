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
    class Titles : List<Title>
    {
        public Titles()
        {
            SQLHelper helper = new SQLHelper();
            string sql = "Select * from Title";
            DataTable titleTable = helper.ExecuteSQL(sql);

            foreach (DataRow dr in titleTable.Rows)
            {
                Title newTitle = new Title(dr);
                this.Add(newTitle);
            }
        }
    }
}
