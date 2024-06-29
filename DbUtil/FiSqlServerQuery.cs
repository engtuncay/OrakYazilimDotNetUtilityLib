using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using OrakYazilimLib.DbGeneric;
using OrakYazilimLib.Util;
using OrakYazilimLib.Util.core;

namespace OrakYazilimLib.DbUtil
{

    public class FiSqlServerQuery
    {
        public string sql { get; set; }

        public List<FiSqlParameter> listParams;

        public List<FiSqlParameter> getListParams()
        {
            if (this.listParams == null)
            {
                this.listParams = new List<FiSqlParameter>();
            }

            return this.listParams;
        }

        public static void updateQuery(Type clazz)
        {


        }

        public static void addSqlMultiParam(List<object> list, string prmName, List<SqlParameter> listSqlParam)
        {
            //List<SqlParameter> listSqlPrm = new List<SqlParameter>();

            int index = 0;
            foreach (object eleman in list)
            {
                listSqlParam.Add(new SqlParameter("@" + prmName + index, eleman));
                index++;
            }

            //return listSqlPrm;
        }

        public static string sqlPrmCevir(string prmName, int size)
        {
            string fullprm = "";
            for (int index = 0; index < size; index++)
            {
                if (index > 0) fullprm += ",";
                fullprm += "@" + prmName + index;
            }

            return fullprm;
        }

        public string GetSqlBindings()
        {
            string sqlBindings = "";

            foreach (var fiSqlParameter in getListParams())
            {
                sqlBindings += " , " + fiSqlParameter.field + "=" + FiString.OrElseEmptyString(fiSqlParameter.value);
            }

            return sqlBindings;
        }

        public string createQuery(Type typeClazz, int typeUpdate)
        {
            var propsEntity = typeClazz.GetProperties(); //typeStudent.GetProperties();

            StringBuilder stbQuery = new StringBuilder();

            string tableName = getTableName(typeClazz);

            if (typeUpdate == (int)EmTblCreateTypes.IfNotThenCreate)
            {
                stbQuery.Append($"If not exists(select name from sysobjects where name = '{tableName}') \n");
            }

            // varsa siler, dikkat edilmesi lazım
            if (typeUpdate == (int)EmTblCreateTypes.Refresh)
            {
                //stbQuery.Append($"If exists(select name from sysobjects where name = '{tableName}') \nBEGIN \n DROP TABLE {tableName} \nEND \n");
            }

            stbQuery.Append("CREATE TABLE " + tableName + " ( ");

            foreach (var field in propsEntity)
            {
                //Console.WriteLine($"Prop Name :{field.Name} Type {field.PropertyType}");

                FiMetaField fiMetaField = getSqlColumnDefinition(field);

                if (fiMetaField.isIgnore == false && fiMetaField.isTypeNotDefined == false)
                {
                    stbQuery.Append("\n " + field.Name + " " + fiMetaField.definition + ", ");
                }

                if (fiMetaField.isTypeNotDefined)
                {
                    stbQuery.Append("\n-- (no type) " + fiMetaField.definition);
                }
            }


            // son virgülü silmek için
            if (stbQuery.Length > 2)
            {
                stbQuery.Remove(stbQuery.Length - 2, 1);
            }

            stbQuery.Append(" ) \n");

            return stbQuery.ToString();
        }

        private string getTableName(Type typeClazz)
        {
            return typeClazz.Name;
            //throw new NotImplementedException();
        }

        private FiMetaField getSqlColumnDefinition(PropertyInfo propField)
        {
            FiMetaField fiMetaField = new FiMetaField();

            FiColumnAttribute fiColumnAttribute = null;
            FiIdAttribute fiIdAttribute = null;

            // Attribute değişkenleri set edilir
            foreach (var attribute in propField.GetCustomAttributes())
            {
                //Console.WriteLine("Attribute Name : {0}", attribute.GetType().Name);

                if (attribute.GetType() == typeof(FiTransientAttribute))
                {
                    fiMetaField.isTransient = true;
                    fiMetaField.isIgnore = true;
                    return fiMetaField;
                }

                if (attribute.GetType() == typeof(FiColumnAttribute))
                {
                    fiColumnAttribute = attribute as FiColumnAttribute;
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

            fiMetaField.field = propField.Name;

            String typeField = null;
            String typeDesc = "";
            int? precision = null;
            int? scale = null;
            int? length = null;
            bool unique = false;
            bool isNotNullable = false; // def:false (in interface)

            string colDefinitionExtra = "";
            string colDefinition = "";

            if (fiColumnAttribute != null)
            {
                precision = fiColumnAttribute.precision;
                scale = fiColumnAttribute.scale;
                length = fiColumnAttribute.length; // default 255
                unique = fiColumnAttribute.isUnique;
                isNotNullable = fiColumnAttribute.isNotNullable;
                colDefinition = fiColumnAttribute.columnDefinition;
                colDefinitionExtra = fiColumnAttribute.colDefinitionExtra;
            }

            // FiColumn attribute na eklendi
            //TemporalType temporalType = null;
            //if(field.isAnnotationPresent(Temporal.class)){
            //      temporalType = field.getAnnotation(Temporal.class).value();
            //}

            if (length == null) length = 0;
            if (precision == null) precision = 0;
            if (scale == null) scale = 0;

            typeField = getSqlFieldType(propField.PropertyType, precision, scale);

            if (typeField == null)
            {
                String typeNullDesc = "\n-- " + propField.Name + " " + propField.PropertyType + ", " +
                                      (precision != 0 ? " Pre.:" + precision : "")
                                      + (scale != 0 ? "Scale :" + scale : "") +
                                      (length != 0 ? " Length:" + length : "");

                return new FiMetaField(true, typeNullDesc);
            }

            if (typeField.Equals("int") || typeField.Equals("System.Int32"))
            {
                if (precision.Equals(1))
                {
                    typeField = "tinyint";
                }
                else
                {
                    typeField = "int";
                }
            }

            if (typeField.Equals("datetime") && fiColumnAttribute != null && fiColumnAttribute.dateType != null)
            {
                if (fiColumnAttribute.dateType == FiColumnAttribute.dateTypes.datetime)
                {
                    typeField = "date";
                }
            }

            if (typeField.Equals("nvarchar"))
            {
                if (length.Equals(255)) length = 50; // default 50 ye çekildi.
                if (length.Equals(0)) length = 50; // default 50 yapıldı.
                typeDesc = " (" + length + ")";
            }

            if (typeField.Equals("decimal"))
            {
                // default precision 18 ,scale 2
                if (precision.Equals(0)) precision = 18;
                if (scale.Equals(0)) scale = 2;
                typeDesc = " (" + precision + "," + scale + ")";
            }

            if (typeField.Equals("float"))
            {
                if (precision > 0 && scale > 0)
                {
                    // default precision 18 ,scale 2
                    typeField = "decimal";
                    typeDesc = " (" + precision + "," + scale + ") ";
                }
            }


            if (colDefinition != null && !colDefinition.Equals(""))
            {
                typeField = colDefinition;
                typeDesc = "";
            }

            bool idfield = false;
            // field configurations

            if (fiIdAttribute != null)
            {
                idfield = true;
                typeDesc += " IDENTITY(1,1) NOT NULL PRIMARY KEY "; //NOT NULL NOT NULL PRIMARY KEY
                fiMetaField.isIDField = true;
            }


            if (unique && !idfield) typeDesc += " UNIQUE";

            if (isNotNullable && fiIdAttribute == null) typeDesc += " NOT NULL";

            if (fiColumnAttribute != null && fiColumnAttribute.defaultValue != null &&
                !fiColumnAttribute.defaultValue.Equals(""))
            {
                if (typeField.Equals("nvarchar"))
                {
                    typeDesc += $" DEFAULT '{fiColumnAttribute.defaultValue}'";
                }

                if (typeField.Equals("decimal") || typeField.Equals("int") || typeField.Equals("tinyint") ||
                    typeField.Equals("date"))
                {
                    typeDesc += $" DEFAULT {fiColumnAttribute.defaultValue}";
                }
            }

            if (fiColumnAttribute != null && colDefinitionExtra != null && !colDefinitionExtra.Equals(""))
                typeDesc += " " + colDefinitionExtra;


            fiMetaField.definition = typeField + typeDesc;

            return fiMetaField;
            //return "string";
        }

        private string getSqlFieldType(Type fieldPropertyType, int? precision, int? scale)
        {

            //            foreach (Type interfaceType in type.GetInterfaces())
            //            {
            //                if (interfaceType.IsGenericType &&
            //                    interfaceType.GetGenericTypeDefinition()
            //                    == typeof(IList<>))
            //                {
            //                    Type itemType = type.GetGenericArguments()[0];
            //                    // do something...
            //                    break;
            //                }
            //            }

            string fieldTypeName = fieldPropertyType.Name;

            //Console.WriteLine($" Field Type: {fieldTypeName}");

            if (fieldTypeName.Equals("Nullable`1"))
            {
                Type genericType = fieldPropertyType.GetGenericArguments()[0];
                fieldTypeName = genericType.Name;
            }

            if (getMapTypeConvertorDotNetToSqlServer().ContainsKey(fieldTypeName))
            {
                String fieldType = getMapTypeConvertorDotNetToSqlServer()[fieldTypeName];

                if (fieldType.Equals("int"))
                {
                    if (precision > 20) fieldType = "bigint";
                }

                if (fieldType.Equals("double"))
                {
                    if (scale > 5) fieldType = "decimal";
                }

                return fieldType;
            }

            return null;
        }

        public Dictionary<String, String> mapTypeConvertorDotNetToSqlServer { get; set; }

        private Dictionary<String, String> getMapTypeConvertorDotNetToSqlServer()
        {
            if (mapTypeConvertorDotNetToSqlServer == null)
            {
                mapTypeConvertorDotNetToSqlServer = new Dictionary<string, string>();

                // Integer
                mapTypeConvertorDotNetToSqlServer.Add("Integer", "int");
                mapTypeConvertorDotNetToSqlServer.Add("Long", "int");
                mapTypeConvertorDotNetToSqlServer.Add("Int16", "tinyint");
                mapTypeConvertorDotNetToSqlServer.Add("Int32", "int");
                mapTypeConvertorDotNetToSqlServer.Add("Short", "tinyint");

                // Floating Point
                mapTypeConvertorDotNetToSqlServer.Add("Float", "float");
                mapTypeConvertorDotNetToSqlServer.Add("Double", "float");
                mapTypeConvertorDotNetToSqlServer.Add("BigDecimal", "decimal");

                // String
                mapTypeConvertorDotNetToSqlServer.Add("String", "nvarchar");
                //mapTypeConvertorDotNetToSqlServer.put("nvarchar","String");

                // Date Time
                mapTypeConvertorDotNetToSqlServer.Add("Date", "datetime");
                mapTypeConvertorDotNetToSqlServer.Add("DateTime", "datetime");

                // Binary
                mapTypeConvertorDotNetToSqlServer.Add("ByteImage", "image");

                //mapTypeConvertorDotNetToSqlServer.put("varbinary","Byte[]");
                //mapTypeConvertorDotNetToSqlServer.put("smallint","Integer"); // tinyint kullanılabilir
            }

            return mapTypeConvertorDotNetToSqlServer;
        }

        public void sqlDeActivate(string sqlVarName)
        {
            if (!FiString.IsEmpty(sqlVarName))
            {
                this.sql = Fsf.build(this.sql).buildDeActivateSqlVar(sqlVarName).get();
            }
        }

        public void sqlDeActiveIfEmpty(string condString, string sqlVarName)
        {
            if (FiString.IsEmpty(condString))
            {
                this.sql = Fsf.build(this.sql).buildDeActivateSqlVar(sqlVarName).get();
            }
        }

        public void sqlDeActiveIfEmptyElseBind(string sqlVarName, object value)
        {
            if (value is string)
            {
                if (FiString.IsEmpty((string)value))
                {
                    this.sql = Fsf.build(this.sql).buildDeActivateSqlVar(sqlVarName).get();
                }
                else
                {
                    BindParams(sqlVarName, value);
                }

            }
            else
            {
                if (FiType.IsEmpty(value))
                {
                    this.sql = Fsf.build(this.sql).buildDeActivateSqlVar(sqlVarName).get();
                }
                else
                {
                    BindParams(sqlVarName, value);
                }
            }

        }

        public void sqlDeActiveIfEmptyElseBindLike(string sqlVarName, object value)
        {
            if (value is string)
            {
                if (FiString.IsEmpty((string)value))
                {
                    buildDeActivateSqlVar(sqlVarName);
                }
                else
                {
                    BindParamsLikeString(sqlVarName, (string)value);
                }

            }
            else
            {
                if (FiType.IsEmpty(value))
                {
                    this.sql = Fsf.build(this.sql).buildDeActivateSqlVar(sqlVarName).get();
                }
                else
                {
                    BindParams(sqlVarName, value);
                }
            }

        }

        public void sqlDeActiveTagIfTrue(bool condition, string sqlVarName)
        {
            if (condition)
            {
                this.sql = Fsf.build(this.sql).buildDeActivateTag(sqlVarName).get();
            }

        }

        public void sqlDeActiveTagIfFalse(bool condition, string sqlVarName)
        {
            if (!condition)
            {
                this.sql = Fsf.build(this.sql).buildDeActivateTag(sqlVarName).get();
            }

        }

        public void BindParams(string key, object value)
        {

            getListParams().Add(new FiSqlParameter(key, value));
        }

        public void BindParamsLikeString(string key, string value)
        {
            value = "%" + value + "%";
            getListParams().Add(new FiSqlParameter(key, value));
        }

        public void BindParamsIfTrue(bool condBind, string key, object value)
        {
            if (condBind)
            {
                BindParams(key, value);
            }
        }

        //private Dictionary<String, String> getMapTypeConvertorSqlserverToJava()
        //{
        //    if (mapTypeConvertorSqlserverToJava == null)
        //    {

        //        mapTypeConvertorSqlserverToJava = new HashMap();

        //        mapTypeConvertorSqlserverToJava.put("int", "Integer");
        //        mapTypeConvertorSqlserverToJava.put("varchar", "String");
        //        mapTypeConvertorSqlserverToJava.put("nvarchar", "String");
        //        mapTypeConvertorSqlserverToJava.put("tinyint", "Integer");
        //        mapTypeConvertorSqlserverToJava.put("decimal", "Double");
        //        mapTypeConvertorSqlserverToJava.put("image", "Byte");
        //        mapTypeConvertorSqlserverToJava.put("datetime", "Date");
        //        mapTypeConvertorSqlserverToJava.put("varbinary", "Byte[]");
        //        mapTypeConvertorSqlserverToJava.put("smallint", "Integer"); // tinyint kullanılabilir


        //    }
        //    return mapTypeConvertorSqlserverToJava;
        //}


        //public static void sqlPrmAdd(List<object> list, string prmName, List<SqlParameter> listSqlParam)
        //{

        //    //List<SqlParameter> listSqlPrm = new List<SqlParameter>();

        //    int index = 0;
        //    foreach (object eleman in list)
        //    {
        //        listSqlParam.Add(new SqlParameter("@" + prmName + index, eleman));
        //        index++;
        //    }

        //    //return listSqlPrm;

        //}

        public string InsertQuery(Type typeClazz)
        {
            var propsEntity = typeClazz.GetProperties(); //typeStudent.GetProperties();

            StringBuilder stbQuery = new StringBuilder();

            string tableName = getTableName(typeClazz);

            //            INSERT INTO table_name (column1, column2, column3, ...)
            //            VALUES (value1, value2, value3, ...);

            var listFields = FieldsList(typeClazz);

            stbQuery.Append("INSERT INTO " + tableName + " ( ");

            stbQuery.Append("\n");
            foreach (var fieldDefinition in listFields)
            {
                if (fieldDefinition.isIDField) continue;

                stbQuery.Append(fieldDefinition.field + ", ");

                if (fieldDefinition.isTypeNotDefined)
                {
                    stbQuery.Append("\n-- (no type) " + fieldDefinition.field);
                }
            }

            // son virgülü silmek için
            if (stbQuery.Length > 2)
            {
                stbQuery.Remove(stbQuery.Length - 2, 1);
            }

            stbQuery.Append(")\nVALUES (");

            stbQuery.Append("\n");
            foreach (var fieldDefinition in listFields)
            {
                if (fieldDefinition.isIDField) continue;

                stbQuery.Append($"@{fieldDefinition.field}, ");
            }

            // son virgülü silmek için
            if (stbQuery.Length > 2)
            {
                stbQuery.Remove(stbQuery.Length - 2, 1);
            }

            stbQuery.Append(")\n");

            return stbQuery.ToString();
        }

        public string InsertQueryWithDefault(Type typeClazz, List<FiSqlParameter> sqlParameters)
        {
            var propsEntity = typeClazz.GetProperties(); //typeStudent.GetProperties();

            StringBuilder stbQuery = new StringBuilder();

            string tableName = getTableName(typeClazz);

            //            INSERT INTO table_name (column1, column2, column3, ...)
            //            VALUES (value1, value2, value3, ...);

            var listFields = FieldsList(typeClazz);

            stbQuery.Append("INSERT INTO " + tableName + " ( ");

            stbQuery.Append("\n");
            foreach (var fieldDefinition in listFields)
            {
                if (fieldDefinition.isIDField) continue;

                stbQuery.Append(fieldDefinition.field + ", ");

                if (fieldDefinition.isTypeNotDefined)
                {
                    stbQuery.Append("\n-- (no type) " + fieldDefinition.field);
                }
            }

            // son virgülü silmek için
            if (stbQuery.Length > 2)
            {
                stbQuery.Remove(stbQuery.Length - 2, 1);
            }

            stbQuery.Append(")\nVALUES (");

            stbQuery.Append("\n");
            foreach (var fieldDefinition in sqlParameters)
            {
                //if (fieldDefinition.isIDField) continue;
                if (fieldDefinition.isDefault)
                {
                    stbQuery.Append($"DEFAULT, ");
                }
                else
                {
                    stbQuery.Append($"{fieldDefinition.field}, ");
                }

            }

            // son virgülü silmek için
            if (stbQuery.Length > 2)
            {
                stbQuery.Remove(stbQuery.Length - 2, 1);
            }

            stbQuery.Append(");\n");

            return stbQuery.ToString();
        }


        public string InsertQueryWithIdAndDefault(Type typeClazz, List<FiSqlParameter> sqlParameters)
        {
            var propsEntity = typeClazz.GetProperties(); //typeStudent.GetProperties();

            StringBuilder stbQuery = new StringBuilder();

            string tableName = getTableName(typeClazz);

            //            INSERT INTO table_name (column1, column2, column3, ...)
            //            VALUES (value1, value2, value3, ...);

            var listFields = FieldsList(typeClazz);

            stbQuery.Append("INSERT INTO " + tableName + " ( ");

            stbQuery.Append("\n");
            foreach (var fieldDefinition in listFields)
            {
                if (fieldDefinition.isIDField) continue;

                stbQuery.Append(fieldDefinition.field + ", ");

                if (fieldDefinition.isTypeNotDefined)
                {
                    stbQuery.Append("\n-- (no type) " + fieldDefinition.field);
                }
            }

            // son virgülü silmek için
            if (stbQuery.Length > 2)
            {
                stbQuery.Remove(stbQuery.Length - 2, 1);
            }

            stbQuery.Append(") \nVALUES (");  // output INSERTED.ID  (ID dönüş yapılan sütun)

            stbQuery.Append("\n");
            foreach (var fieldDefinition in sqlParameters)
            {
                //if (fieldDefinition.isIDField) continue;
                if (fieldDefinition.isDefault)
                {
                    stbQuery.Append($"DEFAULT, ");
                }
                else
                {
                    stbQuery.Append($"{fieldDefinition.field}, ");
                }

            }

            // son virgülü silmek için
            if (stbQuery.Length > 2)
            {
                stbQuery.Remove(stbQuery.Length - 2, 1);
            }

            stbQuery.Append(");\nSELECT SCOPE_IDENTITY();");

            return stbQuery.ToString();
        }

        private List<FiMetaField> FieldsList(Type typeClazz)
        {
            var propsEntity = typeClazz.GetProperties(); //typeStudent.GetProperties();

            var listFields = new List<FiMetaField>();

            foreach (var field in propsEntity)
            {
                //Console.WriteLine($"Prop Name :{field.Name} Type {field.PropertyType}");

                FiMetaField fiMetaField = getSqlColumnDefinition(field);

                if (fiMetaField.isTransient)
                {
                    continue;
                }

                listFields.Add(fiMetaField);

            }

            return listFields;
        }

        public void buildDeActivateSqlVar(string fieldName)
        {
            if (!FiString.IsEmpty(fieldName))
            {
                this.sql = Regex.Replace(this.sql, @"\n\s*.*@(" + fieldName + ").*", "\n-- $1 deactivated");
            }

            //return this;
        }

        public void buildDeActivateTag(string tagValue)
        {
            if (!FiString.IsEmpty(tagValue))
            {
                this.sql = Regex.Replace(this.sql, @"\n\s*.*(" + tagValue + ").*", "\n-- $1 deactivated");
            }

            //return this;
        }
        

    } //cls
}//ns