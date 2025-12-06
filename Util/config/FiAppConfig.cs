using OrakYazilimLib.Util.core;
using System;
using System.Configuration;
using System.Drawing;

namespace OrakYazilimLib.Util.config
{
  public static class FiAppConfig
  {
    public static bool boTestMode = false;
    public static bool boUseConfigManagerDirectly = false; // { get; set; }  //= false;
    public static IFiConfigManager fiConfig;
    public static IFiLogManager fiLog;

    public static void ConvertTestModeTrue()
    {
      boTestMode = true;
    }


    public static string GetConnStringWthTest(string txProfile)
    {
      // config dosyasından key'den sonra test ile geleni alması için.
      if (boTestMode == true) txProfile = txProfile + "-test";
      return fiConfig?.GetConnString(txProfile);
    }

    public static string GetConnectionString(string txProfile)
    {
      // config dosyasından key'den sonra test ile geleni alması için.
      if (boTestMode == true) txProfile = txProfile + "-test";

      if (boUseConfigManagerDirectly)
      {
        return fiConfig?.GetConnString(txProfile);
      }

      string connString = ConfigurationManager.ConnectionStrings[txProfile].ConnectionString;

      FiLogWeb.logWeb("Active GetConnectionString (FiAppConfig) : " + connString);
      //FiLogWeb.logWeb("FiAppConfig ConnString Key : " + key);
      //FiLogWeb.logWeb("FiAppConfig ConnString : " + connString);
      return connString;
    }

    public static string GetBaseUrl(string txProfile)
    {
      // config dosyasından key'den sonra test ile geleni alması için.
      //if (boTestMode == true) txProfile = txProfile + "-test";
      return fiConfig?.GetApiUrl(txProfile);

    }

    public static void LogMessage(string message)
    {
      fiLog?.Debug(message);
    }

  }
}