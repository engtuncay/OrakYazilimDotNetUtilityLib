using OrakYazilimLib.DbGeneric;
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

    public static object GetAsObject(this DataTable dataTable, int lnRowNo, string txFieldName)
    {
      // 1. DataTable null kontrolü
      if (dataTable == null)
        return null;
      //throw new ArgumentNullException(nameof(dataTable), "DataTable boş olamaz!");

      // 2. Satır numarasının geçerli olup olmadığının kontrolü
      if (lnRowNo < 0 || lnRowNo >= dataTable.Rows.Count)
        return null;
      //throw new IndexOutOfRangeException($"Geçersiz satır numarası: {lnRowNo}. DataTable'daki satır sayısı: {dataTable.Rows.Count}");

      // 3. Kolon adının geçerli olup olmadığının kontrolü
      if (!dataTable.Columns.Contains(txFieldName))
        return null;
      //throw new ArgumentException($"Geçerli bir kolon adı verilmelidir. '{txFieldName}' tablodaki bir kolon değil.");

      // 4. Satırda ilgili değer null olabilir, bunu kontrol edelim (isteğe bağlı)
      object value = dataTable.Rows[lnRowNo][txFieldName];

      // if (value == null)
      // return null; // null döner veya alternatif bir işlem yapılabilir

      return value;
    }

    /**
     * GetFieldAsObject
     */
    public static object GetFldAsObject(this DataTable dataTable, int lnRowNo, FiCol fiCol)
    {
        return GetAsObject(dataTable, lnRowNo, fiCol.ofcTxFieldName);
    }
  }

}