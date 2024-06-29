namespace OrakYazilimLib.UtilThirdParty
{
    public static class TextHelper
    {   
        /**
         * Example
         * string phone = TextHelper.Coalesce(customer.Phone, customer.Mobile, "None Available");
         * phone boşsa, mobile 'e , o da boşsa none availble yazar
         */
        public static string Coalesce(params string[] args)
        {
            string value = string.Empty;
            foreach (string arg in args)
            {
                if (!string.IsNullOrEmpty(arg))
                {
                    value = arg;
                    break;
                }
            }
            return value;
        }
    }

}