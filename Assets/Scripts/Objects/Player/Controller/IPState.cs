namespace Assets.Scripts.Objects.Player.Controller
{
    public interface IPState
    {
        void OnStateEnter();
        void HandleInput();
        void OnStateExit();
    }
}
