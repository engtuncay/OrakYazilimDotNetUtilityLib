namespace OrakYazilimLib.FicOrakUtil
{
// Csharp FiCol Class Generation v1
  using OrakYazilimLib.DbGeneric;
  using OrakYazilimLib.Util;
  using OrakYazilimLib.Util.Collection;
  using OrakYazilimLib.Util.ColStruct;

  public class FicOksCoding:IFiTableMeta
  {

    public static string GetTxTableName()
    {
      return "OksCoding";
    }

    public string GetITxTableName()
    {
      return GetTxTableName();
    }

    public FicList GenITableCols()
    {
      return GenTableCols();
    }

    public FicList GenITableColsTrans()
    {
      return GenTableColsTrans();
    }

    public static string GetTxPrefix()
    {
      return "ok";
    }

    public string GetITxPrefix()
    {
      return GetTxPrefix();
    }

    public static void AddFieldDesc(FicList ficolList) {

      foreach (FiCol fiCol in ficolList)
      {

      }

    }


    public static FicList GenTableCols() {
      FicList ficList = new FicList();

      ficList.Add(OkTableName());
      ficList.Add(OkTableFields());
      ficList.Add(OkCsvFields());


      return ficList;
    }

    public static FicList GenTableColsTrans() {
      FicList ficList = new FicList();



      return ficList;
    }

    public static FiCol OkTableName()
    {
      FiCol fiCol = new FiCol("okTableName");

      return fiCol;
    }

    public static FiCol OkTableFields()
    {
      FiCol fiCol = new FiCol("okTableFields");

      return fiCol;
    }

    public static FiCol OkCsvFields()
    {
      FiCol fiCol = new FiCol("okCsvFields");

      return fiCol;
    }



  }

}