using Assets.Scripts.GameSystems.ObjectStorage.Model;
using Assets.Scripts.Objects.Player.Model;
using Assets.Scripts.StageTiles.Model;
using Assets.Scripts.UI.PlayerStatus.View;
using UnityEngine;

namespace Assets.Scripts.Objects.Player.Controller
{
    public class PStatePaint: IPState
    {
        private readonly PlayerModel pM;
        private readonly PStateMachine pSM;
        private readonly PlayerController pC;

        public PStatePaint(PlayerModel playerModel, PStateMachine pStateMachine, PlayerController playerController)
        {
            pM = playerModel;
            pSM = pStateMachine;
            pC = playerController;
        }

        public void OnStateEnter()
        {

        }

        public void HandleInput()
        {
            pM.MoveInput(Input.GetKey(KeyCode.W), Input.GetKey(KeyCode.S), Input.GetKey(KeyCode.A), Input.GetKey(KeyCode.D));
            pC.PlayerView.SetPA(pM.Pos, pM.Angle);
            pC.PlayerView.SetPHurtBox(pM.HurtBox);
            StageTilesModel.Instance.PaintTile(pM.Pos, pM.ColorNameCurrent, false);
            pM.ReduceInk();
            PlayerStatusView.Instance.SetInkBar(pM.InkRatio);
            if (pM.IsDead())
                pSM.ChangeState(new PStateDead(pM, pSM, pC));
            else if (ObjectStorageModel.Instance.IsPlayerTakingDamage())
                pSM.ChangeState(new PStateTakeDamage(pM, pSM, pC));
            else if (!Input.GetMouseButton(0) || pM.IsInkEmpty)
                pSM.ChangeState(new PStateMove(pM, pSM, pC));
        }

        public void OnStateExit()
        {
            StageTilesModel.Instance.CompletePaint(false);
        }
    }
}