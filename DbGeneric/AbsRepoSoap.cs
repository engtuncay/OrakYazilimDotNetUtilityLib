using OrakYazilimLib.Util.config;

namespace OrakYazilimLib.DbGeneric
{
  public abstract class AbsRepoSoap
  {
    public string txBaseUrl {get;set;}

    public string txProfile {get;set;}

    protected AbsRepoSoap(string txProfile)
    {
      this.txProfile = txProfile;
      SetupBaseUrl();
    }
    private void SetupBaseUrl()
    {
      this.txBaseUrl = FiAppConfig.fiConfigManager?.GetApiUrl(txProfile);
    }


  }

}