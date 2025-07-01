namespace Assets.Scripts.Familiar.Model
{
    public interface IFState
    {
        void OnStateEnter();
        void OnStateFixedUpdate();
        void OnStateExit();
    }
}
