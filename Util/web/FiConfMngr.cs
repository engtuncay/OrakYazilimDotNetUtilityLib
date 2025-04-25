using OrakYazilimLib.DbGeneric;
using System.Configuration;

namespace OrakYazilimLib.Util.web
{
  public static class FiConfMngr
  {

    public static string GetWebConfigValue(string customkey)
    {
      return FiString.OrEmpty(ConfigurationManager.AppSettings[customkey]);
    }
    public static string GetWebConfigValueByFc(FiCol fiCol)
    {
      return FiString.OrEmpty(ConfigurationManager.AppSettings[fiCol.ofcTxFieldName]);
    }
  }
}