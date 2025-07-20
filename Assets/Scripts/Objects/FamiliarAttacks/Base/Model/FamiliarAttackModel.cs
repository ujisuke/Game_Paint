using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectsStorage.Model;
using Assets.Scripts.Objects.Common;
using Assets.Scripts.Objects.FamiliarAttacks.Base.Controller;
using UnityEngine;

namespace Assets.Scripts.Objects.FamiliarAttacks.Base.Model
{
    public class FamiliarAttackModel
    {
        private PSA pSA;
        private HitBox hitBox;
        private readonly FamiliarAttackData familiarAttackData;
        private readonly FamiliarAttackController familiarAttackController;
        private readonly ColorName colorName;
        public int Power => familiarAttackData.Power;
        public PSA PSA => pSA;
        public HitBox HitBox => hitBox;
        public FamiliarAttackData FamiliarAttackData => familiarAttackData;
        public ColorName ColorName => colorName;

        public FamiliarAttackModel(FamiliarAttackData familiarAttackData, Vector2 position, FamiliarAttackController familiarAttackController, bool isEnemy, ColorName colorName)
        {
            this.familiarAttackController = familiarAttackController;
            this.familiarAttackData = familiarAttackData;
            pSA = new PSA(position, familiarAttackData.HitBoxScale, 0f);
            hitBox = new(pSA.Pos, familiarAttackData.HitBoxScale);
            this.colorName = colorName;
            ObjectsStorageModel.Instance.AddFamiliarAttack(this, isEnemy);
        }

        public void Move(Vector2 dir) => pSA = pSA.Move(dir);

        public void OnUpdate()
        {
            hitBox = hitBox.Move(pSA.Pos);
        }

        public float GetUniqueParameter(string key) => familiarAttackData.GetUniqueParameter(key);

        public void Destroy()
        {
            ObjectsStorageModel.Instance.RemoveFamiliarAttack(this);
            Object.Destroy(familiarAttackController.gameObject);
        }
    }
}
