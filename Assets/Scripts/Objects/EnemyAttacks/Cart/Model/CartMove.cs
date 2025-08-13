using System;
using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectStorage.Model;
using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using Assets.Scripts.Objects.EnemyAttacks.Base.Model;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Objects.EnemyAttacks.Cart.Model
{
    public class CartMove : IEnemyAttackMove
    {
        private readonly EnemyAttackModel eAM;
        private readonly EnemyAttackController eAC;
        private Vector2 moveDir;
        private bool isInvincible;

        public CartMove(EnemyAttackModel eAM, EnemyAttackController eAC)
        {
            this.eAM = eAM;
            this.eAC = eAC;
        }

        public IEnemyAttackMove Initialize(EnemyAttackModel eAM, EnemyAttackController eAC) => new CartMove(eAM, eAC);

        public void OnAwake()
        {
            moveDir = (StageData.Instance.IsUpperOfStage(eAM.Pos) ? Vector2.down : Vector2.up) * eAM.GetUP("MoveSpeed");
            eAM.Rotate(Vector2.SignedAngle(Vector2.right, moveDir));
            Invincible().Forget();
            PlayAnim().Forget();
        }

        private async UniTask Invincible()
        {
            isInvincible = true;
            await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: eAM.Token);
            isInvincible = false;
        }

        private async UniTask PlayAnim()
        {
            eAC.PlayAnim("Awake", 0.2f);
            await UniTask.Delay(TimeSpan.FromSeconds(0.2f), cancellationToken: eAM.Token);
            eAC.PlayAnim("Move");
        }

        public void OnUpdate()
        {
            eAM.MoveIgnoringStage(Time.deltaTime * moveDir);
            if (StageData.Instance.IsOutOfStage(eAM.Pos) && !isInvincible)
                eAM.Destroy();
        }
    }
}
