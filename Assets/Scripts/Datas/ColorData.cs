using System;
using UnityEngine;

namespace Assets.Scripts.Datas
{
    [Serializable]
    public class ColorData
    {
        [SerializeField] private Color color;
        [SerializeField] private ColorName colorName;

        public Color Color => color;
        public ColorName ColorName => colorName;
    }

    public enum ColorName
    {
        red,
        blue,
        wallColor,
        defaultTileColor,
        enemyColor
    }
}
