using System;

namespace OrakYazilimLib.Util.core
{
    public class FiLog
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }

        public FiLog getInstance()
        {
            return new FiLog();
        }

        public static string GetMessage(Exception ex)
        {
            return ex.Message;
        }
    }
}