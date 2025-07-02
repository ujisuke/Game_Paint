namespace Assets.Scripts.Familiar.Base.Model
{
    public class FStateMachine
    {
        private IFState currentState;

        public FStateMachine(FamiliarModel familiarModel, IFStateAfterBorn fStateAfterBorn)
        {
            currentState = new FStateBorn(familiarModel, fStateAfterBorn);
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
