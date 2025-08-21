using System;
using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectStorage.Model;
using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using Assets.Scripts.Objects.EnemyAttacks.Base.Model;
using Cysharp.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Objects.EnemyAttacks.BigCatchFish.Model
{
    public class BigCatchFishMove : IEnemyAttackMove
    {
        private readonly EnemyAttackModel eAM;
        private readonly EnemyAttackController eAC;
        private Vector2 moveDir;
        private bool isInvincible;
        float seconds;
        float jumpSeconds;
        float jumpHeight;

        public BigCatchFishMove(EnemyAttackModel eAM, EnemyAttackController eAC)
        {
            this.eAM = eAM;
            this.eAC = eAC;
        }

        public IEnemyAttackMove Initialize(EnemyAttackModel eAM, EnemyAttackController eAC) => new BigCatchFishMove(eAM, eAC);

        public void OnAwake()
        {
            jumpSeconds = eAM.GetUP("JumpSeconds");
            Vector2 moveVector = StageData.Instance.StageCenterPos - eAM.Pos;
            moveVector.y = 0;
            moveDir = moveVector.normalized * eAM.GetUP("MoveSpeed");
            eAC.PlayAnim("Awake", jumpSeconds * 2f);
            eAC.FlipX(moveDir.x < 0);
            Invincible().Forget();
            seconds = 0f;
            jumpHeight = eAM.GetUP("JumpHeight");
        }

        private async UniTask Invincible()
        {
            isInvincible = true;
            await UniTask.Delay(TimeSpan.FromSeconds(5f), cancellationToken: eAM.Token);
            isInvincible = false;
        }

        public void OnUpdate()
        {
            seconds += Time.deltaTime;
            if(seconds > jumpSeconds)
                seconds = 0f;
            Vector2 offset = 0.01f * jumpHeight * math.cos(math.PI * seconds / jumpSeconds) * Vector2.up;
            eAM.MoveIgnoringStage(Time.deltaTime * moveDir + offset);
            if (StageData.Instance.IsOutOfStageX(eAM.Pos) && !isInvincible)
                eAM.Destroy();
        }
    }
}
