using Assets.Scripts.Objects.Player.Model;
using UnityEngine;

namespace Assets.Scripts.Objects.Player.Controller
{
    public class PStateMove: IPState
    {
        private readonly PlayerModel pM;
        private readonly PStateMachine pSM;
        private readonly PlayerController pC;

        public PStateMove(PlayerModel playerModel, PStateMachine pStateMachine, PlayerController playerController)
        {
            pM = playerModel;
            pSM = pStateMachine;
            pC = playerController;
        }

        public void OnStateEnter()
        {
            Debug.Log("PStateMove");
            pC.PlayAnim("Walk");
        }

        public void HandleInput()
        {
            pM.MoveInput(Input.GetKey(KeyCode.W), Input.GetKey(KeyCode.S), Input.GetKey(KeyCode.A), Input.GetKey(KeyCode.D));
            pC.PlayerView.SetPA(pM.PA);
            pM.SetColor(Input.mouseScrollDelta.y);
            pC.PlayerView.SetColor(pM.ColorNameCurrent);
            if (Input.GetKey(KeyCode.A))
                pC.FlipX(true);
            else if (Input.GetKey(KeyCode.D))
                pC.FlipX(false);

            if (pM.IsDead())
                pSM.ChangeState(new PStateDead(pM, pSM, pC));
            else if (Input.GetMouseButton(0))
                pSM.ChangeState(new PStatePaint(pM, pSM, pC));
            else if (!(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
                pSM.ChangeState(new PStateIdle(pM, pSM, pC));
        }

        public void OnStateExit()
        {

        }
    }
}