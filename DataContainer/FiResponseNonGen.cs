using System;
using System.Dynamic;
using System.Web.UI.WebControls;
using OrakYazilimLib.Util;

namespace OrakYazilimLib.DataContainer
{
    public class FiResponse : Fdr<object>
    {
        public FiResponse()
        {
            
        }

        public FiResponse(bool v)
        {
            base.blResult = v;
        }
    }

}