using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using Assets.Scripts.Objects.EnemyAttacks.SlashPurple.Model;

namespace Assets.Scripts.Objects.EnemyAttacks.SlashPurple.Controller
{
    public class SlashGuidePurpleController : EnemyAttackController
    {
        private void Awake()
        {
            Initialize(new SlashGuidePurpleMove(null, null));
        }
    }
}
