using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using Assets.Scripts.Objects.EnemyAttacks.Shark.Model;

namespace Assets.Scripts.Objects.EnemyAttacks.Shark.Controller
{
    public class SharkController : EnemyAttackController
    {
        private void Awake()
        {
            Initialize(new SharkMove(null, null));
        }
    }
}
