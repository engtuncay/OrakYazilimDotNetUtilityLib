using OrakYazilimLib.DbGeneric;
using OrakYazilimLib.Util.Collection;
using System.Collections.Generic;

namespace OrakYazilimLib.DataContainer
{
  public class DtoFiCol
  {
    public static List<dynamic> ImportDtoFiCol2(FiColList fiColList)
    {
      List<dynamic> list = new List<dynamic>();

      foreach (var ficol in fiColList)
      {
        dynamic obj = CreateDynamicFiCol2(ficol);
        list.Add(obj);
      }

      return list;
    }
    public static dynamic CreateDynamicFiCol2(FiCol ficol)
    {
      dynamic obj = new System.Dynamic.ExpandoObject();

      obj.ofcTxFieldName = ficol.ofcTxFieldName;
      obj.ofcTxHeader = ficol.ofcTxHeader;
      obj.ofcLength = ficol.ofcLnLength;
      obj.ofcLnPrecision = ficol.ofcLnPrecision;
      obj.ofcTxFieldDesc = ficol.ofcTxFieldDesc;

      return obj;
    }
  }
}