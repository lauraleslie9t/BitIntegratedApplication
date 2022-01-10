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
    class States : List<State>
    {
        public States()
        {
            SQLHelper helper = new SQLHelper();
            string sql = "Select * from State";
            DataTable stateTable = helper.ExecuteSQL(sql);

            foreach (DataRow dr in stateTable.Rows)
            {
                State newState = new State(dr);
                this.Add(newState);
            }
        }
    }
}
