using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitDesktopApplication.Models
{
    public class Region
    {
        private string _regionName;

        public string RegionName
        {
            get { return _regionName; }
            set { this._regionName = value; }
        }


        public Region()
        {

        }
        public Region(DataRow dr)
        {
            this._regionName = dr["Region"].ToString();
        }
        
    }
}
