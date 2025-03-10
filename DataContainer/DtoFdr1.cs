using System;
using System.Data;

namespace OrakYazilimLib.DataContainer
{
    public class DtoFdr1
    {
        public bool? boExecution { get; set; }
        public bool? boResult { get; set; }
        public Object refValue { get; set; }
        public string txMessage { get; set; }

        //public bool? boTknValid { get; set; }

        public static DtoFdr1 ImportFdr<TPrmA>(Fdr<TPrmA> fdr)
        {
            DtoFdr1 dtoFdr1 = new DtoFdr1
            {
                boExecution = fdr.boExecution,
                boResult = fdr.boResult,
                refValue = fdr.refValue,
                txMessage = fdr.txMessage,
            };

            if(fdr.obReturn!=null) dtoFdr1.refValue = fdr.obReturn;

            return dtoFdr1;
        }


    }
}