using OrakYazilimLib.DbGeneric;
using OrakYazilimLib.Util;
using OrakYazilimLib.Util.core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OrakYazilimLib.DbUtil
{
    public class FiQuery
    {
        public string sql { get; set; }

        private FiKeyBean fkbParams { get; set; }

        public FiQuery(string sql)
        {
            this.sql = sql;
        }

        public FiQuery(string sql, FiKeyBean fkbParams)
        {
            this.sql = sql;
            this.fkbParams = new FiKeyBean(fkbParams);
        }

        public static void addMultiParam(List<object> list, string prmName, List<SqlParameter> listSqlParam)
        {
            int index = 0;
            foreach (object eleman in list)
            {
                listSqlParam.Add(new SqlParameter("@" + prmName + index, eleman));
                index++;
            }

        }

        public void addMultiParam(List<object> list, string paramName)
        {
            int index = 0;
            foreach (object eleman in list)
            {
                string paramTemplate = paramName + index;
                fkbParams.Add(paramTemplate, eleman);
                index++;
            }

        }

        public List<SqlParameter> genListSqlParameter()
        {
            if (fkbParams == null) return new List<SqlParameter>();

            List<SqlParameter> list = new List<SqlParameter>();

            foreach (var sqlParam in fkbParams)
            {
                string sqlParamName = "@" + sqlParam.Key;
                //if (!Regex.IsMatch(sqlParam, "^@.*"))
                //{
                //sqlParamName = "@" + sqlParamName;
                //}
                list.Add(new SqlParameter(sqlParamName, sqlParam.Value));
            }

            return list;
        }

        /**
 * Collection (List,Set) Türündeki parametreleri multi param (abc_1,abc_2... gibi) çevirir
 */
        public void convertListParamToMultiParams()
        {
            if (fkbParams == null) return;

            //FiLogWeb.logWeb("fkbParams null degil");
            this.sql = FiQueryTools.convertListParamToMultiParams(sql, fkbParams, false);
            //FiLogWeb.logWeb("sql:" + sql);
        }
        
        public void activateParamsNotNull() {

            if ( fkbParams != null)
            {

                List<string> listParamsWillDeactivate = new List<string>();

                foreach (var param in fkbParams)
                {
                    // Null olanlar deaktif olacak
                    if (param.Value != null) { // null degilse aktif edilir.
                        this.sql = FiQueryTools.activateOptParamMain(sql, param.Key);
                        //setTxQuery(newQuery);
                    } else { // param null ise,deaktif edilir
                        this.sql = FiQueryTools.deActivateOptParamMain(sql, param.Key);
                        listParamsWillDeactivate.Add(param.Key);
                    }
                }

                // deAktif edilen parametreler çıkarıldı.
                foreach (string deActivatedParam in listParamsWillDeactivate) {
                    fkbParams.Remove(deActivatedParam);
                }
            }
        }



        //public static string sqlPrmCevir(string prmName, int size)
        //{
        //    string fullprm = "";
        //    for (int index = 0; index < size; index++)
        //    {
        //        if (index > 0) fullprm += ",";
        //        fullprm += "@" + prmName + index;
        //    }
        //    return fullprm;
        //}

    }
}
