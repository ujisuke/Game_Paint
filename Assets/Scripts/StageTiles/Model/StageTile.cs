using Assets.Scripts.Datas;
using UnityEngine;

namespace Assets.Scripts.StageTiles.Model
{
    public class StageTile
    {
        private readonly ColorName colorNameCorrect;
        private readonly ColorName colorNameCurrent;
        public ColorName ColorNameCurrent => colorNameCurrent;
        private readonly ColorName colorNamePrev;

        public StageTile(ColorName colorNameCorrect, ColorName colorNameCurrent, ColorName colorNamePrev)
        {
            this.colorNameCorrect = colorNameCorrect;
            this.colorNameCurrent = colorNameCurrent;
            this.colorNamePrev = colorNamePrev;
        }

        public bool IsWall()
        {
            return colorNameCorrect == ColorName.wallColor;
        }

        public StageTile Paint(ColorName inputColorName)
        {
            if (IsWall() ||
                (colorNameCurrent == ColorName.defaultColor && colorNameCorrect != inputColorName))
                return this;

            return colorNameCorrect == inputColorName
                ? new StageTile(colorNameCorrect, colorNameCorrect, colorNameCurrent)
                : new StageTile(colorNameCorrect, ColorName.defaultColor, colorNameCurrent);
        }

        public bool IsUpdated()
        {
            return colorNameCurrent != colorNamePrev;
        }
    }
}
