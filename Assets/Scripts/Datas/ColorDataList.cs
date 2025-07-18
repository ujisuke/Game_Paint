using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Datas
{
    [CreateAssetMenu(fileName = "ColorDataList", menuName = "ScriptableObjects/ColorDataList")]
    public class ColorDataList : ScriptableObject
    {
        [SerializeField] private List<ColorData> paintColorDataList;
        [SerializeField] private List<ColorData> stageColorDataList;
        private Dictionary<ColorName, Color> colorDictionary;

        public List<ColorName> PaintColorNameList => paintColorDataList.ConvertAll(colorData => colorData.ColorName);

        private void Initialize()
        {
            colorDictionary = new Dictionary<ColorName, Color>();

            for (int i = 0; i < paintColorDataList.Count; i++)
                colorDictionary = AddDictionary(paintColorDataList[i], colorDictionary);
            for (int i = 0; i < stageColorDataList.Count; i++)
                colorDictionary = AddDictionary(stageColorDataList[i], colorDictionary);
        }

        private static Dictionary<ColorName, Color> AddDictionary(ColorData colorData, Dictionary<ColorName, Color> colorDictionary)
        {
            Dictionary<ColorName, Color> updatedColorDictionary = new(colorDictionary);

            if (!colorDictionary.ContainsKey(colorData.ColorName))
                updatedColorDictionary.Add(colorData.ColorName, colorData.Color);
            return updatedColorDictionary;
        }

        public Color GetColor(ColorName colorName)
        {
            if (colorDictionary == null)
                Initialize();
            return colorDictionary[colorName];
        }

        public List<Image> SetImageColor(List<Image> imageList)
        {
            List<Image> newImageList = new(imageList);
            
            for (int i = 0; i < paintColorDataList.Count; i++)
            {
                Color color = paintColorDataList[i].Color;
                newImageList[i].color = i == 0 ? color : new Color(color.r, color.g, color.b, 0.5f);
            }
            return newImageList;
        }

        public Dictionary<ColorName, Image> GetColorDictionary(List<Image> imageList)
        {
            Dictionary<ColorName, Image> dictionary = new();

            for (int i = 0; i < paintColorDataList.Count; i++)
            {
                ColorName colorName = paintColorDataList[i].ColorName;
                if (!dictionary.ContainsKey(colorName))
                    dictionary.Add(colorName, imageList[i]);
            }
            return dictionary;
        }
    }
}