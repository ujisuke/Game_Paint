namespace Assets.Scripts.Enemies.Base.Model
{
    public interface IEState
    {
        void OnStateEnter();
        void OnStateFixedUpdate();
        void OnStateExit();
    }
}
