using OrakYazilimLib.DbGeneric;
using OrakYazilimLib.Util.core;
using System.Text.RegularExpressions;

namespace OrakYazilimLib.UtilXml
{
  public class FiXmlReq
  {
    public string txXml { get; set; }

    public FiKeybean fkbParams { get; set; }

    public string txBaseUrl { get; set; }

    public FiXmlReq(string prmTxXml, FiKeybean prmFkbParams)
    {
      this.txXml = prmTxXml;
      this.fkbParams = prmFkbParams;
    }

    public FiXmlReq()
    {
    }



    public string GetXmlFinal()
    {
      if(FiCollection.IsEmpty(this.fkbParams)) return txXml;

      this.txXml = FiXmlUtils.ConvertXmlParams(txXml, this.fkbParams);

      return txXml;
    }





  }
}