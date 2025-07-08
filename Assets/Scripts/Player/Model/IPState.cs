namespace Assets.Scripts.Player.Model
{
    public interface IPState
    {
        void OnStateEnter();
        void OnStateFixedUpdate();
        void OnStateExit();
    }
}
