using OrakYazilimLib.Util.core;
using System;

namespace OrakYazilimLib.Util.config
{
    public interface IFiLogManager
    {
        void LogMessage(string message);

        void ErrorMessage(string message);

        //void LogMessage(string message,Type refType);
    }


}