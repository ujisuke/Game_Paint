using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using Assets.Scripts.Objects.EnemyAttacks.SlashGreen.Model;

namespace Assets.Scripts.Objects.EnemyAttacks.SlashGreen.Controller
{
    public class SlashGreenController : EnemyAttackController
    {
        private void Awake()
        {
            Initialize(new SlashGreenMove(null, null));
        }
    }
}
