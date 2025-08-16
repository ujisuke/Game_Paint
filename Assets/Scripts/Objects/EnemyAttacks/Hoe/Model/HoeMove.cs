using System;
using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectStorage.Model;
using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using Assets.Scripts.Objects.EnemyAttacks.Base.Model;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Objects.EnemyAttacks.Hoe.Model
{
    public class HoeMove : IEnemyAttackMove
    {
        private readonly EnemyAttackModel eAM;
        private readonly EnemyAttackController eAC;
        private Vector2 moveDir;
        private bool isInvincible;

        public HoeMove(EnemyAttackModel eAM, EnemyAttackController eAC)
        {
            this.eAM = eAM;
            this.eAC = eAC;
        }

        public IEnemyAttackMove Initialize(EnemyAttackModel eAM, EnemyAttackController eAC) => new HoeMove(eAM, eAC);

        public void OnAwake()
        {
            Vector2 enemyPos = ObjectStorageModel.Instance.GetEnemyPos(eAM.Pos);
            moveDir = (eAM.Pos - enemyPos).normalized * eAM.GetUP("MoveSpeed");
            eAC.PlayAnim("Awake");
            eAM.Rotate(Vector2.SignedAngle(Vector2.right, moveDir));
            Invincible().Forget();
        }

        private async UniTask Invincible()
        {
            isInvincible = true;
            await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: eAM.Token);
            isInvincible = false;
        }

        public void OnUpdate()
        {
            eAM.MoveIgnoringStage(Time.deltaTime * moveDir);
            if (StageData.Instance.IsOutOfStage(eAM.Pos) && !isInvincible)
                eAM.Destroy();
        }
    }
}
