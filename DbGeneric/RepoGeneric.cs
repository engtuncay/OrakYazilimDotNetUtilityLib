using OrakYazilimLib.DbUtil;

namespace OrakYazilimLib.DbGeneric
{
    public class RepoGeneric<T>
    {
        public string connProfile { get; set; }

        public FiDbhms GetDbHelper()
        {
            return FiDbhms.Factory(connProfile);
        }

        public void CheckAndSetConnProfile()
        {
            // TODO metod yaz
        }

    }
}