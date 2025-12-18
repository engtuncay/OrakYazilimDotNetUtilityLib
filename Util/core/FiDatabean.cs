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
  /**
   * Sınıf test edilmeli - kontrol yapılmadı
   */
  public class FiDatabean : Dictionary<int, object>
  {

    public HashSet<FiCol> setFiCol { get; set; }

    /**
     *  Alanların indexlerini tutar
     */
    private Dictionary<string, int> dictFieldToIndex { get; set; } = new Dictionary<string, int>();

    private int lnLastIndex { get; set; } = 1;

    //public string txTemplate {get; set;}

    public FiDatabean()
    {
    }

    public FiDatabean(IDictionary<int, object> dictionary) : base(dictionary)
    {
    }

    public void AddFiCol(FiCol ficol, object objValue)
    {
      GetSetFiColInit().Add(ficol);
      AddField(ficol.ofcTxFieldName, objValue);
    }

    // public void AddForceFiCol(FiCol ficol, object objValue)
    // {
    //   if (ContainsKey(ficol.ofcTxFieldName))
    //   {
    //     Remove(ficol.ofcTxFieldName);
    //   }
    //   else
    //   {
    //     GetSetFiColInit().Add(ficol);
    //   }
    //   Add(ficol.ofcTxFieldName, objValue);
    // }

    public void AddFieldByFiCol(FiCol ficol, object objValue)
    {
      AddField(ficol.ofcTxFieldName, objValue);
    }

    public void AddFieldByFim(FiMeta fiMeta, object objValue)
    {
      AddField(fiMeta.txKey, objValue);
    }

    public void AddFieldBy(FiMeta fiMeta, object objValue)
    {
      Add(GetSetIndexForField(fiMeta.txKey), objValue);
    }

    public void AddFieldBy(FiCol ficol, object objValue)
    {
      Add(GetSetIndexForField(ficol.ofcTxFieldName), objValue);
    }

    /**
     * Default imple. Add Force (if exists, remove it, add)
     *
     * varsa key, üzerinde yazar, değerini değiştirir
     */
    public void AddField(FiCol fiCol, object objValue)
    {
      AddOverWrite(GetSetIndexForField(fiCol.ofcTxFieldName), objValue);
    }

    public void AddField(string txKey, object objValue)
    {
      AddOverWrite(GetSetIndexForField(txKey), objValue);
    }

    public int GetSetIndexForField(string key)
    {
      if (dictFieldToIndex.ContainsKey(key))
      {
        return dictFieldToIndex[key];
      }
      // yoksa index tahsis eder
      dictFieldToIndex.Add(key, lnLastIndex);
      lnLastIndex++;
      return lnLastIndex - 1;
    }

    /**
     * varsa indexi döner, yoksa -1 döner
     */
    public int GetIndexForField(string key)
    {
      if (dictFieldToIndex.ContainsKey(key))
      {
        return dictFieldToIndex[key];
      }
      return -1;
    }

    public void AddOverWrite(int key, object objValue)
    {
      this[key] = objValue;
    }

    /**
     * Yoksa ekleme yapar, varsa birşey yapmaz
     */
    public void AddFieldIfNot(FiCol ficol, object objValue)
    {
      if (GetIndexForField(ficol.ofcTxFieldName) == -1)
      {
        Add(GetSetIndexForField(ficol.ofcTxFieldName), objValue);
      }
    }

    public HashSet<FiCol> GetSetFiColInit()
    {
      return setFiCol ??= new HashSet<FiCol>();
    }

    public bool ContainsKeyByFiCol(FiCol fiCol)
    {
      return ContainsKey(GetIndexForField(fiCol.ofcTxFieldName));
    }

    public bool ContainsAnyKeyByFiCol(params FiCol[] fiCols)
    {
      return fiCols.Any(fiCol => ContainsKey(GetIndexForField(fiCol.ofcTxFieldName)));
    }

    public bool ContainsAllKeyByFiCol(params FiCol[] fiCols)
    {
      return fiCols.All(fiCol => ContainsKey(GetIndexForField(fiCol.ofcTxFieldName)));
    }

    public void ConvertCsvToListString(string txKey)
    {
      string txValue = GetAsString(txKey);

      if (FiString.IsEmpty(txValue)) return;

      List<string> listValues = txValue.Split(',').Select(item => item.Trim()).ToList();
      // Perform additional operations with listValues if needed
      Remove(GetIndexForField(txKey));
      Add(GetSetIndexForField(txKey), listValues);
    }

    /**
     * Boolean ToString yaparken lower yapar (normalde csh True olarak yapıyor)
     */
    public string GetAsString(string txKey)
    {
      // Eğer sözlük belirtilen anahtarı içeriyorsa:
      if (this.ContainsKey(GetIndexForField(txKey)))
      {
        // Değeri al ve string türüne çevir.
        object value = this[GetIndexForField(txKey)];

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

    public string GetFieldAsString(FiCol fiCol)
    {
      return GetAsString(fiCol.ofcTxFieldName);
    }

    public object GetFieldAsObject(FiCol fiCol)
    {
      return GetAsObject(fiCol.ofcTxFieldName);
    }
    public object GetAsObject(string txKey)
    {
      // Eğer sözlük belirtilen anahtarı içeriyorsa:
      if (this.ContainsKey(GetIndexForField(txKey)))
      {
        // Değeri al ve string türüne çevir.
        object value = this[GetIndexForField(txKey)];
        return value;
      }
      // Eğer anahtar bulunamazsa, null döner
      return null;
    }
    public FkbList GetFieldAsFkbList(FiCol fiCol)
    {
      if (this.ContainsKey(GetIndexForField(fiCol.ofcTxFieldName)))
      {
        // Değeri al ve string türüne çevir.
        object value = this[GetIndexForField(fiCol.ofcTxFieldName)];

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
      return GetFieldAsFkbList(fiCol) ?? new FkbList();
    }
    public void RemoveField(FiCol fiCol)
    {
      Remove(GetIndexForField(fiCol.ofcTxFieldName));
    }

    public bool? GetFieldAsBool(FiCol fiCol)
    {
      // Eğer sözlük belirtilen anahtarı içeriyorsa:
      if (this.ContainsKey(GetIndexForField(fiCol.ofcTxFieldName)))
      {
        // Değeri al ve string türüne çevir.
        object value = this[GetIndexForField(fiCol.ofcTxFieldName)];
        ;

        if (value is bool boValue)
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

      if (this.ContainsKey(GetIndexForField(fiCol.ofcTxFieldName)))
      {
        object value = this[GetIndexForField(fiCol.ofcTxFieldName)];

        if (value is double dbValue)
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
      return GetFieldAsDouble(fiCol) ?? 0;
    }
  }
}