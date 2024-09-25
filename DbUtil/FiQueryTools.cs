using Google.Protobuf.WellKnownTypes;
using Newtonsoft.Json.Linq;
using OrakYazilimLib.DbGeneric;
using OrakYazilimLib.Util;
using OrakYazilimLib.Util.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections;

namespace OrakYazilimLib.DbUtil
{
    public class FiQueryTools
    {
        public FiQueryTools() { }

        public static string deActivateSqlParam(string fieldName, string sql)
        {
            if (!FiString.IsEmpty(fieldName))
            {
                sql = Regex.Replace(sql, @"\n\s*.*@(" + fieldName + ").*", "\n-- $1 deactivated");
            }
            return sql;
        }

        public static string fixSqlProblems(string sql)
        {
            // yorum satırların @ varsa # diyeze çevirir.
            string sql2 = Regex.Replace(sql, @"--(.*?)@(\w+)(.*)", "--fixed$1$2"); // 23-12-22
            return sql2;
        }

        /**
	 * Collection (List,Set) değerindeki parametreyi abc_1,abc_2 gibi multi parametreye çevirir
	 *
	 * @param param
	 * @param collParamData
	 * @param boKeepOldParam
	 */
        public static string convertSingleParamToMultiParam(string txQuery, FiKeybean mapParams, String param, IList listParamData, bool boKeepOldParam)
        {

            // (1) şablona göre yeni eklenecek parametre listesi
            // FiKeybean paramsNew = new FiKeybean();
            StringBuilder sbNewParamsForQuery = new StringBuilder();

            int index = 0;
            foreach (var paramVal in listParamData) //for (Object listDatum : collParamData)
            {
                string sablonParam = makeMultiParamTemplate(param, index);
                if (index != 0) sbNewParamsForQuery.Append(",");
                sbNewParamsForQuery.Append("@" + sablonParam);
                //paramsNew.Add(sablonParam, paramVal);
                mapParams.Add(sablonParam, paramVal);
                index++;
            }

            // end-1

            // Sorgu cümlesi güncellenir (eski parametre çıkarılır , yeni multi parametreler eklenir.)
            string sqlNew = Regex.Replace(txQuery, "@" + param, sbNewParamsForQuery.ToString()); //(%s)

            // map paramden eski parametre çıkarılıp, yenileri eklenir
            if (!FiBoolean.IsTrue(boKeepOldParam))
            {
                mapParams.Remove(param);
            }

            //mapParams.Concat(paramsNew);

            return sqlNew;
        }

        public static string makeMultiParamTemplate(string param, int index)
        {
            return param + "_" + index.ToString();
        }

        public static string convertListParamToMultiParams(string txQuery, FiKeybean mapParams, bool boKeepOldMultiParamInFkb)
        {

            if (mapParams == null) return txQuery;

            // (1) List türündeki parametreler bulunur.
            List<string> listMultiParamsName = new List<string>();

            foreach (var sqlParam in mapParams)
            {
                // concurrent modification olmaması amacıyla convert işlemi ayrı yapılacak (aşağıda)
                if (sqlParam.Value is List<string> || sqlParam.Value is List<int>)
                {
                    listMultiParamsName.Add(sqlParam.Key);
                    //FiLogWeb.logWeb("list multi parama eklendi:" + sqlParam.Key);
                }

                //if (value instanceof Set) {
                //    listMultiParamsName.add(param);
                //}
            }

            // --end-1

            // List-Set türünde olan parametreleri , multi tekli parametrelere çevirir. (abc_1,abc_2 gibi)
            string spQuery = txQuery;

            foreach (var param in listMultiParamsName)
            {
                bool boFound = mapParams.TryGetValue(param, out object listParam);
                //FiLogWeb.logWeb("boFound:" + boFound + " : param : " + param);
                spQuery = convertSingleParamToMultiParam(spQuery, mapParams, param, listParam as IList, boKeepOldMultiParamInFkb);

            }

            return spQuery;
        }

        /**
         * 
         */
        public static string deActivateOptParamMain(string sql, string paramKey)
        {
            var regex = $@"--!({paramKey}).*\s*.*"; // 15-10-19
            //Console.WriteLine("deact regex:"+regex);
            var subst = "--$1 deactivated"; // 15-10-19
            return Regex.Replace(sql, regex, subst); //sql.replaceAll(regex, subst);            
            
        }


        public static string activateOptParamMain(string sql, string paramKey)
        {
            // 200317_1741 sql param altındaki ifade yorum satırı olursa, yorum satırını kaldırır.
            var regex = $@"--!({paramKey})\b.*\s*-*(.*)";
            //Console.WriteLine("act regex:"+regex);
            var subst = "--$1 activated \n$2"; // 17-03-2020
            return Regex.Replace(sql,  regex, subst); // sql.replaceAll(regex, subst);
        }
    }
}
