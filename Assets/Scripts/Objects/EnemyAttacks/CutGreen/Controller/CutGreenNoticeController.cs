using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using Assets.Scripts.Objects.EnemyAttacks.CutGreen.Model;

namespace Assets.Scripts.Objects.EnemyAttacks.CutGreen.Controller
{
    public class CutGreenNoticeController : EnemyAttackController
    {
        private void Awake()
        {
            Initialize(new CutGreenNoticeMove(null, null));
        }
    }
}
