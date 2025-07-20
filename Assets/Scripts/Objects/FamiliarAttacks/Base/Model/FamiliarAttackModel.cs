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
        private readonly ColorName colorName;
        public int Power => familiarAttackData.Power;
        public PA PA => pA;
        public HitBox HitBox => hitBox;
        public FamiliarAttackData FamiliarAttackData => familiarAttackData;
        public ColorName ColorName => colorName;

        public FamiliarAttackModel(FamiliarAttackData familiarAttackData, Vector2 pos, FamiliarAttackController familiarAttackController, bool isEnemy, ColorName colorName)
        {
            this.familiarAttackController = familiarAttackController;
            this.familiarAttackData = familiarAttackData;
            pA = new PA(pos, 0f);
            hitBox = new(pA.Pos, familiarAttackData.HitBoxScale);
            this.colorName = colorName;
            ObjectsStorageModel.Instance.AddFamiliarAttack(this, isEnemy);
        }

        public void Move(Vector2 dir) => pA = pA.Move(dir);

        public void OnUpdate()
        {
            hitBox = hitBox.Move(pA.Pos);
        }

        public float GetUniqueParameter(string key) => familiarAttackData.GetUniqueParameter(key);

        public void Destroy()
        {
            ObjectsStorageModel.Instance.RemoveFamiliarAttack(this);
            Object.Destroy(familiarAttackController.gameObject);
        }
    }
}
