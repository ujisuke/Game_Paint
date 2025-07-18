namespace Assets.Scripts.Objects.Enemies.Base.Model
{
    public interface IEState
    {
        void OnStateEnter();
        void OnStateFixedUpdate();
        void OnStateExit();
    }
}
