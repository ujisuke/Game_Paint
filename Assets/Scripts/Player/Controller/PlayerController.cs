using UnityEngine;
using Assets.Scripts.Player.Model;

namespace Assets.Scripts.Player.Controller
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerModel playerModel;

        private void Awake()
        {
            playerModel = new();
        }

        private void FixedUpdate()
        {
            playerModel.Move(
                Input.GetKey(KeyCode.W),
                Input.GetKey(KeyCode.S),
                Input.GetKey(KeyCode.A),
                Input.GetKey(KeyCode.D)
            );

            //Viewクラスができたらそこに記述
            transform.position = playerModel.Pos;
            
            playerModel.Paint(Input.GetMouseButton(0));
            playerModel.SetColor(Input.mouseScrollDelta);
        }
    }
}
