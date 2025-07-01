namespace Assets.Scripts.Familiar.Model
{
    public class FStateMachine
    {
        private IFState currentState;

        public FStateMachine(FamiliarModel familiarModel)
        {
            currentState = new FStateBorn(this, familiarModel);
            currentState.OnStateEnter();
        }

        public void ChangeState(IFState newState)
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
