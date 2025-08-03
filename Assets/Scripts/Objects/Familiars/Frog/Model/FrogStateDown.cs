using Assets.Scripts.Objects.Familiars.Base.Model;
using UnityEngine;
using Assets.Scripts.Objects.Familiars.Base.Controller;

namespace Assets.Scripts.Objects.Familiars.Frog.Model
{
    public class FrogStateDown : IFState
    {
        private readonly FamiliarModel fM;
        private readonly FamiliarController fC;
        private Vector2 lowerPos;
        private Vector2 moveDir;

        public FrogStateDown(FamiliarModel fM, FamiliarController fC, Vector2 lowerPos)
        {
            this.fM = fM;
            this.fC = fC;
            this.lowerPos = lowerPos;
        }

        public void OnStateEnter()
        {
            float downSeconds = fM.FamiliarData.GetUniqueParameter("DownSeconds");
            fC.PlayAnim("Down", downSeconds);
            moveDir = (lowerPos - fM.PA.Pos) / downSeconds;
        }

        public void OnUpdate()
        {
            fM.Move(moveDir * Time.deltaTime);
            if (Mathf.Abs(lowerPos.y - fM.PA.Pos.y) < 0.1f)
                fM.ChangeState(new FrogStateAttack(fM, fC));
        }

        public void OnStateExit()
        {
   
        }
    }
}
