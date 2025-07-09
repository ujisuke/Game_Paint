using UnityEngine;
using Assets.Scripts.Common;
using Assets.Scripts.Datas;

namespace Assets.Scripts.Familiars.Base.View
{
    public class FamiliarView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private ColorDataList colorDataList;
        
        public void SetPSA(PSA pSA)
        {
            transform.position = pSA.Pos;
            transform.localScale = pSA.Scale;
            transform.rotation = Quaternion.Euler(0f, 0f, pSA.Angle);
        }

        public void SetColor(ColorName colorNameInput)
        {
            spriteRenderer.color = colorDataList.GetColor(colorNameInput);
        }
    }
}
