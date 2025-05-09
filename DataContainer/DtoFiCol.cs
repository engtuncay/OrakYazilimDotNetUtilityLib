using OrakYazilimLib.DbGeneric;
using OrakYazilimLib.Util.Collection;
using System.Collections.Generic;

namespace OrakYazilimLib.DataContainer
{
  public class DtoFiCol
  {
    public static List<dynamic> ImportDtoFiCol2(FicList ficList)
    {
      List<dynamic> list = new List<dynamic>();

      foreach (var ficol in ficList)
      {
        dynamic obj = CreateDynamicFiCol2(ficol);
        list.Add(obj);
      }

      return list;
    }
    public static dynamic CreateDynamicFiCol2(FiCol ficol)
    {
      dynamic obj = new System.Dynamic.ExpandoObject();

      if(ficol.ofcTxFieldName != null) obj.ofcTxFieldName = ficol.ofcTxFieldName;
      if(ficol.ofcTxHeader != null) obj.ofcTxHeader = ficol.ofcTxHeader;
      if(ficol.ofcLnLength != null) obj.ofcLength = ficol.ofcLnLength;
      if(ficol.ofcLnPrecision != null) obj.ofcPrecision = ficol.ofcLnPrecision;
      if(ficol.ofcTxFieldDesc != null) obj.ofcFieldDesc = ficol.ofcTxFieldDesc;

      return obj;
    }
  }
}