using OrakYazilimLib.DbGeneric;
using OrakYazilimLib.Util.core;

namespace OrakYazilimLib.UtilXml
{
  public class FiXmlReq
  {
    private string txXml { get; set; }
    FiKeybean fkbParams { get; set; }

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

      return FiXmlUtils.ConvertXmlParams(txXml, fkbParams);
    }
  }
}