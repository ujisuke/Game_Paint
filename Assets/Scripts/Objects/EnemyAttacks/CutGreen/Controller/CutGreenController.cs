using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using Assets.Scripts.Objects.EnemyAttacks.CutGreen.Model;

namespace Assets.Scripts.Objects.EnemyAttacks.CutGreen.Controller
{
    public class CutGreenController : EnemyAttackController
    {
        private void Awake()
        {
            Initialize(new CutGreenMove(null, null));
        }
    }
}
