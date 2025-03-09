using System;
using System.Data;

namespace OrakYazilimLib.DataContainer
{
    public class DtoFdr1
    {
        public bool? boResult { get; set; }
        public bool? boOpResult { get; set; }
        public Object refValue { get; set; }

        public string txMessage { get; set; }

        //public bool? boTknCheck { get; set; }

        public static DtoFdr1 ImportFdr<TPrmA>(Fdr<TPrmA> fdr)
        {
            DtoFdr1 dtoFdr1 = new DtoFdr1
            {
                boResult = fdr.boResult,
                boOpResult = fdr.boOpResult,
                refValue = fdr.refValue,
                txMessage = fdr.txMessage,
            };

            if(fdr.obReturn!=null) dtoFdr1.refValue = fdr.obReturn;

            return dtoFdr1;
        }


    }
}