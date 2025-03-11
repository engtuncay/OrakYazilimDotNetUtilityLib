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

    public string GetTxKbbProfile()
    {
      if (FiString.IsEmptyWithTrim(frTxProfile))
      {
        return FiAppConfig.boTestMode ? "kbb-test" : "kbb";
      }

      string txPref = FiAppConfig.boTestMode ? "kbb-test-" : "kbb-";
      return txPref + frTxProfile;
    }

    public string GetTxPanoProfile()
    {
      if (FiString.IsEmptyWithTrim(frTxProfile))
      {
        if (FiAppConfig.boTestMode)
        {
          return "pano-test";
        }
        return "pano";
      }

      if (FiAppConfig.boTestMode)
      {
        return "pano-test-" + frTxProfile;
      }
      return "pano-" + frTxProfile;
    }
  }
}