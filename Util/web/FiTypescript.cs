using OrakYazilimLib.DbUtil;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace OrakYazilimLib.UtilWeb
{

    public class FiTypescript
    {

        public string generateInterface(Type typeClazz)
        {
            var propsEntity = typeClazz.GetProperties(); //typeStudent.GetProperties();

            StringBuilder stbQuery = new StringBuilder();

            string tableName = getTableName(typeClazz);

            stbQuery.Append($"\n\n export interface I{tableName} {{ ");

            foreach (var field in propsEntity)
            {
                //Console.WriteLine($"Prop Name :{field.Name} Type {field.PropertyType}");

                FiMetaField fiMetaField = getTypescriptFieldDefinition(field);

                if (fiMetaField.isIgnore == false && fiMetaField.isTypeNotDefined == false)
                {
                    stbQuery.Append($"\n  {field.Name} : {fiMetaField.definition}; "+ (fiMetaField.isTransient?"// transient":""));
                }

                if (fiMetaField.isTypeNotDefined)
                {
                    stbQuery.Append("\n //- (no type) " + fiMetaField.definition);
                }

            }

            stbQuery.Append("\n}\n");

            return stbQuery.ToString();

        }


        public string generateFields(Type typeClazz)
        {
            var propsEntity = typeClazz.GetProperties(); //typeStudent.GetProperties();

            StringBuilder stbQuery = new StringBuilder();

            string tableName = getTableName(typeClazz);

            stbQuery.Append("\n\n // " + tableName);

            foreach (var field in propsEntity)
            {
                //Console.WriteLine($"Prop Name :{field.Name} Type {field.PropertyType}");

                FiMetaField fiMetaField = getTypescriptFieldDefinition(field);

                if (fiMetaField.isIgnore == false && fiMetaField.isTypeNotDefined == false)
                {
                    stbQuery.Append($"\n  static {field.Name} = {{ field:'{field.Name}', header:'' }} as OzField;" + (fiMetaField.isTransient ? " // transient" : ""));
                }

                if (fiMetaField.isTypeNotDefined)
                {
                    stbQuery.Append("\n  //- (no type) " + fiMetaField.definition);
                }



            }

            //stbQuery.Append("\n}\n");

            return stbQuery.ToString();

        }


        private string getTableName(Type typeClazz)
        {
            return typeClazz.Name;
            //throw new NotImplementedException();
        }

        private FiMetaField getTypescriptFieldDefinition(PropertyInfo field)
        {
            FiMetaField fiMetaField = new FiMetaField();

            FiColumnAttribute fiColumnAttribute = null;
            FiIdAttribute fiIdAttribute = null;
            FiTransientAttribute fiTransient = null;

            // Attribute değişkenleri set edilir
            foreach (var attribute in field.GetCustomAttributes())
            {
                //Console.WriteLine("Attribute Name : {0}", attribute.GetType().Name);

                if (attribute.GetType() == typeof(FiTransientAttribute))
                {
                    //return new FiFieldDefinition(true);
                    fiTransient = attribute as FiTransientAttribute;
                }

                if (attribute.GetType() == typeof(FiColumnAttribute))
                {
                    fiColumnAttribute = attribute as FiColumnAttribute;
                }

                if (attribute.GetType() == typeof(FiIdAttribute))
                {
                    fiIdAttribute = attribute as FiIdAttribute;
                }

            }
            // ---- end

            String typeField = null;
            String typeDesc = "";

            //int? precision = null;
            //int? scale = null;
            //int? length = null;
            //bool unique = false;
            //bool isNotNullable = false; // def:false (in interface)

            //string colDefinitionExtra = "";
            //string colDefinition = "";

            //if (fiColumnAttribute != null)
            //{
            //    precision = fiColumnAttribute.precision;
            //    scale = fiColumnAttribute.scale;
            //    length = fiColumnAttribute.length; // default 255
            //    unique = fiColumnAttribute.isUnique;
            //    isNotNullable = fiColumnAttribute.isNotNullable;
            //    colDefinition = fiColumnAttribute.columnDefinition;
            //    colDefinitionExtra = fiColumnAttribute.colDefinitionExtra;

            //}

            // FiColumn attribute na eklendi
            //TemporalType temporalType = null;
            //if(field.isAnnotationPresent(Temporal.class)){
            //      temporalType = field.getAnnotation(Temporal.class).value();
            //}

            //if (length == null) length = 0;
            //if (precision == null) precision = 0;
            //if (scale == null) scale = 0;

            typeField = getFieldType(field.PropertyType);

            if (typeField == null)
            {
                String typeNullDesc = "\n//-- " + field.Name + " " + field.PropertyType;
                //+ ", " + (precision != 0 ? " Pre.:" + precision : "")
                //+ (scale != 0 ? "Scale :" + scale : "") + (length != 0 ? " Length:" + length : "");

                return new FiMetaField(true, typeNullDesc);
            }

            if (fiTransient != null)
            {
                fiMetaField.isTransient = true;
            }

            //if (colDefinition != null && !colDefinition.Equals(""))
            //{
            //    typeField = colDefinition;
            //    typeDesc = "";
            //}

            fiMetaField.definition = typeField + typeDesc;

            return fiMetaField;
            //return "string";
        }

        private string getFieldType(Type fieldPropertyType)
        {

            string fieldTypeName = fieldPropertyType.Name;

            //Console.WriteLine($" Field Type: {fieldTypeName}");

            if (fieldTypeName.Equals("Nullable`1"))
            {
                Type genericType = fieldPropertyType.GetGenericArguments()[0];
                fieldTypeName = genericType.Name;
            }
            
            if (getMapTypeConvertorDotNetToTypescript().ContainsKey(fieldTypeName))
            {
                String fieldType = getMapTypeConvertorDotNetToTypescript()[fieldTypeName];
                return fieldType;
            }

            return null;

        }

        public Dictionary<String, String> mapTypeConvertorDotNetToTypescript { get; set; }

        private Dictionary<String, String> getMapTypeConvertorDotNetToTypescript()
        {

            if (mapTypeConvertorDotNetToTypescript == null)
            {

                mapTypeConvertorDotNetToTypescript = new Dictionary<string, string>();

                // Integer
                mapTypeConvertorDotNetToTypescript.Add("Integer", "number");
                mapTypeConvertorDotNetToTypescript.Add("Long", "number");
                mapTypeConvertorDotNetToTypescript.Add("Int16", "number");
                mapTypeConvertorDotNetToTypescript.Add("Int32", "number");
                mapTypeConvertorDotNetToTypescript.Add("Short", "number");

                // Floating Point
                mapTypeConvertorDotNetToTypescript.Add("Float", "number");
                mapTypeConvertorDotNetToTypescript.Add("Double", "number");
                mapTypeConvertorDotNetToTypescript.Add("BigDecimal", "number");

                // String
                mapTypeConvertorDotNetToTypescript.Add("String", "string");
                //mapTypeConvertorDotNetToSqlServer.put("nvarchar","String");

                // Date Time
                mapTypeConvertorDotNetToTypescript.Add("Date", "Date");
                mapTypeConvertorDotNetToTypescript.Add("DateTime", "Date");

                mapTypeConvertorDotNetToTypescript.Add("Boolean", "boolean");

                // Binary
                mapTypeConvertorDotNetToTypescript.Add("ByteImage", "image");

                //mapTypeConvertorDotNetToSqlServer.put("varbinary","Byte[]");
                //mapTypeConvertorDotNetToSqlServer.put("smallint","Integer"); // tinyint kullanılabilir

            }
            return mapTypeConvertorDotNetToTypescript;

        }

    }

}