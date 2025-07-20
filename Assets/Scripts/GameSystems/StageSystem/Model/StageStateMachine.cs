namespace Assets.Scripts.GameSystems.StageSystem.Model
{
    public class SStateMachine
    {
        private ISState currentState;

        public SStateMachine(StageSystemModel model)
        {
            currentState = new SStateInitialize(model);
            currentState.OnStateEnter();
        }

        public void ChangeState(ISState newState)
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
