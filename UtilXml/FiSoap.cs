using OrakYazilimLib.DataContainer;
using OrakYazilimLib.Util.config;
using System;
using System.IO;
using System.Net;
using System.Web;
using System.Xml;

namespace OrakYazilimLib.UtilXml
{
    public static class FiSoap
    {
        public static Fdr Execute(FiXmlReq fiXmlReq)
        {
            Fdr fdrMain = new Fdr();

            try
            {
                HttpWebRequest request = CreateWebRequest(fiXmlReq.txBaseUrl);
                //request.ContentType = "text/xml;charset=\"utf-8\"";
                XmlDocument soapEnvelopeXml = new XmlDocument();
                soapEnvelopeXml.LoadXml(fiXmlReq.GetXmlFinal()); //AddNamespace(txXmlContent) //txXmlContent
                //FiAppConfig.fiLogManager?.LogMessage(fiXmlReq.txXml);
                //FiAppConfig.fiLogManager?.LogMessage(fiXmlReq.txBaseUrl);

                using (Stream stream = request.GetRequestStream())
                {
                    soapEnvelopeXml.Save(stream);
                }

                //request.Method = "GET";
                using (HttpWebResponse response = (HttpWebResponse) request.GetResponse())
                {
                    Stream responseStream = response.GetResponseStream();
                    if (responseStream == null)
                    {
                        fdrMain.SetBoExecAndResultFalse();
                        fdrMain.txMessage = "Response stream is null.";
                        return fdrMain;
                    }

                    using (StreamReader rd = new StreamReader(responseStream))
                    {
                        string soapResult = rd.ReadToEnd();
                        // 1. Escape edilmiş yanıtı decode et
                        //string decodedSoapResult = WebUtility.HtmlDecode(soapResult);

                        // Eğer BOM (Byte Order Mark) karakterinden şüpheleniyorsanız:
                        //decodedSoapResponse = decodedSoapResponse.Replace("\uFEFF", "").Trim();

                        fdrMain.txResponse = soapResult; //decodedSoapResult.Trim();

                        //FiAppConfig.fiLogManager?.LogMessage("[Response XML Start]");
                        //FiAppConfig.fiLogManager?.LogMessage($"[{fdrMain.txResponse}]");
                        //FiAppConfig.fiLogManager?.LogMessage("[Response XML End]");

                        //Console.WriteLine(soapResult);
                        //FiAppConfig.fiLogManager?.LogMessage(soapResult);

                        int statusCode = (int)response.StatusCode;
                        fdrMain.lnStatusCode = statusCode;
                        fdrMain.boExecution = true;
                        //Console.WriteLine($"HTTP Durum Kodu: {statusCode}");
                    }

                }
            }
            catch (Exception ex)
            {
                FiAppConfig.fiLogMngr?.Error(ex.Message);
                FiAppConfig.fiLogMngr?.Error(ex.ToString());
                fdrMain.txMessage = ex.Message;
                fdrMain.SetBoExecAndResultFalse();
            }

            return fdrMain;
        }


        private static HttpWebRequest CreateWebRequest(string url)
        {
            //string url = "";
            HttpWebRequest webRequest = null;

            try
            {
                webRequest = (HttpWebRequest)WebRequest.Create(url);
                //webRequest.Headers.Add(@"SOAPAction","LoginRequest");
                webRequest.ContentType = "text/xml;charset=\"utf-8\"";
                webRequest.Accept = "text/xml";
                webRequest.Method = "POST";
            }
            catch (Exception ex)
            {
                FiAppConfig.fiLogMngr?.Debug(ex.Message);
                FiAppConfig.fiLogMngr?.Debug(ex.ToString());
            }
            return webRequest;
        }

        private static string AddNamespace(string XML)
        {
            string result = string.Empty;
            try
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml(XML);

                XmlElement temproot = xdoc.CreateElement("ws", "Request", "http://example.com/");
                temproot.InnerXml = xdoc.DocumentElement.InnerXml;
                result = temproot.OuterXml;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }

        private static string AppendEnvelope(string data)
        {
            string txHead = @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" ><soapenv:Header/><soapenv:Body>";
            string txEnd = @"</soapenv:Body></soapenv:Envelope>";
            return txHead + data + txEnd;
        }

        // Execute2
        public static Fdr Execute2(string txXmlContent,string txUrl)
        {
            Fdr fdrMain = new Fdr();

            try
            {
                HttpWebRequest request = CreateWebRequest(txUrl);
                XmlDocument soapEnvelopeXml = new XmlDocument();
                soapEnvelopeXml.LoadXml(txXmlContent); //AddNamespace(txXmlContent) //txXmlContent

                using (Stream stream = request.GetRequestStream())
                {
                    soapEnvelopeXml.Save(stream);
                }

                //request.Method = "GET";
                using (HttpWebResponse response = (HttpWebResponse) request.GetResponse())
                {

                    Stream responseStream = response.GetResponseStream();
                    if (responseStream == null)
                    {
                        //throw new InvalidOperationException("Response stream is null.");
                        fdrMain.boExecution = false;
                        fdrMain.txMessage = "Response stream is null.";
                        return fdrMain;
                    }

                    using (StreamReader rd = new StreamReader(responseStream))
                    {
                        string soapResult = rd.ReadToEnd();
                        fdrMain.txResponse = soapResult;
                        Console.WriteLine(soapResult);

                        int statusCode = (int)response.StatusCode;
                        fdrMain.lnStatusCode = statusCode;
                        fdrMain.boExecution = true;
                        Console.WriteLine($"HTTP Durum Kodu: {statusCode}");
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                fdrMain.txMessage = ex.Message;
                fdrMain.boExecution = false;
            }

            return fdrMain;
        }


    }//end class
}