using Assets.Scripts.Datas;

namespace Assets.Scripts.StageTiles.Model
{
    public class StageTile
    {
        private readonly ColorName colorNameCurrent;
        public ColorName ColorNameCurrent => colorNameCurrent;
        private readonly ColorName colorNameInitial;
        public ColorName ColorNameInitial => colorNameInitial;


        public StageTile(ColorName colorNameCurrent)
        {
            this.colorNameCurrent = colorNameCurrent;
        }

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

        public StageTile Paint(ColorName colorNameInput)
        {
            if (IsWall())
                return this;
            return new StageTile(colorNameInput);
        }

        public StageTile ResetColor()
        {
            return new StageTile(colorNameInitial);
        }
    }
}
