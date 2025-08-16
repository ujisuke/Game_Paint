using System;
using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectStorage.Model;
using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using Assets.Scripts.Objects.EnemyAttacks.Base.Model;
using Cysharp.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Objects.EnemyAttacks.AttackFish.Model
{
    public class AttackFishMove : IEnemyAttackMove
    {
        private readonly EnemyAttackModel eAM;
        private readonly EnemyAttackController eAC;
        private Vector2 moveDir;
        private bool isInvincible;
        float seconds;
        float jumpSeconds;

        public AttackFishMove(EnemyAttackModel eAM, EnemyAttackController eAC)
        {
            this.eAM = eAM;
            this.eAC = eAC;
        }

        public IEnemyAttackMove Initialize(EnemyAttackModel eAM, EnemyAttackController eAC) => new AttackFishMove(eAM, eAC);

        public void OnAwake()
        {
            jumpSeconds = eAM.GetUP("JumpSeconds");
            Vector2 enemyPos = ObjectStorageModel.Instance.GetHostilePos(eAM.Pos, false);
            moveDir = (eAM.Pos - enemyPos).normalized * eAM.GetUP("MoveSpeed");
            eAC.PlayAnim("Awake", jumpSeconds * 2f);
            eAC.FlipX(moveDir.x < 0);
            Invincible().Forget();
            seconds = 0f;
        }

        private async UniTask Invincible()
        {
            isInvincible = true;
            await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: eAM.Token);
            isInvincible = false;
        }

        public void OnUpdate()
        {
            seconds += Time.deltaTime;
            if(seconds > jumpSeconds)
                seconds = 0f;
            Vector2 offset = 0.01f * math.cos(math.PI * seconds / jumpSeconds) * Vector2.up;
            eAM.MoveIgnoringStage(Time.deltaTime * moveDir + offset);
            if (StageData.Instance.IsOutOfStage(eAM.Pos) && !isInvincible)
                eAM.Destroy();
        }
    }
}
