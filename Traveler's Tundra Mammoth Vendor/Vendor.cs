using robotManager.Events;
using robotManager.FiniteStateMachine;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using Traveler_s_Tundra_Mammoth_Vendor.Helper;
using wManager.Wow.Helpers;
using wManager.Wow.ObjectManager;

namespace Traveler_s_Tundra_Mammoth_Vendor
{
    public static class VendorMount
    {
        private static Mount _Mount = new Mount("Traveler's Tundra Mammoth");
        public static void Start()
        {
            FiniteStateMachineEvents.OnRunState += OnTownState;
        }

        public static void Stop()
        {
            FiniteStateMachineEvents.OnRunState -= OnTownState;
        }
        #region OnTownStateHandler
        private static void OnTownState(Engine engine, State state, CancelEventArgs cancelable)
        {
            if (string.IsNullOrWhiteSpace(state.DisplayName) ||
                state.DisplayName != "To Town")
                return;

            cancelable.Cancel = true;
            if (_Mount.IsKnown(out string MountName))
            {
                ReMount:
                if (!_Mount.IsMounted())
                {
                    Lua.LuaDoString($"CastSpellByName(\"{MountName}\");");
                    Thread.Sleep(3500);
                }
                else
                {
                    if(_Mount.IsMounted())
                    {
                        var DrixBlackwrench = ObjectManager.GetObjectWoWUnit().Where(x => x.Entry == 32641 && x.IsAlive && x.IsValid);
                        if(DrixBlackwrench.Any())
                        {
                            Interact.InteractGameObject(DrixBlackwrench.FirstOrDefault().GetBaseAddress, true);
                            Thread.Sleep(100);
                            Tools.VendorRun();
                            Thread.Sleep(100);
                            Vendor.RepairAllItems();


                        }
                        else
                        {
                            var Mojodishu = ObjectManager.GetObjectWoWUnit().Where(x => x.Entry == 32642 && x.IsAlive && x.IsValid);
                            if (Mojodishu.Any())
                            {
                                Interact.InteractGameObject(Mojodishu.FirstOrDefault().GetBaseAddress, true);
                                Thread.Sleep(100);
                                Tools.VendorRun();

                            }
                            else
                            {
                                Lua.LuaDoString($"CastSpellByName(\"{MountName}\");");
                                goto ReMount;
                            }
                        }
                        CloseMerchant();
                        Dismount();
                    }
                }

            }
        }

        private static void Dismount() => Lua.LuaDoString("Dismount();");
        private static void CloseMerchant() => Lua.LuaDoString("CloseMerchant();");
        #endregion
    }
}
