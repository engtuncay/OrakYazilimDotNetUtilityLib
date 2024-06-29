using System.Dynamic;
using System.Web.UI.WebControls;

namespace OrakYazilimLib.DataContainer
{
    public class FiReturn2
    {

        public bool result { get; set; }
        public string errorMsgShort { get; set; }
        public string errorMsgDetail { get; set; }
        public int? rowsAffected { get; set; }
        public object respObject { get; set; }
        public int? idAffected { get; set; }

        public void SetObject<TS>(TS value)
        {
            this.respObject = value;
        }

        public TS GetObject<TS>()
        {
            return (TS)this.respObject;
        }

        public FiReturn2(bool prmResult) { this.result = prmResult; }

        public FiReturn2(int prmRowsAffected)
        {
            this.rowsAffected = prmRowsAffected;

            if (prmRowsAffected > 0)
            {
                this.result = true;
            }
            else
            {
                this.result = false;
            }

        }

        public FiReturn2() { }

        public static FiReturn2 FactoryScopeId(int prmIdAffected)
        {
            var fiReturn = new FiReturn2();
            fiReturn.idAffected = prmIdAffected;
            fiReturn.rowsAffected = prmIdAffected;

            if (prmIdAffected > 0)
            {
                fiReturn.result = true;
            }
            else
            {
                fiReturn.result = false;
            }

            return fiReturn;
        }
    }
}