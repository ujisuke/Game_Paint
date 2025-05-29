using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerFacade playerFacade;

        private void Start()
        {
            playerFacade = PlayerFacade.Initialize();
        }

        private void FixedUpdate()
        {
            playerFacade.Move(
                isDirectingUp: Input.GetKey(KeyCode.W),
                isDirectingDown: Input.GetKey(KeyCode.S),
                isDirectingLeft: Input.GetKey(KeyCode.A),
                isDirectingRight: Input.GetKey(KeyCode.D)
            );

            //Viewクラスができたらそこに記述
            transform.position = playerFacade.Pos;
        }
    }
}
