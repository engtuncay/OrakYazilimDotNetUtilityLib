using OrakYazilimLib.AdoNetHelper;
using OrakYazilimLib.Util.core;
using System.Collections.Generic;

namespace OrakYazilimLib.Util.Collection
{
    public class FkbList: List<FiKeybean>
    {
        public string txTemplate { get; set; }

        public string txValue { get; set; }

        public FicList ficList { get; set; }

        public FiKeycol fiKeycol { get; set; }

        public FkbList()
        {
        }

        public FkbList(int capacity) : base(capacity)
        {
        }
        
        public FkbList(IEnumerable<FiKeybean> collection) : base(collection)
        {
        }
        
        
    }
}