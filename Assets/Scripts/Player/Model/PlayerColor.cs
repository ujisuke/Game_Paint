using System.Collections.Generic;
using Assets.Scripts.Datas;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Player.Model
{
    public class PlayerColor
    {
        private readonly ColorName colorNameCurrent;
        public ColorName ColorNameCurrent => colorNameCurrent;
        private static readonly List<ColorName> colorNames = new()
        {
            ColorName.red,
            ColorName.blue,
            ColorName.yellow,
            ColorName.purple,
            ColorName.green,
            ColorName.orange
        };

        public PlayerColor(ColorName colorNameCurrent) => this.colorNameCurrent = colorNameCurrent;

        public static PlayerColor Initialize() => new(ColorName.red);

        public PlayerColor SetColor(Vector2 mouseScrollDelta)
        {
            if (math.abs(mouseScrollDelta.y) < 0.1f)
                return this;

            ColorName newColorName = mouseScrollDelta.y > 0f
                ? colorNames[(colorNames.IndexOf(colorNameCurrent) - 1) % colorNames.Count]
                : colorNames[(colorNames.IndexOf(colorNameCurrent) + 1) % colorNames.Count];
            return new PlayerColor(newColorName);
        }
    }
}
