using UnityEngine;

namespace Assets.Scripts.Player.Model
{
    public class PlayerModel
    {
        private PlayerMove playerMove;
        public Vector2 Pos => playerMove.Pos;
        private PlayerColor playerColor;
        private readonly PlayerPaint playerPaint;

        public PlayerModel()
        {
            playerMove = PlayerMove.Initialize();
            playerColor = PlayerColor.Initialize();
            playerPaint = new();
        }

        public void Move(bool isDirectingUp, bool isDirectingDown, bool isDirectingLeft, bool isDirectingRight)
        {
            playerMove = playerMove.Move(
                isDirectingUp: isDirectingUp,
                isDirectingDown: isDirectingDown,
                isDirectingLeft: isDirectingLeft,
                isDirectingRight: isDirectingRight
            );
        }

        public void Paint(bool isGettingMouse0)
        {
            playerPaint.Paint(playerMove.Pos, playerColor.ColorNameCurrent, isGettingMouse0);
        }

        public void SetColor(Vector2 mouseScrollDelta)
        {
            playerColor = playerColor.SetColor(mouseScrollDelta, playerPaint);
        }
    }
}
