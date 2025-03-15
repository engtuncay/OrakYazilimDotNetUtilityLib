using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OrakYazilimLib.Util
{

    /// <summary>
    ///
    /// </summary>
    public static class FiString
    {
        public static void check()
        {
            //
            //
        }

        public static bool IsEmpty(string value)
        {
            return value == null || value.Trim().Equals("");
        }

        public static bool IsEmptyWoutTrim(string value)
        {
            if (value == null || value.Equals("")) return true;
            return false;
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



    }
}