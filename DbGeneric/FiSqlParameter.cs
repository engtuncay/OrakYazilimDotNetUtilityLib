using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace OrakYazilimLib.DbGeneric
{
    public class FiSqlParameter
    {
        public string field { get; set; }
        public object value { get; set; }
        public bool isDefault { get; set; }
        public string clField { get; set; }

        public FiSqlParameter(string prmField, object prmValue)
        {
            this.field = prmField;
            this.value = prmValue;
        }

        public FiSqlParameter()
        {
        }

        public static List<SqlParameter> convertSqlParameter(List<FiSqlParameter> listPrmSqlParameters)
        {
            if (listPrmSqlParameters == null) return new List<SqlParameter>();

            List<SqlParameter> list = new List<SqlParameter>();

            foreach (var sqlParameter in listPrmSqlParameters)
            {
                string sqlVariable = sqlParameter.field;
                if (!Regex.IsMatch(sqlVariable, "^@.*"))
                {
                    sqlVariable = "@" + sqlVariable;
                }

                list.Add(new SqlParameter(sqlParameter.field,sqlParameter.value));
            }

            return list;
        }
    }
}