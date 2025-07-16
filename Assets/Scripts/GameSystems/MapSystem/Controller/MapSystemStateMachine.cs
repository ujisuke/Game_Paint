using Assets.Scripts.GameSystems.MapSystem.Model;

namespace Assets.Scripts.GameSystems.MapSystem.Controller
{
    public class MapSystemStateMachine
    {
        private IMState currentState;

        public MapSystemStateMachine(MapSystemModel mapSystemModel)
        {
            currentState = new MStateInitial(mapSystemModel, this);
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
