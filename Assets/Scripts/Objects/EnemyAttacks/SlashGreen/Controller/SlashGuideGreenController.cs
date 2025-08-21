using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using Assets.Scripts.Objects.EnemyAttacks.SlashGreen.Model;

namespace Assets.Scripts.Objects.EnemyAttacks.SlashGreen.Controller
{
    public class SlashGuideGreenController : EnemyAttackController
    {
        private void Awake()
        {
            Initialize(new SlashGuideGreenMove(null, null));
        }
    }
}
