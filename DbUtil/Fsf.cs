using System;
using System.Text.RegularExpressions;
using OrakYazilimLib.Util;
using OrakYazilimLib.Util.core;

namespace OrakYazilimLib.DbGeneric
{
    /**
     * FiSql String Formatter
     * 
     * Use FiQueryTools
     */
    public class Fsf
    {
        private string sql;

        public string get()
        {
            return sql;
        }

        private Fsf()
        {
        }

        public Fsf(string sql)
        {
            this.sql = sql;
        }

        public static Fsf build(string sql)
        {
            return new Fsf(sql);
        }

        public Fsf buildDeActivateSqlVar(string fieldName)
        {
            if (!FiString.IsEmpty(fieldName))
            {
                this.sql = Regex.Replace(this.sql, @"\n\s*.*@(" + fieldName + ").*", "\n-- $1 deactivated");
            }

            return this;
        }

        public Fsf buildDeActivateTag(string tagValue)
        {
            if (!FiString.IsEmpty(tagValue))
            {
                this.sql = Regex.Replace(this.sql, @"\n\s*.*(" + tagValue + ").*", "\n-- $1 deactivated");
            }

            return this;
        }
    }
}