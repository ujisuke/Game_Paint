using Assets.Scripts.Objects.Common;
using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectsStorage.Model;
using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using UnityEngine;
using Assets.Scripts.Objects.FamiliarAttacks.Base.Model;

namespace Assets.Scripts.Objects.EnemyAttacks.Base.Model
{
    public class EnemyAttackModel
    {
        private PA pA;
        public PA PA => pA;
        private HitBox hitBox;
        public HitBox HitBox => hitBox;
        private readonly EnemyAttackData enemyAttackData;
        private readonly EnemyAttackController enemyAttackController;
        public int Power => enemyAttackData.Power;

        public EnemyAttackModel(EnemyAttackData enemyAttackData, Vector2 pos, EnemyAttackController enemyAttackController)
        {
            this.enemyAttackData = enemyAttackData;
            pA = new PA(pos, 0f);
            hitBox = new(pA.Pos, enemyAttackData.HitBoxScale);
            this.enemyAttackController = enemyAttackController;
            ObjectsStorageModel.Instance.AddEnemyAttack(this);
        }

        public void Move(Vector2 dir) => pA = pA.Move(dir);

        public void OnUpdate()
        {
            hitBox = hitBox.Move(pA.Pos);
        }

        public float GetUniqueParameter(string key) => enemyAttackData.GetUniqueParameter(key);

        public void Break(FamiliarAttackModel familiarAttack)
        {
            if (familiarAttack.ColorName == ColorName.orange)
                Destroy();
        }

        public void Destroy()
        {
            if (enemyAttackController == null)
                return;
            ObjectsStorageModel.Instance.RemoveEnemyAttack(this);
            GameObject.Destroy(enemyAttackController.gameObject);
        }
    }
}
