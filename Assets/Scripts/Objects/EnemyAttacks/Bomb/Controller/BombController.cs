using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using Assets.Scripts.Objects.EnemyAttacks.Bomb.Model;

namespace Assets.Scripts.Objects.EnemyAttacks.Bomb.Controller
{
    public class BombController : EnemyAttackController
    {
        private void Awake()
        {
            Initialize(new BombMove(null, null));
        }
    }
}
