using OWML.ModHelper;
using System;

namespace OWTokiPona {
    public interface ILocalizationAPI {
        void RegisterLanguage(ModBehaviour mod, string name, string translationPath);
        void AddLanguageFont(ModBehaviour mod, string name, string assetBundlePath, string fontPath);
        void AddLanguageFixer(string name, Func<string, string> fixer);
        void SetLanguageFontSizeModifier(string name, float fontSizeModifier);
        void SetLanguageDefaultFontSpacing(string languageName, float defaultFontSpacing);
    }
}