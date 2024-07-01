using OrakYazilimLib.DbUtil;
using OrakYazilimLib.Util.config;

namespace OrakYazilimLib.DbGeneric
{
    public class RepoCommonGen<T>
    {
        public string connProfile { get; set; }
        public string connString{ get; set; }

        //public FiConnConfig fiConnConfig { get; set; }

        public FiDbhms getDbHelper()
        {
            return FiDbhms.FactoryWitProfile(connProfile);
        }

        public void checkAndSetConnProfile()
        {
            // TODO metod yaz
        }

    }
}