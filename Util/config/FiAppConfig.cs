using System;
using System.Configuration;

namespace OrakYazilimLib.Util.config
{
	public static class FiAppConfig
	{
		public static bool boTestMode = false;

		public static void convertTestModeTrue()
		{
			boTestMode = true;
		}

		public static String GetConnectionString(string key)
		{
			// config dosyasından key'den sonra test ile geleni alması için.
			if (boTestMode == true) key = key + "Test";

			string connString = ConfigurationManager.ConnectionStrings[key].ConnectionString;

			FiLogWeb.logWeb("Active GetConnectionString (FiAppConfig) : "+ connString);
			//FiLogWeb.logWeb("FiAppConfig ConnString Key : " + key);
			//FiLogWeb.logWeb("FiAppConfig ConnString : " + connString);
			return connString;
		}

	}
}