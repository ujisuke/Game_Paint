namespace Assets.Scripts.Enemies.Base.Model
{
    public interface IEStateAfterBorn : IEState
    {
        IEState Initialize(EnemyModel enemyModel);
    }
}
