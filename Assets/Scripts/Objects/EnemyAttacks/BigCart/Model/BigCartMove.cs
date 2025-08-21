using System;
using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectStorage.Model;
using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using Assets.Scripts.Objects.EnemyAttacks.Base.Model;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Objects.EnemyAttacks.BigCart.Model
{
    public class BigCartMove : IEnemyAttackMove
    {
        private readonly EnemyAttackModel eAM;
        private readonly EnemyAttackController eAC;

        public BigCartMove(EnemyAttackModel eAM, EnemyAttackController eAC)
        {
            this.eAM = eAM;
            this.eAC = eAC;
        }

        public IEnemyAttackMove Initialize(EnemyAttackModel eAM, EnemyAttackController eAC) => new BigCartMove(eAM, eAC);

        public void OnAwake()
        {
            Move().Forget();
        }

        private async UniTask Move()
        {
            float moveDistance = (StageData.Instance.StageCenterPos.x - eAM.Pos.x) * 2f;
            Vector2 moveDir = new(moveDistance * 0.01f, 0f);
            float moveSecondsDelta = eAM.GetUP("MoveSeconds") * 0.01f;
            eAC.PlayAnim("Move");
            for (int i = 0; i < 100; i++)
            {
                eAM.MoveIgnoringStage(moveDir);
                await UniTask.Delay(TimeSpan.FromSeconds(moveSecondsDelta), cancellationToken: eAM.Token);
            }
            eAM.Destroy();
        }

        public void OnUpdate()
        {

        }
    }
}
