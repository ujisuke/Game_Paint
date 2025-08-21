using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using Assets.Scripts.Objects.EnemyAttacks.MudPurple.Model;

namespace Assets.Scripts.Objects.EnemyAttacks.MudPurple.Controller
{
    public class MudPurpleController : EnemyAttackController
    {
        private void Awake()
        {
            Initialize(new MudPurpleMove(null, null));
        }
    }
}
