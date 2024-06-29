using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrakYazilimLib.Util.core
{
    public class FiCalender
    {
        private DateTime bufferDate { get; set; }

        public FiCalender BuildFirstDay()
        {
            int year = DateTime.Now.Year;
            DateTime firstDay = new DateTime(year , 1, 1);
            this.bufferDate = firstDay;
            return this;
        }

        public FiCalender BuildLastDay()
        {
            int year = DateTime.Now.Year;
            DateTime lastDay = new DateTime(year , 12, 31);
            this.bufferDate = lastDay;
            return this;
        }

        public DateTime GetDate()
        {
            return this.bufferDate;
        }

        public static FiCalender Build()
        {
            return new FiCalender();
        }



    }
}
