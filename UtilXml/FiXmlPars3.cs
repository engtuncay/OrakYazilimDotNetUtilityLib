using OrakYazilimLib.DbGeneric;
using OrakYazilimLib.Util;
using OrakYazilimLib.Util.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Globalization;

namespace OrakYazilimLib.UtilXml
{
  /// <summary>
  /// Xml Parser (xml alanlarını objeye atar)
  /// </summary>
  public class FiXmlPars3
  {
    private string txXmlRaw { get; set; }

    public FiXmlPars3(string txXml)
    {
      this.txXmlRaw = txXml;
    }

    public FiXmlPars3()
    {
    }

    // public static FiXmlPars3 BuiParseXml(string txXml)
    // {
    //     // create document instance using XML file path
    //     // XDocument doc = XDocument.Load(filePath);
    //     FiXmlPars3 xmlPars3 = new FiXmlPars3(txXml);
    //     return xmlPars3;
    // }

    private string StripAngleBrackets(string elementName)
    {
      return elementName.TrimStart('<').TrimEnd('>');
    }

    public string GetTxFirstElement(string txElemName)
    {

      // <> şeklinde verilirse
      //string cleanTagName = StripAngleBrackets(txElemName);
      string openingTag = $"<{txElemName}>";
      string closingTag = $"</{txElemName}>";

      return ExtractFirstBetween(this.txXmlRaw, openingTag, closingTag);
    }

    /**
     * fiCol.ofcTxRefField'e göre Değeri Çeker
     */
    public string GetElemByRefAsTx(FiCol fiCol)
    {
      return GetTxFirstElement(fiCol.ofcTxRefField);
    }

    /**
     * string gelen değeri double a çevirir
     */
    public double GetElemByRefAsDouble(FiCol fiCol, int? lnScale = null)
    {
      string txFirstElement = GetTxFirstElement(fiCol.ofcTxRefField);

      if (lnScale != null)
      {
        txFirstElement = FiString.RoundString(txFirstElement);
      }

      Double.TryParse(txFirstElement, NumberStyles.Any, CultureInfo.InvariantCulture, out double result);;
      return result;

      // nullable dönüş yapmak istenirse
      //    string txFirstElement = GetTxFirstElement(fiCol.ofcTxRefField);
      // if (string.IsNullOrWhiteSpace(txFirstElement))
      // return null;
      // if (Double.TryParse(txFirstElement, out double result))
      // return result;
      // return null;

    }


    public bool? GetElemByRefAsBool(FiCol fiCol)
    {
      string txElemByRef = GetElemByRefAsTx(fiCol);
      if (txElemByRef.Equals("true", StringComparison.OrdinalIgnoreCase)) return true;
      if (txElemByRef.Equals("false", StringComparison.OrdinalIgnoreCase)) return false;
      return null;
    }

    public static string ExtractFirstBetween(string source, string startKey, string endKey)
    {
      var result = "";

      if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(startKey) || string.IsNullOrEmpty(endKey))
        return result;

      int currentPosition = 0;

      int startIdx = source.IndexOf(startKey, currentPosition, StringComparison.Ordinal);
      if (startIdx == -1) return result;

      startIdx += startKey.Length;
      int endIdx = source.IndexOf(endKey, startIdx, StringComparison.Ordinal);
      if (endIdx == -1) return result;

      result = source.Substring(startIdx, endIdx - startIdx);

      return result;
    }


  } //end class
}