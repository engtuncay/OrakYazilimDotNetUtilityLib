using OrakYazilimLib.AdoNetHelper;
using OrakYazilimLib.Util.core;
using System.Collections.Generic;

namespace OrakYazilimLib.Util.Collection
{
    public class FimList: List<FiMeta>
    {
        public FimList()
        {
        }

        public FimList(int capacity) : base(capacity)
        {
        }
        
        public FimList(IEnumerable<FiMeta> collection) : base(collection)
        {
        }
        
        
    }
}