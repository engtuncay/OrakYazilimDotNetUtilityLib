using OrakYazilimLib.Util.core;
using System.Collections.Generic;

namespace OrakYazilimLib.Util.config
{
    public interface IFiConfigManager
    {
        //Dictionary<string, string?> mapConnString { get; set; }

        string GetConnString(string profile);
        string GetApiUrl(string txProfile);
    }
}