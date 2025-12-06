using OrakYazilimLib.DbUtil;
using OrakYazilimLib.Util.config;

namespace OrakYazilimLib.DbGeneric
{
    public class AbsRepoCommon // <T> kaldırıldı
    {
        protected string connProfile { get; set; }

        //public string connString{ get; set; }

        protected FiMssqlu GetDbHelper()
        {
            return FiMssqlu.BuiWitProfile(connProfile);
        }

        // public void checkAndSetConnProfile()
        // {
        //     //
        // }

    }
}