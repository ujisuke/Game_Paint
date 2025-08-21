using System;
using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectStorage.Model;
using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using Assets.Scripts.Objects.EnemyAttacks.Base.Model;
using Cysharp.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Objects.EnemyAttacks.SlashGreen.Model
{
    public class SlashGreenMove : IEnemyAttackMove
    {
        private readonly EnemyAttackModel eAM;
        private readonly EnemyAttackController eAC;

        public SlashGreenMove(EnemyAttackModel eAM, EnemyAttackController eAC)
        {
            this.eAM = eAM;
            this.eAC = eAC;
        }

        public IEnemyAttackMove Initialize(EnemyAttackModel eAM, EnemyAttackController eAC) => new SlashGreenMove(eAM, eAC);

        public void OnAwake()
        {
            Slash().Forget();
        }

        private async UniTask Slash()
        {
            eAC.PlayAnim("Slash");
            float moveSpeed = eAM.GetUP("MoveSpeed");
            Vector2 targetPos = eAM.Pos.y > StageData.Instance.StageCenterPos.y
                ? new Vector2(eAM.Pos.x, StageData.StageEdgePosMin.y) : new Vector2(eAM.Pos.x, StageData.Instance.StageEdgePosMax.y);
            Vector2 moveVector = targetPos - eAM.Pos;
            Vector2 moveDir = 0.01f * moveSpeed * new Vector2(0f, moveVector.y).normalized;
            float moveYCount = math.abs(moveVector.y) / moveSpeed * 100;
            for (int j = 0; j < moveYCount; j++)
            {
                eAM.MoveIgnoringStage(moveDir);
                await UniTask.Delay(TimeSpan.FromSeconds(0.01f), cancellationToken: eAM.Token);
            }
            await UniTask.Delay(TimeSpan.FromSeconds(0.2f), cancellationToken: eAM.Token);

            eAM.Destroy();
        }

        public void OnUpdate()
        {

        }
    }
}
