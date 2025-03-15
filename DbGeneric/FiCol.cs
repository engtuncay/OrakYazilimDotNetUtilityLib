using OrakYazilimLib;
using OrakYazilimLib.Util;

namespace OrakYazilimLib.DbGeneric
{

  public class FiCol
  {
    public string ofcTxFieldName { get; set; }
    public string ofcTxHeader { get; set; }

    /**
     * Alanın Veri Türü (FiColType dan alınabilir)
     */
    public string ofcTxColType { get; set; }

    public bool? ofcBoTransient { get; set; }

    public bool? ofcBoNullable { get; set; }

    public string ofcTxFieldDesc { get; set; }

    public string ofcTxFieldType { get; set; }

    public int? ofcLnLength { get; set; }
    public int? ofcLnPrecision { get; set; }
    public int? ofcLnScale { get; set; }

    public string ofcTxDbField { get; set; }

    /**
     * Alanın varsayılan veri türü
     *
     */
    public FiColType fiColType { get; set; }

    public FiCol(string ofcTxFieldName)
    {
      this.ofcTxFieldName = ofcTxFieldName;
    }

    public FiCol(string ofcTxFieldName, string ofcTxHeader)
    {
      this.ofcTxFieldName = ofcTxFieldName;
      this.ofcTxHeader = ofcTxHeader;
    }

    public string GetFieldName()
    {
      return this.ofcTxFieldName;
    }

    public FiCol()
    {
    }

    public override string ToString()
    {
      return this.ofcTxFieldName ?? "";
    }

    public FiCol BuiColType(FiColType fiColType)
    {
      this.fiColType = fiColType;
      return this;
    }
    public string GetOfcTxDbFieldOr()
    {
      return FiString.IsEmpty(ofcTxDbField) ? ofcTxFieldName : ofcTxDbField;
    }
  }

}