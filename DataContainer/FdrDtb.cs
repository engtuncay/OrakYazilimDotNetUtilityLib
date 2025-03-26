using System.Data;

namespace OrakYazilimLib.DataContainer
{
  public class FdrDtb : Fdr<DataTable>
  {
    public FdrDtb(bool prmBlResult) : base(prmBlResult)
    {
    }
    public FdrDtb(int prmLnRowsAffected) : base(prmLnRowsAffected)
    {
    }
    public FdrDtb()
    {
    }

  }
}