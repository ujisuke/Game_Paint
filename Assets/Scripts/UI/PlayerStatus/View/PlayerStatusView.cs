using System.Collections.Generic;
using Assets.Scripts.Datas;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.PlayerStatus.View
{
    public class PlayerStatusView : MonoBehaviour
    {
        [SerializeField] private List<GameObject> colorIndicators;
        [SerializeField] private ColorDataList colorDataList;
        [SerializeField] private GameObject tank;
        [SerializeField] private Image inkBar;
        [SerializeField] private Image hPBar;
        private SpriteRenderer tankSR;
        private Dictionary<ColorName, Animator> animDictionary;
        private Animator currentColorIndicatorAnimator;
        private static PlayerStatusView instance;
        public static PlayerStatusView Instance => instance;

        private void Awake()
        {
            instance = this;
            colorDataList.SetSpriteColor(colorIndicators);
            animDictionary = colorDataList.GetAnimDictionary(colorIndicators);
            ColorName initColorName = colorDataList.PaintColorNameList[1];
            currentColorIndicatorAnimator = animDictionary[initColorName];
            tankSR = tank.GetComponent<SpriteRenderer>();
            SetColor(colorDataList.PaintColorNameList[0]);
            SetHPBar(1f);
            SetInkBar(1f);
        }

        public void SetColor(ColorName colorName)
        {
            if (currentColorIndicatorAnimator == animDictionary[colorName])
                return;
            currentColorIndicatorAnimator.Play("DeSelected");
            currentColorIndicatorAnimator = animDictionary[colorName];
            tankSR.color = colorDataList.GetColor(colorName);
            inkBar.color = colorDataList.GetColor(colorName);
            currentColorIndicatorAnimator.Play("Selected");
        }

        public void SetHPBar(float hPRatio)
        {
            hPBar.fillAmount = hPRatio;
        }

        public void SetInkBar(float inkRatio)
        {
            inkBar.fillAmount = inkRatio;
        }
    }
}
