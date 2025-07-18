using Assets.Scripts.Objects.Player.Model;
using Assets.Scripts.StageTiles.Model;
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
            Debug.Log("PStatePaint");
        }

        public void HandleInput()
        {
            pM.MoveInput(Input.GetKey(KeyCode.W), Input.GetKey(KeyCode.S), Input.GetKey(KeyCode.A), Input.GetKey(KeyCode.D));
            pC.PlayerView.SetPSA(pM.PSA);
            StageTilesModel.Instance.PaintTile(pM.PSA.Pos, pM.ColorNameCurrent);
            if (pM.IsDead())
                pSM.ChangeState(new PStateDead(pM, pSM, pC));
            else if (!Input.GetMouseButton(0))
                pSM.ChangeState(new PStateMove(pM, pSM, pC));
        }

        public void OnStateExit()
        {
            StageTilesModel.Instance.CompletePaint(pM.ColorNameCurrent);
        }
    }
}