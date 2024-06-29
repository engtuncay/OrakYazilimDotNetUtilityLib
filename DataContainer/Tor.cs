using System;
using System.Dynamic;
using System.Web.UI.WebControls;
using OrakYazilimLib.Util;

namespace OrakYazilimLib.DataContainer
{
    public class Tor<T>
    {

        public bool? blResult { get; set; }
        public T obReturn { get; set; }
        public string txErrorMsgShort { get; set; }
        public string txErrorMsgDetail { get; set; }
        public int lnRowsAffected { get; set; }
        public int? lnIdAffected { get; set; }
        public int? lnTotalLength { get; set; }
        public object spec1 { get; set; }

        public string txId { get; set; }

        public string txMessage { get; set; }

        public void SetObject<TS>(TS value)
        {
            this.spec1 = value;
        }

        public TS GetObject<TS>()
        {
            return (TS)this.spec1;
        }

        public Tor(bool prmBlResult) { this.blResult = prmBlResult; }

        public Tor(int prmLnRowsAffected)
        {
            this.lnRowsAffected = prmLnRowsAffected;

            if (prmLnRowsAffected > 0)
            {
                this.blResult = true;
            }
            else
            {
                this.blResult = false;
            }

        }

        public Tor() { }

        public static Tor<T> FactoryScopeId(int prmIdAffected)
        {
            var fiReturn = new Tor<T>();
            fiReturn.lnIdAffected = prmIdAffected;
            fiReturn.lnRowsAffected = prmIdAffected;

            if (prmIdAffected > 0)
            {
                fiReturn.blResult = true;
            }
            else
            {
                fiReturn.blResult = false;
            }

            return fiReturn;
        }

        public static Tor<T> FactoryObject(T returnObject)
        {
            var fiReturn = new Tor<T>();
            fiReturn.obReturn = returnObject;
            return fiReturn;
        }

        public void ExceptionQueryErrorLog(Exception exception)
        {
            this.txErrorMsgShort = exception.Message;
            this.txErrorMsgDetail = FiLogWeb.getStackTrace(exception);
            this.lnRowsAffected = -1;
            this.blResult = false;
        }

        public bool isTrueResult()
        {
            if (this.blResult == null) return false;
            return blResult.Value;
        }
    }
}