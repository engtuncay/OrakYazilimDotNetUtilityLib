using OrakYazilimLib.Util.core;


namespace OrakYazilimLib.Util.FiMetas
{
  public static class FimLogTypes
  {
    public static FiMeta Info(){
        return new FiMeta("info");
    }

    public static FiMeta Error(){
        return new FiMeta("error");
    }

    public static FiMeta Alert(){
      return new FiMeta("alert");
    }

    public static FiMeta Critical(){
      return new FiMeta("critical");
    }

    public static FiMeta Warning(){
      return new FiMeta("warning");
    }

    public static FiMeta Debug(){
      return new FiMeta("debug");
    }

    public static FiMeta Notice()
    {
      return new FiMeta("notice");
    }
  }
}
/*
    const EMERGENCY = 'emergency';
    const ALERT     = 'alert';
    const CRITICAL  = 'critical';
    const ERROR     = 'error';
    const WARNING   = 'warning';
    const NOTICE    = 'notice';
    const INFO      = 'info';
    const DEBUG     = 'debug';
*/