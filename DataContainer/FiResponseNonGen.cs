using System;
using System.Dynamic;
using System.Web.UI.WebControls;
using OrakYazilimLib.Util;

namespace OrakYazilimLib.DataContainer
{
    public class Fdr : Fdr<object>
    {
        public Fdr()
        {
            
        }

        public Fdr(bool v)
        {
            base.blResult = v;
        }
    }

}