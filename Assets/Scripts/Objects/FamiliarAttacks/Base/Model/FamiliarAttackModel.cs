using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectsStorage.Model;
using Assets.Scripts.Objects.Common;
using Assets.Scripts.Objects.FamiliarAttacks.Base.Controller;
using UnityEngine;

namespace Assets.Scripts.Objects.FamiliarAttacks.Base.Model
{
    public class FamiliarAttackModel
    {
        private PA pA;
        private HitBox hitBox;
        private readonly FamiliarAttackData familiarAttackData;
        private readonly FamiliarAttackController familiarAttackController;
        private readonly bool isSpeedDecreased;
        private readonly ColorName colorName;
        public ColorEffectData colorEffectData;
        public int Power => familiarAttackData.Power;
        public PA PA => pA;
        public HitBox HitBox => hitBox;
        public FamiliarAttackData FamiliarAttackData => familiarAttackData;
        public ColorName ColorName => colorName;

        public FamiliarAttackModel(FamiliarAttackData familiarAttackData, Vector2 pos, FamiliarAttackController familiarAttackController, bool isSpeedDecreased, bool isEnemy, ColorName colorName, ColorEffectData colorEffectData)
        {
            this.familiarAttackData = familiarAttackData;
            pA = new PA(pos, 0f);
            hitBox = new(pA.Pos, familiarAttackData.HitBoxScale);
            this.familiarAttackController = familiarAttackController;
            this.isSpeedDecreased = isSpeedDecreased;
            this.colorName = colorName;
            this.colorEffectData = colorEffectData;
            ObjectsStorageModel.Instance.AddFamiliarAttack(this, isEnemy);
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

        public void Break(FamiliarAttackModel familiarAttackModel)
        {
            if (familiarAttackModel.ColorName == ColorName.yellow)
                Destroy();
        }

        public float GetUniqueParameter(string key) => familiarAttackData.GetUniqueParameter(key);

        public void Destroy()
        {
            if (familiarAttackController == null)
                return;
            ObjectsStorageModel.Instance.RemoveFamiliarAttack(this);
            Object.Destroy(familiarAttackController.gameObject);
        }
    }
}
