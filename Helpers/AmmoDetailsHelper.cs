using EFT.InventoryLogic;
using EFT.UI;
using HarmonyLib;
using System.Collections.Generic;

namespace Tosox.ChamberAmmoInfo.Helpers
{
    public class AmmoDetailsHelper
    {
        private static AccessTools.FieldRef<EftBattleUIScreen, GInterface472> _screenControllerRef;
        private static AccessTools.FieldRef<EftBattleUIScreen, AmmoCountPanel> _ammoCountPanelRef;

        public static void Show(string details)
        {
            if (string.IsNullOrEmpty(details))
                return;

            var battleUIScreen = CommonUI.Instance?.EftBattleUIScreen;
            if (battleUIScreen == null)
                return;

            if (_screenControllerRef == null)
                _screenControllerRef = AccessTools.FieldRefAccess<EftBattleUIScreen, GInterface472>("ScreenController");
            if (_ammoCountPanelRef == null)
                _ammoCountPanelRef = AccessTools.FieldRefAccess<EftBattleUIScreen, AmmoCountPanel>("_ammoCountPanel");

            var screenController = _screenControllerRef(battleUIScreen);
            var ammoCountPanel = _ammoCountPanelRef(battleUIScreen);
            if (screenController == null || ammoCountPanel == null)
                return;

            if (screenController.AzimuthActive)
                battleUIScreen.HideAzimuth();

            ammoCountPanel.Show(null, details);
        }

        public static string JoinChambers(Slot[] chambers)
        {
            if (chambers == null || chambers.Length == 0)
                return string.Empty;

            var cartridgeDetails = new List<string>();
            foreach (var chamber in chambers)
            {
                var ammoItem = chamber?.ContainedItem as AmmoItemClass;
                var ammoName = ammoItem?.Name.Localized(null);
                cartridgeDetails.Add(string.IsNullOrEmpty(ammoName) ? "Empty".Localized(null) : ammoName);
            }

            return cartridgeDetails.Count > 0
                    ? string.Join("\n", cartridgeDetails)
                    : string.Empty;
        }
    }
}
