using Assets.Scripts.Objects.Enemies.Base.Controller;

namespace Assets.Scripts.Objects.Enemies.Base.Model
{
    public class EStateMachine
    {
        private IEState currentState;

        public EStateMachine(EnemyModel enemyModel, IEStateAfterBorn eStateAfterBorn, EnemyController enemyController)
        {
            currentState = new EStateBorn(enemyModel, enemyController, eStateAfterBorn);
            currentState.OnStateEnter();
        }

        public void ChangeState(IEState newState)
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
