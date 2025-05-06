using System;
using System.Collections.Generic;
using System.Globalization;

namespace OrakYazilimLib.Util.core
{

  /// <summary>
  ///
  /// </summary>
  public static class FiString
  {
    public static bool IsEmpty(string value)
    {
      return value == null || value.Trim().Equals("");
    }

    public static bool IsEmptyWoutTrim(string value)
    {
      return value == null || value.Equals("");
    }

    public static bool IsEmpty(object value)
    {
      if (value == null) return true;
      return false;
    }

    public static string OrEmptyElseTos(object p)
    {
      return p == null ? "" : p.ToString();
    }

    public static string OrEmpty(string p)
    {
      return p ?? "";
    }

    public static string OrEmpty2(object objVal)
    {
      return objVal switch
      {
        null => "",
        string txVal => txVal,
        _ => objVal.ToString()
      };
    }

    public static string ToUpperCamelCase(string input)
    {
      if (string.IsNullOrEmpty(input))
      {
        return input;
      }

      // Büyük harfleri algılamak ve her birini parçalara ayırmak için bir algoritma.
      var words = new System.Text.StringBuilder();
      for (int i = 0; i < input.Length; i++)
      {
        // İlk harf veya bir harften sonra gelen büyük harf yeni bir kelime olarak kabul edilir.
        if (i == 0 || char.IsUpper(input[i]) && (i > 0 && char.IsLower(input[i - 1])))
        {
          if (words.Length > 0)
            words.Append(" "); // Kelimeleri ayırmak için boşluk ekle.
        }

        words.Append(char.ToUpper(input[i]));
      }

      // Her parçanın ilk harfini büyük yap ve birleştir.
      var textInfo = CultureInfo.InvariantCulture.TextInfo;
      return textInfo.ToTitleCase(words.ToString().ToLower()).Replace(" ", string.Empty);
    }


    /**
     *  null değeri varsa false sonuç döner.
     */
    public static bool Equals(string txVal1, string txVal2)
    {
      if (txVal1 == null || txVal2 == null) return false;
      return txVal1.Equals(txVal2);
    }

    public static List<string> ExtractAllBetween(string source, string startKey, string endKey)
    {
      var results = new List<string>();

      if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(startKey) || string.IsNullOrEmpty(endKey))
        return results;

      int currentPosition = 0;
      while (true)
      {
        int startIdx = source.IndexOf(startKey, currentPosition, StringComparison.Ordinal);
        if (startIdx == -1) break;

        startIdx += startKey.Length;
        int endIdx = source.IndexOf(endKey, startIdx, StringComparison.Ordinal);
        if (endIdx == -1) break;

        string value = source.Substring(startIdx, endIdx - startIdx);
        results.Add(value);

        currentPosition = endIdx + endKey.Length;
      }

      return results;
    }

    public static string ExtractFirstBetween(string source, string startKey, string endKey)
    {
      var result = "";

      if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(startKey) || string.IsNullOrEmpty(endKey))
        return result;

      int currentPosition = 0;

      int startIdx = source.IndexOf(startKey, currentPosition, StringComparison.Ordinal);
      if (startIdx == -1) return "";

      startIdx += startKey.Length;
      int endIdx = source.IndexOf(endKey, startIdx, StringComparison.Ordinal);
      if (endIdx == -1) return result;

      result = source.Substring(startIdx, endIdx - startIdx);
      //currentPosition = endIdx + endKey.Length;

      return result;
    }

    public static string ClearExtraZero(string txValue)
    {
      string cleanNumber = System.Text.RegularExpressions.Regex.Replace(txValue, @"(\.\d*?00)0+$", "$1"); // sonunda fazla sıfırları kaldırır
      //temizSayi = System.Text.RegularExpressions.Regex.Replace(temizSayi, @"\.0+$", ""); // sadece ".0" ise onu da kaldırır
      return cleanNumber;
    }

    public static string RoundString(string txValue)
    {
      string cleanNumber = System.Text.RegularExpressions.Regex.Replace(txValue, @"(\.\d{4})\d*$", "$1"); // sonunda fazla sıfırları kaldırır
      //temizSayi = System.Text.RegularExpressions.Regex.Replace(temizSayi, @"\.0+$", ""); // sadece ".0" ise onu da kaldırır
      return cleanNumber;
    }

    /**
     * Add Prefix If Not Empty (txValue)
     * <br/>
     * Boş Değilse TxValue'ya TxPrev'i ön ek olarak ekler
     */
    public static string AddPrefixIfnEmpty(string txValue, string txPrefix = "")
    {
      if (FiString.IsEmpty(txValue)) return "";
      return txPrefix + txValue;
    }
  }
}