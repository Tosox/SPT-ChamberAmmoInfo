using EFT;
using HarmonyLib;
using SPT.Reflection.Patching;
using System.Reflection;
using Tosox.ChamberAmmoInfo.Helpers;

namespace Tosox.ChamberAmmoInfo.Patches
{
    public class CheckUnderbarrelChamberPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(
                typeof(Player.FirearmController.GClass2040),
                nameof(Player.FirearmController.GClass2040.CheckChamber)
            );
        }

        [PatchPostfix]
        public static void PatchPostfix(Player.FirearmController.GClass2040 __instance, bool __result)
        {
            if (!__instance.Player_0.IsYourPlayer || !__result)
                return;

            var launcher = __instance.LauncherItemClass;
            var details = AmmoDetailsHelper.JoinChambers(launcher.Chambers);

            AmmoDetailsHelper.Show(details);
        }
    }
}
