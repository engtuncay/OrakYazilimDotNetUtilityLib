using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace OrakYazilimLib.Util.core
{
  public class FiConsole
  {
    public static void PrintAllMembers(Object obj)
    {
      foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj))
      {
        string name = descriptor.Name;
        object value = descriptor.GetValue(obj);
        Console.WriteLine($"{name}={value}");
      }
    }

    public static string TextFkb(FiKeybean fkb)
    {
      StringBuilder sb = new StringBuilder();
      int index = 0;
      foreach (KeyValuePair<string, object> keyValuePair in fkb)
      {
        if (index > 0) sb.Append("\n");
        sb.Append($"{keyValuePair.Key}={keyValuePair.Value}");
        index++;
      }
      return sb.ToString();
    }
  }
}