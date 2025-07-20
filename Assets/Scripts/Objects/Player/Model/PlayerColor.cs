using System.Collections.Generic;
using Assets.Scripts.Datas;
using Unity.Mathematics;

namespace Assets.Scripts.Objects.Player.Model
{
    public class PlayerColor
    {
        private ColorName colorNameCurrent;
        public ColorName ColorNameCurrent => colorNameCurrent;
        private readonly List<ColorName> colorNames;

        public PlayerColor(ColorDataList colorDataList)
        {
            colorNames = colorDataList.PaintColorNameList;
            colorNameCurrent = colorNames[0];
        }

        public void SetColor(float mouseScrollDelta)
        {
            if (math.abs(mouseScrollDelta) < 0.1f)
                return;

            colorNameCurrent = mouseScrollDelta > 0f
                ? colorNames[(colorNames.IndexOf(colorNameCurrent) - 1 + colorNames.Count) % colorNames.Count]
                : colorNames[(colorNames.IndexOf(colorNameCurrent) + 1 + colorNames.Count) % colorNames.Count];
        }
    }
}
