using Newtonsoft.Json.Linq;
using OrakYazilimLib.Util;
using OrakYazilimLib.Util.Collection;
using OrakYazilimLib.Util.config;
using OrakYazilimLib.Util.core;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace OrakYazilimLib.UtilXml
{
  public class FiXmlUtils
  {

    public static string ConvertXmlParams(string txXmlTemp, FiKeybean fkbParams)
    {
      if (FiString.IsEmpty(txXmlTemp) || fkbParams == null || fkbParams.Count == 0)
        return txXmlTemp;

      var keys = fkbParams.Keys.ToList(); // Keys'i geçici bir listeye kopyalıyoruz

      // Değerler ilgili düzeltmeler
      foreach (var key1 in keys)
      {
        object objValue = fkbParams.GetAsObject(key1);

        // JArray FkbList'e çevrilir, objValue FkbList olur
        if (objValue is JArray jarrList)
        {
          var fkbListChild = FiJArray.ConvertFkbList(jarrList);
          fkbParams.AddForce(key1, fkbListChild); // Orijinal koleksiyona ekleme yapabilirsiniz
          objValue = fkbListChild;
        }

        if (objValue is FkbList fkbListChildElem)
        {
          (string txXmlNew, string txXmlExtracted) = ProcessChildFkbList(txXmlTemp, key1);
          txXmlTemp = txXmlNew;
          fkbListChildElem.txTemplate = txXmlExtracted;

          StringBuilder sbChild = new StringBuilder();
          foreach (FiKeybean fkbChild in fkbListChildElem)
          {
            string convertXmlParams = ConvertXmlParams(txXmlExtracted, fkbChild);
            //FiAppConfig.fiLogManager?.LogMessage("Converted:"+ convertXmlParams);
            //Console.WriteLine("Converted:"+ convertXmlParams);
            sbChild.Append(convertXmlParams);
          }

          fkbListChildElem.txValue = sbChild.ToString();
          //continue;
        }

      }

      //
      foreach (var key in fkbParams.Keys)
      {
        string placeholder = "{{" + key + "}}"; // Şablondaki büyük parantezler
        string value = "";

        object objValue = fkbParams.GetAsObject(key);

        FiAppConfig.fiLogManager?.LogMessage(objValue.GetType().ToString());

        if (objValue is FkbList fkbListChild)
        {
          value = fkbListChild.txValue;
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

    public static (string txXmlNew, string txXmlExtracted) ProcessChildFkbList(string txXml, string key)
    {
      // Regex deseni: <!--!psgRfSipSatirList--> ile <!--!psgRfSipSatirList--> arasındaki içeriği yakalar
      string pattern = $"<!--!{key}-->(.*?)<!--!{key}-->";

      // İçeriği yakala ve değiştir
      string txXmlExtracted = "";
      string txXmlNew = Regex.Replace(txXml, pattern, match =>
      {
        txXmlExtracted = match.Groups[1].Value; // Aradaki içeriği al
        return "{{" + key + "}}"; // {{key}} olarak değiştir
      }, RegexOptions.Singleline);

      return (txXmlNew, txXmlExtracted); // Hem değiştirilmiş XML'i hem de bulunan içerik döndürülür
    }
  }
}