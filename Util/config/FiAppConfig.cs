using OrakYazilimLib.Util.core;
using System;
using System.Configuration;
using System.Drawing;

namespace OrakYazilimLib.Util.config
{
  public static class FiAppConfig
  {
    public static bool boTestMode = false;
    public static bool boUseConfigManager = false; // { get; set; }  //= false;
    public static IFiConfigManager fiConfigManager;
    public static IFiLogManager fiLogManager;

    public static void ConvertTestModeTrue()
    {
      boTestMode = true;
    }


    public static string GetConnectionString2(string txProfile)
    {
      // config dosyasından key'den sonra test ile geleni alması için.
      if (boTestMode == true) txProfile = txProfile + "-test";

      return fiConfigManager?.GetConnString(txProfile);
    }

    public static string GetConnectionString(string txProfile)
    {
      // config dosyasından key'den sonra test ile geleni alması için.
      if (boTestMode == true) txProfile = txProfile + "-test";

      if (boUseConfigManager)
      {
        return fiConfigManager?.GetConnString(txProfile);
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
      return fiConfigManager?.GetApiUrl(txProfile);

    }

    public static void LogMessage(string message)
    {
      fiLogManager?.LogMessage(message);
    }

  }
}