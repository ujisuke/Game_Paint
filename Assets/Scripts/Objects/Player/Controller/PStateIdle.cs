using Assets.Scripts.Objects.Player.Model;
using UnityEngine;

namespace Assets.Scripts.Objects.Player.Controller
{
    public class PStateIdle: IPState
    {
        private readonly PlayerModel pM;
        private readonly PStateMachine pSM;
        private readonly PlayerController pC;

        public PStateIdle(PlayerModel playerModel, PStateMachine pStateMachine, PlayerController playerController)
        {
            pM = playerModel;
            pSM = pStateMachine;
            pC = playerController;
        }

        public void OnStateEnter()
        {
            pC.PlayAnim("Idle");
        }

        public void HandleInput()
        {
            pM.SetColor(Input.mouseScrollDelta.y);
            pC.PlayerView.SetColor(pM.ColorNameCurrent);
            if (pM.IsDead())
                pSM.ChangeState(new PStateDead(pM, pSM, pC));
            else if (Input.GetMouseButton(0))
                pSM.ChangeState(new PStatePaint(pM, pSM, pC));
            else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                pSM.ChangeState(new PStateMove(pM, pSM, pC));
        }

        public void OnStateExit()
        {

        }
    }
}