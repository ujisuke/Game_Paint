using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using Assets.Scripts.Objects.EnemyAttacks.BigCatchFish.Model;

namespace Assets.Scripts.Objects.EnemyAttacks.BigCatchFish.Controller
{
    public class BigCatchFishController : EnemyAttackController
    {
        private void Awake()
        {
            Initialize(new BigCatchFishMove(null, null));
        }
    }
}
