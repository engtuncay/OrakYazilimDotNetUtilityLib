using OrakYazilimLib.Util;
using OrakYazilimLib.Util.core;

namespace OrakYazilimLib.DbGeneric
{

  public class FiCol
  {
    public string ofcTxFieldName { get; set; }

    /**
     * Veritabanında farklı bir alan ismi varsa, dbField'a yazılır
     */
    public string ofcTxDbField { get; set; }
    public string ofcTxHeader { get; set; }

    /**
     * Farklı sistemdeki ismi (xml alanındaki)
     */
    public string ofcTxRefField { get; set; }

    public string ofcTxFieldType { get; set; }

    /**
     * Alanın Veri Türü (FiColType dan alınabilir)
     */
    public string ofcTxColType { get; set; }

    public bool? ofcBoTransient { get; set; }

    public bool? ofcBoNullable { get; set; }

    public string ofcTxFieldDesc { get; set; }

    public int? ofcLnLength { get; set; }
    public int? ofcLnPrecision { get; set; }
    public int? ofcLnScale { get; set; }


    /**
     * Alanın alacağı şablon, xml için düşünüldü.
     */
    public string ficTxTemplate { get; set; }

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

    // ReSharper disable once InconsistentNaming
    public string tof()
    {
      return ofcTxFieldName;
    }
  }

}