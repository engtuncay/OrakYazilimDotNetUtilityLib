using System;
using System.Data;
using System.Text;

namespace OrakYazilimLib.FiExtensions
{
  public static class FiDataTableExt
  {
    // DataTable'ı CSV formatına çeviren extension metot
    public static string ToCsv(this DataTable dataTable, char delimiter = ',')
    {
      if (dataTable == null)
        throw new ArgumentNullException(nameof(dataTable));

      StringBuilder csvBuilder = new StringBuilder();

      // Sütun başlıklarını yazma
      for (int i = 0; i < dataTable.Columns.Count; i++)
      {
        csvBuilder.Append(dataTable.Columns[i].ColumnName);
        if (i < dataTable.Columns.Count - 1)
          csvBuilder.Append(delimiter);
      }
      csvBuilder.AppendLine();

      // Satır verilerini yazma
      foreach (DataRow row in dataTable.Rows)
      {
        for (int i = 0; i < dataTable.Columns.Count; i++)
        {
          csvBuilder.Append(row[i]?.ToString());
          if (i < dataTable.Columns.Count - 1)
            csvBuilder.Append(delimiter);
        }
        csvBuilder.AppendLine();
      }

      return csvBuilder.ToString();
    }
    public static object GetAsObject(this DataTable dataTable, int lnRowNo, string ofcTxFieldName)
    {
      return dataTable.Rows[lnRowNo][ofcTxFieldName];
    }
  }

}