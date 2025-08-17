using System;
using System.Collections.Generic;
using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectStorage.Model;
using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using Assets.Scripts.Objects.EnemyAttacks.Base.Model;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Objects.EnemyAttacks.Shark.Model
{
    public class SharkMove : IEnemyAttackMove
    {
        private readonly EnemyAttackModel eAM;
        private readonly EnemyAttackController eAC;

        public SharkMove(EnemyAttackModel eAM, EnemyAttackController eAC)
        {
            this.eAM = eAM;
            this.eAC = eAC;
        }

        public IEnemyAttackMove Initialize(EnemyAttackModel eAM, EnemyAttackController eAC) => new SharkMove(eAM, eAC);

        public void OnAwake()
        {
            Move().Forget();
        }

        private async UniTask Move()
        {
            eAC.PlayAnim("Move");
            eAM.SetActiveHitBox(false);
            List<Vector2> posList = new()
            {StageData.StageEdgePosMin, new Vector2(StageData.StageEdgePosMin.x, StageData.Instance.StageEdgePosMax.y),
                StageData.Instance.StageEdgePosMax, new Vector2(StageData.Instance.StageEdgePosMax.x, StageData.StageEdgePosMin.y)};
            int j = 0;
            float moveLength = 2f * (StageData.Instance.Width + StageData.Instance.Height);
            float moveLapSeconds = eAM.GetUP("MoveLapSeconds");
            int moveEdgeCount = (int)eAM.GetUP("MoveLapCount") * posList.Count;
            while (moveEdgeCount > 0)
            {
                j++;
                Vector2 targetPos = posList[j % posList.Count];
                Vector2 moveDir = 0.01f * (targetPos - eAM.Pos);
                float moveSecondsDelta = 0.01f * moveLapSeconds * Vector2.Distance(eAM.Pos, targetPos) / moveLength;
                for (int i = 0; i < 100; i++)
                {
                    if(ObjectStorageModel.Instance.IsHitPFAtoEA(eAM))
                        await Attack();

                    eAM.MoveIgnoringStage(moveDir);
                    eAC.FlipX(moveDir.x < 0);
                    await UniTask.Delay(TimeSpan.FromSeconds(moveSecondsDelta), cancellationToken: eAM.Token);
                }
                moveEdgeCount--;
            }
            eAM.Destroy();
        }

        private async UniTask Attack()
        {
            eAC.PlayAnim("Hide");
            await UniTask.Delay(TimeSpan.FromSeconds(eAM.GetUP("HideSeconds")), cancellationToken: eAM.Token);
            eAC.PlayAnim("Up");
            eAM.SetActiveHitBox(true);
            Vector2 playerPos = ObjectStorageModel.Instance.GetPlayerPos(eAM.Pos);
            Vector2 attackDir = 0.01f * eAM.GetUP("AttackSpeed") * (playerPos - eAM.Pos).normalized;
            eAC.FlipX(attackDir.x < 0);
            while (!StageData.Instance.IsOutOfStage(eAM.Pos, 10f))
            {
                eAM.MoveIgnoringStage(attackDir);
                await UniTask.Delay(TimeSpan.FromSeconds(0.01f), cancellationToken: eAM.Token);
            }
            eAM.Destroy();
        }

        public void OnUpdate()
        {

        }
    }
}
