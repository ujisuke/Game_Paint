namespace Assets.Scripts.Objects.Enemies.Base.Model
{
    public interface IEState
    {
        void OnStateEnter();
        void OnUpdate();
        void OnStateExit();
    }
}
