using Assets.Scripts.Objects.Player.Model;
using UnityEngine;

namespace Assets.Scripts.Objects.Player.Controller
{
    public class PStateBorn : IPState
    {
        private readonly PlayerModel pM;
        private readonly PStateMachine pSM;
        private readonly PlayerController pC;

        public PStateBorn(PlayerModel pM, PStateMachine pSM, PlayerController pC)
        {
            this.pM = pM;
            this.pSM = pSM;
            this.pC = pC;
        }

        public void OnStateEnter()
        {
            Debug.Log("PStateBorn");
        }

        public void HandleInput()
        {
            pSM.ChangeState(new PStateMove(pM, pSM, pC));
        }

        public void OnStateExit()
        {

        }
    }
}