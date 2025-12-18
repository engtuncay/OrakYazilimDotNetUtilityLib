using OrakYazilimLib.Util;
using OrakYazilimLib.Util.config;
using OrakYazilimLib.Util.core;

namespace OrakYazilimLib.UtilRequest
{
  /// <summary>
  /// Request yapılırken gönderilen temel bilgiler : kullanıcı vs.
  /// </summary>
  public class FiRequest
  {
    public string frTxProfile { get; set; }
    public string frTxUser { get; set; }
    public string frTxPass { get; set; }
    public string frTxToken { get; set; }
    public FiKeybean frFkbParams { get; set; }

    public bool frBoShowDoc { get; set; }
    public string frTxDb { get; set; }

    public string GetTxProfile()
    {
      return frTxProfile;
    }

    public string GetTxKbbProfile()
    {
      if (FiString.IsEmpty(frTxProfile))
      {
        return FiAppConfig.boTestMode ? "kbb-test" : "kbb";
      }

      string txPref = FiAppConfig.boTestMode ? "kbb-test-" : "kbb-";
      return txPref + frTxProfile;
    }

    public string GetTxPanoProfile()
    {
      return "pano" + FiString.AddPrefixIfnEmpty(frTxProfile,"-");
    }

    public string GetTxMikroProfile()
    {
      if (FiString.IsEmpty(frTxProfile))
      {
        if (FiAppConfig.boTestMode)
        {
          return "mikro-test";
        }
        return "mikro";
      }

      if (FiAppConfig.boTestMode)
      {
        return "mikro-test-" + frTxProfile;
      }
      return "mikro-" + frTxProfile;
    }


    public string GetDbConnProfile()
    {
      return frTxProfile + (FiString.IsEmpty(frTxDb)?"": "-" + frTxDb);
    }

    public string GetConnProfile()
    {
      return frTxProfile??"";
    }

  }
}