using UnityEngine;
using Assets.Scripts.Datas;

namespace Assets.Scripts.StageTiles.Model
{
    public class StageTile
    {
        private readonly ColorName colorNameCurrent;
        private readonly ColorName colorNameInitial;
        private readonly bool isWall;
        public ColorName ColorNameCurrent => colorNameCurrent;
        public ColorName ColorNameInitial => colorNameInitial;
        public bool IsWall => isWall;

        private StageTile(ColorName colorNameCurrent, ColorName colorNameInitial, bool isWall)
        {
            this.colorNameCurrent = colorNameCurrent;
            this.colorNameInitial = colorNameInitial;
            this.isWall = isWall;
        }

        public static StageTile Initialize(ColorName colorNameInitial, bool isWall)
        {
            return new StageTile(colorNameInitial, colorNameInitial, isWall);
        }

        public StageTile Paint(ColorName colorNameInput)
        {
            return new StageTile(colorNameInput, colorNameInitial, isWall);
        }

        public StageTile ResetColor()
        {
            return new StageTile(colorNameInitial, colorNameInitial, isWall);
        }
    }
}
