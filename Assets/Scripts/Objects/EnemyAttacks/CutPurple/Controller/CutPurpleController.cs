using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using Assets.Scripts.Objects.EnemyAttacks.CutPurple.Model;

namespace Assets.Scripts.Objects.EnemyAttacks.CutPurple.Controller
{
    public class CutPurpleController : EnemyAttackController
    {
        private void Awake()
        {
            Initialize(new CutPurpleMove(null, null));
        }
    }
}
