using Newtonsoft.Json.Linq;
using OrakYazilimLib.Util;
using OrakYazilimLib.Util.Collection;
using OrakYazilimLib.Util.config;
using OrakYazilimLib.Util.core;
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace OrakYazilimLib.UtilXml
{
  public class FiXmlUtils
  {

    public static string ConvertXmlParams(string txXmlTemp, FiKeybean fkbParams)
    {
      if (FiString.IsEmpty(txXmlTemp) || fkbParams == null || fkbParams.Count == 0)
        return txXmlTemp;

      foreach (var key in fkbParams.Keys)
      {
        string placeholder = "{{" + key + "}}"; // Şablondaki büyük parantezler
        string value = "";

        object objValue = fkbParams.GetAsObject(key);

        FiAppConfig.fiLogManager?.LogMessage(objValue.GetType().ToString());

        if (objValue is FkbList fkbListChild)
        {
          (string txXmlReplaced, string txDetayXml) = ProcessFkbListKey(txXmlTemp, key);
          txXmlTemp = txXmlReplaced;

          StringBuilder sbChild = new StringBuilder();
          foreach (FiKeybean fkbChild in fkbListChild)
          {
            sbChild.Append(ConvertXmlParams(txDetayXml, fkbChild));
          }

          value = sbChild.ToString();

        }
        else if (objValue is DateTime dtValue)
        {
          value = dtValue.ToString("s"); // O olursa milisecond lı oluyor
        }
        else if (objValue is bool)
        {
          value = fkbParams.GetAsString(key).ToLower();
        }
        else if (objValue is JValue jsonValue)
        {
          if (jsonValue.Type == JTokenType.Boolean)
          {
            value = fkbParams.GetAsString(key).ToLower();
          }
          else
          {
            value = fkbParams.GetAsString(key) ?? string.Empty;
          }
        }
        else
        {
          value = fkbParams.GetAsString(key) ?? string.Empty; // Anahtara karşılık gelen değeri al
        }

        // Şablondaki placeholder'ları değerle değiştir
        txXmlTemp = txXmlTemp.Replace(placeholder, value);

      }

      return txXmlTemp; // Güncellenmiş metni döndür
    }

    public static (string txXmlReplaced, string txDetayXml) ProcessFkbListKey(string txXml, string key)
    {
      // Regex deseni: <!--!psgRfSipSatirList--> ile <!--!psgRfSipSatirList--> arasındaki içeriği yakalar
      string pattern = $"<!--!{key}-->(.*?)<!--!{key}-->";

      // İçeriği yakala ve değiştir
      string extractedContent = "";
      string replacedXml = Regex.Replace(txXml, pattern, match =>
      {
        extractedContent = match.Groups[1].Value; // Aradaki içeriği al
        return "{{" + key + "}}"; // {{key}} olarak değiştir
      }, RegexOptions.Singleline);

      return (replacedXml, extractedContent); // Hem değiştirilmiş XML'i hem de bulunan içerik döndürülür
    }
  }
}