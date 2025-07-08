using UnityEngine;
using Assets.Scripts.Player.Model;
using Assets.Scripts.Datas;
using Assets.Scripts.Player.View;

namespace Assets.Scripts.Player.Controller
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerData playerData;
        private PlayerModel playerModel;
        [SerializeField] private PlayerView playerView;

        private void Awake()
        {
            playerModel = new(playerData, transform.position, this);
            playerView.SetPSA(playerModel.PSA);
        }

        private void FixedUpdate()
        {
            playerModel.FixedUpdate();
            playerView.SetPSA(playerModel.PSA);
        }
    }
}
