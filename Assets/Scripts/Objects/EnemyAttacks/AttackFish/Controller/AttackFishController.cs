using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using Assets.Scripts.Objects.EnemyAttacks.AttackFish.Model;

namespace Assets.Scripts.Objects.EnemyAttacks.AttackFish.Controller
{
    public class AttackFishController : EnemyAttackController
    {
        private void Awake()
        {
            Initialize(new AttackFishMove(null, null));
        }
    }
}
