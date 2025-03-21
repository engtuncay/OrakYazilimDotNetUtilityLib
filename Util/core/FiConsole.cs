using System;
using System.ComponentModel;

namespace OrakYazilimLib.Util.core
{
    public class FiConsole
    {
        public static void printAllMembers(Object obj)
        {
            foreach(PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj))
            {
                string name=descriptor.Name;
                object value=descriptor.GetValue(obj);
                Console.WriteLine($"{name}={value}");
            }
        }

        public static void printFkb(FiKeybean fkb)
        {
            foreach(PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj))
            {
                string name=descriptor.Name;
                object value=descriptor.GetValue(obj);
                Console.WriteLine($"{name}={value}");
            }
        }
    }
}