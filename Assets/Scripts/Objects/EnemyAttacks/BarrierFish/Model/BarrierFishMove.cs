using System;
using Assets.Scripts.GameSystems.ObjectStorage.Model;
using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using Assets.Scripts.Objects.EnemyAttacks.Base.Model;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Objects.EnemyAttacks.AttackFish.Model
{
    public class BarrierFishMove : IEnemyAttackMove
    {
        private readonly EnemyAttackModel eAM;
        private readonly EnemyAttackController eAC;

        public BarrierFishMove(EnemyAttackModel eAM, EnemyAttackController eAC)
        {
            this.eAM = eAM;
            this.eAC = eAC;
        }

        public IEnemyAttackMove Initialize(EnemyAttackModel eAM, EnemyAttackController eAC) => new BarrierFishMove(eAM, eAC);

        public void OnAwake()
        {
            eAC.PlayAnim("Awake");
            Move().Forget();
        }

        private async UniTask Move()
        {
            Vector2 enemyPosPrev = ObjectStorageModel.Instance.GetHostilePos(eAM.Pos, false);
            float aliveSeconds = eAM.GetUP("AliveSeconds");
            float patrolAngleDelta = 360 / eAM.GetUP("PatrolSeconds") * 0.01f;
            while (aliveSeconds > 0)
            {
                Vector2 enemyPos = ObjectStorageModel.Instance.GetHostilePos(eAM.Pos, false);
                eAM.MoveIgnoringStage(enemyPos - enemyPosPrev);
                Vector2 targetPos = Quaternion.Euler(0, 0, patrolAngleDelta) * (eAM.Pos - enemyPos);
                eAM.MoveIgnoringStage(enemyPos + targetPos - eAM.Pos);
                enemyPosPrev = enemyPos;
                eAC.FlipX(enemyPos.y < eAM.Pos.y);
                await UniTask.Delay(TimeSpan.FromSeconds(0.01f), cancellationToken: eAM.Token);
                aliveSeconds -= 0.01f;
            }
            eAM.Destroy();
        }

        public void OnUpdate()
        {

        }
    }
}
