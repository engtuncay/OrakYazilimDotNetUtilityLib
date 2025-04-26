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

        /// <summary>
        /// Verilen string içinde {{key}} formatında bir yapı olup olmadığını kontrol eder.
        /// </summary>
        /// <param name="txTemplate">Kontrol edilecek metin.</param>
        /// <param name="txKey">Aranılan Key Değer</param>
        /// <returns>True, eğer {{key}} yapısı varsa; aksi halde False.</returns>
        public static bool ContainsTemplateKey(string txTemplate, string txKey )
        {
            if (string.IsNullOrEmpty(txTemplate))
                return false;

            // {{key}} formatını kontrol eden regex
            var regex = new Regex(@"\{\{"+ txKey + @"\}\}");
            return regex.IsMatch(txTemplate);
        }
    }
}