using UnityEngine;
using Assets.Scripts.Datas;

namespace Assets.Scripts.StageTiles.Model
{
    public class StageTile
    {
        private readonly ColorName colorNameCurrent;
        public ColorName ColorNameCurrent => colorNameCurrent;
        private readonly ColorName colorNameInitial;
        public ColorName ColorNameInitial => colorNameInitial;

        private StageTile(ColorName colorNameCurrent, ColorName colorNameInitial)
        {
            this.colorNameCurrent = colorNameCurrent;
            this.colorNameInitial = colorNameInitial;
        }

        public static StageTile Initialize(ColorName colorNameInitial)
        {
            return new StageTile(colorNameInitial, colorNameInitial);
        }

        public bool IsWall()
        {
            return colorNameCurrent == ColorName.wallColor;
        }

        private bool IsDefaultColor()
        {
            return colorNameCurrent == ColorName.defaultColor;
        }

        public StageTile Paint(ColorName colorNameInput)
        {
            if (!IsDefaultColor())
                return this;
            return new StageTile(colorNameInput, colorNameInitial);
        }

        public StageTile ResetColor()
        {
            Debug.Log($"Resetting color from {colorNameCurrent} to {colorNameInitial}");
            return new StageTile(colorNameInitial, colorNameInitial);
        }
    }
}
