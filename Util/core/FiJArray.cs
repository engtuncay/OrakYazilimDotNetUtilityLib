using Newtonsoft.Json.Linq;
using OrakYazilimLib.Util.Collection;

namespace OrakYazilimLib.Util.core
{
  public class FiJArray
  {

    public static FkbList ConvertFkbList(JArray jarrValue)
    {
      FkbList fkbList = new FkbList();

      // JArray içindeki elemanlarda döngü
      foreach (var element in jarrValue)
      {
        // Her elemanın tipi ne olduğunu kontrol ediyoruz
        if (element is JObject obj) // Eğer JObject ise JProperty'lere ulaşabiliriz
        {
          FiKeybean fkbRow = new FiKeybean();

          //Console.WriteLine("Bir JObject bulundu:");
          foreach (JProperty property in obj.Properties())
          {
            //Console.WriteLine($"  - Key: {property.Name}, Value: {property.Value}");
            fkbRow.Add(property.Name, property.Value);
          }
          fkbList.Add(fkbRow);
        }
        else if (element is JArray innerArray) // Eğer JArray ise
        {
          // Console.WriteLine("Bir iç içe JArray bulundu:");
          // foreach (var item in innerArray)
          // {
          //   Console.WriteLine($"  - {item}");
          // }
        }
        else if (element is JValue jValue) // Eğer düz bir değer ise
        {
          //Console.WriteLine($"Bir JValue bulundu: {jValue.Value}");
        }
        else
        {
          //Console.WriteLine($"Bilinmeyen türde bir eleman: {element}");
        }
      }

      return fkbList;
    } //convertFkbLists
  }
}