using OrakYazilimLib.DbGeneric;
using System;
using System.Data;
using System.Text;

namespace OrakYazilimLib.FiExtensions
{
  public static class FiDataTableExt
  {
    // DataTable'ı CSV formatına çeviren extension metot
    public static string ToCsvFi(this DataTable dataTable, char delimiter = ',')
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

    public static object GetCellAsObjectFi(this DataTable dataTable, int lnRowNo, string txFieldName)
    {
      // 1. DataTable null kontrolü
      if (dataTable == null) return null;

      // 2. Satır numarasının geçerli olup olmadığının kontrolü
      if (lnRowNo < 0 || lnRowNo >= dataTable.Rows.Count) return null;

      // 3. Kolon adının geçerli olup olmadığının kontrolü
      if (!dataTable.Columns.Contains(txFieldName)) return null;

      // 4. Satırda ilgili değer null olabilir, bunu kontrol edelim (isteğe bağlı)
      object value = dataTable.Rows[lnRowNo][txFieldName];

      return value;
    }

    /**
     * GetFieldAsObject
     */
    public static object GetFldAsObject(this DataTable dataTable, int lnRowNo, FiCol fiCol)
    {
      return GetCellAsObjectFi(dataTable, lnRowNo, fiCol.ofcTxFieldName);
    }

    // DataTable'dan bir sütunu silen extension metot
    public static void RemoveColumnFi(this DataTable dataTable, string columnName)
    {
      // Kontroller: DataTable'ın ve sütunun geçerli olması
      if (dataTable == null)
      {
        //throw new ArgumentNullException(nameof(dataTable), "DataTable boş olamaz!");
        return;
      }


      if (string.IsNullOrWhiteSpace(columnName)) return;
      //throw new ArgumentException("Sütun adı boş veya geçersiz olamaz!", nameof(columnName));

      if (!dataTable.Columns.Contains(columnName)) return;
      //throw new ArgumentException($"'{columnName}' adlı sütun DataTable'da mevcut değil.", nameof(columnName));

      // Sütunu sil
      dataTable.Columns.Remove(columnName);
    }

    // DataTable'dan bir sütunu index ile silen extension metot
    // public static void RemoveColumnAtFi(this DataTable dataTable, int columnIndex)
    // {
    //   // Kontroller: DataTable'ın ve index'in geçerli olması
    //   if (dataTable == null)
    //     throw new ArgumentNullException(nameof(dataTable), "DataTable boş olamaz!");
    //
    //   if (columnIndex < 0 || columnIndex >= dataTable.Columns.Count)
    //     throw new IndexOutOfRangeException($"Geçersiz sütun index'i: {columnIndex}. Geçerli değerler 0-{dataTable.Columns.Count - 1} arasındadır.");
    //
    //   // Sütunu sil
    //   dataTable.Columns.RemoveAt(columnIndex);
    // }
  }
}