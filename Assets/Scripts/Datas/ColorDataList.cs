using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Datas
{
    [CreateAssetMenu(fileName = "ColorDataList", menuName = "ScriptableObjects/ColorDataList")]
    public class ColorDataList : ScriptableObject
    {
        [SerializeField] private List<ColorData> paintColorDataList;
        [SerializeField] private List<ColorData> stageColorDataList;
        [SerializeField] private ColorData enemyColorData;
        private Dictionary<ColorName, Color> colorDictionary;

        public List<ColorName> PaintColorNameList => paintColorDataList.ConvertAll(colorData => colorData.ColorName);

        private void Initialize()
        {
            colorDictionary = new Dictionary<ColorName, Color>();

            for (int i = 0; i < paintColorDataList.Count; i++)
                colorDictionary = AddDictionary(paintColorDataList[i], colorDictionary);
            for (int i = 0; i < stageColorDataList.Count; i++)
                colorDictionary = AddDictionary(stageColorDataList[i], colorDictionary);
            colorDictionary = AddDictionary(enemyColorData, colorDictionary);
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

        public void SetSpriteColor(List<GameObject> imageList)
        {
            for (int i = 0; i < paintColorDataList.Count; i++)
                imageList[i].GetComponent<SpriteRenderer>().color = paintColorDataList[i].Color;
        }

        public Dictionary<ColorName, Animator> GetAnimDictionary(List<GameObject> animatorList)
        {
            Dictionary<ColorName, Animator> dictionary = new();

            for (int i = 0; i < paintColorDataList.Count; i++)
            {
                ColorName colorName = paintColorDataList[i].ColorName;
                if (!dictionary.ContainsKey(colorName))
                    dictionary.Add(colorName, animatorList[i].GetComponent<Animator>());
            }
            return dictionary;
        }
    }
}