using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitDesktopApplication.Models
{
    public class State
    {
        private string _stateName;

        public string StateName
        {
            get { return _stateName; }
            set { this._stateName = value; }
        }


        public State()
        {

        }
        public State(DataRow dr)
        {
            this._stateName = dr["state"].ToString();
        }
    }
}
