using Assets.Scripts.Datas;
using Assets.Scripts.GameSystems.ObjectsStorage.Model;
using Assets.Scripts.Objects.Common.Model;
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
        private readonly float power;
        public float Power => power;
        public PA PA => pA;
        public HitBox HitBox => hitBox;
        public FamiliarData FamiliarData => familiarData;
        public ColorName ColorName => colorName;

        public FamiliarAttackModel(FamiliarData familiarData, Vector2 pos, FamiliarAttackController familiarAttackController, bool isEnemy, ColorName colorName, ColorEffectData colorEffectData)
        {
            this.familiarData = familiarData;
            pA = new PA(pos, 0f);
            hitBox = new(pA.Pos, familiarData.HitBoxScale, true);
            this.familiarAttackController = familiarAttackController;
            this.colorName = colorName;
            power = familiarData.Power * (colorName == ColorName.red ? colorEffectData.PowerMultiplier : 1f);
            ObjectsStorageModel.Instance.AddFamiliarAttack(this, isEnemy);
        }

        public void MoveIgnoringStage(Vector2 dir)
        {
            pA = pA.MoveIgnoringStage(dir);
            hitBox = hitBox.SetPos(pA.Pos);
        }

        public void MoveInStage(Vector2 dir)
        {
            pA = pA.MoveInStage(dir);
            hitBox = hitBox.SetPos(pA.Pos);
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
