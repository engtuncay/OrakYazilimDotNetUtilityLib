using OrakYazilimLib.DbGeneric;
using OrakYazilimLib.Util;
using OrakYazilimLib.Util.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

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
        
        public string GetTxFirstElement(string txElemName){

            // <> şeklinde verilirse
            //string cleanTagName = StripAngleBrackets(txElemName);
            string openingTag = $"<{txElemName}>";
            string closingTag = $"</{txElemName}>";

            return FiString.ExtractFirstBetween(this.txXmlRaw, openingTag, closingTag);
        }

        /**
         * fiCol.ofcTxRefField'e göre Değeri Çeker
         */
        public string GetTxFirstElementByRef(FiCol fiCol)
        {
            return GetTxFirstElement(fiCol.ofcTxRefField);
        }



    }//end class
}