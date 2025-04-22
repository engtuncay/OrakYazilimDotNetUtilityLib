using OrakYazilimLib.AdoNetHelper;
using OrakYazilimLib.Util.core;
using System.Collections.Generic;

namespace OrakYazilimLib.Util.Collection
{
    public class FkbList: List<FiKeybean>
    {
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