using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrakYazilimLib.Util.core
{
    public class FiMeta
    {
        /**
         * TxCode (TxKodu)
        */
        public string txKey { get; set; }

        public string txValue { get; set; }

        /**
         * LnCode (LnKodu)
         * <p>
         * Key Meta Karşılık Gelen Integer Kod varsa
         */
        public int lnKey { get; set; }

        /**
         * Açıklama (Description) gibi düşünebiliriz
         */
        public string txLabel { get; set; }

        public string txType { get; set; }

        public FiMeta() {}

        public FiMeta(string txKey)
        {
            this.txKey = txKey;
        }


        public static FiMeta BuiLn(int lnKey)
        {
            return new FiMeta() { lnKey = lnKey };
        }
        public static FiMeta BuiLnAndKey(int lnKey, string txKey)
        {
            return new FiMeta() { lnKey = lnKey, txKey = txKey };
        }
    }
}