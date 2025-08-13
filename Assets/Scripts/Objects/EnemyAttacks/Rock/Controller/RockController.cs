using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using Assets.Scripts.Objects.EnemyAttacks.Rock.Model;

namespace Assets.Scripts.Objects.EnemyAttacks.Rock.Controller
{
    public class RockController : EnemyAttackController
    {
        private void Awake()
        {
            Initialize(new RockMove(null, null));
        }
    }
}
