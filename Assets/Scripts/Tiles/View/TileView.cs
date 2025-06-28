using Assets.Scripts.Datas;
using UnityEngine;

namespace Assets.Scripts.Tiles.View
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

        private void OnValidate()
        {
            Paint(colorNameCorrect);
        }

        //仮設
        private void Awake()
        {
            Paint(colorNameCorrect == ColorName.wallColor ? ColorName.wallColor : ColorName.defaultColor);
        }
    }
}
