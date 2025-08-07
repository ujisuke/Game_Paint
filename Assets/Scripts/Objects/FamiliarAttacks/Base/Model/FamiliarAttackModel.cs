using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectsStorage.Model;
using Assets.Scripts.Objects.Common;
using Assets.Scripts.Objects.FamiliarAttacks.Base.Controller;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;

namespace Assets.Scripts.Objects.FamiliarAttacks.Base.Model
{
    public class FamiliarAttackModel
    {
        private PA pA;
        private HitBox hitBox;
        private readonly FamiliarData familiarData;
        private readonly FamiliarAttackController familiarAttackController;
        private readonly ColorName colorName;
        public ColorEffectData colorEffectData;
        public float Power => familiarData.Power;
        public PA PA => pA;
        public HitBox HitBox => hitBox;
        public FamiliarData FamiliarData => familiarData;
        public ColorName ColorName => colorName;

        public FamiliarAttackModel(FamiliarData familiarData, Vector2 pos, FamiliarAttackController familiarAttackController, bool isEnemy, ColorName colorName, ColorEffectData colorEffectData)
        {
            this.familiarData = familiarData;
            pA = new PA(pos, 0f);
            hitBox = new(pA.Pos, familiarData.HitBoxScale);
            this.familiarAttackController = familiarAttackController;
            this.colorName = colorName;
            this.colorEffectData = colorEffectData;
            ObjectsStorageModel.Instance.AddFamiliarAttack(this, isEnemy);
        }

        public void Move(Vector2 dir)
        {
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

        public float GetUniqueParameter(string key) => familiarData.GetUniqueParameter(key);

        public void Destroy()
        {
            if (familiarAttackController == null)
                return;
            ObjectsStorageModel.Instance.RemoveFamiliarAttack(this);
            Object.Destroy(familiarAttackController.gameObject);
            familiarAttackController.OnDestroy();
        }
    }
}
