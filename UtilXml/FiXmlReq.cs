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
      if (FiCollection.IsEmpty(this.fkbParams)) return txXml;

      this.txXml = FiXmlUtils.ConvertXmlParams(txXml, this.fkbParams);

      return txXml;
    }

    public void PrepFkbParams()
    {
      this.txXml = FiXmlUtils.PrepFkbParams(txXml, this.fkbParams);
    }
    public void DeactiveAllParams()
    {
      this.txXml = FiXmlUtils.DeActivateAllParams(this.txXml);
    }
    /**
     * Xml deki parametreleri değerlerle değiştirir.
     */
    public void ProcessParams()
    {
      if (FiCollection.IsEmpty(this.fkbParams)) return;
      this.txXml = FiXmlUtils.ConvertXmlParams(this.txXml, this.fkbParams);
    }

    public void ProcessParamsWitPrep()
    {
      if (FiCollection.IsEmpty(this.fkbParams)) return;
      this.txXml = FiXmlUtils.PrepFkbParams(this.txXml, this.fkbParams);
      this.txXml = FiXmlUtils.ConvertXmlParams(this.txXml, this.fkbParams);
    }
    public void DeactiveField(FiCol fiCol)
    {
      this.txXml = FiXmlUtils.DeActivateParam(this.txXml, fiCol.ofcTxFieldName);
    }

  }

}