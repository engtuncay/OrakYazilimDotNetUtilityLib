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
      if (FiString.IsEmpty(txXmlTemp) || fkbParams == null || fkbParams.Count == 0
        // template key yoksa, değiştirilecek bir şey yok
        || !FiXmlUtils.ContainsTemplateKey(txXmlTemp))
        return txXmlTemp;

      //
      foreach (var key in fkbParams.Keys)
      {
        string placeholder = "{{" + key + "}}"; // Şablondaki büyük parantezler
        string value = "";

        object objValue = fkbParams.GetAsObject(key);

        //FiAppConfig.fiLogManager?.LogMessage(objValue.GetType().ToString());


        if (objValue is FkbList fkbListChild)
        {
          if(!FiRegex.ContainsTemplateKey(txXmlTemp,key)){
            continue;
          }

          StringBuilder sbChild = new StringBuilder();

          foreach (FiKeybean fkbChild in fkbListChild)
          {
            string convertXmlParams = ConvertXmlParams(fkbListChild.txTemplate, fkbChild);
            sbChild.Append(convertXmlParams);
          }

          //fkbListChildElem.txValue = sbChild.ToString();
          value = sbChild.ToString(); //fkbListChild.txValue;

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

    public static string PrepFkbParams(string txXmlTemp, FiKeybean fkbParams)
    {
      var keys = fkbParams.Keys.ToList(); // Keys'i geçici bir listeye kopyalıyoruz

      // Değerler ile ilgili düzeltmeler
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
          //continue;
        }

      }

      return txXmlTemp;
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

    public static string DeActivateAllParams(string txXml)
    {
      //"--!(\\w+).*\\s*.*"; // @ verbatim operatörü eklenince ikinci slashlar kaldırılır
      const string regex = @"(.*\{\{(.*?)\}\}.*\n)"; // 20250426
      const string subst = "<!--$2 deactive-->\n";
      return Regex.Replace(txXml, regex, subst);
    }

    public static string DeActivateParam(string txXml, string key)
    {
      string regex = @"(.*\{\{(" + key + @")\}\}.*\n)"; // 20250426
      const string subst = "<!--$2 deactive-->\n";
      return Regex.Replace(txXml, regex, subst);
    }


    /// <summary>
    /// Verilen string içinde {{key}} formatında bir yapı olup olmadığını kontrol eder.
    /// </summary>
    /// <param name="txXml">Kontrol edilecek metin.</param>
    /// <returns>True, eğer {{key}} yapısı varsa; aksi halde False.</returns>
    public static bool ContainsTemplateKey(string txXml)
    {
      if (String.IsNullOrEmpty(txXml)) return false;

      // {{key}} formatını kontrol eden regex
      var regex = new Regex(@"\{\{.+?\}\}");
      return regex.IsMatch(txXml);
    }

  }

}
public static class FiRegex
{
    /// <summary>
    /// Verilen string içinde {{key}} formatında bir yapı olup olmadığını kontrol eder.
    /// </summary>
    /// <param name="txTemplate">Kontrol edilecek metin.</param>
    /// <param name="txKey">Aranılan Key Değer</param>
    /// <returns>True, eğer {{key}} yapısı varsa; aksi halde False.</returns>
    public static bool ContainsTemplateKey(string txTemplate, string txKey)
    {
        if (string.IsNullOrEmpty(txTemplate))
            return false;

        // Escape special characters in txKey to ensure it's treated as a literal.
        string escapedKey = Regex.Escape(txKey);

        // Build the regex to match the template key.
        var regex = new Regex(@"\{\{" + escapedKey + @"\}\}");
        return regex.IsMatch(txTemplate);
    }
}