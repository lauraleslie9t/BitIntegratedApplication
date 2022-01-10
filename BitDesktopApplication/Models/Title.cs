using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitDesktopApplication.Models
{
    public class Title
    {
        private string _titleName;

        public string TitleName
        {
            get { return _titleName; }
            set { this._titleName = value; }
        }


        public Title()
        {

        }
        public Title(DataRow dr)
        {
            this._titleName = dr["title"].ToString();
        }
    }
}
