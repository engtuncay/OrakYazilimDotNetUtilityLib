using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrakYazilimLib.DataContainer;
using OrakYazilimLib.DbUtil;

namespace OrakYazilimLib.Util
{
    public class FiLogWeb
    {
        public static bool testEnabled=false;
        public static bool debugDetailEnabled = false;

        public static void logWeb(String message)
        {
            if (testEnabled)
            {
                Debug.WriteLine(message);
            }
        }

        public static void logException(Exception ex)
        {
            if (testEnabled)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
        }


        public static string getStackTrace(Exception exception)
        {
            if (debugDetailEnabled)
            {
                return exception.StackTrace;
            }
            return null;
        }

        public static string GetDetailSqlLog(FiSqlServerQuery fiSqlServerQuery)
        {
            if (debugDetailEnabled)
            {
                string log = "Query:" + fiSqlServerQuery.sql + "\n Params \n" + fiSqlServerQuery.GetSqlBindings();
                return log;
            }

            return null;
            
            
        }

        public static string GetMessage(Exception ex)
        {
            return ex.Message;
        }
    }
}
