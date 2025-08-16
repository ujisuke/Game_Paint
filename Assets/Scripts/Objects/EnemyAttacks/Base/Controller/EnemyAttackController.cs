using System;
using Assets.Scripts.Datas;
using Assets.Scripts.Objects.EnemyAttacks.Base.Model;
using Assets.Scripts.Objects.EnemyAttacks.Base.View;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Objects.EnemyAttacks.Base.Controller
{
    public class EnemyAttackController : MonoBehaviour
    {
        [SerializeField] private EnemyAttackData enemyAttackData;
        private EnemyAttackModel enemyAttackModel;
        [SerializeField] private EnemyAttackView enemyAttackView;
        public EnemyAttackModel EnemyAttackModel => enemyAttackModel;

        protected void Initialize(IEnemyAttackMove enemyAttackMove)
        {
            enemyAttackModel = new EnemyAttackModel(enemyAttackData, transform.position, this, enemyAttackMove);
            enemyAttackView.SetPA(enemyAttackModel.Pos, enemyAttackModel.Angle);
            enemyAttackView.SetViewScale(enemyAttackData.ViewScale);
            enemyAttackView.InstantiateHitBox(enemyAttackModel.HitBox);
            enemyAttackView.InstantiateHurtBox(enemyAttackModel.HurtBox);
        }

        private void Update()
        {
            if (enemyAttackModel == null)
                return;
            enemyAttackModel.OnUpdate();
            enemyAttackView.SetPA(enemyAttackModel.Pos, enemyAttackModel.Angle);
            enemyAttackView.SetPHitBox(enemyAttackModel.HitBox);
            enemyAttackView.SetPHurtBox(enemyAttackModel.HurtBox);
        }

        public void OnDestroy()
        {
            enemyAttackView.OnDestroy();
            Destroy(gameObject);
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
