using System;
using System.Data;

namespace OrakYazilimLib.DataContainer
{
  public static class DtoFdr1
  {
    // public bool? boExecution { get; set; }
    // public bool? boResult { get; set; }
    // public Object refValue { get; set; }
    // public string txMessage { get; set; }
    //
    // public string txErrorMsgShort { get; set; }

    //public bool? boTknValid { get; set; }

    // public static DtoFdr1 ImportFdr1Old<TPrmA>(Fdr<TPrmA> fdr)
    // {
    //   DtoFdr1 dtoFdr1 = new DtoFdr1
    //   {
    //     boExecution = fdr.boExecution,
    //     boResult = fdr.boResult,
    //     refValue = fdr.refValue,
    //     txMessage = fdr.txMessage,
    //     txErrorMsgShort = fdr.txErrorMsgShort
    //   };
    //
    //   if (fdr.obReturn != null) dtoFdr1.refValue = fdr.obReturn;
    //
    //   return dtoFdr1;
    // }


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

      // geçici olarak eklendi
      if (fdr.refValue == null && fdr.obReturn != null) obj.refValue = fdr.obReturn;

      return obj;
    }
  }
}