using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrakYazilimLib.Util.core
{
  public class FiCal
  {
    public static DateTime GetYearFirstDay()
    {
      int year = DateTime.Now.Year;
      DateTime firstDay = new DateTime(year, 1, 1);
      return firstDay;
    }

    public static DateTime GetYearLastDay()
    {
      int year = DateTime.Now.Year;
      DateTime lastDay = new DateTime(year, 12, 31);
      return lastDay;
    }

    public static int GetYear()
    {
        return DateTime.Now.Year;
    }



    // public DateTime GetDate()
    // {
    //     return this.bufferDate;
    // }

  }
}