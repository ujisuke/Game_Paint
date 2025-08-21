using Assets.Scripts.Objects.Player.Model;

namespace Assets.Scripts.Objects.Player.Controller
{
    public class PStateDead : IPState
    {
        private readonly PlayerModel pM;
        private readonly PStateMachine pSM;
        private readonly PlayerController pC;

        public PStateDead(PlayerModel playerModel, PStateMachine pStateMachine, PlayerController playerController)
        {
            pM = playerModel;
            pSM = pStateMachine;
            pC = playerController;
        }

        public void OnStateEnter()
        {
            pM.Destroy();
            pC.OnDestroy();
        }

        public void HandleInput()
        {

        }

        public void OnStateExit()
        {

        }
    }
}