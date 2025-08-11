using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using Assets.Scripts.Objects.EnemyAttacks.Scythe.Model;

namespace Assets.Scripts.Objects.EnemyAttacks.Scythe.Controller
{
    public class ScytheController : EnemyAttackController
    {
        private void Awake()
        {
            Initialize(new ScytheMove(null, null));
        }
    }
}
