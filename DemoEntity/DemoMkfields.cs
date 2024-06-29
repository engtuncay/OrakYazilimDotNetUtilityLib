using System;

namespace OrakYazilimLib.DemoEntity
{
    public enum DemoMkfields
    {
        cha_satici_kodu,cha_kod
    }

    public sealed class DemoMkfields2
    {
        private readonly String name;
        private readonly int value;

        public static readonly DemoMkfields2 cha_satici_kodu = new DemoMkfields2("@cha_satici_kodu");

        private DemoMkfields2(String name, int value)
        {
            this.name = name;
            this.value = value;
        }

        private DemoMkfields2(String name)
        {
            this.name = name;
        }

        public override String ToString()
        {
            //string returnObjectName = nameof(this); //typeof(Mkfields).Name;
            return name;
        }

    }

}