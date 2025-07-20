using Assets.Scripts.Objects.Player.Model;

namespace Assets.Scripts.Objects.Player.Controller
{
    public class PStateMachine
    {
        private IPState currentState;

        public PStateMachine(PlayerModel playerModel, PlayerController playerController)
        {
            currentState = new PStateBorn(playerModel, this, playerController);
            currentState.OnStateEnter();
        }

        public void ChangeState(IPState newState)
        {
            currentState.OnStateExit();
            currentState = newState;
            currentState.OnStateEnter();
        }

        public void HandleInput()
        {
            currentState.HandleInput();
        }
    }
}
