using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using Assets.Scripts.Objects.EnemyAttacks.Plant.Model;

namespace Assets.Scripts.Objects.EnemyAttacks.Plant.Controller
{
    public class PlantController : EnemyAttackController
    {
        private void Awake()
        {
            Initialize(new PlantMove(null, null));
        }
    }
}
