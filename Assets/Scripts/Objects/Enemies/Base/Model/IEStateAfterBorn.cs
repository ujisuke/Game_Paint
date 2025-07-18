namespace Assets.Scripts.Objects.Enemies.Base.Model
{
    public interface IEStateAfterBorn : IEState
    {
        IEState Initialize(EnemyModel enemyModel);
    }
}
