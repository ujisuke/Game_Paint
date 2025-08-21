using Assets.Scripts.Datas;
using UnityEngine;

namespace Assets.Scripts.StageTiles.View
{
    public class StageTileView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private ColorDataList colorDataList;
        [SerializeField] private ColorName colorNameInitial;
        public ColorName ColorNameCorrect => colorNameInitial;

        public void Paint(ColorName newColorName)
        {
            spriteRenderer.color = colorDataList.GetColor(newColorName);
        }

        public void Reset()
        {
            Paint(colorNameInitial);
        }

        private void OnValidate()
        {
            Paint(colorNameInitial);
        }

        //仮設
        private void Awake()
        {
            Paint(colorNameInitial == ColorName.wallColor ? ColorName.wallColor : ColorName.defaultTileColor);
        }
    }
}
