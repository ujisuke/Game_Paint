using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Common.View
{
    public class SpriteToImageConverter : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Image image;

        private void Update()
        {
            image.sprite = spriteRenderer.sprite;
            image.color = spriteRenderer.color;
            image.rectTransform.sizeDelta = spriteRenderer.sprite.textureRect.size;
            image.rectTransform.pivot = spriteRenderer.sprite.pivot / spriteRenderer.sprite.rect.size;
        }
    }
}
