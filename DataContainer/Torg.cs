using System;
using System.Dynamic;
using System.Web.UI.WebControls;
using OrakYazilimLib.Util;

namespace OrakYazilimLib.DataContainer
{
    public class Torg : Tor<object>
    {
        public Torg()
        {
            
        }

        public Torg(bool v)
        {
            base.blResult = v;
        }
    }

}