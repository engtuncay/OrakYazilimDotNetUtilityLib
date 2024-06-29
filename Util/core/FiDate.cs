using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrakYazilimLib.Util.core
{
    public class FiDate
    {
        public static string dtNowWoTime()
        {
            return DateOnly.FromDateTime(DateTime.Now).ToString("yyyy-MM-dd");
        }
    }
}
