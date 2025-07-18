namespace Assets.Scripts.GameSystems.BattleSystem.Model
{
    public class BStateMachine
    {
        private IBState currentState;

        public BStateMachine(BattleSystemModel model)
        {
            currentState = new BStateInitialize(model);
            currentState.OnStateEnter();
        }

        public void ChangeState(IBState newState)
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
