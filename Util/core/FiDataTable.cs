using System.Collections.Generic;
using System.Data;

namespace OrakYazilimLib.Util.core
{
    public class FiDataTable
    {
        public static List<string> convertListString(DataTable dataTable,string txFieldName)
        {
            List<string> list = new List<string>();
            
            foreach (DataRow dataTableRow in dataTable.Rows)
            {
                if (dataTableRow[txFieldName]==null)
                {
                    continue;
                }

                list.Add(dataTableRow[txFieldName].ToString());
            }
            
            // Linq ile yapılışı
            // (from DataRow dataTableRow in dataTable.Rows where dataTableRow[txFieldName] != null select dataTableRow[txFieldName].ToString()).ToList();

            return list;
        }
    }
}