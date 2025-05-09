using OrakYazilimLib.DbGeneric;
using System.Collections.Generic;

namespace OrakYazilimLib.Util.Collection
{
    public class FicList: List<FiCol>
    {
        public FicList()
        {
        }
        public FicList(int capacity) : base(capacity)
        {
        }
        public FicList(IEnumerable<FiCol> collection) : base(collection)
        {
        }
    }
}