namespace Assets.Scripts.Objects.Familiars.Base.Model
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

        public void OnUpdate()
        {
            currentState.OnUpdate();
        }
    }
}
