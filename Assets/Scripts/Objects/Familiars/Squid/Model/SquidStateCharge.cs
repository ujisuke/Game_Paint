using Assets.Scripts.Objects.Familiars.Base.Model;
using UnityEngine;
using Assets.Scripts.Objects.Familiars.Base.Controller;
using Assets.Scripts.GameSystems.ObjectStorage.Model;

namespace Assets.Scripts.Objects.Familiars.Squid.Model
{
    public class SquidStateCharge : IFStateAfterBorn
    {
        private readonly FamiliarModel fM;
        private readonly FamiliarController fC;
        private float seconds;
        private Vector2 targetPos;

        public SquidStateCharge(FamiliarModel fM, FamiliarController fC)
        {
            this.fM = fM;
            this.fC = fC;
            seconds = 0f;
        }

        public IFState Initialize(FamiliarModel fM, FamiliarController fC) => new SquidStateCharge(fM, fC);

        public void OnStateEnter()
        {
            targetPos = ObjectStorageModel.Instance.GetHostilePos(fM.PA.Pos, fM.IsEnemy);
            fC.FlipX(targetPos.x - fM.PA.Pos.x < 0f);
            fC.PlayAnim("Charge");
        }

        public void OnUpdate()
        {
            seconds += Time.deltaTime;
            if (seconds >= fM.FamiliarData.GetUP("ChargeSeconds"))
                fM.ChangeState(new SquidStateAttack(fM, fC, targetPos));
        }

        public void OnStateExit()
        {
   
        }
    }
}
