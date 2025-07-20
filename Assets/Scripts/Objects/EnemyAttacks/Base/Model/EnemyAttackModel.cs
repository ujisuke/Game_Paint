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
        private HitBox hitBox;
        private readonly EnemyAttackData enemyAttackData;
        private readonly EnemyAttackController enemyAttackController;
        private readonly ColorEffectData colorEffectData;
        private readonly bool isSpeedDecreased;
        public PA PA => pA;
        public int Power => enemyAttackData.Power;
        public HitBox HitBox => hitBox;

        public EnemyAttackModel(EnemyAttackData enemyAttackData, Vector2 pos, EnemyAttackController enemyAttackController, bool isSpeedDecreased, ColorEffectData colorEffectData)
        {
            this.enemyAttackData = enemyAttackData;
            pA = new PA(pos, 0f);
            hitBox = new(pA.Pos, enemyAttackData.HitBoxScale);
            this.enemyAttackController = enemyAttackController;
            this.colorEffectData = colorEffectData;
            this.isSpeedDecreased = isSpeedDecreased;
            ObjectsStorageModel.Instance.AddEnemyAttack(this);
        }

        public void Move(Vector2 dir)
        {
            if (isSpeedDecreased)
                dir *= colorEffectData.AttackSpeedMultiplier;
            pA = pA.Move(dir);
        }

        public void OnUpdate()
        {
            hitBox = hitBox.Move(pA.Pos);
        }

        public float GetUniqueParameter(string key) => enemyAttackData.GetUniqueParameter(key);

        public void Break(FamiliarAttackModel familiarAttackModel)
        {
            if (familiarAttackModel.ColorName == ColorName.yellow)
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
