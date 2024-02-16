using OWML.Common;
using OWML.ModHelper;
using HarmonyLib;
using System.Reflection;

namespace OWTokiPona;

public class OWTokiPona : ModBehaviour
{
	public static OWTokiPona Instance;

	public void Awake() {
		Instance = this;
        Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
    }

	private void Start()
	{
		var api = ModHelper.Interaction.TryGetModApi<ILocalizationAPI>("xen.LocalizationUtility");
        api.RegisterLanguage(this, "toki pona", "assets/Translation.xml");
		api.AddLanguageFont(this, "toki pona", "AssetBundles/fonts", "nasin-nanpa-3.1.0");
		api.AddLanguageFixer("toki pona", sitelenPonaFixer);
		//api.SetLanguageFontSizeModifier("toki pona", 0.9f);
	}

	// The font has been altered so that the sitelen pona glyphs start at 0x88, so that the game can actually display them properly
	// (at the expense of completely ignoring the unicode standard)
	public string sitelenPonaFixer(string str) {
		string new_str = "";
		for(int i = 0; i < str.Length; ++i) {
			if (char.ConvertToUtf32(str, i) >= 0xF1900) {
				new_str += (char)(char.ConvertToUtf32(str, i) + 0x88-0xF1900);
				i++;
			} else {
				new_str += str[i];
			}
		}
		if (new_str == "") return str;
		return new_str;
	}
}

