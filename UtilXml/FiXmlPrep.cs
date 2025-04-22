using OrakYazilimLib.Util;
using OrakYazilimLib.Util.core;

namespace OrakYazilimLib.UtilXml
{
  public class FiXmlPrep
  {

    public static string ConvertXmlParams(string txXmlTemp, FiKeybean fkbParams)
    {
      if (FiString.IsEmpty(txXmlTemp) || fkbParams == null || fkbParams.Count == 0)
        return txXmlTemp;

      foreach (var key in fkbParams.Keys)
      {
        string placeholder = "{{" + key + "}}"; // Şablondaki büyük parantezler
        string value = fkbParams.GetAsString(key) ?? string.Empty; // Anahtara karşılık gelen değeri al

        // Şablondaki placeholder'ları değerle değiştir
        txXmlTemp = txXmlTemp.Replace(placeholder, value);
      }

      return txXmlTemp; // Güncellenmiş metni döndür
    }
  }
}