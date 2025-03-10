using OrakYazilimLib.DataContainer;
using OrakYazilimLib.Util.config;
using System;
using System.Data;

namespace OrakYazilimLib.DbUtil
{

  /// <summary>
  /// Fi Wrapper For DataTable
  /// </summary>
  public class FiwDataTable
  {
    public DataTable dataTable { get; set; }

    public FiwDataTable(DataTable dt)
    {
      dataTable = dt;
    }

    /// <summary>
    /// İlk satırdaki belirtilen sütun adının değerini döner.
    /// </summary>
    /// <param name="columnName">Sütun adı</param>
    /// <returns>Sütunun ilk satırdaki değeri</returns>
    public Fdr GetValueFromFirstRow(string columnName)
    {
      Fdr fdr = new Fdr();

      if (dataTable != null && dataTable.Rows.Count > 0)
      {
        if (dataTable.Columns.Contains(columnName))
        {
          fdr.boExecution = true;
          fdr.refValue = dataTable.Rows[0][columnName];
          return fdr;
        }

        fdr.boExecution = false;
        fdr.txMessage = "Belirtilen sütun adı DataTable'da mevcut değil.";

        return fdr;
      }

      fdr.boExecution = false;
      fdr.txMessage = "DataTable boş veya hiç satır içermiyor.";

      return fdr;
    }

    /// <summary>
    /// İlk satırdaki belirtilen sütun adının değerini belirtilen türe dönüştürerek döner.
    /// eğer null ise fdr'ye atama yapmaz fdr dönüşü yapar
    /// </summary>
    /// <typeparam name="T">Dönüştürülecek tür</typeparam>
    /// <param name="columnName">Sütun adı</param>
    /// <param name="defaultValue"></param>
    /// <returns>İlk satırdaki sütun verisinin belirtilen türde döndürülmüş hali</returns>
    public Fdr<T> GetValueFromFirstRow2<T>(string columnName, T defaultValue = default)
    {
      FiAppConfig.fiLogManager?.LogMessage("col : " + columnName);

      Fdr<T> fdrMain = new Fdr<T>();

      if (dataTable != null && dataTable.Rows.Count > 0)
      {
        if (dataTable.Columns.Contains(columnName))
        {
          object value = dataTable.Rows[0][columnName];

          FiAppConfig.fiLogManager?.LogMessage("value : " + value);

          // // Eğer değer null ise, varsayılan değeri döndür
          // if (value == DBNull.Value || value == null)
          // {
          //   //fdr.refValue = default;
          //   return fdrMain;
          // }
          // null ise default değeri ver
          if (value == DBNull.Value || value == null)
          {
            fdrMain.refValue = defaultValue;
            return fdrMain;
          }

          // Tür dönüşümünü yap ve döndür
          fdrMain.refValue = (T)Convert.ChangeType(value, typeof(T));
          return fdrMain;
        }

        fdrMain.boExecution = false;
        fdrMain.txMessage = "Belirtilen sütun adı DataTable'da mevcut değil.";
        return fdrMain;
      }

      fdrMain.boExecution = false;
      fdrMain.txMessage = "DataTable boş veya hiç satır içermiyor.";
      return fdrMain;
    }

  }
}