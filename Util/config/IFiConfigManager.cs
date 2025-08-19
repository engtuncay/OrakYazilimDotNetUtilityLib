using OrakYazilimLib.Util.core;
using System.Collections.Generic;

namespace OrakYazilimLib.Util.config
{
  /**
   * App tarafında implemente eden class'dan bilgiler alınır
   */
  public interface IFiConfigManager
  {
    //Dictionary<string, string?> mapConnString { get; set; }

    string GetConnString(string profile);
    string GetApiUrl(string txProfile);

    /**
    * Ayar dosyasından okunarak alınacak profile
    */
    string GetProfile();
  }
}