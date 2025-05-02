using System;

namespace OrakYazilimLib.Util.core
{
    public static class FiBool
    {
        public static bool IsTrue(bool? value)
        {
            if (value == null) return false;
            return (bool) value;
        }
        public static bool IsFalse(bool? value)
        {
            if (value == null) return false;
            return (bool) !value;
        }
    }
}