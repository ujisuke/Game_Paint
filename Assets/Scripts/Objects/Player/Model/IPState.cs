namespace Assets.Scripts.Objects.Player.Model
{
    public interface IPState
    {
        void OnStateEnter();
        void OnStateFixedUpdate();
        void OnStateExit();
    }
}
