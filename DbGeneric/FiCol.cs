using OrakYazilimLib.DataContainer;
using OrakYazilimLib.Util;
using OrakYazilimLib.Util.Collection;
using OrakYazilimLib.Util.core;
using System;

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

    // /**
    //  * Alanın Veri Türü (FiColType dan alınabilir)
    //  */
    // public string ofcTxColType { get; set; }

    public bool? ofcBoTransient { get; set; }

    public bool? ofcBoNullable { get; set; }

    public string ofcTxFieldDesc { get; set; }

    public int? ofcLnLength { get; set; }
    public int? ofcLnPrecision { get; set; }
    public int? ofcLnScale { get; set; }

    public string ofcTxIdType { get; set; }

    public bool boInsertCol { get; set; }

    /**
     * Sorguda güncellenmesi gereken alan olduğunu belirtir, aksi listedeki tüm sütunlara güncellenecek alan olarak düşünür
     */
    public bool boUpdateCol { get; set; }

    /**
     * Primary Key olduğunu gösterir
     */
    public bool ofcBoPriKey { get; set; }

    //public FiWpfCntx refWpfCntx { get; set; }

    /**
     * Alanın alacağı şablon, xml için düşünüldü.
     */
    public string ficTxTemplate { get; set; }

    /**
     * Alanın varsayılan veri türü
     *
     */
    public FiColType fiColType { get; set; }

    public string ofcTxCompType { get; set; }

    public FimList ofcRefFimList { get; set; }

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
    public bool CheckFiColIfPrimaryKey()
    {
      return !FiString.IsEmpty(this.ofcTxIdType);
    }

    public bool CheckFiColIfIdentityPrimaryKey()
    {
      if (FiString.IsEmpty(this.ofcTxIdType))
      {
        return false;
      }

      return this.ofcTxIdType == "identity";
    }

    public string GetTxDbFieldOrTxFieldName()
    {
      return !FiString.IsEmpty(ofcTxDbField) ? ofcTxDbField : ofcTxFieldName;
    }

    // ReSharper disable once InconsistentNaming
    public string fnm()
    {
      return this.ofcTxFieldName;
    }

    public string fnmTemplate()
    {
      return "{{" + this.ofcTxFieldName + "}}";
    }

    public bool IsText()
    {
      if(this.ofcTxFieldType == null)return false;

      if(this.ofcTxFieldType.ToLower().Equals("text")
      || this.ofcTxFieldType.IndexOf("nvarchar", StringComparison.OrdinalIgnoreCase)!=-1
       )
      {
        return true;
      }
      return false;
    }
    public bool IsBool()
    {
      if(this.ofcTxFieldType == null)return false;

      if(this.ofcTxFieldType.ToLower().Equals("bool"))
      {
        return true;
      }
      return false;
    }
  } // end class
}