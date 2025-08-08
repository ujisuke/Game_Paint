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
        [SerializeField] private EnemyAttackView enemyAttackView;
        [SerializeField] private ColorEffectData colorEffectData;
        public EnemyAttackModel EnemyAttackModel => enemyAttackModel;

        protected void Initialize(IEnemyAttackMove enemyAttackMove)
        {
            enemyAttackModel = new EnemyAttackModel(enemyAttackData, transform.position, this, enemyAttackMove);
            enemyAttackView.SetPA(enemyAttackModel.PA);
            enemyAttackView.SetViewScale(enemyAttackData.ViewScale);
            enemyAttackView.InstantiateHitBox(enemyAttackModel.HitBox);
        }

        private void Update()
        {
            if (enemyAttackModel == null)
                return;
            enemyAttackModel.OnUpdate();
            enemyAttackView.SetPA(enemyAttackModel.PA);
            enemyAttackView.SetPHitBox(enemyAttackModel.HitBox);
        }

        public void OnDestroy()
        {
            enemyAttackView.OnDestroy();
        }

        public void PlayAnim(string animName, float playSeconds = 1f)
        {
            enemyAttackView.PlayAnim(animName, playSeconds);
        }

        public void FlipX(bool isLeft)
        {
            enemyAttackView.FlipX(isLeft);
        }
    }
}
