using Assets.Scripts.Objects.Familiars.Base.Model;
using UnityEngine;
using Assets.Scripts.Objects.Familiars.Base.Controller;
using Assets.Scripts.GameSystems.ObjectStorage.Model;

namespace Assets.Scripts.Objects.Familiars.Frog.Model
{
    public class FrogStateUp : IFStateAfterBorn
    {
        private readonly FamiliarModel fM;
        private readonly FamiliarController fC;
        private float seconds;
        private Vector2 upperPos;
        private Vector2 lowerPos;


        public FrogStateUp(FamiliarModel fM, FamiliarController fC)
        {
            this.fM = fM;
            this.fC = fC;
            seconds = 0f;
        }

        public IFState Initialize(FamiliarModel fM, FamiliarController fC) => new FrogStateUp(fM, fC);

        public void OnStateEnter()
        {
            upperPos = fM.PA.Pos + new Vector2(0f, fM.FamiliarData.GetUP("JumpHeight"));
            lowerPos = fM.PA.Pos;
            fC.FlipX(ObjectStorageModel.Instance.GetHostilePos(fM.PA.Pos, fM.IsEnemy).x - fM.PA.Pos.x < 0f);
            fC.PlayAnim("Up", fM.FamiliarData.GetUP("UpSeconds"));
        }

        public void OnUpdate()
        {
            fM.MoveIgnoringStage((upperPos - fM.PA.Pos) * Time.deltaTime * 2f);
            seconds += Time.deltaTime;
            if (seconds >= fM.FamiliarData.GetUP("UpSeconds"))
                fM.ChangeState(new FrogStateDown(fM, fC, lowerPos));
        }

        public void OnStateExit()
        {
   
        }
    }
}
