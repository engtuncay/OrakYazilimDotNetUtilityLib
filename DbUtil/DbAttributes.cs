using System;
using System.Collections;
using System.Collections.Generic;

namespace OrakYazilimLib.DbUtil
{
    public class FiIdAttribute : Attribute
    {

    }
    
    /**
     *  Precision : Total Length
     *  Scale : Decimal point length (ondalık kısmı)
     *
     */
    public class FiColumnAttribute : Attribute
    {
        public string columnDefinition { get; set; }
        public bool isNotNullable { get; set; }
        public bool isUnique { get; set;}
        public int length { get; set; }
        public string name { get; set; }
        public int precision { get; set; }
        public int scale { get; set; }
        public enum dateTypes{datetime,shortdate}
        public dateTypes? dateType { get; set; }
        public string colDefinitionExtra { get; set; }
        public string defaultValue { get; set; }

    }

    public class FiTransientAttribute : Attribute
    {

    }

    public class FiCKeyAttribute: Attribute
    {
        public string[] Values { get; set; }

        public FiCKeyAttribute(params string[] values)
        {
            this.Values = values;
        }
    }
}