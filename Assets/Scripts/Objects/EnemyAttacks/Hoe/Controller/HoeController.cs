using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using Assets.Scripts.Objects.EnemyAttacks.Hoe.Model;

namespace Assets.Scripts.Objects.EnemyAttacks.Hoe.Controller
{
    public class HoeController : EnemyAttackController
    {
        private void Awake()
        {
            Initialize(new HoeMove(null, null));
        }
    }
}
