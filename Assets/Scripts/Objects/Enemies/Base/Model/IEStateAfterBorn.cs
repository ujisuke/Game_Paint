using Assets.Scripts.Objects.Enemies.Base.Controller;

namespace Assets.Scripts.Objects.Enemies.Base.Model
{
    public interface IEStateAfterBorn : IEState
    {
        IEState Initialize(EnemyModel enemyModel, EnemyController enemyController);
    }
}
