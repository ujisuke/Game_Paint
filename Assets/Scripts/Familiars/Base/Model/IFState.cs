namespace Assets.Scripts.Familiars.Base.Model
{
    public interface IFState
    {
        void OnStateEnter();
        void OnStateFixedUpdate();
        void OnStateExit();
    }
}
