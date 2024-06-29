namespace OrakYazilimLib.DbUtil
{
    public class FiMetaField
    {
        public string field { get; set; }
        public string fieldDefinition { get; set; }

        public string definition { get; set; }
        public bool isIgnore { get; set; }
        public bool isTypeNotDefined { get; set; }

        public bool isTransient{ get; set; }

        public bool isIDField{ get; set; }

        public bool existDefault{ get; set; }


        public FiMetaField(bool isIgnore)
        {
            this.isIgnore = isIgnore;
        }

        public FiMetaField()
        {
        }

        public FiMetaField(string definition)
        {
            this.definition = definition;
        }

        public FiMetaField(bool isTypeNotDefined, string typeNullDesc)
        {
            this.isTypeNotDefined = isTypeNotDefined;
            this.definition = typeNullDesc;
        }

       
    }
}