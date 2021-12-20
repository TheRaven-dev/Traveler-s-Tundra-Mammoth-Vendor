using System;
using Traveler_s_Tundra_Mammoth_Vendor;
using wManager.Plugin;

public class Main : IPlugin
{
    public void Initialize()
    {
        wManager.wManagerSetting.CurrentSetting.UseMammoth = false;
        VendorMount.Start();
    }

    public void Settings()
    {
       
    }

    public void Dispose()
    {
        VendorMount.Stop();
    }
}
