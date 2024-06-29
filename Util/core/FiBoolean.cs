using System;

namespace OrakYazilimLib.Util.core
{
    public class FiBoolean
    {
        public static bool IsTrue(bool? value)
        {
            if (value == null) return false;
            return (bool) value;
        }
    }
}