using OrakYazilimLib.Util.Collection;

namespace OrakYazilimLib.Util.ColStruct
{
    public interface IFiTableMeta
    {
        string GetITxTableName();

        string GetITxPrefix();

        FicList GenITableCols();

        FicList GenITableColsTrans();
    }
}