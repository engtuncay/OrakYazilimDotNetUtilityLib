using OrakYazilimLib.Util.core;
using System;

namespace OrakYazilimLib.Util.config
{
    public interface IFiLogManager
    {
        void Debug(string message);

        void Error(string message);

        //void LogMessage(string message,Type refType);
    }


}