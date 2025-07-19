namespace Assets.Scripts.Objects.Familiars.Base.Model
{
    public interface IFState
    {
        void OnStateEnter();
        void OnUpdate();
        void OnStateExit();
    }
}
