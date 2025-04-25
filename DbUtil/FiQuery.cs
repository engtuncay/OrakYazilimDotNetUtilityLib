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

    public FiKeybean fkbParams { get; set; }

    public FiQuery(string sql)
    {
      this.sql = sql;
    }

    public FiQuery(string sql, FiKeybean fkbParams)
    {
      this.sql = sql;
      this.fkbParams = fkbParams; // new FiKeybean(fkbParams); // orjinali bozmamak istenirse kopya oluşturlabilir
    }

    // URFIX fkbparams ekleme yapmalı
    public static void AddMultiParam(List<object> list, string prmName, List<SqlParameter> listSqlParam)
    {
      int index = 0;

      foreach (object eleman in list)
      {
        listSqlParam.Add(new SqlParameter("@" + prmName + index, eleman));
        index++;
      }

    }

    public void AddMultiParam(List<object> list, string paramName)
    {
      int index = 0;
      foreach (object eleman in list)
      {
        string paramTemplate = paramName + index;
        fkbParams.Add(paramTemplate, eleman);
        index++;
      }

    }

    /// Converts the parameters stored in the FiKeybean object to a list of SQL parameters for use in SQL queries.
    /// Each entry in the FiKeybean is converted to a SqlParameter object with the key as the parameter name prefixed with "@".
    /// Returns an empty list if the FiKeybean is null.
    ///
    public List<SqlParameter> GetParamsAsSqlParamList()
    {
      if (fkbParams == null) return new List<SqlParameter>();

      List<SqlParameter> list = new List<SqlParameter>();

      foreach (var fkbItem in fkbParams)
      {
        string sqlParamName = "@" + fkbItem.Key;
        //if (!Regex.IsMatch(sqlParam, "^@.*"))
        //{
        //sqlParamName = "@" + sqlParamName;
        //}
        list.Add(new SqlParameter(sqlParamName, fkbItem.Value));
      }

      return list;
    }

    /**
    * Collection (List,Set) Türündeki parametreleri multi param (abc_1,abc_2... gibi) çevirir
    */
    public void ConvertListParamToMultiParams()
    {
      if (FiCollection.IsEmpty(fkbParams)) return;
      //FiLogWeb.logWeb("fkbParams null degil");
      sql = FiQueryUtils.ConvertListParamToMultiParams(sql, fkbParams, false);
      //FiLogWeb.logWeb("sql:" + sql);
    }

    public void ActivateParamsNotNull()
    {

      if (!FiCollection.IsEmpty(fkbParams))
      {
        this.sql = FiQueryUtils.ActivateParamsNotNull(sql, fkbParams);
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

    /**
     * boActivateOnlyFullParams parametresine false değeri gönderir.
     *
     * <para><see cref="FiQueryUtils.ActivateParamsMain"/></para>
     */
    public void ActivateParamsByMapParams()
    {
      ActivateParamsMain(false);
    }

    /**
     * ActivateParamsByMapParams(),DeActivateAllOptParams(); metodlarını çalıştırır
     * <para><see cref="FiQueryUtils.ActivateParamsMain"/></para>
     * <para><see cref="FiQueryUtils.DeActivateAllOptParams"/></para>
     */
    public void ProcessAllParamsByMapParams()
    {
      ActivateParamsMain(false);
      DeActivateAllOptParams();
    }

    /// <summary>
    /// FiMapParam'da olan parametreleri aktive eder.
    /// <para>
    /// <see cref="FiQueryUtils.ActivateParamsMain"/>
    /// </para>
    /// </summary>
    /// <param name="boActivateOnlyFullParams"></param>
    public void ActivateParamsMain(bool boActivateOnlyFullParams)
    {
      if (fkbParams != null)
      {
        this.sql = FiQueryUtils.ActivateParamsMain(sql, fkbParams, boActivateOnlyFullParams);
      }
    }

    /// <summary>
    /// <see cref="FiQueryUtils.DeActivateAllOptParams"/>
    /// </summary>
    public void DeActivateAllOptParams()
    {
      sql = FiQueryUtils.DeActivateAllOptParams(sql);
    }



  }
}