using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace KGCBank.Models
{
    public class ErrorLogger
    {
        public static void Log(string message)
        {
            string filePath = HttpContext.Current.Server.MapPath("~/App_Data/ErrorLog.txt");
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(DateTime.Now.ToString() + ": " + message);
            }
        }
    }
}