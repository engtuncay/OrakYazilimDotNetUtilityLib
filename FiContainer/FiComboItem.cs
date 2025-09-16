using OrakYazilimLib.Util.core;

namespace OrakYazilimLib.FiContainer
{
  public class FiComboItem
  {
    public string txKey { get; set; }

    public string txValue { get; set; }

    /**
     * LnCode (LnKodu)
     * <p>
     * Key Meta Karşılık Gelen Integer Kod varsa
     */
    public int lnKey { get; set; }

    /**
     * Açıklama (Description) gibi düşünebiliriz
     */
    public string txLabel { get; set; }

    public string txType { get; set; }

    public override string ToString()
    {
      return txValue; //base.ToString();
    }

    public static FiComboItem convert(FiMeta fiMeta)
    {
      FiComboItem fiComboItem = new FiComboItem()
      {
        txKey = fiMeta.txKey,
        txValue = fiMeta.txValue,
        lnKey = fiMeta.lnKey,
        txLabel = fiMeta.txLabel,
        txType = fiMeta.txType
      };
      return fiComboItem;
    }

  }
}