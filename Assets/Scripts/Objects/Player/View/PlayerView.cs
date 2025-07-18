using UnityEngine;
using Assets.Scripts.Objects.Common;
using Assets.Scripts.Datas;
using System;

namespace Assets.Scripts.Objects.Player.View
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [NonSerialized] public ColorDataList ColorDataList;
        
        public void SetPSA(PSA pSA)
        {
            transform.position = pSA.Pos;
            transform.localScale = pSA.Scale;
            transform.rotation = Quaternion.Euler(0f, 0f, pSA.Angle);
        }

        public void SetColor(ColorName colorNameInput)
        {
            spriteRenderer.color = ColorDataList.GetColor(colorNameInput);
            PlayerColorIndicator.Instance?.SetColor(colorNameInput);
        }
    }
}
