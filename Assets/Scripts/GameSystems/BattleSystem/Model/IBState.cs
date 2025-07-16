namespace Assets.Scripts.GameSystems.BattleSystem.Model
{
    public interface IBState
    {
        void OnStateEnter();
        void OnStateFixedUpdate();
        void OnStateExit();
    }
}