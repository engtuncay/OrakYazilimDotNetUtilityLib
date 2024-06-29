using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrakYazilimLib.Util.core
{
    public class FiType
    {
        public static bool IsTrue(bool? isTransient)
        {
            if (isTransient == null) return false;
            return (bool) isTransient;
        }

        public static bool IsEmpty(string value)
        {
            if (value == null || value.Equals("")) return true;
            return false;
        }

        public static bool IsEmpty(object value)
        {
            if (value == null) return true;
            return false;
        }

        public static string orEmpty(object objValue)
        {
            if (objValue == null) return "";
            return objValue.ToString();
        }

        
    }
}
