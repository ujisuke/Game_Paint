using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using Assets.Scripts.Objects.EnemyAttacks.SlashPurple.Model;

namespace Assets.Scripts.Objects.EnemyAttacks.Slash.Controller
{
    public class SlashController : EnemyAttackController
    {
        private void Awake()
        {
            Initialize(new SlashMove(null, null));
        }
    }
}
