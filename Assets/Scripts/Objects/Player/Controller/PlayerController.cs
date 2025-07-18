using UnityEngine;
using Assets.Scripts.Objects.Player.Model;
using Assets.Scripts.Datas;
using Assets.Scripts.Objects.Player.View;

namespace Assets.Scripts.Objects.Player.Controller
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerData playerData;
        private PStateMachine pStateMachine;
        private PlayerModel playerModel;
        [SerializeField] private PlayerView playerView;
        public PlayerView PlayerView => playerView;
        [SerializeField] private ColorDataList colorDataList;

        private void Awake()
        {
            playerModel = new(playerData, transform.position, this, colorDataList);
            playerView.ColorDataList = colorDataList;
            playerView.SetPSA(playerModel.PSA);
            pStateMachine = new PStateMachine(playerModel, this);
        }

        private void Update()
        {
            pStateMachine.HandleInput();
        }
    }
}
