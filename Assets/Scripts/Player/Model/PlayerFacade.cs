using UnityEngine;

namespace Assets.Scripts.Player.Model
{
    public class PlayerFacade
    {
        private PlayerMove playerMove;
        public Vector2 Pos => playerMove.Pos;

        public PlayerFacade(PlayerMove playerMove)
        {
            this.playerMove = playerMove;
        }

        public static PlayerFacade Initialize()
        {
            return new PlayerFacade(PlayerMove.Initialize());
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

        public void Paint()
        {
            PlayerPaint.Paint(playerMove.Pos);
        }
    }
}
