using System.Configuration;

namespace OrakYazilimLib.Util.web
{
  public static class FiConfMngr
  {

    public static string GetWebConfigValue(string customkey)
    {
      return FiString.OrEmpty(ConfigurationManager.AppSettings[customkey]);
    }
  }
}