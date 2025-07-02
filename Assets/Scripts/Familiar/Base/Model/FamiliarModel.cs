using Assets.Scripts.CommonObject.Model;
using Assets.Scripts.Datas;

namespace Assets.Scripts.Familiar.Base.Model
{
    public class FamiliarModel
    {
        private Power power;
        private Position position;
        private HP hP;
        private HitBox hitBox;
        private FStateMachine fStateMachine;

        public FamiliarModel(FamiliarData familiarData, IFStateAfterBorn fStateAfterBorn, Position position)
        {
            power = familiarData.DefaultPower;
            this.position = position;
            hP = familiarData.MaxHP;
            hitBox = familiarData.HitBox;
            fStateMachine = new FStateMachine(this, fStateAfterBorn);
        }

        public void FixedUpdate()
        {
            fStateMachine.FixedUpdate();
        }
    }
}
