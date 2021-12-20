using System;
using Traveler_s_Tundra_Mammoth_Vendor;
using wManager.Plugin;

public class Main : IPlugin
{
    Boolean UseMammoth;
    public void Initialize()
    {
        UseMammoth = wManager.wManagerSetting.CurrentSetting.UseMammoth;
        wManager.wManagerSetting.CurrentSetting.UseMammoth = false;
        VendorMount.Start();
    }

    public void Settings()
    {
       
    }

    public void Dispose()
    {
        wManager.wManagerSetting.CurrentSetting.UseMammoth = UseMammoth;
        VendorMount.Stop();
    }
}
