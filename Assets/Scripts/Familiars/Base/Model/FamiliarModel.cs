using Assets.Scripts.Common;
using Assets.Scripts.Datas;
using Assets.Scripts.Familiars.Base.Controller;
using Assets.Scripts.GameSystems.Model;
using UnityEngine;

namespace Assets.Scripts.Familiars.Base.Model
{
    public class FamiliarModel
    {
        private Power power;
        private PSA pSA;
        public PSA PSA => pSA;
        private HP hP;
        private HurtBox hurtBox;
        private readonly FStateMachine fStateMachine;
        private readonly FamiliarData familiarData;
        public FamiliarData FamiliarData => familiarData;
        private readonly FamiliarController familiarController;
        private readonly ColorName colorName;
        public ColorName ColorName => colorName;

        public FamiliarModel(FamiliarData familiarData, IFStateAfterBorn fStateAfterBorn, Vector2 position, FamiliarController familiarController, ColorName colorName)
        {
            this.familiarData = familiarData;
            power = familiarData.DefaultPower;
            pSA = new PSA(position, familiarData.Scale, 0f);
            hP = familiarData.MaxHP;
            hurtBox = familiarData.HurtBox;
            fStateMachine = new FStateMachine(this, fStateAfterBorn);
            this.familiarController = familiarController;
            this.colorName = colorName;
            ObjectStorageModel.Instance.AddFamiliar(this);
        }

        public void FixedUpdate() => fStateMachine.FixedUpdate();
        public void Move(Vector2 dir) => pSA = pSA.Move(dir);
        public void ChangeState(IFState state) => fStateMachine.ChangeState(state);

        public void Destroy()
        {
            ObjectStorageModel.Instance.RemoveFamiliar(this);
            Object.Destroy(familiarController.gameObject);
        }
    }
}
