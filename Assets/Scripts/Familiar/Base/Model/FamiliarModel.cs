using Assets.Scripts.Common;
using Assets.Scripts.Datas;
using Assets.Scripts.Familiar.Base.Controller;
using UnityEngine;

namespace Assets.Scripts.Familiar.Base.Model
{
    public class FamiliarModel
    {
        private Power power;
        private PSA pSA;
        public PSA PSA => pSA;
        private HP hP;
        private HitBox hitBox;
        private readonly FStateMachine fStateMachine;
        private readonly FamiliarData familiarData;
        private readonly FamiliarController familiarController;
        private readonly ColorName colorName;
        public ColorName ColorName => colorName;

        public FamiliarModel(FamiliarData familiarData, IFStateAfterBorn fStateAfterBorn, Vector2 position, FamiliarController familiarController, ColorName colorName)
        {
            this.familiarData = familiarData;
            power = familiarData.DefaultPower;
            pSA = new PSA(position, familiarData.Scale, 0f);
            hP = familiarData.MaxHP;
            hitBox = familiarData.HitBox;
            fStateMachine = new FStateMachine(this, fStateAfterBorn);
            this.familiarController = familiarController;
            this.colorName = colorName;
        }

        public void FixedUpdate() => fStateMachine.FixedUpdate();

        public void Move(Vector2 dir) => pSA = pSA.Move(dir);

        public void ChangeState(IFState state) => fStateMachine.ChangeState(state);

        public float GetUP(string key) => familiarData.GetUP(key);

        public void Destroy() => Object.Destroy(familiarController.gameObject);
    }
}
