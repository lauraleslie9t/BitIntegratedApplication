using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitDesktopApplication.Models
{
    public class Priority
    {
        private string _priorityLevel;

        public string PriorityLevel
        {
            get { return _priorityLevel; }
            set { this._priorityLevel = value; }
        }


        public Priority()
        {

        }
        public Priority(DataRow dr)
        {
            this.PriorityLevel = dr["priorityLevel"].ToString();
        }
    }
}
