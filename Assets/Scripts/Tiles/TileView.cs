using Assets.Scripts.Datas;
using UnityEngine;

namespace Assets.Scripts.Tiles
{
    public class TileView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private ColorDataList colorDataList;
        [SerializeField] private ColorName colorNameCorrect;
        public ColorName ColorNameCorrect => colorNameCorrect;

        public void Paint(ColorName newColorName)
        {
            spriteRenderer.color = colorDataList.GetColor(newColorName);
        }
    }
}
