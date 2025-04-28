using OrakYazilimLib.DbGeneric;
using OrakYazilimLib.Util.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrakYazilimLib.Util.core
{
  public class FiKeycol : Dictionary<string, FiCol>
  {

    public HashSet<FiCol> setFiCol { get; set; }

    //public string txTemplate {get; set;}

    public FiKeycol()
    {
    }

    public FiKeycol(IDictionary<string, FiCol> dictionary) : base(dictionary)
    {
    }

    public void AddFiCol(FiCol ficol, FiCol objValue)
    {
      GetSetFiColInit().Add(ficol);
      Add(ficol.ofcTxFieldName, objValue);
    }

    public void AddForceFiCol(FiCol ficol, FiCol objValue)
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

    public void AddField(FiCol ficol, FiCol objValue)
    {
      Add(ficol.ofcTxFieldName, objValue);
    }

    /**
     * Daha önceden eklenmiş key varsa, remove eder, sonrasında ekler
     */
    public void AddCheckField(FiCol ficol, FiCol objValue)
    {
      AddForce(ficol.ofcTxFieldName, objValue);
    }

    public void AddFieldForce(FiCol fiCol, FiCol objValue)
    {
      AddForce(fiCol.ofcTxFieldName, objValue);
    }

    public void AddForce(string key, FiCol objValue)
    {
      if (ContainsKey(key))
      {
        Remove(key);
      }
      Add(key, objValue);
    }

    /**
     * Yoksa ekleme yapar, varsa birşey yapmaz
     */
    public void AddFieldIfNot(FiCol ficol, FiCol objValue)
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

    // public void ConvertCsvToListString(string txKey)
    // {
    //   string txValue = GetAsString(txKey);
    //
    //   if (FiString.IsEmpty(txValue)) return;
    //
    //   List<string> listValues = txValue.Split(',').Select(item => item.Trim()).ToList();
    //   // Perform additional operations with listValues if needed
    //   Remove(txKey);
    //   Add(txKey, listValues);
    // }

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

        return value?.ToString() ?? ""; // Null kontrolü yaparak değeri döndür.
      }
      // Eğer anahtar bulunamazsa, null döner
      return null;
    }

    public string GetFieldAsString(FiCol fiCol){
      return GetAsString(fiCol.ofcTxFieldName);
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
  }
}