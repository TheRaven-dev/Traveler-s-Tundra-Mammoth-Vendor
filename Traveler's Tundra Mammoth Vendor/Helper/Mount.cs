using robotManager.Helpful;
using System;
using System.Collections.Generic;
using System.Linq;
using wManager.Wow.Helpers;

namespace Traveler_s_Tundra_Mammoth_Vendor
{
    public class Mount
    {
        public String MountName { get; set; }

        public Mount(string _MountName)
        {
            MountName = _MountName;
        }

        #region Simple Booleans
        public Boolean IsKnown(out string Mount)
        {
            Mount = string.Empty;
            var MountExist = this.MountList().Where(x => x.ToString() == this.MountName);
            if (MountExist.Any())
            {
                Mount = MountExist.FirstOrDefault();
            }
            return !string.IsNullOrWhiteSpace(Mount);
        }

        public Boolean IsMounted()
        {
            return SpellManager.HaveBuffLua(this.MountName);
        }
        #endregion

        #region MountBuilder
        private List<String> MountList()
        {
            List<String> Info = new List<string>();
            String[] Fatch = Lua.LuaDoString<String>(@"local RandomTable = {};
                                                        for i = 1, 100 do
                                                            local MountInfo = {GetCompanionInfo('MOUNT', i)};
                                                            if MountInfo[2] then
                                                                tinsert(RandomTable, MountInfo[2]..'^');
                                                            end
                                                        end
                                                        return unpack(RandomTable); ".Replace("RandomTable", Others.GetRandomString(12))).Split('^');

            foreach (var item in Fatch)
            {
                Info.Add(item);
            }
            return Info;
        }
        #endregion
    }
}
