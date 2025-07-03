using Assets.Scripts.Datas;
using Assets.Scripts.StageTiles.Model;
using UnityEngine;

namespace Assets.Scripts.Player.Model
{
    public class PlayerPaint
    {
        private bool isPainting;
        public bool IsPainting => isPainting;

        public PlayerPaint()
        {
            isPainting = false;
        }

        public void Paint(Vector2 pos, ColorName colorNameInput, bool isGettingMouse0)
        {
            if (isGettingMouse0)
            {
                isPainting = true;
                StageTilesModel.Instance.PaintTile(pos, colorNameInput);
            }

            if (isPainting && !isGettingMouse0)
            {
                StageTilesModel.Instance.CompletePaint(colorNameInput);
                isPainting = false;
            }
        }
    }
}
