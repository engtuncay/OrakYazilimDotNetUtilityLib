using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrakYazilimLib.Util
{
    public class FiString
    {
        public static void check()
        {
            //
            //
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

        public static string OrElseEmptyString(object p)
        {
            if (p == null) return "";

            return p.ToString();
        }

        public static bool isEmptyWithTrim(string kisaevrakadi)
        {
            if (kisaevrakadi == null) return true;
            return FiString.IsEmpty(kisaevrakadi.ToString().Trim());
        }
    }
}