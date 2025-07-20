using Assets.Scripts.Datas;
using Assets.Scripts.Objects.EnemyAttacks.Base.Model;
using Assets.Scripts.Objects.EnemyAttacks.Base.View;
using UnityEngine;

namespace Assets.Scripts.Objects.EnemyAttacks.Base.Controller
{
    public class EnemyAttackController : MonoBehaviour
    {
        [SerializeField] private EnemyAttackData enemyAttackData;
        private EnemyAttackModel enemyAttackModel;
        public EnemyAttackModel EnemyAttackModel => enemyAttackModel;
        [SerializeField] private EnemyAttackView enemyAttackView;
        [SerializeField] private ColorEffectData colorEffectData;

        public void Initialize(bool isSpeedDecreased)
        {
            enemyAttackModel = new EnemyAttackModel(enemyAttackData, transform.position, this, isSpeedDecreased, colorEffectData);
        }

        private void Update()
        {
            if (enemyAttackModel == null)
                return;
            enemyAttackModel.OnUpdate();
            enemyAttackView.SetPAS(enemyAttackModel.PA, enemyAttackData.HitBoxScale);
        }
    }
}
