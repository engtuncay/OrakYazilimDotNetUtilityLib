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

    public static string TextAllMembers(Object obj)
    {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine("Object Content");
      foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj))
      {
        sb.AppendLine($"{descriptor.Name}={descriptor.GetValue(obj)}");
      }
      return sb.ToString();
    }

    public static string TextFkb(FiKeybean fkb)
    {
      StringBuilder sb = new StringBuilder();
      sb.AppendLine("Fkb Content:");
      int index = 0;
      foreach (KeyValuePair<string, object> keyValuePair in fkb)
      {
        sb.AppendLine($"{keyValuePair.Key}={keyValuePair.Value}");
        index++;
      }
      return sb.ToString();
    }
  }
}