using System;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace OrakYazilimLib.Util.core
{
    public class FiConsoleWeb
    {
        public static bool testOpened = false;

        public static void printAllMembers(Object obj)
        {
            foreach(PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj))
            {
                string name=descriptor.Name;
                object value=descriptor.GetValue(obj);
                //Console.WriteLine($"{name}={value}");
                FiLogWeb.logWeb($"{name}={value}");
            }
        }
    }
}