using OrakYazilimLib.DbUtil;
using System;

namespace OrakYazilimLib.DbGeneric
{
  public abstract class AbsRepoCore //<T>
  {
    /**
     * connProfile shows server and sometimes db
     *
     * hp -> shows server
     *
     * hp-kraft -> shows server and db
     */
    public string connProfile { get; set; }

    // public AbsRepoGeneric()
    // {
    // }

    public AbsRepoCore(string connProfile)
    {
      this.connProfile = connProfile;
    }

    public FiMssqlu GetDbHelper()
    {
      //Console.WriteLine($"dbhelper: {connProfile}");
      return FiMssqlu.BuiWitProfile(connProfile);
    }

    public FiMssqlu GetDbHelperc()
    {
      //Console.WriteLine($"dbhelper: {connProfile}");
      return FiMssqlu.BuiWitProfile(connProfile);
    }

    public void CheckAndSetConnProfile()
    {
      // TODO metod yaz
    }

  }

  // public abstract class AbsRepoGeneric : AbsRepoGeneric<Object>
  // {
  //   protected AbsRepoGeneric(string connProfile) : base(connProfile)
  //   {
  //   }
  //
  // }

}