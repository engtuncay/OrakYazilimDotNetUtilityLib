using System;
using System.Configuration;

namespace OrakYazilimLib.Util.config
{
    public static class FiAppConfig
    {

        public static String GetConnectionString(string key)
        {
            string connString = ConfigurationManager.ConnectionStrings[key].ConnectionString;

            FiLogWeb.logWeb(ConfigurationManager.AppSettings[key]);
            //FiLogWeb.logWeb("FiAppConfig ConnString Key : " + key);
            //FiLogWeb.logWeb("FiAppConfig ConnString : " + connString);
            return connString;
        }

    }
}