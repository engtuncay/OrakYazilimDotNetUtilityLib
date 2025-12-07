using OrakYazilimLib.DbGeneric;
using OrakYazilimLib.Util.Collection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrakYazilimLib.Util.core
{
  public class FiKeybean : Dictionary<string, object>
  {

    public HashSet<FiCol> setFiCol { get; set; }

    //public string txTemplate {get; set;}

    public FiKeybean()
    {
    }

    public FiKeybean(IDictionary<string, object> dictionary) : base(dictionary)
    {
    }

    public void AddFiCol(FiCol ficol, object objValue)
    {
      GetSetFiColInit().Add(ficol);
      Add(ficol.ofcTxFieldName, objValue);
    }

    public void AddForceFiCol(FiCol ficol, object objValue)
    {
      if (ContainsKey(ficol.ofcTxFieldName))
      {
        Remove(ficol.ofcTxFieldName);
      }
      else
      {
        GetSetFiColInit().Add(ficol);
      }
      Add(ficol.ofcTxFieldName, objValue);
    }

    public void AddFieldByFiCol(FiCol ficol, object objValue)
    {
      Add(ficol.ofcTxFieldName, objValue);
    }

    /**
     * Default imple. Add Force (if exist, remove it, add)
     */
    public void AddField(FiCol fiCol, object objValue)
    {
      AddOverWrite(fiCol.ofcTxFieldName, objValue);
    }

    public void AddOverWrite(string key, object objValue)
    {
      this[key] = objValue;
      // if (ContainsKey(key))
      // {
      //   Remove(key);
      // }
      // Add(key, objValue);
    }

    /**
     * Yoksa ekleme yapar, varsa birşey yapmaz
     */
    public void AddFieldIfNot(FiCol ficol, object objValue)
    {
      if (!ContainsKey(ficol.ofcTxFieldName))
      {
        Add(ficol.ofcTxFieldName, objValue);
      }
    }

    public HashSet<FiCol> GetSetFiColInit()
    {
      return setFiCol ??= new HashSet<FiCol>();
    }

    public bool ContainsKeyByFiCol(FiCol fiCol)
    {
      return ContainsKey(fiCol.ofcTxFieldName);
    }

    public bool ContainsAnyKeyByFiCol(params FiCol[] fiCols)
    {
      return fiCols.Any(fiCol => ContainsKey(fiCol.ofcTxFieldName));
    }

    public bool ContainsAllKeyByFiCol(params FiCol[] fiCols)
    {
      return fiCols.All(fiCol => ContainsKey(fiCol.ofcTxFieldName));
    }
    public void ConvertCsvToListString(string txKey)
    {
      string txValue = GetAsString(txKey);

      if (FiString.IsEmpty(txValue)) return;

      List<string> listValues = txValue.Split(',').Select(item => item.Trim()).ToList();
      // Perform additional operations with listValues if needed
      Remove(txKey);
      Add(txKey, listValues);
    }

    /**
     * Boolean ToString yaparken lower yapar (normalde csh True olarak yapıyor)
     */
    public string GetAsString(string txKey)
    {
      // Eğer sözlük belirtilen anahtarı içeriyorsa:
      if (this.ContainsKey(txKey))
      {
        // Değeri al ve string türüne çevir.
        object value = this[txKey];

        // if(value is bool)
        // {
        //   return value.ToString().ToLower();
        // }

        return value switch
        {
          // Double ise, InvariantCulture ile formatla
          double doubleValue => doubleValue.ToString(CultureInfo.InvariantCulture),
          int intValue => intValue.ToString(CultureInfo.InvariantCulture),
          _ => value?.ToString() ?? ""
        };

      }
      // Eğer anahtar bulunamazsa, null döner
      return null;
    }

    public string GetFieldAsString(FiCol fiCol){
      return GetAsString(fiCol.ofcTxFieldName);
    }

    public object GetFieldAsObject(FiCol fiCol){
      return GetAsObject(fiCol.ofcTxFieldName);
    }
    public object GetAsObject(string txKey)
    {
      // Eğer sözlük belirtilen anahtarı içeriyorsa:
      if (this.ContainsKey(txKey))
      {
        // Değeri al ve string türüne çevir.
        object value = this[txKey];
        return value;
      }
      // Eğer anahtar bulunamazsa, null döner
      return null;
    }
    public FkbList GetFieldAsFkbList(FiCol fiCol)
    {
      if (this.ContainsKey(fiCol.ofcTxFieldName))
      {
        // Değeri al ve string türüne çevir.
        object value = this[fiCol.ofcTxFieldName];

        if (value is FkbList fkbList)
        {
          return fkbList;
        }
      }
      // Eğer anahtar bulunamazsa, null döner
      return null;
    }

    public FkbList GetFieldAsFkbListNtn(FiCol fiCol)
    {
      return GetFieldAsFkbList(fiCol)??new FkbList();
    }
    public void RemoveField(FiCol fiCol)
    {
      Remove(fiCol.ofcTxFieldName);
    }
    public bool? GetFieldAsBool(FiCol psrsBoSuccess)
    {
      // Eğer sözlük belirtilen anahtarı içeriyorsa:
      if (this.ContainsKey(psrsBoSuccess.ofcTxFieldName))
      {
        // Değeri al ve string türüne çevir.
        object value = this[psrsBoSuccess.ofcTxFieldName];;

        if(value is bool boValue)
        {
          return boValue;
        }

        return null;
      }
      // Eğer anahtar bulunamazsa, null döner
      return null;
    }
    public double? GetFieldAsDouble(FiCol fiCol)
    {

      if (this.ContainsKey(fiCol.ofcTxFieldName))
      {
        object value = this[fiCol.ofcTxFieldName];

        if(value is double dbValue)
        {
          return dbValue;
        }

        return null;
      }
      // Eğer anahtar bulunamazsa, null döner
      return null;
    }


    public double GetFieldAsDoubleNtn(FiCol fiCol)
    {
      return GetFieldAsDouble(fiCol)??0;
    }
  }
}