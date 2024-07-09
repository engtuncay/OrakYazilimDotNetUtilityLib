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
		public static bool boTestEnabled = false;
		public static bool boDebugDetailEnabled = false;

		public static void logWeb(String message)
		{
			if (boTestEnabled)
			{
				Debug.WriteLine(message);
			}
		}

		public static void logDebug(String message)
		{
			Debug.WriteLine(message);
		}

		public static void logException(Exception ex)
		{
			if (boTestEnabled)
			{
				Debug.WriteLine(ex.Message);
				Debug.WriteLine(ex.StackTrace);
			}
		}


		public static string getStackTrace(Exception exception)
		{
			if (boDebugDetailEnabled)
			{
				return exception.StackTrace;
			}
			return null;
		}

		public static string GetDetailSqlLog(FiSqlServerQuery fiSqlServerQuery)
		{
			if (boDebugDetailEnabled)
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