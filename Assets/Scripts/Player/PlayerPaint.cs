using Assets.Scripts.Datas;
using Assets.Scripts.Tiles;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public static class PlayerPaint
    {
        public static void Paint(Vector2 pos)
        {
            ColorName inputColorName = ColorName.green;
            TilesFacade.Instance.PaintTile(pos, inputColorName);
        }
    }
}
