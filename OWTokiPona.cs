using OWML.Common;
using OWML.ModHelper;
using HarmonyLib;
using System.Reflection;
using UnityEngine.SceneManagement;
using UnityEngine;

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

		SceneManager.sceneLoaded += onSceneLoaded;

        onSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
	}

	private void onSceneLoaded(Scene scene, LoadSceneMode mode) {
		if (scene.name != "TitleScreen") return;

		GameObject.Find("TitleCanvasHack/TitleLayoutGroup/OW_Logo_Anim/OW_Logo_Anim/OUTER").transform.localScale = Vector3.zero;
		GameObject.Find("TitleCanvasHack/TitleLayoutGroup/OW_Logo_Anim/OW_Logo_Anim/WILDS").transform.localScale = Vector3.zero;
		var eoteobject = GameObject.Find("TitleMenu/TitleCanvas/TitleLayoutGroup/Logo_EchoesOfTheEye");
		eoteobject.AddComponent<HandleEotETranslation>();
		eoteobject.transform.localScale = new Vector3(1f, 1f, 1);


		var gfxController = GameObject.Find("TitleMenuManagers").GetComponent<TitleScreenManager>()._gfxController;
		gfxController.OnTitleLogoAnimationComplete += OnTitleLogoAnimationComplete;
	}

	public void OnTitleLogoAnimationComplete() {
		Texture2D kasiWekaLogo = ModHelper.Assets.GetTexture("assets/kasi_weka.png");
		var kasiWekaLogoSize = new Vector2(kasiWekaLogo.width, kasiWekaLogo.height);

		var logo = GameObject.Find("TitleCanvasHack/TitleLayoutGroup/OW_Logo_Anim/OW_Logo_Anim");
		var image = logo.AddComponent<UnityEngine.UI.Image>();
		image.sprite = Sprite.Create(kasiWekaLogo, new Rect(Vector2.zero, kasiWekaLogoSize), kasiWekaLogoSize / 2f);

		var root = GameObject.Find("TitleCanvasHack/TitleLayoutGroup/OW_Logo_Anim");
		root.transform.localRotation = Quaternion.Euler(0, 0, 0);
		root.transform.localScale = new Vector3(2.5f, 3.5f, 1);
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

