using System;
using System.Collections.Generic;
using wManager.Wow.Enums;
using wManager.Wow.Helpers;

namespace Traveler_s_Tundra_Mammoth_Vendor.Helper
{
    public class Tools
    {
        private static List<WoWItemQuality> Quality()
        {
            List<WoWItemQuality> quality = new List<WoWItemQuality>();
            if (wManager.wManagerSetting.CurrentSetting.SellGray)
                quality.Add(WoWItemQuality.Poor);
            if (wManager.wManagerSetting.CurrentSetting.SellWhite)
                quality.Add(WoWItemQuality.Common);
            if (wManager.wManagerSetting.CurrentSetting.SellGreen)
                quality.Add(WoWItemQuality.Uncommon);
            if (wManager.wManagerSetting.CurrentSetting.SellBlue)
                quality.Add(WoWItemQuality.Rare);
            return quality;
        }

        public static void VendorRun()
        {
            List<String> ItemList = new List<string>();
            foreach (var item in Bag.GetBagItem())
            {
                ItemList.Add(item.Name);
            }
            Vendor.SellItems(ItemList, wManager.wManagerSetting.CurrentSetting.DoNotSellList, Quality());
        }
    }
}
