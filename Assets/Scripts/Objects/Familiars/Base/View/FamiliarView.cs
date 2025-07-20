using UnityEngine;
using Assets.Scripts.Objects.Common;
using Assets.Scripts.Datas;

namespace Assets.Scripts.Objects.Familiars.Base.View
{
    public class FamiliarView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private ColorDataList colorDataList;
        
        public void SetPA(PA pA)
        {
            transform.SetPositionAndRotation(pA.Pos, Quaternion.Euler(0f, 0f, pA.Angle));
        }

        public void SetColor(ColorName colorNameInput)
        {
            spriteRenderer.color = colorDataList.GetColor(colorNameInput);
        }
    }
}
