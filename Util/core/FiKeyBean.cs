using OrakYazilimLib.DbGeneric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrakYazilimLib.Util.core
{
    public class FiKeybean : Dictionary<string, object>
    {
        
        public HashSet<FiCol> setFiCol { get; set; }
        
        public FiKeybean()
        {
        }

        public FiKeybean(IDictionary<string, object> dictionary) : base(dictionary)
        {
        }
        
        public void AddByFiCol(FiCol ficol, object objValue)
        {
            GetSetFiColInit().Add(ficol);
            Add(ficol.ofcTxFieldName,objValue);
        }

        public HashSet<FiCol> GetSetFiColInit()
        {
            return setFiCol ?? (setFiCol = new HashSet<FiCol>());
        }

        public bool ContainsKeyByFiCol(FiCol fiCol)
        {
            return ContainsKey(fiCol.ofcTxFieldName);
        }
    }
}
