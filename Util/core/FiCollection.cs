using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrakYazilimLib.Util.core
{
    public class FiCollection
    {
        
        public static List<T> BuildList<T>(params T[] items)
        {
            return new List<T>(items);
        }

        public static bool IsFull<T>(T[] prms)
        {
            if (prms == null) return false;

            if (prms.Length > 0) return true;

            return false;
        }
        public static bool IsEmpty(ICollection collection)
        {
            if (collection == null) return true; // Eğer null ise boş kabul edilir
            return !collection.Cast<object>().Any();
        }
    }
}
