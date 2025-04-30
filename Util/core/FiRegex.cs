using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace OrakYazilimLib.Util.core
{
    public static class FiRegex
    {

        public static (string txXmlNew, string txXmlExtracted) ExtractAndReplace(string txXml, string keyStart, string keyEnd)
        {
            string pattern = @$"{keyStart}(.*?){keyEnd}";

            // İçeriği yakala ve değiştir
            string txXmlExtracted = "";

            string txXmlNew = Regex.Replace(txXml, pattern, match =>
            {
                txXmlExtracted = match.Groups[1].Value; // Aradaki içeriği al
                return ""; //  olarak değiştir
            }, RegexOptions.Singleline);

            return (txXmlNew, txXmlExtracted); // Hem değiştirilmiş XML'i hem de bulunan içerik döndürülür
        }


    }
}