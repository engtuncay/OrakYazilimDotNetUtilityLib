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
    public class FiXmlPars2
    {
        private XmlDocument xdoc { get; set; }

        public FiXmlPars2(string txXml)
        {
            this.xdoc = new XmlDocument();
            this.xdoc.LoadXml(txXml);
        }

        public FiXmlPars2()
        {
        }

        public static FiXmlPars2 BuiParseXml(string txXml)
        {
            // create document instance using XML file path
            // XDocument doc = XDocument.Load(filePath);
            FiXmlPars2 fiXmlPars2 = new FiXmlPars2(txXml);
            return fiXmlPars2;
        }

        public string GetTxFirstElement(string txElemName){
            // Örnek: Bir eleman değeri okuma
            var rootElement = this.xdoc.DocumentElement;
            var firstElement = rootElement.SelectSingleNode(txElemName)?.InnerText;
            // Console.WriteLine(firstElement);
            return firstElement;
        }



    }//end class
}