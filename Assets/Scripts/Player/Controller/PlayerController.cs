using UnityEngine;
using Assets.Scripts.Player.Model;

namespace Assets.Scripts.Player.Controller
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerFacade playerFacade;
        bool isGettingMouse0;

        private void Start()
        {
            playerFacade = PlayerFacade.Initialize();
            isGettingMouse0 = false;
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

            if (Input.GetMouseButton(0))
            {
                isGettingMouse0 = true;
                playerFacade.Paint();
            }

            if (!Input.GetMouseButton(0) && isGettingMouse0)
            {
                PlayerFacade.CompletePaint();
                isGettingMouse0 = false;
            }
        }
    }
}
