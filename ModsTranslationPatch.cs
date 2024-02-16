using HarmonyLib;
using OWML.ModHelper;
using OWML.ModHelper.Menus;
using OWML.Common.Menus;
using UnityEngine;

namespace OWTokiPona;

[HarmonyPatch]
public class ModsTranslationPatch {

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
}

