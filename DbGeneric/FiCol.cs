using OrakYazilimLib;

namespace OrakYazilimLib.DbGeneric
{

    public class FiCol
    {
        public string txFieldName { get; set; }
        public string txHeaderName { get; set; }

        /**
         * Alanın varsayılan veri türü
         * 
         */
        public FiColType fiColType { get; set; }

        public FiCol(string fieldName)
        {
            this.txFieldName = fieldName;
        }

        public FiCol(string txFieldName,string txHeaderName)
        {
            this.txFieldName = txFieldName;
            this.txHeaderName = txHeaderName;
        }

        public string getFieldName()
        {
            return this.txFieldName;
        }

        public FiCol()
        {
        }

        public override string ToString()
        {
            if(this.txFieldName == null) { return ""; }
            return this.txFieldName;
        }
    }

}