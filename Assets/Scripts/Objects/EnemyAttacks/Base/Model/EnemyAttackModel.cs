using Assets.Scripts.Objects.Common;
using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectsStorage.Model;
using Assets.Scripts.Objects.EnemyAttacks.Base.Controller;
using UnityEngine;

namespace Assets.Scripts.Objects.EnemyAttacks.Base.Model
{
    public class EnemyAttackModel
    {
        private Power power;
        public int PowerValue => power.CurrentPower;
        private PSA pSA;
        public PSA PSA => pSA;
        private HitBox hitBox;
        public HitBox HitBox => hitBox;
        private readonly EnemyAttackData enemyAttackData;
        private readonly EnemyAttackController enemyAttackController;

        public EnemyAttackModel(EnemyAttackData enemyAttackData, Vector2 position, EnemyAttackController enemyAttackController)
        {
            this.enemyAttackData = enemyAttackData;
            power = enemyAttackData.DefaultPower;
            pSA = new PSA(position, enemyAttackData.HitBoxScale, 0f);
            hitBox = new(pSA.Pos, enemyAttackData.HitBoxScale);
            this.enemyAttackController = enemyAttackController;
            ObjectsStorageModel.Instance.AddEnemyAttack(this);
        }

        public void Move(Vector2 dir) => pSA = pSA.Move(dir);

        public void OnUpdate()
        {
            hitBox = hitBox.Move(pSA.Pos);
        }

        public float GetUniqueParameter(string key) => enemyAttackData.GetUniqueParameter(key);

        public void Destroy()
        {
            ObjectsStorageModel.Instance.RemoveEnemyAttack(this);
            Object.Destroy(enemyAttackController.gameObject);
        }
    }
}
