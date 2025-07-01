using Assets.Scripts.CommonObject.Model;
using Assets.Scripts.Datas;
using UnityEngine;

namespace Assets.Scripts.Familiar.Model
{
    public class FamiliarModel
    {
        private Power power;
        private Position position;
        private HP hP;
        private HitBox hitBox;
        private FStateMachine fStateMachine;

        public FamiliarModel(FamiliarData familiarData)
        {
            power = new Power(familiarData.DefaultPower);
            position = new Position(Vector2.zero);
            hP = new HP(familiarData.MaxHP);
            hitBox = new HitBox(familiarData.HitBoxSize);
            fStateMachine = new FStateMachine(this);
        }

        public void FixedUpdate()
        {
            fStateMachine.FixedUpdate();
        }
    }
}
