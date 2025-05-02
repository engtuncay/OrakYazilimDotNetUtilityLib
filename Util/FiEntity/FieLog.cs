using OrakYazilimLib.Util.core;

namespace OrakYazilimLib.Util.FiEntity
{
  public class FieLog
  {
    public string txType { get; set; }
    public string txMess { get; set; }

    public FieLog()
    {
    }

    public FieLog(FiMeta fimType, string txMess)
    {
      this.txType = fimType.txKey; //txType;
      this.txMess = txMess;
    }
  }
}