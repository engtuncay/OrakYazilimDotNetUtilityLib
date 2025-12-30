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
      ProcessParams();
      return txXml;
    }

    public void PrepFkbParams()
    {
      this.txXml = FiXmlUtil.PrepFkbParams(txXml, this.fkbParams);
    }
    public void DeactiveAllParams()
    {
      this.txXml = FiXmlUtil.DeActivateAllParams(this.txXml);
    }
    /**
     * Xml deki parametreleri değerlerle değiştirir.
     */
    public void ProcessParams()
    {
      if (FiCollection.IsEmpty(this.fkbParams)) return;
      this.txXml = FiXmlUtil.ConvertXmlParams(this.txXml, this.fkbParams);
    }

    public void ProcessParamsWitPrep()
    {
      if (FiCollection.IsEmpty(this.fkbParams)) return;
      this.txXml = FiXmlUtil.PrepFkbParams(this.txXml, this.fkbParams);
      this.txXml = FiXmlUtil.ConvertXmlParams(this.txXml, this.fkbParams);
    }
    public void DeactiveField(FiCol fiCol)
    {
      this.txXml = FiXmlUtil.DeActivateParam(this.txXml, fiCol.ofcTxFieldName);
    }

  }

}