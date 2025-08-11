using Assets.Scripts.Objects.Common.Model;
using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectsStorage.Model;
using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using UnityEngine;
using Assets.Scripts.Objects.FamiliarAttacks.Base.Model;
using System.Threading;

namespace Assets.Scripts.Objects.EnemyAttacks.Base.Model
{
    public class EnemyAttackModel
    {
        private PA pA;
        private HitBox hitBox;
        private readonly EnemyAttackData enemyAttackData;
        private readonly EnemyAttackController enemyAttackController;
        private readonly IEnemyAttackMove enemyAttackMove;
        private readonly CancellationTokenSource cts;
        private readonly CancellationToken token;
        public PA PA => pA;
        public int Power => enemyAttackData.Power;
        public HitBox HitBox => hitBox;
        public CancellationToken Token => token;

        public EnemyAttackModel(EnemyAttackData enemyAttackData, Vector2 pos, EnemyAttackController enemyAttackController, IEnemyAttackMove enemyAttackMove)
        {
            this.enemyAttackData = enemyAttackData;
            pA = new PA(pos, 0f);
            hitBox = new(pA.Pos, enemyAttackData.HitBoxScale, true);
            this.enemyAttackController = enemyAttackController;
            cts = new CancellationTokenSource();
            token = cts.Token;
            this.enemyAttackMove = enemyAttackMove.Initialize(this, enemyAttackController);
            ObjectsStorageModel.Instance.AddEnemyAttack(this);
            this.enemyAttackMove.OnAwake();
        }

        public void Move(Vector2 dir)
        {
            pA = pA.Move(dir);
            hitBox = hitBox.Move(dir);
        }

        public void Rotate(float angle)
        {
            pA = pA.Rotate(angle);
        }

        public void OnUpdate()
        {
            enemyAttackMove.OnUpdate();
        }

        public float GetUniqueParameter(string key) => enemyAttackData.GetUniqueParameter(key);

        public void Break(FamiliarAttackModel familiarAttackModel)
        {
            if (familiarAttackModel.ColorName == ColorName.blue)
                Destroy();
        }

        public void Destroy()
        {
            if (enemyAttackController == null)
                return;
            cts?.Cancel();
            cts?.Dispose();
            ObjectsStorageModel.Instance.RemoveEnemyAttack(this);
            GameObject.Destroy(enemyAttackController.gameObject);
            enemyAttackController.OnDestroy();
        }

        public void SetActiveHitBox(bool isActive)
        {
            hitBox = hitBox.SetActive(isActive);
        }
    }
}
