using UnityEngine;

namespace Assets.Scripts.Player
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
            return new PlayerFacade(new PlayerMove(new Vector2(10.5f,6.5f)));
        }

        public PlayerFacade Move(bool isDirectingUp, bool isDirectingDown, bool isDirectingLeft, bool isDirectingRight)
        {
            return new PlayerFacade(
                playerMove.Move(
                isDirectingUp: isDirectingUp,
                isDirectingDown: isDirectingDown,
                isDirectingLeft: isDirectingLeft,
                isDirectingRight: isDirectingRight
            ));
        }
    }
}
