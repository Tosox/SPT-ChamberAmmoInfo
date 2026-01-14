using EFT;
using EFT.InventoryLogic;
using HarmonyLib;
using SPT.Reflection.Patching;
using System.Reflection;
using Tosox.ChamberAmmoInfo.Helpers;

namespace Tosox.ChamberAmmoInfo.Patches
{
    public class CheckWeaponChamberPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(
                typeof(Player.FirearmController.GClass2037),
                nameof(Player.FirearmController.GClass2037.CheckChamber)
            );
        }

        [PatchPrefix]
        public static void PatchPrefix(Player.FirearmController.GClass2037 __instance, ref bool __state)
        {
            __state = false;

            var weapon = __instance.Weapon_0;
            if (weapon == null)
                return;

            __state = (weapon.MalfState.State == Weapon.EMalfunctionState.None);
        }

        [PatchPostfix]
        public static void PatchPostfix(Player.FirearmController.GClass2037 __instance, bool __result, bool __state)
        {
            if (!__result || !__state)
                return;

            var weapon = __instance.Weapon_0;
            if (weapon == null)
                return;

            string details;
            if (weapon.HasChambers)
            {
                details = AmmoDetailsHelper.JoinChambers(weapon.Chambers);
            }
            else
            {
                var lastCartridge = weapon.GetCurrentMagazine()?.Cartridges?.Last as AmmoItemClass;
                details = lastCartridge?.Name.Localized(null) ?? string.Empty;
            }

            AmmoDetailsHelper.Show(details);
        }
    }
}
