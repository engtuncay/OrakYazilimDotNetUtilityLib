using System;
using OrakYazilimLib.DbUtil;
using OrakYazilimLib.Util.config;

namespace OrakYazilimLib.AdoNetHelper
{
    public class DbCreateHelper
    {
        public int CreateTableWithCsAndRefresh(string connStr, Type typeTable)
        {
            var database = new Database(connStr);

            int rowsaffected = database.RunQuery(new FiSqlServerQuery().createQuery(typeTable, (int)EmTblCreateTypes.Refresh));

            return rowsaffected;
        }

        public int CreateTableWithRefresh(String csConfigKey, Type typeTable)
        {
            return CreateTableWithCsAndRefresh(FiAppConfig.GetConnectionString(csConfigKey), typeTable);
        }
    }
}