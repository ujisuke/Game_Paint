namespace Assets.Scripts.Enemies.Base.Model
{
    public class EStateMachine
    {
        private IEState currentState;

        public EStateMachine(EnemyModel enemyModel, IEStateAfterBorn eStateAfterBorn)
        {
            currentState = new EStateBorn(enemyModel, eStateAfterBorn);
            currentState.OnStateEnter();
        }

        public void ChangeState(IEState newState)
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
