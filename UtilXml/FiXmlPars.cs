using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace OrakYazilimLib.UtilXml
{
    /// <summary>
    /// Xml Parser (xml alanlarını objeye atar)
    /// </summary>
    public class FiXmlPars
    {
        private XDocument xdoc { get; set; }

        public FiXmlPars(string txXml)
        {
            this.xdoc = XDocument.Parse(txXml);
        }

        public FiXmlPars()
        {
        }

        public static FiXmlPars BuiParseXml(string txXml)
        {
            // create document instance using XML file path
            // XDocument doc = XDocument.Load(filePath);
            XDocument doc = XDocument.Parse(txXml);

            FiXmlPars fiArb = new FiXmlPars
            {
                xdoc = doc
            };

            return fiArb;

            // get the namespace to that within of the XML (xmlns="...")
            // XElement root = doc.Root;
            // XNamespace ns = root.GetDefaultNamespace();
            //
            // // obtain a list of elements with specific tag
            // IEnumerable<XElement> elements = from c in doc.Descendants(ns + "exampleTagName") select c;

            // obtain a single element with specific tag (first instance), useful if only expecting one instance of the tag in the target doc
            //XElement element = (from c in doc.Descendants(ns + "exampleTagName" select c).First();

            // obtain an element from within an element, same as from doc
            //XElement embeddedElement = (from c in element.Descendants(ns + "exampleEmbeddedTagName" select c).First();

            // obtain an attribute from an element
            //XAttribute attribute = element.Attribute("exampleAttributeName");
        }

        public string GetTxFirstElement(string elementName){
            XElement firstElem = xdoc.Descendants(elementName).FirstOrDefault();

            return firstElem?.Value; //.Descendants(childElementName).FirstOrDefault();
            // if (firstBook != null)
            // {
            //     string title = firstBook.Element(childElementName)?.Value;
            //     Console.WriteLine($"First child value: {title}");
            //     return title;
            // }
            // return null;
        }



    }//end class
}