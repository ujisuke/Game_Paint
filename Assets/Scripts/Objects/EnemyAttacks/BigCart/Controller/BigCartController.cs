using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using Assets.Scripts.Objects.EnemyAttacks.BigCart.Model;

namespace Assets.Scripts.Objects.EnemyAttacks.BigCart.Controller
{
    public class BigCartController : EnemyAttackController
    {
        private void Awake()
        {
            Initialize(new BigCartMove(null, null));
        }
    }
}
