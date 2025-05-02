namespace OrakYazilimLib.FiExtensions
{
  using System;
  using System.Collections.Generic;

  public static class DictionaryUtils
  {
    /// <summary>
    //*Bir kaynak sözlüğün tüm öğelerini hedef sözlüğe kopyalar.
    // * </summary >
    // * <typeparam name = "TKey" > Sözlük anahtarının türü.</typeparam >
    // * <typeparam name = "TValue" > Sözlük değerinin türü.</typeparam >
    // * <param name = "target" > Öğelerin ekleneceği hedef sözlük.</param >
    // * <param name = "source" > Öğelerin kopyalanacağı kaynak sözlük.</param >
    // * <param name = "overwriteExisting" > Eğer true ise ve hedef sözlükte aynı anahtar zaten varsa, değeri kaynaktaki değerle günceller.Eğer false ise, mevcut anahtarlar atlanır.</param >
    /// <exception cref="ArgumentNullException">Hedef veya kaynak sözlük null ise fırlatılır.</exception>

    /**
     * Source dictionary içeriğini Target'a aktarır
     */
    public static void MergeDictionariesFi<TKey, TValue>(
      this Dictionary<TKey, TValue> target, // 'this' anahtar kelimesi ile extension metot yapıyoruz
      Dictionary<TKey, TValue> source,
      bool overwriteExisting = true) // Varsayılan olarak üzerine yazsın
    {
      if (target == null || source == null) return;

      // if (target == null)
      // {
      //   throw new ArgumentNullException(nameof(target));
      // }
      // if (source == null)
      // {
      //   throw new ArgumentNullException(nameof(source));
      // }

      foreach (var kvp in source)
      {

        if (overwriteExisting)
        {
          target[kvp.Key] = kvp.Value; // Mevcut anahtar varsa, güncelle
        }
        else // overWrite False
        {
          // Hedefte anahtar yoksa veya üzerine yazma aktifse ekle/güncelle
          if (!target.ContainsKey(kvp.Key)) // Anahtar yoksa ekle
          {
            target.Add(kvp.Key, kvp.Value);
          }
          //else // Eğer overwriteExisting false ise, mevcut anahtarlar atlanır
          //continue;

        }

      }

      //foreach (KeyValuePair<TKey, TValue> kvp in source.Where(kvp => overwriteExisting || !target.ContainsKey(kvp.Key)))
      //{
      //  target[kvp.Key] = kvp.Value; // Bu ifade hem ekleme hem de güncelleme yapar
      // }

    }
  }

}