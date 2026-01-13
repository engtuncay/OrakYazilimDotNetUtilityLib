using System.Collections.Generic;

namespace OrakYazilimLib.DataContainer
{
  public static class DtoFdr1
  {
    public static object GenFdr1<TP>(Fdr<TP> fdr)
    {
      dynamic obj = new System.Dynamic.ExpandoObject();

      //if (fdr.boResult != null)
      //if (fdr.refValue != null)
      obj.boResult = fdr.boResult;
      obj.refValue = fdr.refValue;

      if (fdr.boExecution != null) obj.boExecution = fdr.boExecution;
      if (fdr.fqpLnTotal != null) obj.fqpLnTotal = fdr.fqpLnTotal;
      if (fdr.txMessage != null) obj.txMessage = fdr.txMessage;
      if (fdr.txErrorMsgShort != null) obj.txErrorMsgShort = fdr.txErrorMsgShort;
      if (fdr.txResponse != null) obj.txResponse = fdr.txResponse;
      if (fdr.GetListFieLogInit().Count > 0) obj.listFieLog = fdr.GetListFieLogInit();
      if (fdr.refDtbVal != null) obj.refDtbVal = fdr.refDtbVal;

      // geçici olarak eklendi
      if (obj.refValue == null && fdr.obReturn != null) obj.refValue = fdr.obReturn;

      // var expando = (IDictionary<string, object>)obj;
      // if (!expando.ContainsKey("boResult") || expando["boResult"] == null) expando["boResult"] = null;
      // if (!expando.ContainsKey("refValue") || expando["refValue"] == null) expando["refValue"] = null;

      return obj;
    }


  } // end class
}