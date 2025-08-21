using System;
using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectStorage.Model;
using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using Assets.Scripts.Objects.EnemyAttacks.Base.Model;
using Cysharp.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Objects.EnemyAttacks.SlashPurple.Model
{
    public class SlashPurpleMove : IEnemyAttackMove
    {
        private readonly EnemyAttackModel eAM;
        private readonly EnemyAttackController eAC;

        public SlashPurpleMove(EnemyAttackModel eAM, EnemyAttackController eAC)
        {
            this.eAM = eAM;
            this.eAC = eAC;
        }

        public IEnemyAttackMove Initialize(EnemyAttackModel eAM, EnemyAttackController eAC) => new SlashPurpleMove(eAM, eAC);

        public void OnAwake()
        {
            Slash().Forget();
        }

        private async UniTask Slash()
        {
            eAC.PlayAnim("Slash");
            float moveSpeed = eAM.GetUP("MoveSpeed");
            Vector2 targetPos = eAM.Pos.x > StageData.Instance.StageCenterPos.x
                ? new Vector2(StageData.StageEdgePosMin.x, eAM.Pos.y) : new Vector2(StageData.Instance.StageEdgePosMax.x, eAM.Pos.y);
            Vector2 moveVector = targetPos - eAM.Pos;
            Vector2 moveDir = 0.01f * moveSpeed * new Vector2(moveVector.x, 0f).normalized;
            float moveXCount = math.abs(moveVector.x) / moveSpeed * 100;
            for (int j = 0; j < moveXCount; j++)
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
