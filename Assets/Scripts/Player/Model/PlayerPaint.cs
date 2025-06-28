using Assets.Scripts.Datas;
using Assets.Scripts.StageTiles.Model;
using UnityEngine;

namespace Assets.Scripts.Player.Model
{
    public static class PlayerPaint
    {
        public static void Paint(Vector2 pos)
        {
            ColorName inputColorName = ColorName.green;
            StageTilesFacade.Instance.PaintTile(pos, inputColorName);
        }
    }
}
