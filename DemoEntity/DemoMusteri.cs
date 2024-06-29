using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrakYazilimLib.DbAttr;

namespace OrakYazilimLib.DemoEntity
{
    public class DemoMusteri
    {
        [IsPK]
        public Guid ID { get; set; }
        public int CariNumarasi { get; set; }
        public string Isim { get; set; }
        public string Soyisim { get; set; }
        public string EmailAdres { get; set; }

        public DemoMusteri()
        {

        }

        public DemoMusteri(Guid ID, int CariNumarasi)
        {
            this.ID = ID;
            this.CariNumarasi = CariNumarasi;
        }

        public DemoMusteri(Guid ID, int CariNumarasi, string Isim, string Soyisim)
        {
            this.ID = ID;
            this.CariNumarasi = CariNumarasi;
            this.Isim = Isim;
            this.Soyisim = Soyisim;
        }

        public void EkranaYaz()
        {
            Console.WriteLine($"ID : {ID.ToString()} - Cari Numarası : {CariNumarasi.ToString()} Isim Soyisim : {Isim} {Soyisim}");
        }

        public void CariNoGuncelle(int YeniCariNumarasi)
        {
            this.CariNumarasi = YeniCariNumarasi;
        }
    }

    
}
