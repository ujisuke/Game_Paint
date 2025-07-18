using Assets.Scripts.GameSystems.MapSystem.Model;

namespace Assets.Scripts.GameSystems.MapSystem.Controller
{
    public class MStateMachine
    {
        private IMState currentState;

        public MStateMachine(MapSystemModel mapSystemModel, MapSystemController mapSystemController)
        {
            currentState = new MStateInitial(mapSystemModel, this, mapSystemController);
            currentState.OnStateEnter();
        }

        public void ChangeState(IMState newState)
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
