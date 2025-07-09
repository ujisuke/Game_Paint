namespace Assets.Scripts.Player.Model
{
    public class PStateMachine
    {
        private IPState currentState;

        public PStateMachine(PlayerModel playerModel)
        {
            currentState = new PStateBorn(playerModel);
            currentState.OnStateEnter();
        }

        public void ChangeState(IPState newState)
        {
            currentState.OnStateExit();
            currentState = newState;
            currentState.OnStateEnter();
        }

        public void FixedUpdate()
        {
            currentState.OnStateFixedUpdate();
        }
    }
}
