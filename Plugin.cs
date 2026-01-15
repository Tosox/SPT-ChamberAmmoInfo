using BepInEx;
using Tosox.ChamberAmmoInfo.Patches;

namespace Tosox.ChamberAmmoInfo
{
    [BepInPlugin("de.tosox.chamberammoinfo", PluginName, PluginVersion)]
    public class Plugin : BaseUnityPlugin
    {
        public const string PluginName = "Chamber Ammo Info";
        public const string PluginVersion = "1.0.1";
        public const string PluginAuthor = "Tosox";
        public const string PluginSource = "https://github.com/Tosox/SPT-ChamberAmmoInfo";

        public void Awake()
        {
            new CheckWeaponChamberPatch().Enable();
            new CheckUnderbarrelChamberPatch().Enable();

            Logger.LogInfo("Plugin loaded successfully");
        }
    }
}
