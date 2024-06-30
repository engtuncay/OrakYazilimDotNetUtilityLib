using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Web;
using System.Linq;

namespace OrakYazilimLib.DbUtil
{
    public class FiMySqlQuery
    {

        public static void updateQuery(Type clazz)
        {

            //var attributes = typeof(Foo)
            //        .GetMethod("Window_Loaded")
            //        .GetCustomAttributes(typeof(AuthenticationRequired), true)
            //        .Cast<AuthenticationRequired>();

            //foreach (var attribute in attributes)
            //{
            //    Console.WriteLine(attribute.ToString());
            //}

            //Object theObject = Activator.CreateInstance(clazz);

            //k


            //          Field[] fields = clazz.getDeclaredFields(); // returns all members including private members but not inherited members.

            //          List<Field> fieldListFilterAnno = Arrays.asList(fields).stream().filter(field-> !(field.isAnnotationPresent(Transient.class) || field.isAnnotationPresent(Id.class)))
            //		.collect(Collectors.toList());

            //      String tableName = getTableName(clazz);

            //if (tableName == null) tableName = clazz.getSimpleName();

            ////System.out.println("Table Name:"+ tableName);

            //StringBuilder query = new StringBuilder();

            //      query.append("INSERT INTO " + tableName + " ( ");

            //fieldListFilterAnno.forEach(field -> query.append(field.getName() + ", "));

            //// son virgülü silmek için
            //query.delete(query.length() - 2, query.length());

            //query.append(" ) \n VALUES ( ");

            //fieldListFilterAnno.forEach(field -> query.append("@" + field.getName() + " , "));

            //// son virgülü silmek için
            //query.delete(query.length() - 2, query.length());

            //query.append(" )");

            //return query.toString();

        }

        public static void sqlPrmAdd(List<object> list, string prmName, List<SqlParameter> listSqlParam)
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

        public string createQuery(Type entity)
        {
            PropertyInfo[] props = entity.GetProperties();
            foreach (var item in props)
            {
                //lst_props.Items.Add(item.Name);

            }



            return "";
        }

    }
}