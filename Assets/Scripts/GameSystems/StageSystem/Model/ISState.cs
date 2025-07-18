namespace Assets.Scripts.GameSystems.StageSystem.Model
{
    public interface ISState
    {
        void OnStateEnter();
        void OnStateFixedUpdate();
        void OnStateExit();
    }
}