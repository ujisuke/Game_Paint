using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using Assets.Scripts.Objects.EnemyAttacks.SlashPurple.Model;

namespace Assets.Scripts.Objects.EnemyAttacks.SlashPurple.Controller
{
    public class SlashPurpleController : EnemyAttackController
    {
        private void Awake()
        {
            Initialize(new SlashPurpleMove(null, null));
        }
    }
}
