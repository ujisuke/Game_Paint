using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using Assets.Scripts.Objects.EnemyAttacks.AttackFish.Model;

namespace Assets.Scripts.Objects.EnemyAttacks.BarrierFish.Controller
{
    public class BarrierFishController : EnemyAttackController
    {
        private void Awake()
        {
            Initialize(new BarrierFishMove(null, null));
        }
    }
}
