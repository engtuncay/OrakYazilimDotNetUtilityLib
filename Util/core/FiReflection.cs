namespace OrakYazilimLib.Util.core
{
    public class FiReflection
    {
        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName)?.GetValue(src, null);
        }
    }
}