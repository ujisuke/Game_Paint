using Assets.Scripts.Objects.Common.Model;
using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectStorage.Model;
using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using UnityEngine;
using Assets.Scripts.Objects.FamiliarAttacks.Base.Model;
using System.Threading;
using Cysharp.Threading.Tasks;
using System;

namespace Assets.Scripts.Objects.EnemyAttacks.Base.Model
{
    public class EnemyAttackModel
    {
        private HP hP;
        private PA pA;
        private HitBox hitBox;
        private HurtBox hurtBox;
        private readonly EnemyAttackData enemyAttackData;
        private readonly EnemyAttackController enemyAttackController;
        private readonly IEnemyAttackMove enemyAttackMove;
        private bool isDisposed;
        private readonly CancellationTokenSource cts;
        private readonly CancellationToken token;
        public Vector2 Pos => pA.Pos;
        public float Angle => pA.Angle;
        public int Power => enemyAttackData.Power;
        public HitBox HitBox => hitBox;
        public HurtBox HurtBox => hurtBox;
        public CancellationToken Token => token;

        public EnemyAttackModel(EnemyAttackData enemyAttackData, Vector2 pos, EnemyAttackController enemyAttackController, IEnemyAttackMove enemyAttackMove)
        {
            this.enemyAttackData = enemyAttackData;
            hP = new HP(enemyAttackData.MaxHP);
            pA = new PA(pos, 0f);
            hitBox = new(pA.Pos, enemyAttackData.HitBoxScale, true);
            hurtBox = new(pA.Pos, enemyAttackData.HurtBoxScale, true);
            this.enemyAttackController = enemyAttackController;
            cts = new CancellationTokenSource();
            token = cts.Token;
            this.enemyAttackMove = enemyAttackMove.Initialize(this, enemyAttackController);
            ObjectStorageModel.Instance.AddEnemyAttack(this, enemyAttackData.IsBreakable);
            this.enemyAttackMove.OnAwake();
            isDisposed = false;
        }

        public void MoveIgnoringStage(Vector2 dir)
        {
            if (isDisposed)
                return;
            pA = pA.MoveIgnoringStage(dir);
            hitBox = hitBox.SetPos(pA.Pos);
            hurtBox = hurtBox.SetPos(pA.Pos);
        }

        public void MoveInStage(Vector2 dir)
        {
            if (isDisposed)
                return;
            pA = pA.MoveInStage(dir);
            hitBox = hitBox.SetPos(pA.Pos);
            hurtBox = hurtBox.SetPos(pA.Pos);
        }

        public void Rotate(float angle)
        {
            if (isDisposed)
                return;
            pA = pA.Rotate(angle);
            hitBox = hitBox.SetAngle(angle);
        }

        public void OnUpdate()
        {
            enemyAttackMove.OnUpdate();
        }

        public float GetUP(string key) => enemyAttackData.GetUniqueParameter(key);

        public void TakeDamage(float damageValue)
        {
            hP = hP.TakeDamage(damageValue);
            Invinciblize().Forget();
        }

        private async UniTask Invinciblize()
        {
            hurtBox = hurtBox.SetActive(false);
            if (hP.IsDead())
            {
                isDisposed = true;
                Destroy();
                return;
            }
            await UniTask.Delay(TimeSpan.FromSeconds(0.3f), cancellationToken: token);
            hurtBox = hurtBox.SetActive(true);
        }

        public void Destroy()
        {
            if (isDisposed)
                return;
            cts?.Cancel();
            cts?.Dispose();
            ObjectStorageModel.Instance.RemoveEnemyAttack(this);  
            enemyAttackController?.OnDestroy();
        }

        public void SetActiveHitBox(bool isActive)
        {
            hitBox = hitBox.SetActive(isActive);
        }
    }
}
