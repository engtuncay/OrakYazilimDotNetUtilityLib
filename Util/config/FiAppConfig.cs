using System;
using System.Configuration;

namespace OrakYazilimLib.Util.config
{
    public static class FiAppConfig
    {

        public static String GetConnectionString(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ConnectionString;
        }

    }
}