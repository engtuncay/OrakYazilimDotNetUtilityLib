using OrakYazilimLib.DbGeneric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrakYazilimLib.Util.core
{
  public class FiKeybean : Dictionary<string, object>
  {

    public HashSet<FiCol> setFiCol { get; set; }

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

    public void AddCheckFiCol(FiCol ficol, object objValue)
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

    public void AddField(FiCol ficol, object objValue)
    {
      Add(ficol.ofcTxFieldName, objValue);
    }

    /**
     * Daha önceden eklenmiş key varsa, remove eder, sonrasında ekler
     */
    public void AddCheckField(FiCol ficol, object objValue)
    {
      if (ContainsKey(ficol.ofcTxFieldName))
      {
        Remove(ficol.ofcTxFieldName);
      }
      Add(ficol.ofcTxFieldName, objValue);
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
    public void ConvertCsvToListString(string txKey)
    {
      string txValue = GetAsString(txKey);

      if (FiString.IsEmpty(txValue)) return;

      List<string> listValues = txValue.Split(',').Select(item => item.Trim()).ToList();
      // Perform additional operations with listValues if needed
      Remove(txKey);
      Add(txKey, listValues);
    }
    public string GetAsString(string txKey)
    {
      // Eğer sözlük belirtilen anahtarı içeriyorsa:
      if (this.ContainsKey(txKey))
      {
        // Değeri al ve string türüne çevir.
        object value = this[txKey];
        return value?.ToString() ?? ""; // Null kontrolü yaparak değeri döndür.
      }
      // Eğer anahtar bulunamazsa, null döner
      return null;
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
  }
}