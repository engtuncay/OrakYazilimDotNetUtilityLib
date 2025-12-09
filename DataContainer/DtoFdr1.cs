namespace OrakYazilimLib.DataContainer
{
  public static class DtoFdr1
  {
    public static object ImportFdr1<TP>(Fdr<TP> fdr)
    {
      dynamic obj = new System.Dynamic.ExpandoObject();

      if (fdr.boExecution != null) obj.boExecution = fdr.boExecution;
      if (fdr.boResult != null) obj.boResult = fdr.boResult;
      if (fdr.fqpLnTotal != null) obj.fqpLnTotal = fdr.fqpLnTotal;
      if (fdr.txMessage != null) obj.txMessage = fdr.txMessage;
      if (fdr.txErrorMsgShort != null) obj.txErrorMsgShort = fdr.txErrorMsgShort;
      if (fdr.refValue != null) obj.refValue = fdr.refValue;
      if (fdr.txResponse != null) obj.txResponse = fdr.txResponse;
      if (fdr.GetListFieLogInit().Count > 0) obj.listFieLog = fdr.GetListFieLogInit();
      if (fdr.refDtbVal != null) obj.refDtbVal = fdr.refDtbVal;

      // geçici olarak eklendi
      if (fdr.refValue == null && fdr.obReturn != null) obj.refValue = fdr.obReturn;


      return obj;
    }


  } // end class
}