using UnityEngine;
using UnityEngine.UI;

namespace OWTokiPona;

class HandleEotETranslation : MonoBehaviour {

    public void Start()
        {
            GetComponent<CanvasGroup>().alpha = 1;
            var graphic = GetComponent<Graphic>();
            var image =   GetComponent<UnityEngine.UI.Image>();

            Texture2D newEotELogo = OWTokiPona.Instance.ModHelper.Assets.GetTexture("assets/kalama_pini_lukin.png");
            var newEotELogoSize = new Vector2(newEotELogo.width, newEotELogo.height);

            image.sprite = Sprite.Create(newEotELogo, new Rect(0.0f, 0.0f, newEotELogo.width, 374), new Vector2(0.5f, 0.5f), 200.0f);
            OWTokiPona.Instance.ModHelper.Console.WriteLine($"{image.sprite.bounds.size}");

        }
    
}