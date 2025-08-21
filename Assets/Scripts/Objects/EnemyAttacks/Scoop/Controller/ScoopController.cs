using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using Assets.Scripts.Objects.EnemyAttacks.Scoop.Model;

namespace Assets.Scripts.Objects.EnemyAttacks.Scoop.Controller
{
    public class ScoopController : EnemyAttackController
    {
        private void Awake()
        {
            Initialize(new ScoopMove(null, null));
        }
    }
}
