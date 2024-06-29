namespace GfiYazilimLib.DataContainer
{
    public class FiReturn<T>
    {
        
        bool result { get; set; }
        T returnObject { get; set; }
        string errorMsgShort { get; set; }
        string errorMsgDetail { get; set; }

        public FiReturn(bool prmResult){ this.result = prmResult; }

        public FiReturn(){ }

    }
}