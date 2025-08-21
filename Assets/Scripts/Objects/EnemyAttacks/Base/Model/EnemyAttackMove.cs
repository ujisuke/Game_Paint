using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;

namespace Assets.Scripts.Objects.EnemyAttacks.Base.Model
{
    public interface IEnemyAttackMove
    {
        IEnemyAttackMove Initialize(EnemyAttackModel eAM, EnemyAttackController eAC);
        void OnAwake();
        void OnUpdate();
    }
}
