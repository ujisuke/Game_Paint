using System.Collections.Generic;
using Assets.Scripts.Datas;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Objects.Player.View
{
    public class PlayerColorIndicator : MonoBehaviour
    {
        [SerializeField] private List<Image> colorIndicators;
        [SerializeField] private ColorDataList colorDataList;
        private Dictionary<ColorName, Image> colorIndicatorDictionary;
        private Image currentColorIndicator;
        private static PlayerColorIndicator instance;
        public static PlayerColorIndicator Instance => instance;

        private void Awake()
        {
            instance = this;
            colorIndicators = colorDataList.SetImageColor(colorIndicators);
            colorIndicatorDictionary = colorDataList.GetColorDictionary(colorIndicators);
            currentColorIndicator = colorIndicators[0];
        }

        public void SetColor(ColorName colorName)
        {
            currentColorIndicator.color = new Color(currentColorIndicator.color.r, currentColorIndicator.color.g, currentColorIndicator.color.b, 0.5f);
            currentColorIndicator = colorIndicatorDictionary[colorName];
            currentColorIndicator.color = new Color(currentColorIndicator.color.r, currentColorIndicator.color.g, currentColorIndicator.color.b, 1f);
        }
    }
}
