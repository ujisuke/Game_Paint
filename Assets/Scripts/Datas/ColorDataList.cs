using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Datas
{
    [CreateAssetMenu(fileName = "ColorDataList", menuName = "ScriptableObjects/ColorDataList")]
    public class ColorDataList : ScriptableObject
    {
        [SerializeField] private List<ColorData> paintColorDataList;
        private Dictionary<ColorName, Color> colorDictionary;

        public void Initialize()
        {
            colorDictionary = new Dictionary<ColorName, Color>();

            for (int i = 0; i < paintColorDataList.Count; i++)
                colorDictionary = AddDictionary(paintColorDataList[i], colorDictionary);
        }

        private static Dictionary<ColorName, Color> AddDictionary(ColorData colorData, Dictionary<ColorName, Color> colorDictionary)
        {
            Dictionary<ColorName, Color> updatedColorDictionary = new(colorDictionary);

            if (!colorDictionary.ContainsKey(colorData.Colorname))
                updatedColorDictionary.Add(colorData.Colorname, colorData.Color);
            return updatedColorDictionary;
        }

        public Color GetColor(ColorName colorName)
        {
            if (colorDictionary == null)
                Initialize();
            return colorDictionary[colorName];
        }
    }
}