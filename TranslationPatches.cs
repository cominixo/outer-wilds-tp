using HarmonyLib;
using OWML.ModHelper;
using OWML.ModHelper.Menus;
using OWML.Common.Menus;
using UnityEngine;
using UnityEngine.UI;

namespace OWTokiPona;

[HarmonyPatch]
public class TranslationPatches {

    // for now, i'm removing the "Mods" button until there's a way to change its font
    [HarmonyPrefix]
    [HarmonyPatch(typeof(ModTitleButton), nameof(ModTitleButton.Duplicate), new System.Type[] { typeof(string) })]
    public static bool ModTitleButton_Duplicate_Prefix(ref IModButton __result, ModTitleButton __instance, string title) {
        /*if (title == "MODS") {
            __result = __instance.Duplicate("Ā"); // this is namako
            return false;
        }
        __result = null;
        return true;*/
        if (title == "MODS") {
            return false;
        }        
        return true;
    }
    // TODO wait for localization utility update?
    /*[HarmonyPrefix]
    [HarmonyPatch(typeof(UIStyleManager), nameof(UIStyleManager.GetMenuFont))]
    public static bool StyleManager_GetFont_Prefix(ref Font __result) {
        if (TextTranslation.s_theTable == null) {
            __result = null;
            return false;
        }
        
        __result = AssetBundle.LoadFromFile(OWTokiPona.Instance.ModHelper.Manifest.ModFolderPath + "AssetBundles/fonts").LoadAsset<Font>("nasin-nanpa-3.1.0");
        return false;
    }*/

    [HarmonyPostfix]
    [HarmonyPatch(typeof(SignalscopeUI), nameof(SignalscopeUI.Activate))]
    public static void SignalscopeUI_Postfix(SignalscopeUI __instance) {
        if (PlayerState.AtFlightConsole()) {
            __instance._signalscopeLabel.transform.localScale = new Vector3(5,5,1);
            __instance._signalscopeLabel.fontSize = 50;

            __instance._distanceLabel.transform.localScale = new Vector3(6,6,1);
            __instance._distanceLabel.fontSize = 50;
            __instance._distanceLabel.alignment = TextAnchor.LowerCenter;
        }
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(ShipNotificationDisplay), nameof(ShipNotificationDisplay.Start))]
    public static void ShipNotificationDisplay_Start_Postfix() {
        GameObject.Find("Ship_Body/Module_Cockpit/Systems_Cockpit/ShipCockpitUI/CockpitCanvases/ShipWorldSpaceUI/ConsoleDisplay").GetComponent<Canvas>().scaleFactor = 10;
    }

}

