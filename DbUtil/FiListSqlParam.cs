using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrakYazilimLib.DbUtil
{
    public class FiListSqlParam : List<SqlParameter>
    {
        public void addSqlParam(String value,Object objvalue)
        {
            Add(new SqlParameter(value, objvalue));
        }

    }
}
