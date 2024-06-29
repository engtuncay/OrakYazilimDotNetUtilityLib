using OrakYazilimLib.DbUtil;

namespace OrakYazilimLib.DbGeneric
{
    public class RepoCommon<T>
    {
        public string connProfile { get; set; }
        public string connString{ get; set; }

        //public FiConnConfig fiConnConfig { get; set; }

        public FiDbhms getDbHelper()
        {
            return FiDbhms.FactoryWitCs(connString);
        }

        public void checkAndSetConnProfile()
        {
            // TODO metod yaz
        }

    }
}