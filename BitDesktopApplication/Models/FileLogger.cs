using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BitDesktopApplication.Models
{
    class FileLogger
    {
        //location of log file
        public string filePath = @"PageLog.txt";

        public void LogClickedPages(string message)
        {
            using (StreamWriter streamWriter = new StreamWriter(filePath, true))
            {
                streamWriter.WriteLine(message + " clicked on " + DateTime.Now);

                streamWriter.Close();
            }
        }
    }
}
