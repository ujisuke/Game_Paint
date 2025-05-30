using Assets.Scripts.Datas;
using UnityEngine;

namespace Assets.Scripts.Tiles
{
    public class Tile
    {
        private readonly ColorName colorNameCorrect;
        private readonly ColorName colorNameCurrent;
        public ColorName ColorNameCurrent => colorNameCurrent;
        private readonly ColorName colorNamePrev;

        public Tile(ColorName colorNameCorrect, ColorName colorNameCurrent, ColorName colorNamePrev)
        {
            this.colorNameCorrect = colorNameCorrect;
            this.colorNameCurrent = colorNameCurrent;
            this.colorNamePrev = colorNamePrev;
        }

        public bool IsWall()
        {
            return colorNameCorrect == ColorName.wallColor;
        }

        public Tile Paint(ColorName inputColorName)
        {
            if (IsWall() ||
                (colorNameCurrent == ColorName.defaultColor && colorNameCorrect != inputColorName))
                return this;

            return colorNameCorrect == inputColorName
                ? new Tile(colorNameCorrect, colorNameCorrect, colorNameCurrent)
                : new Tile(colorNameCorrect, ColorName.defaultColor, colorNameCurrent);
        }

        public bool IsUpdated()
        {
            return colorNameCurrent != colorNamePrev;
        }
    }
}
