namespace Assets.Scripts.Familiar.Base.Model
{
    public interface IFState
    {
        void OnStateEnter();
        void OnStateFixedUpdate();
        void OnStateExit();
    }
}
