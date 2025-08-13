using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using Assets.Scripts.Objects.EnemyAttacks.Cart.Model;

namespace Assets.Scripts.Objects.EnemyAttacks.Cart.Controller
{
    public class CartController : EnemyAttackController
    {
        private void Awake()
        {
            Initialize(new CartMove(null, null));
        }
    }
}
